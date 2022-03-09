using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace GFMSG
{
    public class MsgFormatter
    {
        internal const ushort EOM_CODE = 0x0000;
        internal const ushort LINE_FEED_CODE = 0x000A;
        internal const ushort TAG_START_CODE = 0x0010;

        public static readonly Regex RawRegex = new(@"\{TAG_.+?\}|\[0x.+?\]|.");
        public static readonly Regex MarkupRegex = new(@"\{[^\{]+?\}|\\\\|\\u....|\\[0-9a-z]|.");

        public TagProcessor Tags { get; set; } = new();
        public CharProcessor Chars { get; set; } = new();

        public Func<RequireArguments, string?> RequireText { get; set; }

        public MsgFormatter()
        {
        }

        public string Format(SymbolSequence symbols, StringOptions options)
        {
            if(options.Format == StringFormat.Raw)
            {
                return string.Join(string.Empty, symbols.Symbols.Select(x => x.ToString()));
            }

            var sr = new SymbolQueue(symbols.Symbols);
            var lst = new List<string>();
            bool removeLinebreaks = options.RemoveLineBreaks && options.Format is StringFormat.Plain or StringFormat.Html;

            while (sr.ReadNext(out ISymbol? symbol))
            {
                var s = symbol switch
                {
                    TagSymbol ts => Tags.ToText(ts, options, sr),
                    CharSymbol cs when cs.Code == LINE_FEED_CODE && removeLinebreaks => "\n",
                    CharSymbol cs => Chars.ToText(cs, options, sr),
                    _ => throw new NotSupportedException(),
                };
                lst.Add(s);
            }

            if (removeLinebreaks)
            {
                RemoveLineBreaks(lst, options);
            }

            return string.Join(string.Empty, lst);
        }

        private static void RemoveLineBreaks(List<string> text, StringOptions options)
        {
            Debug.Assert(options.Format is StringFormat.Plain or StringFormat.Html);

            var linebreakReplacement = options.LanguageCode == null ? " "
                : options.LanguageCode.StartsWith("ja") && options.JapaneseFullwidthSpace ? "　"
                : options.LanguageCode.StartsWith("zh") ? ""
                : " ";
            char? lastchar = null;

            for (var i = 0; i < text.Count; i++)
            {
                if (text[i] == "\n")
                {
                    bool needSpace = i > 0 && i < text.Count - 1 && lastchar.HasValue;
                    if (needSpace)
                    {
                        if (options.LanguageCode?.StartsWith("ja") == true)
                        {
                            needSpace &= !char.IsWhiteSpace(lastchar!.Value);
                            if (options.JapaneseSpaceAfterPunctuation)
                            {
                                needSpace &= !char.IsPunctuation(lastchar!.Value);
                            }
                        }
                        else if (options.LanguageCode?.StartsWith("zh") == true)
                        {
                        }
                        else
                        {
                            needSpace &= !char.IsWhiteSpace(lastchar!.Value);
                        }
                    }
                    text[i] = needSpace ? linebreakReplacement : string.Empty;
                }
                if (text[i] != "") lastchar = text[i][^1];
            }
        }

        public void AddTagGroup(byte id, string name)
        {
            Tags.GroupNames.Add((id, name));
        }

        public void AddTagIndex(byte groupValue, byte indexValue, string indexName)
        {
            Tags.IndexNames.Add((groupValue, indexValue, indexName));
        }

        public void AddConverter(TagConverter converter)
        {
            Tags.Converters.Add(converter);
        }

        public void AddConverter(CharConverter converter)
        {
            Chars.Converters.Add(converter);
        }

        public void AddChar(ushort code, string text, StringFormat formats)
        {
            foreach(var format in Enum.GetValues<StringFormat>())
            {
                if (!formats.HasFlag(format)) continue;
                switch (format)
                {
                    case StringFormat.Plain:
                        Chars.PlainMap.Add(code, text);
                        break;
                    case StringFormat.Html:
                        Chars.HtmlMap.Add(code, text);
                        break;
                }
            }
        }

        public static ISymbol[] RawToSymbols(string input)
        {
            var matches = RawRegex.Matches(input).Select(x => x.Value).ToArray();

            var symbols = new List<ISymbol>();
            foreach (var s in matches)
            {
                if (s.StartsWith("{TAG_"))
                {
                    symbols.Add(TagSymbol.FromString(s));
                }
                else if (s.StartsWith("[0x"))
                {
                    symbols.Add(CharSymbol.FromString(s));
                }
                else
                {
                    symbols.Add(new CharSymbol(s[0]));
                }
            }

            return symbols.ToArray();
        }

        public ISymbol[] MarkupToSymbols(string input)
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
                    var cs = Chars.FromMarkup(s);
                    symbols.Add(cs);
                }
            }

            return symbols.ToArray();
        }

        public static ISymbol[] CodesToSymbols(ushort[] codes, out bool isnullFilled)
        {
            if (codes[^1] != EOM_CODE)
            {
                throw new InvalidDataException($"The codes must end with an EOM_CODE(0x{EOM_CODE:X4})");
            }

            var symbols = new List<ISymbol>();
            isnullFilled = false;

            for (var i = 0; i < codes.Length - 1; i++) // ignore eom
            {
                if (codes[i] == TAG_START_CODE)
                {
                    ushort numParams = codes[i + 1];
                    ushort tagCode = codes[i + 2];
                    var tagGroup = (byte)(tagCode >> 8 & 0xFF);
                    var tagIndex = (byte)(tagCode & 0xFF);
                    var parameters = codes[(i + 3)..(i + 3 + numParams - 1)];
                    var symbol = new TagSymbol(tagGroup, tagIndex, parameters);
                    symbols.Add(symbol);
                    i += numParams + 1;
                }
                else if (codes[i] == EOM_CODE)
                {
                    if ((codes.Length % 2 == 1)
                        && (i == (codes.Length - 1) / 2)
                        && codes[(i + 1)..^1].All(x => x == EOM_CODE)
                        )
                    {
                        isnullFilled = true;
                        break;
                    }
                }
                else
                {
                    symbols.Add(new CharSymbol(codes[i]));
                }
            }

            return symbols.ToArray();
        }

        public static ushort[] SymbolsToCodes(ISymbol[] symbols, bool nullfill = false)
        {
            var codes = new List<ushort>();

            foreach (var symbol in symbols)
            {
                switch (symbol)
                {
                    case TagSymbol ts:
                        codes.Add(TAG_START_CODE);
                        codes.Add((ushort)(ts.Parameters.Length + 1));
                        codes.Add((ushort)((ts.Group << 8) | ts.Index));
                        codes.AddRange(ts.Parameters);
                        break;
                    case CharSymbol cs:
                        codes.Add(cs.Code);
                        break;
                }
            }

            if (nullfill)
            {
                codes.AddRange(Enumerable.Repeat(EOM_CODE, codes.Count));
            }

            codes.Add(EOM_CODE);

            return codes.ToArray();
        }
    }
}