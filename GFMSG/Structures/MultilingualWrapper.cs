﻿using System.Diagnostics;
using System.Text;

namespace GFMSG;

public class MultilingualCollection
{
    public Dictionary<string, MsgWrapper[]> Wrappers { get; set; } = new();
    public FileVersion Version { get; set; }

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
            formatter.RequireText = (args) => {
                if (args.EntryIndex == null && args.EntryName == null) return null;
                if (string.IsNullOrEmpty(args.StringOptions.LanguageCode)) return null;
                if (!Wrappers.ContainsKey(args.StringOptions.LanguageCode)) return null;
                var wrapper = Wrappers[args.StringOptions.LanguageCode].FirstOrDefault(x => x.Name == args.Filename);
                if (wrapper == null) return null;
                wrapper.Load();
                var entry = args.EntryIndex != null ? wrapper[args.EntryIndex.Value]
                    : args.EntryName != null ? wrapper[args.EntryName]
                    : null;
                if (entry == null) return null;
                var text = formatter.Format(entry.Sequences[args.LanguageIndex], args.StringOptions);
                return text;
            };
        }
    }

    public MultilingualWrapper[] TransposeWrappers()
    {
        var names = Wrappers.Values.SelectMany(x => x.Select(y => y.Name).ToArray()).Distinct().ToArray();
        var list = new List<MultilingualWrapper>();
        foreach (var name in names)
        {
            var mmw = new MultilingualWrapper(name);
            foreach (var (groupname, wrappers) in Wrappers)
            {
                var wrapper = wrappers.FirstOrDefault(x => x.Name == name);
                if (wrapper != null)
                {
                    mmw.Add(groupname, wrapper);
                }
            }
            list.Add(mmw);
        }
        return list.ToArray();
    }

    private MsgWrapper[] GetWrappers(string langcode)
    {
        if (langcode != null && Wrappers.ContainsKey(langcode))
        {
            return Wrappers[langcode];
        }

        if (string.IsNullOrEmpty(langcode))
        {
            return Wrappers.Count > 0
                ? Wrappers.First().Value
                : Array.Empty<MsgWrapper>();
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
                langcode = prime;
            }
        }

        var key = Wrappers.Keys.FirstOrDefault(x => x.StartsWith(langcode + '-'));
        if (key != null)
        {
            return Wrappers[key];
        }

        throw new KeyNotFoundException();
    }

    // for debug
    public string GetString(string langcode, string filename, int index)
    {
        var wrappers = GetWrappers(langcode);
        var wrapper = wrappers.First(x => x.Name == filename);
        wrapper.Load();
        var entry = wrapper.TryGetEntry(index);
        if (entry == null) return "";
        var options = new StringOptions(StringFormat.Plain, langcode);
        return Formatter.Format(entry[0], options);
    }

    public string GetString(string langcode, string filename, string name)
    {
        var wrappers = GetWrappers(langcode);
        var wrapper = wrappers.First(x => x.Name == filename);
        wrapper.Load();
        var entry = wrapper.TryGetEntry(name);
        if (entry == null) return "";
        var options = new StringOptions(StringFormat.Plain, langcode);
        return Formatter.Format(entry[0], options);
    }

    public string GetString(string langcode, string filename, ulong hash)
    {
        var wrappers = GetWrappers(langcode);
        var wrapper = wrappers.First(x => x.Name == filename);
        wrapper.Load();
        var entry = wrapper.TryGetEntry(hash);
        if (entry == null) return "";
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

    public void Add(string groupname, MsgWrapper wrapper)
    {
        wrapper.Group = groupname;
        Wrappers.Add(groupname, wrapper);
    }
    
}
