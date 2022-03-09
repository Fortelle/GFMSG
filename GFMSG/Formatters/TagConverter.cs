using static GFMSG.TagProcessor;

namespace GFMSG
{

    public class TagConverter
    {
        public delegate string? ToTextDelegate(TagToTextHandler handler);
        public delegate ushort[] ToSymbolDelegate(TagToSymbolHandler handler);

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

    public class TagToTextHandler
    {
        public TagInfo Tag { get; set; }
        public ushort[] Parameters { get; set; }
        public StringOptions Options { get; set; }
        [Obsolete]public SymbolQueue Queue { get; set; }

        //public string? Begin { get; set; }
        //public string? End { get; set; }
        //public string? Before { get; set; }
        //public string? After { get; set; }
    }

    public class TagToSymbolHandler
    {
        public TagInfo Tag { get; set; }
        public string[] Arguments { get; set; }
        [Obsolete]public List<ushort> Append { get; set; }
    }

}