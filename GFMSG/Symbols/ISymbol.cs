using System.Diagnostics;

namespace GFMSG;

public interface ISymbol
{
    public int Size { get; }
    public string ToString();
}
