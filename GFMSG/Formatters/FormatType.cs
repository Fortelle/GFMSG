namespace GFMSG
{

    [Flags]
    public enum StringFormat
    {
        Raw = 0,
        Markup = 1,
        Plain = 2,
        Html = 4,
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

}