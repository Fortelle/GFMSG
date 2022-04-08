namespace GFMSG
{

    [Flags]
    public enum StringFormat
    {
        Raw = 1,
        Markup = 2,
        Plain = 4,
        Html = 8,
        Hex8 = 16,
        Hex16 = 32,
    }

    public enum GenderForm
    {
        All,
        ForceMasculine,
        ForceFeminine,
        ForceNeuter,
    }

    public enum NumberForm
    {
        All,
        ForceSingular,
        ForcePlural,
    }

    public enum ExportFormat
    {
        Text = 0,
        Json = 1,
        Xml = 2,
    }

    public enum FileVersion
    {
        Unknown = 0,

        GenIV,   // dp, hgss, pl
        GenV,    // bw, b2w2
        GenVI,   // 3ds
        GenVIII, // ns
    }

}