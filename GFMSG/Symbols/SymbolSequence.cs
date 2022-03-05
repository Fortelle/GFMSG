namespace GFMSG
{
    public class SymbolSequence
    {
        public string? Name { get; set; }
        public ISymbol[] Symbols { get; set; }
        public bool NullFill { get; set; }
        public GrammaticalAttribute Grammatical { get; set; }

        public SymbolSequence(ISymbol[] symbols, GrammaticalAttribute grammatical)
        {
            Symbols = symbols;
            Grammatical = grammatical;
        }

        public SymbolSequence(ushort[] codes, GrammaticalAttribute grammatical)
        {
            Symbols = MsgFormatter.CodesToSymbols(codes, out bool isnullFilled);
            Grammatical = grammatical;
            NullFill = isnullFilled;
        }

        public ushort[] ToCodes()
        {
            return MsgFormatter.SymbolsToCodes(Symbols, NullFill);
        }
    }
}
