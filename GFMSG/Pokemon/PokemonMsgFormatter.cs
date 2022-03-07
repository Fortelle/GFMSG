using System.Diagnostics;

namespace GFMSG.Pokemon
{
    public class PokemonMsgFormatter : MsgFormatter
    {
        public virtual Dictionary<string, string> LanguageMap { get; } = new()
        {
            ["ja-Hrkt"] = "JPN",
            ["ja-Jpan"] = "JPN_KANJI",
            ["en-US"] = "English",
            ["fr"] = "French",
            ["it"] = "Italian",
            ["de"] = "German",
            ["es"] = "Spanish",
            ["ko"] = "Korean",
            ["zh-Hans"] = "Simp_Chinese",
            ["zh-Hant"] = "Trad_Chinese",
        };

        protected virtual Color?[] GeneralColorTable { get; } = new Color?[] {
            Color.FromArgb(0xFF, 0x00, 0x00),
            Color.FromArgb(0x00, 0xC0, 0x00),
            Color.FromArgb(0x00, 0x8C, 0xFF),
            Color.FromArgb(0xFF, 0x80, 0x00),
            Color.FromArgb(0xFF, 0x80, 0xFF),
            Color.FromArgb(0x8C, 0x00, 0xFF),
            Color.FromArgb(0x50, 0xC8, 0xE4),
            Color.FromArgb(0xFF, 0xE6, 0x50),
            Color.FromArgb(0x64, 0xB4, 0xFF),
            Color.FromArgb(0xFF, 0xFF, 0x50),
        };

        protected virtual Color?[] SystemColorTable { get; } = new Color?[] {
            Color.FromArgb(0x00, 0x00, 0x00),
            Color.FromArgb(0xFF, 0x00, 0x00),
            Color.FromArgb(0x00, 0x8C, 0xFF),
            Color.FromArgb(0x00, 0xC0, 0x00),
            null,
            null,
            Color.FromArgb(0xFF, 0x80, 0xFF),
            null,
            Color.FromArgb(0x50, 0xC8, 0xE4),
        };

        public string TrainerNameFieldFilename = "";

