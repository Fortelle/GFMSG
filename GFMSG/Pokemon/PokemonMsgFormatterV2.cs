using System.Diagnostics;

namespace GFMSG.Pokemon
{
    public class PokemonMsgFormatterV2 : MsgFormatterV2
    {
        public string TrainerNameFieldFilename = ""; // for TAG_01_14(TRAINER_NAME_FIELD)

        public PokemonMsgFormatterV2() : this(FileVersion.GenVIII)
        {

        }
        public PokemonMsgFormatterV2(FileVersion version) : base(version)
        {
            /*
            0x00: NULL
            0x01: WORD
            0x02: NUMBER
            0x10: GRAMMAR_FORCE
            0x11: STRING_SELECT
            0x12: JAPANESE
            0x13: ENGLISH
            0x14: FRENCH
            0x15: ITARIAN
            0x16: GERMAN
            0x17: SPANISH
            0x19: KOREAN
            0xBD: GENERAL_CTRL
            0xBE: STREAM_CTRL
            0xFF: SYSTEM
            */

            AddConverter(new TagConverter(0x01, 0x14, "TRAINER_NAME_FIELD", StringFormat.Plain | StringFormat.Html)
            {
                ToText = (handler) => {
                    if (handler.Options.IgnoreSpeaker) return "";
                    if (string.IsNullOrEmpty(TrainerNameFieldFilename)) return "";
                    var trainerId = handler.Parameters[0];
                    var args = new RequireArguments()
                    {
                        Filename = TrainerNameFieldFilename,
                        EntryIndex = trainerId,
                        LanguageIndex = 0,
                        StringOptions = handler.Options,
                    };
                    var trname = RequireText?.Invoke(args);
                    if (trname == null) return "";
                    return " (" + trname + ")";
                },
            });

            AddConverter(new TagConverter(0xBE, 0x00, "LINE_FEED", StringFormat.Plain | StringFormat.Html)
            {
                ToText = (handler) => {
                    handler.Queue.Insert(new CharSymbol('\n'));
                    return "";
                },
            });

            AddConverter(new TagConverter(0xBE, 0x01, "PAGE_CLEAR", StringFormat.Plain | StringFormat.Html)
            {
                ToText = (handler) => {
                    handler.Queue.Insert(new CharSymbol('\n'));
                    return "";
                },
            });

            for (byte index = 0; index <= 10; index++) // ??
            {
                AddConverter(new TagConverter(0x01, index, $"NUMBER_{index}digit", StringFormat.Plain | StringFormat.Html)
                {
                    ToText = (handler) =>
                    {
                        var digit = handler.Index;
                        var sepCode = handler.Parameters.Length >= 2 ? handler.Parameters[1] : 0;
                        var separator = sepCode > 0 ? $"{(char)sepCode}" : "";

                        if (handler.Options.ConvertWordsetToPlaceholder)
                        {
                            var varname = new string('?', digit > 0 ? (int)digit : 1);
                            if (separator.Length > 0)
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
                            var varname = handler.Parameters.Length > 0 ? $"num{handler.Parameters[0] + 1}" : "num";
                            return handler.Options switch
                            {
                                { Format: StringFormat.Plain } => $"<{varname}>",
                                { Format: StringFormat.Html } => $"<var>{varname}</var>",
                                _ => throw new NotSupportedException(),
                            };
                        }
                    },
                });
            }

            var selectstringdict = new Dictionary<byte, string>()
            {
                [0x00] = "BY_GENDER",
                [0x01] = "BY_QUANTITY",
                [0x02] = "BY_GENDER_QUANTITY",
                [0x03] = "BY_GENDER_QUANTITY_GERMAN",
            };
            foreach (var (index, name) in selectstringdict) {
                AddConverter(new TagConverter(0x11, index, name) {
                    ToText = SelectStringToText,
                    ToSymbol = SelectStringToSymbol
                });
            }

            AddConverter(new TagConverter(0xBD, 0x00, "CHANGE_COLOR")
            {
                ToText = (handler) =>
                {
                    ushort colorIndex = handler.Parameters[0];
                    return handler.Options switch
                    {
                        { Format: StringFormat.Markup } => $@"{colorIndex}",
                        { Format: StringFormat.Html } => $@"<font color=""{ColorTranslator.ToHtml(GeneralColorTable[colorIndex].Value)}"">",
                        { Format: StringFormat.Plain } => $@"",
                        _ => throw new NotSupportedException(),
                    };
                },

                ToSymbol = (handler) =>
                {
                    ushort colorIndex = Convert.ToUInt16(handler.Arguments[0]);
                    return new[] { colorIndex };
                },
            });

            AddConverter(new TagConverter(0xBD, 0x01, "BACK_COLOR", StringFormat.Plain | StringFormat.Html)
            {
                ToText = (handler) =>
                {
                    return handler.Options switch
                    {
                        { Format: StringFormat.Html } => $@"</font>",
                        { Format: StringFormat.Plain } => $@"",
                        _ => throw new NotSupportedException(),
                    };
                },
            });

            AddConverter(new TagConverter(0xFF, 0x00, "COLOR")
            {
                ToText = (handler) =>
                {
                    ushort colorIndex = handler.Parameters[0];
                    return handler.Options switch
                    {
                        { Format: StringFormat.Markup } => $@"{colorIndex}",
                        { Format: StringFormat.Html } when colorIndex == 0 => $@"</font>",
                        { Format: StringFormat.Html } when colorIndex > 0 => $@"<font color=""{ColorTranslator.ToHtml(SystemColorTable[colorIndex].Value)}"">",
                        { Format: StringFormat.Plain } => $@"",
                        _ => throw new NotSupportedException(),
                    };
                },

                ToSymbol = (handler) =>
                {
                    ushort colorIndex = Convert.ToUInt16(handler.Arguments[0]);
                    return new[] { colorIndex };
                },
            });

            AddConverter(new TagConverter(0xFF, 0x01, "RUBY")
            {
                ToText = (handler) =>
                {
                    var rblen = handler.Parameters[0];
                    var rtlen = handler.Parameters[1];
                    var rb = new string(handler.Parameters[2..(2 + rblen)].Select(x => (char)x).ToArray());
                    var rt = new string(handler.Parameters[(2 + rblen)..(2 + rblen + rtlen)].Select(x => (char)x).ToArray());
                    var def = handler.Queue.Read(rblen);
                    return handler.Options switch
                    {
                        { Format: StringFormat.Markup } => $@"{rb},{rt}",
                        { Format: StringFormat.Html, IgnoreRuby: false } => $@"<ruby><rb>{rb}</rb><rp>(</rp><rt>{rt}</rt><rp>)</rp></ruby>",
                        { Format: StringFormat.Html, IgnoreRuby: true } => rb,
                        { Format: StringFormat.Plain, IgnoreRuby: false } => $@"{rb}({rt})",
                        { Format: StringFormat.Plain, IgnoreRuby: true } => rb,
                        _ => throw new NotSupportedException(),
                    };
                },

                ToSymbol = (handler) =>
                {
                    var rb = handler.Arguments[0];
                    var rt = handler.Arguments[1];
                    var list = new List<ushort>();
                    list.Add((ushort)rb.Length);
                    list.Add((ushort)rt.Length);
                    list.AddRange(rb.Select(x => (ushort)x));
                    list.AddRange(rt.Select(x => (ushort)x));
                    handler.Append.AddRange(rb.Select(x => (ushort)x));
                    return list.ToArray();
                },
            });

            AddConverter(new TagConverter(0x10, 0x00, "SINGULAR"));
            AddConverter(new TagConverter(0x10, 0x01, "PLURAL"));
            AddConverter(new TagConverter(0x10, 0x02, "MASCULINE"));
            AddConverter(new TagConverter(0x10, 0x03, "FEMININE"));
            AddConverter(new TagConverter(0x10, 0x04, "NEUTER"));
            AddConverter(new TagConverter(0xBD, 0xFF, "NOTUSED_MESSAGE"));
            AddConverter(new TagConverter(0xBE, 0x02, "WAIT_ONE"));
            AddConverter(new TagConverter(0xBE, 0x03, "WAIT_CONT"));
            AddConverter(new TagConverter(0xBE, 0x04, "WAIT_RESET"));
            AddConverter(new TagConverter(0xBE, 0x05, "CALLBACK_ONE"));
            AddConverter(new TagConverter(0xBE, 0x06, "CALLBACK_CONT"));
            AddConverter(new TagConverter(0xBE, 0x07, "CALLBACK_RESET"));
            AddConverter(new TagConverter(0xBE, 0x08, "CLEAR_WIN"));
            AddConverter(new TagConverter(0xBE, 0x09, "CTRL_SPEED"));
        }

