using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFMSG
{
    public class MultilingualCollection
    {
        public Dictionary<string, MsgWrapper[]> Wrappers { get; set; } = new();

        private MsgFormatter formatter;
        public MsgFormatter Formatter
        {
            get {
                return formatter;
            }
            set
            {
                Debug.Assert(formatter == null);
                formatter = value;
                formatter.Tags.RequireText = (filename, index, options) => {
                    if (string.IsNullOrEmpty(options.LanguageCode)) return null;
                    if (!Wrappers.ContainsKey(options.LanguageCode)) return null;
                    var wrapper = Wrappers[options.LanguageCode].FirstOrDefault(x => x.Name == filename);
                    if (wrapper == null) return null;
                    wrapper.Load();
                    var text = formatter.Format(wrapper[index].Sequences[0], options);
                    return text;
                };
            }
        }

        public MultilingualWrapper[] ToWrappers()
        {
            var names = Wrappers.Values.SelectMany(x => x.Select(y => y.Name).ToArray()).Distinct().ToArray();
            var list = new List<MultilingualWrapper>();
            foreach (var name in names)
            {
                var mmw = new MultilingualWrapper(name);
                foreach (var (langcode, wrappers) in Wrappers)
                {
                    var wrapper = wrappers.FirstOrDefault(x => x.Name == name);
                    if (wrapper != null)
                    {
                        mmw.Add(langcode, wrapper);
                    }
                }
                list.Add(mmw);
            }
            return list.ToArray();
        }

        private MsgWrapper[] GetWrappers(string langcode)
        {
            if (Wrappers.ContainsKey(langcode))
            {
                return Wrappers[langcode];
            }
            if (langcode.Contains('-'))
            {
                var prime = langcode.Split('-')[0];
                if (Wrappers.ContainsKey(prime))
                {
                    return Wrappers[prime];
                }
                else
                {
                    prime += '-';
                    string? langcode2 = Wrappers.Keys.FirstOrDefault(x => x.StartsWith(prime));
                    if (langcode2 != null)
                    {
                        return Wrappers[langcode2];
                    }
                }
            }
            return null;
        }

        // for debug
        public string GetString(string langcode, string filename, string index)
        {
            var wrappers = GetWrappers(langcode);
            var wrapper = wrappers.First(x => x.Name == filename);
            wrapper.Load();
            var entry = wrapper[index];
            var options = new StringOptions(StringFormat.Plain, langcode);
            return Formatter.Format(entry[0], options);
        }
        public string[] GetStrings(string langcode, string filename)
        {
            var wrappers = GetWrappers(langcode);
            var wrapper = wrappers.First(x => x.Name == filename);
            var options = new StringOptions(StringFormat.Plain, langcode);
            return wrapper.GetTextEntries()
                .Select(x =>Formatter.Format(x[0], options))
                .ToArray();
        }

    }

    public class MultilingualWrapper
    {
        public string Name { get; set; }
        public Dictionary<string, MsgWrapper> Wrappers { get; set; }

        public MultilingualWrapper(string name)
        {
            Name = name;
            Wrappers = new();
        }

        public void Add(string langcode, MsgWrapper wrapper)
        {
            Wrappers.Add(langcode, wrapper);
        }
        
    }
}