        public PokemonMsgFormatter() : base()
        {
            AddTagGroup(0x00, "NULL");

            // word set
            AddTagGroup(0x01, "WORD"); 
            AddTagGroup(0x02, "NUMBER");

            AddTagGroup(0x10, "GRAMMAR_FORCE");
            AddTagGroup(0x11, "STRING_SELECT");

            // word form
            AddTagGroup(0x12, "JAPANESE"); 
            AddTagGroup(0x13, "ENGLISH");
            AddTagGroup(0x14, "FRENCH");
            AddTagGroup(0x15, "ITARIAN");
            AddTagGroup(0x16, "GERMAN");
            AddTagGroup(0x17, "SPANISH");
            AddTagGroup(0x19, "KOREAN");

            AddTagGroup(0xBD, "GENERAL_CTRL");
            AddTagGroup(0xBE, "STREAM_CTRL");
            AddTagGroup(0xFF, "SYSTEM");

            AddTagIndex(0x10, 0x00, "SINGULAR");
            AddTagIndex(0x10, 0x01, "PLURAL");
            AddTagIndex(0x10, 0x02, "MASCULINE");
            AddTagIndex(0x10, 0x03, "FEMININE");
            AddTagIndex(0x10, 0x04, "NEUTER");

            AddTagIndex(0x11, 0x00, "BY_GENDER");
            AddTagIndex(0x11, 0x01, "BY_QUANTITY");
            AddTagIndex(0x11, 0x02, "BY_GENDER_QUANTITY");
            AddTagIndex(0x11, 0x03, "BY_GENDER_QUANTITY_GERMAN");

            AddTagIndex(0x19, 0x00, "PARTICLE");

            AddTagIndex(0xBD, 0x00, "CHANGE_COLOR");
            AddTagIndex(0xBD, 0x01, "BACK_COLOR");
            AddTagIndex(0xBD, 0x02, "X_CENTERING");
            AddTagIndex(0xBD, 0x03, "X_FIT_RIGHT");
            AddTagIndex(0xBD, 0x04, "X_ADD");
            AddTagIndex(0xBD, 0x05, "X_MOVE");
            AddTagIndex(0xBD, 0x06, "BATTLE_ONELINE");
            AddTagIndex(0xBD, 0x07, "FIX_WIDTH_START");
            AddTagIndex(0xBD, 0x08, "FIX_WIDTH_DIRECT");
            AddTagIndex(0xBD, 0x09, "FIX_WIDTH_END");
            AddTagIndex(0xBD, 0xFF, "NOTUSED_MESSAGE");

            AddTagIndex(0xBE, 0x00, "LINE_FEED");
            AddTagIndex(0xBE, 0x01, "PAGE_CLEAR");
            AddTagIndex(0xBE, 0x02, "WAIT_ONE");
            AddTagIndex(0xBE, 0x03, "WAIT_CONT");
            AddTagIndex(0xBE, 0x04, "WAIT_RESET");
            AddTagIndex(0xBE, 0x05, "CALLBACK_ONE");
            AddTagIndex(0xBE, 0x06, "CALLBACK_CONT");
            AddTagIndex(0xBE, 0x07, "CALLBACK_RESET");
            AddTagIndex(0xBE, 0x08, "CLEAR_WIN");
            AddTagIndex(0xBE, 0x09, "CTRL_SPEED");

            AddTagIndex(0xFF, 0x00, "CHANGE_COLOR");
            AddTagIndex(0xFF, 0x01, "RUBY");


            AddConverter(new TagConverter("WORD", "TRAINER_NAME_FIELD", StringFormat.Plain | StringFormat.Html)
            {
                ToText = (tag, args, options, buff) => {
                    if (options.IgnoreSpeaker) return "";
                    if (string.IsNullOrEmpty(TrainerNameFieldFilename)) return "";
                    var trainer_id = args[0];
                    var trname = Tags.RequireText(TrainerNameFieldFilename, trainer_id, options);
                    if (trname == null) return "";
                    return " (" + trname + ")";
                },
            });

            AddConverter(new TagConverter("STREAM_CTRL", "PAGE_CLEAR", StringFormat.Plain | StringFormat.Html)
            {
                ToText = (tag, args, options, buff) => {
                    buff.Insert(CharSymbol.LineFeed);
                    return "";
                },
            });

            AddConverter(new TagConverter("STREAM_CTRL", "LINE_FEED", StringFormat.Plain | StringFormat.Html)
            {
                ToText = (tag, args, options, buff) => {
                    buff.Insert(CharSymbol.LineFeed);
                    return "";
                },
            });

            AddConverter(new TagConverter("WORD", StringFormat.Plain | StringFormat.Html)
            {
                ToText = (tag, args, options, buff) =>
                {
                    if (options.ConvertWordsetToPlaceholder)
                    {
                        var varname = options.LanguageCode.Split('-')[0].ToLower() switch
                        {
                            "ja" or "zh" => "〇〇〇〇〇",
                            _ => "*****",
                        };
                        return varname;
                    }
                    else
                    {
                        var varname = tag.IndexName?.ToLower() ?? "var";
                        return options switch
                        {
                            { Format: StringFormat.Plain } => $"<{varname}>",
                            { Format: StringFormat.Html } => $"<var>{varname}</var>",
                            _ => throw new NotSupportedException(),
                        };
                    }
                },
            });

            AddConverter(new TagConverter("NUMBER", StringFormat.Plain | StringFormat.Html)
            {
                ToText = (tag, args, options, buff) =>
                {
                    var digit = tag.IndexValue;
                    var sepCode = args.Length >= 2 ? args[1] : 0;
                    var separator = sepCode > 0 ? $"{(char)sepCode}" : "";

                    if (options.ConvertWordsetToPlaceholder)
                    {
                        var varname = new string('?', digit > 0 ? (int)digit : 1);
                        if(separator .Length > 0)
                        {
                            varname = string.Join(separator, varname
                                .Reverse()
                                .Chunk(3)
                                .Select(x => string.Join("", x.Reverse()))
                                .Reverse()
                                );
                        }
                        return varname;
                    }
                    else
                    {
                        var varname = $"num{args[0] + 1}";
                        return $"<num{args[0] + 1}>";
                    }
                },
            });

            AddConverter(new TagConverter("STRING_SELECT")
            {
                ToText = (tag, args, options, buff) =>
                {
                    var ref_word_id = args[0];
                    var stringLength = (args.Length - 1) * 2;
                    // Debug.Assert(stringLength <= 4);
                    var lengths = new byte[4];
                    for (var j = 0; j < args.Length - 1; j++)
                    {
                        var param = args[j + 1];
                        lengths[j * 2] = (byte)(param & 0xff);
                        lengths[j * 2 + 1] = (byte)(param >> 8);
                    }
                    var texts = new List<string>();
                    for (var j = 0; j < stringLength; j++)
                    {
                        var text = buff.Read(lengths[j]);
                        texts.Add(text);
                    }
                    return options switch
                    {
                        { Format: StringFormat.Markup } => ref_word_id + "," + string.Join(",", texts),

                        { DefaultGender: GenderForm.ForceMasculine } when tag.IndexName == "BY_GENDER" => texts[0],
                        { DefaultGender: GenderForm.ForceFeminine } when tag.IndexName == "BY_GENDER" => texts[1],

                        { DefaultNumber: NumberForm.ForceSingular } when tag.IndexName == "BY_QUANTITY" => texts[0],
                        { DefaultNumber: NumberForm.ForcePlural } when tag.IndexName == "BY_QUANTITY" => texts[1],

                        { DefaultGender: GenderForm.ForceMasculine, DefaultNumber: NumberForm.ForceSingular } when tag.IndexName == "BY_GENDER_QUANTITY" => texts[0],
                        { DefaultGender: GenderForm.ForceFeminine, DefaultNumber: NumberForm.ForceSingular } when tag.IndexName == "BY_GENDER_QUANTITY" => texts[1],
                        { DefaultGender: GenderForm.ForceMasculine, DefaultNumber: NumberForm.ForcePlural } when tag.IndexName == "BY_GENDER_QUANTITY" => texts[2],
                        { DefaultGender: GenderForm.ForceFeminine, DefaultNumber: NumberForm.ForcePlural } when tag.IndexName == "BY_GENDER_QUANTITY" => texts[3],

                        { DefaultGender: GenderForm.ForceMasculine, DefaultNumber: NumberForm.ForceSingular } when tag.IndexName == "BY_GENDER_QUANTITY_GERMAN" => texts[0],
                        { DefaultGender: GenderForm.ForceFeminine, DefaultNumber: NumberForm.ForceSingular } when tag.IndexName == "BY_GENDER_QUANTITY_GERMAN" => texts[1],
                        { DefaultGender: GenderForm.ForceNeuter, DefaultNumber: NumberForm.ForceSingular } when tag.IndexName == "BY_GENDER_QUANTITY_GERMAN" => texts[2],
                        { DefaultNumber: NumberForm.ForcePlural } when tag.IndexName == "BY_GENDER_QUANTITY_GERMAN" => texts[3],

                        _ => texts.Count(x => x.Length > 0) == 1 ? $"({texts.First(x => x.Length > 0)})" : string.Join("/", texts),
                    };
                },

                ToSymbol = (tag, args, append) =>
                {
                    var list = new List<ushort>();
                    var ref_word_id = Convert.ToUInt16(args[0]);
                    var texts = args[1..];

                    list.Add(ref_word_id);
                    for (var i = 0; i < texts.Length / 2; i++)
                    {
                        ushort len = (ushort)texts[i].Length;
                        len |= (ushort)(texts[i * 2 + 1].Length << 8);
                        list.Add(len);
                    }

                    for (var i = 0; i < texts.Length; i++)
                    {
                        append.AddRange(texts[i].Select(x => (ushort)x));
                    }

                    return list.ToArray();
                },
            });

            AddConverter(new TagConverter("GENERAL_CTRL", "CHANGE_COLOR")
            {
                ToText = (tag, args, options, buff) =>
                {
                    ushort colorIndex = args[0];
                    return options switch
                    {
                        { Format: StringFormat.Markup } => $@"{colorIndex}",
                        { Format: StringFormat.Html } => $@"<font color=""{ColorTranslator.ToHtml(GeneralColorTable[colorIndex].Value)}"">",
                        { Format: StringFormat.Plain } => $@"",
                        _ => throw new NotSupportedException(),
                    };
                },

                ToSymbol = (tag, args, append) =>
                {
                    ushort colorIndex = Convert.ToUInt16(args[0]);
                    return new[] { colorIndex };
                },
            });

            AddConverter(new TagConverter("GENERAL_CTRL", "BACK_COLOR", StringFormat.Plain | StringFormat.Html)
            {
                ToText = (tag, args, options, buff) =>
                {
                    return options switch
                    {
                        { Format: StringFormat.Html } => $@"</font>",
                        { Format: StringFormat.Plain } => $@"",
                        _ => throw new NotSupportedException(),
                    };
                },
            });

            AddConverter(new TagConverter("SYSTEM", "CHANGE_COLOR")
            {
                ToText = (tag, args, options, buff) =>
                {
                    ushort colorIndex = args[0];
                    return options switch
                    {
                        { Format: StringFormat.Markup } => $@"{colorIndex}",
                        { Format: StringFormat.Html } when colorIndex == 0 => $@"</font>",
                        { Format: StringFormat.Html } when colorIndex > 0 => $@"<font color=""{ColorTranslator.ToHtml(SystemColorTable[colorIndex].Value)}"">",
                        { Format: StringFormat.Plain } => $@"",
                        _ => throw new NotSupportedException(),
                    };
                },

                ToSymbol = (tag, args, append) =>
                {
                    ushort colorIndex = Convert.ToUInt16(args[0]);
                    return new[] { colorIndex };
                },
            });

            AddConverter(new TagConverter("SYSTEM", "RUBY")
            {
                ToText = (tag, args, options, buff) =>
                {
                    var rblen = args[0];
                    var rtlen = args[1];
                    var rb = new string(args[2..(2 + rblen)].Select(x => (char)x).ToArray());
                    var rt = new string(args[(2 + rblen)..(2 + rblen + rtlen)].Select(x => (char)x).ToArray());
                    var def = buff.Read(rblen);
                    return options switch
                    {
                        { Format: StringFormat.Markup } => $@"{rb},{rt}",
                        { Format: StringFormat.Html, IgnoreRuby: false } => $@"<ruby><rb>{rb}</rb><rp>(</rp><rt>{rt}</rt><rp>)</rp></ruby>",
                        { Format: StringFormat.Html, IgnoreRuby: true } => rb,
                        { Format: StringFormat.Plain, IgnoreRuby: false } => $@"{rb}({rt})",
                        { Format: StringFormat.Plain, IgnoreRuby: true } => rb,
                        _ => throw new NotSupportedException(),
                    };
                },

                ToSymbol = (tag, args, append) =>
                {
                    var rb = args[0];
                    var rt = args[1];
                    var list = new List<ushort>();
                    list.Add((ushort)rb.Length);
                    list.Add((ushort)rt.Length);
                    list.AddRange(rb.Select(x => (ushort)x));
                    list.AddRange(rt.Select(x => (ushort)x));
                    append.AddRange(rb.Select(x => (ushort)x));
                    return list.ToArray();
                },
            });

        }
    }
}