namespace GFMSG
{
    public class CharProcessor
    {
        public List<CharConverter> Converters { get; set; } = new ();

        public Dictionary<ushort, string> MarkupMap { get; set; } = new ();
        public Dictionary<ushort, string> HtmlMap { get; set; } = new ();
        public Dictionary<ushort, string> PlainMap { get; set; } = new ();

        public CharProcessor()
        {
            MarkupMap.Add(0x0000, @"\0");
            MarkupMap.Add(0x000A, @"\n");
            MarkupMap.Add(0x000D, @"\r");

            HtmlMap.Add(0x000A, "<br />");
        }

        public string ToText(CharSymbol cs, StringOptions options, SymbolQueue buff)
        {
            var converters = GetConverters(cs.Code, options.Format);
            foreach(var converter in converters)
            {
                var ret = converter.ToText(cs.Code, options);
                if (ret != null)
                {
                    return ret;
                }
            }

            return options.Format switch
            {
                StringFormat.Raw => cs.ToString(),
                StringFormat.Markup => ToMarkup(cs),
                StringFormat.Plain => ToPlain(cs),
                StringFormat.Html => ToHtml(cs),
                _ => throw new NotSupportedException(),
            };
        }

        private string ToMarkup(CharSymbol cs)
        {
            return cs switch
            {
                _ when MarkupMap.ContainsKey(cs.Code) => MarkupMap[cs.Code],
                { IsPrintable: false } or { IsPrivate: true } => $"\\u{cs.Code:X4}",
                _ => ((char)cs.Code).ToString(),
            };
        }

        private string ToPlain(CharSymbol cs)
        {
            return cs switch
            {
                { Code: 0 } => "",
                { IsPrintable: true } => ((char)cs.Code).ToString(),
                _ when PlainMap.ContainsKey(cs.Code) => PlainMap[cs.Code],
                { IsPrivate: true } => $"[0x{cs.Code:X4}]",
                _ => ((char)cs.Code).ToString(),
            };
        }

        private string ToHtml(CharSymbol cs)
        {
            return cs switch
            {
                { Code: 0 } => "",
                { IsPrintable: false } => ((char)cs.Code).ToString(),
                _ when HtmlMap.ContainsKey(cs.Code) => HtmlMap[cs.Code],
                { IsPrivate: true } => $"[0x{cs.Code:X4}]",
                _ => ((char)cs.Code).ToString(),
            };
        }

        private CharConverter[] GetConverters(ushort code, StringFormat format)
        {
            var converters = Converters.AsEnumerable();
            converters = converters.Where(x => code >= x.CodeStart && code <= x.CodeEnd);
            converters = converters.Where(x => x.Formats == null || x.Formats.Value.HasFlag(format));
            return converters.ToArray();
        }

        public CharSymbol FromMarkup(string text)
        {
            if (text.StartsWith("\\u"))
            {
                var hex = text.Substring(2, text.Length - 2);
                var code = Convert.ToUInt16(hex, 16);
                return new CharSymbol(code);
            }
            else if (text.Length > 1 && text.StartsWith("\\"))
            {
                foreach (var (code, value) in MarkupMap)
                {
                    if (text == value)
                    {
                        return new CharSymbol(code);
                    }
                }
                throw new NotSupportedException("Unrecognized escape character.");
            }
            else if (text == @"\\")
            {
                return new CharSymbol('\\');
            }
            else
            {
                ushort code = text[0];
                return new CharSymbol(code);
            }
        }

    }
}
