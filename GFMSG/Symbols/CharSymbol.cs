namespace GFMSG
{
    public class CharSymbol : ISymbol
    {
        public static CharSymbol LineFeed => new(MsgFormatter.LINE_FEED_CODE);

        public ushort Code { get; set; }

        public int Size => 2;

        public bool IsPrintable => Code
            is not (>= 0x0000 and <= 0x001F) // latin control
            or (>= 0x007F and <= 0x009F) // latin-1 control
            or (0xFEFF)
            or (>= 0xFFF0 and <= 0xFFFF) // specials
            ;

        public bool IsPrivate => Code is (>= 0xE000 and <= 0xF8FF); // private use area

        public CharSymbol(ushort code)
        {
            Code = code;
        }

        public char ToChar()
        {
            return (char)Code;
        }

        public override string ToString()
        {
            if(!IsPrintable || IsPrivate)
            {
                return $"[0x{Code:X4}]";
            }
            else
            {
                return ((char)Code).ToString();
            }
        }

        public static CharSymbol FromString(string text)
        {
            text = text.TrimStart('[').TrimEnd(']');
            ushort code = Convert.ToUInt16(text, 16);
            return new CharSymbol(code);
        }

    }

}
