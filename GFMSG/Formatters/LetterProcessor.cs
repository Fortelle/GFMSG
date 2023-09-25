namespace GFMSG;

public class LetterProcessor
{
    public Dictionary<string, List<LetterInfo>> LetterLists { get; set; } = new();
    
    public LetterProcessor()
    {
    }

    public string ToText(LetterSymbol cs, StringOptions options, SymbolQueue buff)
    {
        switch (options.Format)
        {
            case StringFormat.Raw:
                return cs.ToString();
        }

        var li = FindLetter(options.LanguageCode, x => x.Code == cs.Code);
        if (li == null)
        {
            return "";
        }

        return options.Format switch
        {
            StringFormat.Markup => li.Value.MarkupText,
            StringFormat.Plain => li.Value.PlainText,
            StringFormat.Html => li.Value.PlainText,
            _ => throw new NotSupportedException(),
        };
    }

    public LetterSymbol FromMarkup(string text, string lang)
    {
        var li = FindLetter(lang, x => x.MarkupText == text);

        if (li == null)
        {
            throw new NotSupportedException("Unrecognized markup text.");
        }

        return new LetterSymbol(li.Value.Code);
    }

    private LetterInfo? FindLetter(string lang, Predicate<LetterInfo> match)
    {
        lang ??= "";
        lang = lang.Split('-')[0];

        if (lang != "" && !LetterLists.ContainsKey(lang)) {
            lang = "";
        }
        if (!LetterLists.ContainsKey(lang))
        {
            return null;
        }

        var i = LetterLists[lang].FindIndex(match);
        if (i >= 0)
        {
            return LetterLists[lang][i];
        }

        if (lang == "")
        {
            return null;
        }

        i = LetterLists[""].FindIndex(match);
        if (i >= 0)
        {
            return LetterLists[""][i];
        }

        return null;
    }
}

public struct LetterInfo
{
    public ushort Code;
    public string MarkupText;
    public string PlainText;

    public LetterInfo(ushort code, string markupText, string plainText)
    {
        Code = code;
        MarkupText = markupText;
        PlainText = plainText;
    }

    public LetterInfo(ushort code, string text)
    {
        Code = code;
        MarkupText = text;
        PlainText = text;
    }
}
