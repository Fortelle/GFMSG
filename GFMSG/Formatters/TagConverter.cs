using static GFMSG.TagProcessor;

namespace GFMSG
{

    public class TagConverter
    {
        public delegate string? ToTextDelegate(TagInfo tag, ushort[] args, StringOptions options, SymbolQueue buff);
        public delegate ushort[] ToSymbolDelegate(TagInfo tag, string[] args, List<ushort> append);

        public string GroupName { get; }
        public string? IndexName { get; }
        public byte? IndexValue { get; }
        public StringFormat? Formats { get; }

        public ToTextDelegate ToText { get; init; }
        public ToSymbolDelegate? ToSymbol { get; init; }

        public TagConverter(string groupName, StringFormat? formats = null)
        {
            GroupName = groupName;
            Formats = formats;
        }

        public TagConverter(string groupName, string indexName, StringFormat? formats = null)
        {
            GroupName = groupName;
            IndexName = indexName;
            Formats = formats;
        }

        public TagConverter(string groupName, byte indexValue, StringFormat? formats = null)
        {
            GroupName = groupName;
            IndexValue = indexValue;
            Formats = formats;
        }

    }

    public class CharConverter
    {
        public delegate string? ToTextDelegate(ushort code, StringOptions options);

        public ushort CodeStart { get; }
        public ushort CodeEnd { get; }
        public StringFormat? Formats { get; }

        public ToTextDelegate ToText { get; init; }

        public CharConverter(ushort start, ushort end, StringFormat? formats = null)
        {
            CodeStart = start;
            CodeEnd = end;
            Formats = formats;
        }
    }

    public class TagHandler
    {
        public TagInfo Tag { get; set; }
        public ushort[] Arguments { get; set; }
        public StringOptions Options { get; set; }
        public SymbolQueue Buff { get; set; }
    }

}