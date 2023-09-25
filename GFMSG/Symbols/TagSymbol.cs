using System.Text;
using System.Text.RegularExpressions;

namespace GFMSG;

public class TagSymbol : ISymbol
{
    private static readonly Regex TagRegex = new(@"^\{TAG_(..)_(..)((?:[:,].+?)*)\}$");
    public const char ParameterSeparator = ':';

    public byte Group { get; init; }
    public byte Index { get; init; }
    public ushort[] Parameters { get; init; }

    public int Size => 6 + Parameters.Length * 2;
    public ushort Code => (ushort)((Group << 8) | Index);
    public string GetName() => $"TAG_{Group:X2}_{Index:X2}";

    public TagSymbol(byte group, byte index, ushort[] parameters)
    {
        Group = group;
        Index = index;
        Parameters = parameters;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(@"{");
        sb.Append(GetName());
        for (var i = 0; i < Parameters.Length; i++)
        {
            sb.Append(i == 0 ? ':' : ParameterSeparator);
            sb.Append($"0x{Parameters[i]:X4}");
        }
        sb.Append(@"}");
        return sb.ToString();
    }

    public static TagSymbol FromString(string text)
    {
        var m = TagRegex.Match(text);
        var group = Convert.ToByte(m.Groups[1].Value, 16);
        var index = Convert.ToByte(m.Groups[2].Value, 16);
        var parameters = m.Groups[3].Value == ""
            ? Array.Empty<ushort>()
            : m.Groups[3].Value
                .TrimStart(new[] { ':', ParameterSeparator })
                .Split(',')
                .Select(x => Convert.ToUInt16(x, 16))
                .ToArray();
        return new TagSymbol(group, index, parameters);
    }
}
