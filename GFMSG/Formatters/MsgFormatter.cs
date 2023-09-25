using System.Text.RegularExpressions;

namespace GFMSG;

public abstract class MsgFormatter
{
    protected static readonly Regex RawRegex = new(@"\{TAG_.+?\}|\[0x.+?\]|.");
    protected static readonly Regex MarkupRegex = new(@"\{[^\{]+?\}|\[[A-Za-z_]+?\]|\\\\|\\u....|\\[0-9a-z]|.");

    public CharProcessor Chars { get; set; } = new();
    public LetterProcessor Letters { get; set; } = new();
    public TagProcessor Tags { get; set; } = new();

    protected abstract string[] Process(SymbolSequence symbols, StringOptions options);
    public abstract ISymbol[] GetSymbols(SymbolSequence sequence);
    public abstract ushort[] GetCodes(ISymbol[] symbols, bool nullfill);
    public abstract ISymbol[] MarkupToSymbols(string input, string langcode);

    public Func<RequireArguments, string?> RequireText { get; set; }
    public FileVersion Version { get; set; }

    protected MsgFormatter(FileVersion version)
    {
        Version = version;
    }

    public string Format(SymbolSequence sequence, StringOptions options)
    {
        switch (options.Format)
        {
            case StringFormat.Hex8:
                return string.Join(' ', sequence.Codes.Select(x => $"{x & 0xFF:X2} {x >> 8:X2}"));
            case StringFormat.Hex16:
                return string.Join(' ', sequence.Codes.Select(x => $"{x:X4}"));
        }

        var list = Process(sequence, options);

        RemoveLineBreaks(list, options);

        return string.Join(string.Empty, list);
    }

    protected static void RemoveLineBreaks(string[] text, StringOptions options)
    {
        if (!options.RemoveLineBreaks)
        {
            if (options.Format == StringFormat.Html)
            {
                for (var i = 0; i < text.Length; i++)
                {
                    if (text[i] == "\n")
                    {
                        text[i] = "<br>";
                    }
                }
            }
            return;
        }

        if(options.Format is not StringFormat.Plain or StringFormat.Html)
        {
            return;
        }

        var linebreakReplacement = options.LanguageCode == null ? " "
            : options.LanguageCode.StartsWith("ja") && options.JapaneseFullwidthSpace ? "　"
            : options.LanguageCode.StartsWith("zh") ? ""
            : " ";
        char? lastchar = null;

        for (var i = 0; i < text.Length; i++)
        {
            if (text[i] == "\n")
            {
                bool needSpace = i > 0 && i < text.Length - 1 && lastchar.HasValue;
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

    protected static string? WordToText(TagToTextHandler handler)
    {
        if (handler.Options.ConvertWordsetToPlaceholder)
        {
            var varname = handler.Options.LanguageCode.Split('-')[0].ToLower() switch
            {
                "ja" or "zh" => "〇〇〇〇〇",
                _ => "*****",
            };
            return varname;
        }
        else
        {
            var varname = handler.Name.StartsWith("TAG_") ? "var" : handler.Name.ToLower();
            return handler.Options switch
            {
                { Format: StringFormat.Plain } => $"<{varname}>",
                { Format: StringFormat.Html } => $"<var>{varname}</var>",
                _ => throw new NotSupportedException(),
            };
        }
        throw new NotSupportedException();
    }
}