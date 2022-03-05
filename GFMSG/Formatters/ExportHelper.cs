using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace GFMSG
{
    public static class ExportHelper
    {
        public static string GetText(MsgWrapper wrapper, MsgFormatter formatter, StringOptions options, bool includeId)
        {
            var sb = new StringBuilder();
            options.LanguageCode = wrapper.LanguageCode;
            foreach (var entry in wrapper.GetTextEntries())
            {
                var text = formatter.Format(entry.Sequences![0], options);
                if (!options.RemoveLineBreaks)
                {
                    text = text.Replace("\n", "\\n");
                }
                if (includeId && entry.Name != null)
                {
                    text = entry.Name + "=" + text;
                }
                sb.AppendLine(text);
            }
            return sb.ToString();
        }

        public static string GetCSV(MultilingualWrapper mw, MsgFormatter formatter, StringOptions options)
        {
            var sb = new StringBuilder();
            var firstWrapper = mw.Wrappers.First().Value;
            foreach(var x in mw.Wrappers.Values)
            {
                x.Load();
            }
            var langcodes = mw.Wrappers.Keys.ToArray();
            {
                var list = new List<string>();
                if (firstWrapper.HasNameTable)
                {
                    list.Add("id");
                    list.AddRange(langcodes);
                }
                sb.AppendLine(string.Join("\t", list));
            }
            var length = firstWrapper.GetTextEntries().Length;
            for (var i = 0; i < length; i++)
            {
                var list = new List<string>();
                if (firstWrapper.HasNameTable)
                {
                    list.Add(firstWrapper[i].Name);
                }
                foreach (var langcode in langcodes)
                {
                    var wrapper = mw.Wrappers[langcode];
                    options.LanguageCode = langcode;
                    var entry = wrapper.Entries[i];
                    var text = formatter.Format(entry.Sequences[0], options);
                    if (!options.RemoveLineBreaks)
                    {
                        text = text.Replace("\n", "\\n");
                    }
                    list.Add(text);
                }
                var line = string.Join("\t", list.Select(x =>
                {
                    if (x.Contains(',') || x.Contains('\"'))
                    {
                        x = x.Replace("\"", "\"\"");
                        x = "\"" + x + "\"";
                    }
                    return x;
                }));
                sb.AppendLine(line);
            }
            return sb.ToString();
        }

        public static JsonNode GetJsonNode(MsgWrapper wrapper, MsgFormatter formatter, StringOptions options, bool includeId)
        {
            options.LanguageCode = wrapper.LanguageCode;
            if (includeId)
            {
                // warnning: ids are not guaranteed to be unique
                var json = new JsonObject();
                foreach (var entry in wrapper.GetTextEntries())
                {
                    var id = entry.Name;
                    if (entry.Sequences.Count == 1)
                    {
                        var text = formatter.Format(entry.Sequences![0], options);
                        json.Add(id, JsonValue.Create(text));
                    }
                    else
                    {
                        var text = entry.Sequences!.Select(x => formatter.Format(x, options)).ToArray();
                        json.Add(id, JsonValue.Create(text));
                    }
                }
                return json;
            }
            else
            {
                var json = new JsonArray();
                foreach (var entry in wrapper.GetTextEntries())
                {
                    if (entry.Sequences.Count == 1)
                    {
                        var text = formatter.Format(entry.Sequences![0], options);
                        json.Add(JsonValue.Create(text));
                    }
                    else
                    {
                        var text = entry.Sequences!.Select(x => formatter.Format(x, options)).ToArray();
                        json.Add(JsonValue.Create(text));
                    }
                }
                return json;
            }
        }

        public static string Export(MsgWrapper wrapper, MsgFormatter formatter, ExportOptions exportOptions)
        {
            switch (exportOptions.Extension)
            {
                case ".txt":
                    {
                        var text = GetText(wrapper, formatter, exportOptions.StringOptions, exportOptions.IncludeId);
                        if (!string.IsNullOrEmpty(exportOptions.Path))
                        {
                            File.AppendAllText(exportOptions.Path, text);
                        }
                        return text;
                    }
                case ".json":
                    {
                        var json = GetJsonNode(wrapper, formatter, exportOptions.StringOptions, exportOptions.IncludeId);
                        var text = JsonSerialize(json);
                        if (!string.IsNullOrEmpty(exportOptions.Path))
                        {
                            File.WriteAllText(exportOptions.Path, text);
                        }
                        return text;
                    }
                case ".csv":
                    {
                        return "";
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public static string Export(MsgWrapper[] wrappers, MsgFormatter formatter, ExportOptions exportOptions)
        {
            switch (exportOptions.Extension)
            {
                case ".txt" when !exportOptions.Merged:
                    {
                        var text = "";
                        foreach (var wrapper in wrappers)
                        {
                            text = GetText(wrapper, formatter, exportOptions.StringOptions, exportOptions.IncludeId);
                            if (!string.IsNullOrEmpty(exportOptions.Path))
                            {
                                var path = Path.Combine(exportOptions.Path, wrapper.Name);
                                path = Path.ChangeExtension(path, ".txt");
                                Directory.CreateDirectory(Path.GetDirectoryName(path));
                                File.WriteAllText(path, text);
                            }
                        }
                        return text;
                    }
                case ".txt" when exportOptions.Merged:
                    {
                        var sb = new StringBuilder();
                        foreach (var wrapper in wrappers)
                        {
                            sb.AppendFormat("[{0}]", wrapper.Name).AppendLine();
                            var lines = GetText(wrapper, formatter, exportOptions.StringOptions, exportOptions.IncludeId);
                            sb.AppendLine(lines);
                            sb.AppendLine();
                        }
                        var text = sb.ToString();
                        if (!string.IsNullOrEmpty(exportOptions.Path))
                        {
                            File.WriteAllText(exportOptions.Path, text);
                        }
                        return text;
                    }
                case ".json" when !exportOptions.Merged:
                    {
                        var text = "";
                        foreach (var wrapper in wrappers)
                        {
                            var json = GetJsonNode(wrapper, formatter, exportOptions.StringOptions, exportOptions.IncludeId);
                            text = JsonSerialize(json);
                            if (!string.IsNullOrEmpty(exportOptions.Path))
                            {
                                var path = Path.Combine(exportOptions.Path, wrapper.Name);
                                path = Path.ChangeExtension(path, ".json");
                                File.WriteAllText(path, text);
                            }
                        }
                        return text;
                    }
                case ".json" when exportOptions.Merged && wrappers[0].Name == null:
                    {
                        var json = new JsonArray();
                        foreach (var wrapper in wrappers)
                        {
                            var node = GetJsonNode(wrapper, formatter, exportOptions.StringOptions, exportOptions.IncludeId);
                            var name = Path.GetFileNameWithoutExtension(wrapper.Name);
                            json.Add(node);
                        }
                        var text = JsonSerialize(json);
                        if (!string.IsNullOrEmpty(exportOptions.Path))
                        {
                            File.WriteAllText(exportOptions.Path, text);
                        }
                        return text;
                    }
                case ".json" when exportOptions.Merged && wrappers[0].Name != null:
                    {
                        var json = new JsonObject();
                        foreach (var wrapper in wrappers)
                        {
                            var node = GetJsonNode(wrapper, formatter, exportOptions.StringOptions, exportOptions.IncludeId);
                            var name = Path.GetFileNameWithoutExtension(wrapper.Name);
                            json.Add(name, node);
                        }
                        var text = JsonSerialize(json);
                        if (!string.IsNullOrEmpty(exportOptions.Path))
                        {
                            File.WriteAllText(exportOptions.Path, text);
                        }
                        return text;
                    }
                default:
                    throw new NotImplementedException();
            }
        }


        public static string Export(MultilingualWrapper[] multiwrappers, MsgFormatter formatter, ExportOptions exportOptions)
        {
            switch (exportOptions.Extension)
            {
                case ".txt" when !exportOptions.Merged:
                    {
                        var text = "";
                        foreach (var multiwrapper in multiwrappers )
                        {
                            foreach(var (langcode, wrapper) in multiwrapper.Wrappers)
                            {
                                text = GetText(wrapper, formatter, exportOptions.StringOptions, exportOptions.IncludeId);
                                if (!string.IsNullOrEmpty(exportOptions.Path))
                                {
                                    var path = Path.Combine(exportOptions.Path, langcode, wrapper.Name);
                                    path = Path.ChangeExtension(path, ".txt");
                                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                                    File.WriteAllText(path, text);
                                }
                            }
                        }
                        return text;
                    }
                case ".json" when !exportOptions.Merged:
                    {
                        var text = "";
                        foreach (var multiwrapper in multiwrappers)
                        {
                            foreach (var (langcode, wrapper) in multiwrapper.Wrappers)
                            {
                                var json = GetJsonNode(wrapper, formatter, exportOptions.StringOptions, exportOptions.IncludeId);
                                text = JsonSerialize(json);
                                if (!string.IsNullOrEmpty(exportOptions.Path))
                                {
                                    var path = Path.Combine(exportOptions.Path, langcode, wrapper.Name);
                                    path = Path.ChangeExtension(path, ".txt");
                                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                                    File.WriteAllText(path, text);
                                }
                            }
                        }
                        return text;
                    }
                case ".csv":
                    {
                        var text = "";
                        foreach (var multiwrapper in multiwrappers)
                        {
                            text = GetCSV(multiwrapper, formatter, exportOptions.StringOptions);
                            if (!string.IsNullOrEmpty(exportOptions.Path))
                            {
                                var path = Path.Combine(exportOptions.Path, multiwrapper.Name);
                                path = Path.ChangeExtension(path, ".csv");
                                Directory.CreateDirectory(Path.GetDirectoryName(path));
                                File.WriteAllText(path, text, Encoding.Unicode);
                            }
                        }
                        return text;
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        private static string JsonSerialize<T>(T obj)
        {
            var option = new JsonSerializerOptions()
            {
                WriteIndented = true,
                IgnoreReadOnlyProperties = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            
            option.Converters.Add(new JsonStringEnumConverter());
            var text = JsonSerializer.Serialize(obj, option);
            text = text.Replace(@"\u3000", "\u3000");
            return text;
        }

    }
}