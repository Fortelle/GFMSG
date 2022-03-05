namespace GFMSG.GUI
{
    public class CellInfo
    {
        public MsgWrapper.Entry Entry { get; set; }
        public SymbolSequence Sequence => Entry[Index];

        public string LanguageCode { get; set; }
        public int Index { get; set; }

        public int Row { get; set; }
        //public int Column { get; set; }
    }

}
