namespace GFMSG;

public class SymbolSequence
{
    public string? Name { get; set; }
    public ushort[] Codes { get; set; }
    //public ISymbol[] Symbols { get; set; }
    public bool NullFill { get; set; }
    public bool Compressed { get; set; }
    public GrammaticalAttribute? Grammatical { get; set; }
    public string Language { get; set; }
    /*
    public SymbolSequence(ISymbol[] symbols)
    {
        Symbols = symbols;
    }

    public SymbolSequence(ISymbol[] symbols, GrammaticalAttribute grammatical)
    {
        Symbols = symbols;
        Grammatical = grammatical;
    }
    */
    public SymbolSequence(ushort[] codes)
    {
        Codes = codes;
    }

    public SymbolSequence(ushort[] codes, string lang)
    {
        Codes = codes;
        Language = lang;
    }

    public SymbolSequence(ushort[] codes, string lang, GrammaticalAttribute grammatical)
    {
        Codes = codes;
        Language = lang;
        Grammatical = grammatical;
    }
}