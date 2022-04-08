namespace GFMSG.Pokemon
{
    public class BwMsgFormatter : PokemonMsgFormatterV2
    {
        public virtual Dictionary<string, string[]> LanguageCodes { get; } = new()
        {
            ["jpn"] = new[] { "ja-Hrkt", "ja-Jpan" },
            ["usa"] = new[] { "en-US" },
            ["fra"] = new[] { "fr" },
            ["ita"] = new[] { "it" },
            ["ger"] = new[] { "de" },
            ["spa"] = new[] { "es" },
            ["kor"] = new[] { "ko" },
        };

        public BwMsgFormatter () : base(FileVersion.GenV)
        {
        }
    }
}