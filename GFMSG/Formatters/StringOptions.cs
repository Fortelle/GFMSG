using System.ComponentModel;

namespace GFMSG
{
    public struct StringOptions
    {
        [Category("Format")]
        [Description("The format of the output text.")]
        [DefaultValue(StringFormat.Raw)]
        public StringFormat Format { get; set; } = StringFormat.Raw;

        [Category("Format")]
        [Description("The language of the text.")]
        [DefaultValue("")]
        public string LanguageCode { get; set; } = "";

        [Category("Format")]
        [Description("Indicates whether to remove the line breaks.")]
        [DefaultValue(false)]
        public bool RemoveLineBreaks { get; set; } = false;

        [Category("Grammar")]
        [Description("Indicates how to render the gender tags.")]
        [DefaultValue(GenderForm.All)]
        public GenderForm Gender { get; set; } = GenderForm.All;

        [Category("Grammar")]
        [Description("Indicates how to render the number tags.")]
        [DefaultValue(NumberForm.All)]
        public NumberForm Number { get; set; } = NumberForm.All;

        [Category("Tags")]
        [DefaultValue(false)]
        [Description("Indicates whether to ignore the ruby text.")]
        public bool IgnoreRuby { get; set; } = false;

        [Category("Tags")]
        [DefaultValue(false)]
        [Description("Indicates whether to ignore the name of the speaker in dialogue.")]
        public bool IgnoreSpeaker { get; set; } = false;


        [DefaultValue(false)]
        public bool ConvertWordsetToPlaceholder { get; set; } = false;

        [DefaultValue(true)]
        public bool JapaneseSpaceAfterPunctuation { get; set; } = true;


        [Obsolete]
        [DefaultValue(true)]
        public bool JapaneseFullwidthSpace { get; set; } = true;

        [Obsolete]
        [DefaultValue(false)]
        public bool LatinHalfwidthPunctuation { get; set; } = false;



        public StringOptions()
        {
        }

        public StringOptions(StringFormat format, string langcode)
        {
            Format = format;
            LanguageCode = langcode;
        }

    }
}
