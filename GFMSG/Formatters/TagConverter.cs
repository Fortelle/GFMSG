namespace GFMSG
{

    public class TagConverter
    {
        public delegate string? ToTextDelegate(TagToTextHandler handler);
        public delegate ushort[] ToSymbolDelegate(TagToSymbolHandler handler);

        public byte Group { get; set; }
        public byte? Index { get; set; }
        public string? Name { get; set; }

        public StringFormat? Formats { get; }

        public ToTextDelegate? ToText { get; init; }
        public ToSymbolDelegate? ToSymbol { get; init; }

        public TagConverter(byte group, byte index, string name, StringFormat? formats = null)
        {
            Group = group;
            Index = index;
            Name = name;
            Formats = formats;
        }

        public TagConverter(byte group, StringFormat? formats = null)
        {
            Group = group;
            Formats = formats;
        }

        public bool CheckFormat(StringFormat format)
        {
            return Formats == null || Formats.Value.HasFlag(format);
        }
    }

    public class TagToTextHandler
    {
        public byte Group { get; set; }
        public byte Index { get; set; }
        public string? Name { get; set; }
        public ushort[] Parameters { get; set; }
        public StringOptions Options { get; set; }
        [Obsolete]public SymbolQueue Queue { get; set; }
    }

    public class TagToSymbolHandler
    {
        //public TagInfo Tag { get; set; }
        public byte Group { get; set; }
        public byte Index { get; set; }
        public string Name { get; set; }
        public string[] Arguments { get; set; }
        [Obsolete]public List<ushort> Append { get; set; }
    }

}