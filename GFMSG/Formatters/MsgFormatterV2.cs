namespace GFMSG;

public class MsgFormatterV2 : MsgFormatter
{
    protected ushort EomCode;
    protected ushort LinefeedCode;
    protected ushort TagStartCode;

    protected MsgFormatterV2(FileVersion version) : base(version)
    {
        switch (version)
        {
            case FileVersion.GenV:
                EomCode = 0xFFFF;
                LinefeedCode = 0x000A;
                TagStartCode = 0xF000;
                break;
            case FileVersion.GenVI:
                EomCode = 0x0000;
                LinefeedCode = 0x000A;
                TagStartCode = 0xF000;
                break;
            case FileVersion.GenVIII:
                EomCode = 0x0000;
                LinefeedCode = 0x000A;
                TagStartCode = 0x0010;
                break;
        }
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
                CharSymbol cs => Chars.ToText(cs, options, queue),
                _ => throw new NotSupportedException(),
            };
            list.Add(s);
        }

        return list.ToArray();
    }

    public override ISymbol[] GetSymbols(SymbolSequence sequence)
    {
        var codes = sequence.Codes;
        var symbols = new List<ISymbol>();
        var isnullFilled = false;

        for (var i = 0; i < codes.Length - 1; i++) // ignore eom
        {
            if (codes[i] == TagStartCode && Version == FileVersion.GenV)
            {
                ushort tagCode = codes[i + 1];
                ushort numParams = codes[i + 2];
                var tagGroup = (byte)(tagCode >> 8 & 0xFF);
                var tagIndex = (byte)(tagCode & 0xFF);
                var parameters = numParams > 0 ? codes[(i + 3)..(i + 3 + numParams - 1)] : Array.Empty<ushort>();
                var symbol = new TagSymbol(tagGroup, tagIndex, parameters);
                symbols.Add(symbol);
                i += 2 + numParams;
            }
            else if(codes[i] == TagStartCode && Version >= FileVersion.GenVI)
            {
                ushort numParams = codes[i + 1];
                ushort tagCode = codes[i + 2];
                var tagGroup = (byte)(tagCode >> 8 & 0xFF);
                var tagIndex = (byte)(tagCode & 0xFF);
                var parameters = numParams > 0 ? codes[(i + 3)..(i + 3 + numParams - 1)] : Array.Empty<ushort>();
                var symbol = new TagSymbol(tagGroup, tagIndex, parameters);
                symbols.Add(symbol);
                i += 1 + numParams;
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
                symbols.Add(new CharSymbol(codes[i]));
            }
        }

        return symbols.ToArray();
    }

    public override ushort[] GetCodes(ISymbol[] symbols, bool nullfill = false)
    {
        var codes = new List<ushort>();

        foreach (var symbol in symbols)
        {
            switch (symbol)
            {
                case TagSymbol ts when Version == FileVersion.GenV:
                    codes.Add(TagStartCode);
                    codes.Add((ushort)((ts.Group << 8) | ts.Index));
                    codes.Add((ushort)(ts.Parameters.Length + 1));
                    codes.AddRange(ts.Parameters);
                    break;
                case TagSymbol ts when Version >= FileVersion.GenVI:
                    codes.Add(TagStartCode);
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
                var cs = Chars.FromMarkup(s);
                symbols.Add(cs);
            }
        }

        return symbols.ToArray();
    }
}