        private string? SelectStringToText(TagToTextHandler handler)
        {
            var ref_word_id = handler.Parameters[0];
            var stringLength = (handler.Parameters.Length - 1) * 2;
            // Debug.Assert(stringLength <= 4);
            var lengths = new byte[4];
            for (var j = 0; j < handler.Parameters.Length - 1; j++)
            {
                var param = handler.Parameters[j + 1];
                lengths[j * 2] = (byte)(param & 0xff);
                lengths[j * 2 + 1] = (byte)(param >> 8);
            }
            var texts = new List<string>();
            for (var j = 0; j < stringLength; j++)
            {
                var text = handler.Queue.Read(lengths[j]);
                texts.Add(text);
            }
            return handler.Options switch
            {
                { Format: StringFormat.Markup } => ref_word_id + "," + string.Join(",", texts),

                { Gender: GenderForm.ForceMasculine } when handler.Name == "BY_GENDER" => texts[0],
                { Gender: GenderForm.ForceFeminine } when handler.Name == "BY_GENDER" => texts[1],

                { Number: NumberForm.ForceSingular } when handler.Name == "BY_QUANTITY" => texts[0],
                { Number: NumberForm.ForcePlural } when handler.Name == "BY_QUANTITY" => texts[1],

                { Gender: GenderForm.ForceMasculine, Number: NumberForm.ForceSingular } when handler.Name == "BY_GENDER_QUANTITY" => texts[0],
                { Gender: GenderForm.ForceFeminine, Number: NumberForm.ForceSingular } when handler.Name == "BY_GENDER_QUANTITY" => texts[1],
                { Gender: GenderForm.ForceMasculine, Number: NumberForm.ForcePlural } when handler.Name == "BY_GENDER_QUANTITY" => texts[2],
                { Gender: GenderForm.ForceFeminine, Number: NumberForm.ForcePlural } when handler.Name == "BY_GENDER_QUANTITY" => texts[3],

                { Gender: GenderForm.ForceMasculine, Number: NumberForm.ForceSingular } when handler.Name == "BY_GENDER_QUANTITY_GERMAN" => texts[0],
                { Gender: GenderForm.ForceFeminine, Number: NumberForm.ForceSingular } when handler.Name == "BY_GENDER_QUANTITY_GERMAN" => texts[1],
                { Gender: GenderForm.ForceNeuter, Number: NumberForm.ForceSingular } when handler.Name == "BY_GENDER_QUANTITY_GERMAN" => texts[2],
                { Number: NumberForm.ForcePlural } when handler.Name == "BY_GENDER_QUANTITY_GERMAN" => texts[3],

                _ => texts.Count(x => x.Length > 0) == 1 ? $"({texts.First(x => x.Length > 0)})" : string.Join("/", texts),
            };
        }

        private ushort[] SelectStringToSymbol(TagToSymbolHandler handler)
        {
            var list = new List<ushort>();
            var ref_word_id = Convert.ToUInt16(handler.Arguments[0]);
            var texts = handler.Arguments[1..];

            list.Add(ref_word_id);
            for (var i = 0; i < texts.Length / 2; i++)
            {
                ushort len = (ushort)texts[i].Length;
                len |= (ushort)(texts[i * 2 + 1].Length << 8);
                list.Add(len);
            }

            for (var i = 0; i < texts.Length; i++)
            {
                handler.Append.AddRange(texts[i].Select(x => (ushort)x));
            }

            return list.ToArray();
        }

        protected void AddWordSet(byte index, string name)
        {
            AddConverter(new TagConverter(0x01, index, name, StringFormat.Plain | StringFormat.Html)
            {
                ToText = WordToText,
            });
        }

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

    }
}