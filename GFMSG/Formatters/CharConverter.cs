namespace GFMSG;

public class CharConverter
{
    public delegate string? ToTextDelegate(CharToTextHandler handler);

    public ushort CodeStart { get; }
    public ushort CodeEnd { get; }
    public StringFormat? Formats { get; }

    public ToTextDelegate ToText { get; init; }

    public CharConverter(ushort start, ushort end, StringFormat? formats = null)
    {
        CodeStart = start;
        CodeEnd = end;
        Formats = formats;
    }
}

public class CharToTextHandler
{
    public ushort Code { get; set; }
    public StringOptions Options { get; set; }

    public ushort CodeStart { get; set; }
    public ushort CodeEnd { get; set; }

    public int CodeIndex => Code - CodeStart;
}