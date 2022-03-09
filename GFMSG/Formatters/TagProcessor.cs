using System.Text.RegularExpressions;

namespace GFMSG
{
    public class TagProcessor
    {
        public record TagInfo(byte? GroupValue, string? GroupName, byte? IndexValue, string? IndexName);

        public static readonly Regex RawTagRegex = new(@"^\{TAG_(..)_(..)(?::(.+))*\}$");
        public static readonly Regex MarkupTagRegex = new(@"^\{(.+?):(.+?)(?::(.+))*\}$");

        public List<TagConverter> Converters { get; set; } = new();
        public List<(byte Group, string Name)> GroupNames { get; set; } = new();
        public List<(byte Group, byte Index, string Name)> IndexNames { get; set; } = new();

        public TagProcessor()
        {
        }

        public string ToText(TagSymbol ts, StringOptions options, SymbolQueue buff)
        {
            string? ret = null;

            var taginfo = GetTagInfo(ts.Group, ts.Index);
            var converters = GetConverters(taginfo, options.Format);

            foreach (var converter in converters)
            {
                var handler = new TagToTextHandler()
                {
                    Tag = taginfo,
                    Parameters = ts.Parameters,
                    Options = options,
                    Queue = buff,
                };
                ret = converter.ToText(handler);
                if (ret != null) break;
            }

            if (ret != null && options.Format == StringFormat.Markup)
            {
                ret = "{"
                    + (taginfo.GroupName ?? "0x" + ts.Group.ToString("X2"))
                    + ":"
                    + (taginfo.IndexName ?? "0x" + ts.Index.ToString("X2"))
                    + (ret == "" ? "" : (":" + ret))
                    + "}";
            }

            if (ret == null) // no parser matched or parser returns null
            {
                switch (options.Format)
                {
                    case StringFormat.Raw:
                        ret = ts.ToString();
                        break;
                    case StringFormat.Markup:
                        {
                            ret = "{";
                            ret += taginfo.GroupName ?? "0x" + ts.Group.ToString("X2");
                            ret += ":";
                            ret += taginfo.IndexName ?? "0x" + ts.Index.ToString("X2");
                            for (var i = 0; i < ts.Parameters.Length; i++)
                            {
                                ret += i == 0 ? ":" : ',';
                                ret += $"0x{ts.Parameters[i]:X4}";
                            }
                            ret += "}";
                            break;
                        }
                    case StringFormat.Plain:
                        ret = "";
                        break;
                    case StringFormat.Html:
                        ret = "";
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return ret;
        }


        public ISymbol[] FromMarkup(string text)
        {
            var m = MarkupTagRegex.Match(text);
            var tagGroup = m.Groups[1].Value;
            var tagIndex = m.Groups[2].Value;
            var args = m.Groups[3].Value != ""
                ? m.Groups[3].Value.Split(',')
                : Array.Empty<string>();

            var taginfo = GetTagInfo(tagGroup, tagIndex);
            var converter = GetConverters(taginfo, StringFormat.Markup).FirstOrDefault(x => x.ToSymbol != null);
            if (converter != null)
            {
                var handler = new TagToSymbolHandler()
                {
                    Tag = taginfo,
                    Arguments = args,
                    Append = new List<ushort>(),
                };
                var parameters = converter.ToSymbol!(handler);
                return new ISymbol[] {
                    new TagSymbol(taginfo.GroupValue.Value, taginfo.IndexValue.Value, parameters),
                }
                .Concat(handler.Append.Select(x=> new CharSymbol(x)))
                .ToArray();
            }
            else if(taginfo.GroupValue.HasValue && taginfo.IndexValue.HasValue)
            {
                var parameters = args.Select(x => Convert.ToUInt16(x, 16)).ToArray();
                return new[] {
                    new TagSymbol(taginfo.GroupValue.Value, taginfo.IndexValue.Value, parameters)
                };
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static TagSymbol FromRaw(string text)
        {
            var m = RawTagRegex.Match(text);
            var tagGroup = Convert.ToByte(m.Groups[1].Value, 16);
            var tagIndex = Convert.ToByte(m.Groups[2].Value, 16);
            var parameters = m.Groups[3].Value != ""
                ? m.Groups[3].Value.Split(',').Select(x => Convert.ToUInt16(x, 16)).ToArray()
                : Array.Empty<ushort>();
            var ts = new TagSymbol(tagGroup, tagIndex, parameters);
            return ts;
        }

        private TagConverter[] GetConverters(TagInfo taginfo, StringFormat format)
        {
            var converters = Converters.AsEnumerable();

            if (taginfo.GroupName != null)
            {
                converters = converters.Where(x => x.GroupName == taginfo.GroupName);
            }
            else
            {
                return null;
            }

            converters = converters.Where(x => x.Formats == null || x.Formats.Value.HasFlag(format));

            if (taginfo.IndexName != null)
            {
                converters = converters
                    .Where(x => (x.IndexName == null && x.IndexValue == null) || x.IndexName == taginfo.IndexName)
                    .OrderBy(x => x.IndexName == null);
            }
            else if (taginfo.IndexValue != null)
            {
                converters = converters
                    .Where(x => (x.IndexName == null && x.IndexValue == null) || x.IndexValue == taginfo.IndexValue)
                    .OrderBy(x => x.IndexValue == null);
            }
            else
            {
                converters = converters
                    .Where(x => x.IndexValue == null);
            }

            return converters.ToArray();
        }

        private TagInfo GetTagInfo(string? groupName, string? indexName)
        {
            byte? groupValue = null;
            byte? indexValue = null;

            if (groupName!.StartsWith("0x"))
            {
                groupValue = Convert.ToByte(groupName, 16);
                groupName = null;
            }
            else
            {
                var i = GroupNames.FindIndex(x => x.Name == groupName);
                if (i != -1)
                {
                    groupValue = GroupNames[i].Group;
                }
            }

            if (indexName!.StartsWith("0x"))
            {
                indexValue = Convert.ToByte(indexName, 16);
                indexName = null;
            }

            if (groupValue.HasValue)
            {
                var i = IndexNames.FindIndex(x => x.Group == groupValue && x.Name == indexName);
                if (i != -1)
                {
                    indexValue = IndexNames[i].Index;
                }
            }

            return new TagInfo(groupValue, groupName, indexValue, indexName);
        }

        private TagInfo GetTagInfo(byte groupValue, byte indexValue)
        {
            string? groupName = null;
            string? indexName = null;

            {
                var i = GroupNames.FindIndex(x => x.Group == groupValue);
                if (i != -1)
                {
                    groupName = GroupNames[i].Name;
                }
            }

            {
                var i = IndexNames.FindIndex(x => x.Group == groupValue && x.Index == indexValue);
                if (i != -1)
                {
                    indexName = IndexNames[i].Name;
                }
            }

            return new TagInfo(groupValue, groupName, indexValue, indexName);
        }
    }
}