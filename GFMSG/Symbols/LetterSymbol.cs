namespace GFMSG
{
    public class LetterSymbol : ISymbol
    {
        public ushort Code { get; set; }

        public int Size => 2;

        public LetterSymbol(ushort code)
        {
            Code = code;
        }

        public char ToChar()
        {
            return (char)Code;
        }

        public override string ToString()
        {
            return $"[0x{Code:X4}]";
        }

        public static CharSymbol FromString(string text)
        {
            text = text.TrimStart('[').TrimEnd(']');
            ushort code = Convert.ToUInt16(text, 16);
            return new CharSymbol(code);
        }
    }

}
