namespace GFMSG
{
    public class MsgFormatterV1 : MsgFormatter
    {
        protected ushort EomCode;
        protected ushort LinefeedCode;
        protected ushort TagStartCode;

        public MsgFormatterV1() : base(FileVersion.GenIV)
        {
            EomCode = 0xFFFF;
            LinefeedCode = 0xE000;
            TagStartCode = 0xFFFE;
        }

        public void AddLetter(string lang, LetterInfo li)
        {
            if (!Letters.LetterLists.ContainsKey(lang))
            {
                Letters.LetterLists.Add(lang, new());
            }
            Letters.LetterLists[lang].Add(li);
        }


        protected override string[] Process(SymbolSequence sequence, StringOptions options)
        {
            var symbols = GetSymbols(sequence);
            var queue = new SymbolQueue(symbols);
            var list = new List<string>();
            while (queue.ReadNext(out ISymbol? symbol))
            {
                var s = symbol switch
                {
                    TagSymbol ts => Tags.ToText(ts, options, queue),
                    LetterSymbol ls => Letters.ToText(ls, options, queue),
                    _ => throw new NotSupportedException(),
                };
                list.Add(s);
            }

            return list.ToArray();
        }

        public override ISymbol[] GetSymbols(SymbolSequence sequence)
        {
            var codes = sequence.Codes;
            if (codes[^1] != EomCode)
            {
                throw new InvalidDataException($"The codes must end with an EOM code(0x{EomCode:X4})");
            }

            var symbols = new List<ISymbol>();
            var isnullFilled = false;

            for (var i = 0; i < codes.Length - 1; i++) // ignore eom
            {
                if (codes[i] == TagStartCode)
                {
                    ushort tagCode = codes[i + 1];
                    ushort numParams = codes[i + 2];
                    var tagGroup = (byte)(tagCode >> 8 & 0xFF);
                    var tagIndex = (byte)(tagCode & 0xFF);
                    var parameters = numParams > 0 ? codes[(i + 3)..(i + 3 + numParams)] : Array.Empty<ushort>();
                    var symbol = new TagSymbol(tagGroup, tagIndex, parameters);
                    symbols.Add(symbol);
                    i += 2 + numParams;
                }
                else if (codes[i] == EomCode)
                {
                    if ((codes.Length % 2 == 1)
                        && (i == (codes.Length - 1) / 2)
                        && codes[(i + 1)..^1].All(x => x == EomCode)
                        )
                    {
                        isnullFilled = true;
                        break;
                    }
                }
                else
                {
                    symbols.Add(new LetterSymbol(codes[i]));
                }
            }

            sequence.NullFill = isnullFilled;
            return symbols.ToArray();
        }

        public override ushort[] GetCodes(ISymbol[] symbols, bool nullfill = false)
        {
            var codes = new List<ushort>();

            foreach (var symbol in symbols)
            {
                switch (symbol)
                {
                    case TagSymbol ts:
                        codes.Add(TagStartCode);
                        codes.Add((ushort)((ts.Group << 8) | ts.Index));
                        codes.Add((ushort)ts.Parameters.Length);
                        codes.AddRange(ts.Parameters);
                        break;
                    case LetterSymbol cs:
                        codes.Add(cs.Code);
                        break;
                }
            }

            if (nullfill)
            {
                codes.AddRange(Enumerable.Repeat(EomCode, codes.Count));
            }

            codes.Add(EomCode);

            return codes.ToArray();
        }

        public override ISymbol[] MarkupToSymbols(string input, string langcode)
        {
            var matches = MarkupRegex.Matches(input).Select(x => x.Value).ToArray();

            var symbols = new List<ISymbol>();
            foreach (var s in matches)
            {
                if (s[0] == '{' && s[^1] == '}')
                {
                    var ts = Tags.FromMarkup(s);
                    symbols.AddRange(ts);
                }
                else
                {
                    var ls = Letters.FromMarkup(s, langcode);
                    symbols.Add(ls);
                }
            }

            return symbols.ToArray();
        }
    }
}