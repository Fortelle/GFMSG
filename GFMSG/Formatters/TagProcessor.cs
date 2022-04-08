using System.Text.RegularExpressions;

namespace GFMSG
{
    public class TagProcessor
    {
        public record ConverterInfo(byte Group, byte Index, string Name);

        public static readonly Regex RawTagRegex = new(@"^\{TAG_(..)_(..)(?::(.+))*\}$");
        public static readonly Regex MarkupTagRegex = new(@"^\{(.+?)(?::(.+))*\}$");
        
        public List<TagConverter> Converters { get; set; } = new();

        public TagProcessor()
        {
        }

        public string ToText(TagSymbol ts, StringOptions options, SymbolQueue buff)
        {
            switch (options.Format)
            {
                case StringFormat.Raw:
                    return ts.ToString();
            }
            
            string? ret = null;

            var converter = GetConverter(ts.Group, ts.Index);
            var tagName = converter?.Name ?? ts.GetName();

            if (converter != null)
            {
                if (converter.CheckFormat(options.Format) && converter.ToText != null)
                {
                    var handler = new TagToTextHandler()
                    {
                        Group = ts.Group,
                        Index = ts.Index,
                        Name = tagName,
                        Parameters = ts.Parameters,
                        Options = options,
                        Queue = buff,
                    };
                    ret = converter.ToText(handler);
                }
            }

            if (ret != null && options.Format == StringFormat.Markup)
            {
                ret = "{"
                    + tagName
                    + (ret == "" ? "" : (":" + ret))
                    + "}";
            }

            if (ret == null)
            {
                switch (options.Format)
                {
                    case StringFormat.Raw:
                        ret = ts.ToString();
                        break;
                    case StringFormat.Markup:
                        {
                            ret = "{";
                            ret += tagName;
                            for (var i = 0; i < ts.Parameters.Length; i++)
                            {
                                ret += i == 0 ? ":" : TagSymbol.ParameterSeparator;
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
            var tagName = m.Groups[1].Value;
            var args = m.Groups[2].Value != ""
                ? m.Groups[2].Value.Split(':')
                : Array.Empty<string>();

            if (tagName.StartsWith("TAG_"))
            {
                var tagGroup = Convert.ToByte(tagName.Substring(4, 2), 16);
                var tagIndex = Convert.ToByte(tagName.Substring(7, 2), 16);

                var parameters = args.Select(x => Convert.ToUInt16(x, 16)).ToArray();
                return new[] {
                    new TagSymbol(tagGroup, tagIndex, parameters)
                };
            }
            else
            {
                var converter = GetConverter(tagName);

                if (converter != null)
                {
                    var tagGroup = converter.Group;
                    var tagIndex = converter.Index!.Value;

                    if (converter.CheckFormat(StringFormat.Markup) && converter.ToSymbol != null)
                    {
                        var handler = new TagToSymbolHandler()
                        {
                            Group = tagGroup,
                            Index = tagIndex,
                            Name = converter.Name!,
                            Arguments = args,
                            Append = new List<ushort>(),
                        };
                        var parameters = converter.ToSymbol(handler);
                        return new ISymbol[] {
                            new TagSymbol(tagGroup, tagIndex, parameters),
                        }
                        .Concat(handler.Append.Select(x => new CharSymbol(x)))
                        .ToArray();
                    }
                    else
                    {
                        var parameters = args.Select(x => Convert.ToUInt16(x, 16)).ToArray();
                        return new[] {
                            new TagSymbol(tagGroup, tagIndex, parameters)
                        };
                    }
                }
            }

            throw new NotSupportedException($"Unrecognized tag name \"{tagName}\".");
        }

        public static TagSymbol FromRaw(string text)
        {
            var m = RawTagRegex.Match(text);
            var tagGroup = Convert.ToByte(m.Groups[1].Value, 16);
            var tagIndex = Convert.ToByte(m.Groups[2].Value, 16);
            var parameters = m.Groups[3].Value != ""
                ? m.Groups[3].Value.Split(TagSymbol.ParameterSeparator).Select(x => Convert.ToUInt16(x, 16)).ToArray()
                : Array.Empty<ushort>();
            var ts = new TagSymbol(tagGroup, tagIndex, parameters);
            return ts;
        }

        private TagConverter? GetConverter(byte group, byte index)
        {
            var i = Converters.FindIndex(x => x.Group == group && x.Index == index);
            if (i != -1)
            {
                return Converters[i];
            }

            i = Converters.FindIndex(x => x.Group == group && x.Index == null);
            if (i != -1)
            {
                return Converters[i];
            }

            return null;
        }

        private TagConverter? GetConverter(string name)
        {
            var i = Converters.FindIndex(x => x.Name == name);
            if (i != -1)
            {
                return Converters[i];
            }

            return null;
        }

    }
}