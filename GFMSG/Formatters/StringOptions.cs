namespace GFMSG
{
    public struct StringOptions
    {
        public StringFormat Format { get; set; } = StringFormat.Raw;
        public string LanguageCode { get; set; } = "";

        public bool RemoveLineBreaks { get; set; } = false;
        public bool JapaneseSpaceAfterPunctuation { get; set; } = true;
        public bool JapaneseFullwidthSpace { get; set; } = true;
        public bool LatinHalfwidthPunctuation { get; set; } = false;

        public GenderForm DefaultGender { get; set; } = GenderForm.All;
        public NumberForm DefaultNumber { get; set; } = NumberForm.All;
        public bool IgnoreRuby { get; set; } = false;
        public bool IgnoreSpeaker { get; set; } = false;

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
