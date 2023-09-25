namespace GFMSG;

public static class FnvHash
{
    private const ulong FnvPrime64 = 0x00000100_000001b3;
    private const ulong OffsetBasis64 = 0xCBF29CE4_84222645;

    public static ulong Fnv1a_64(string text)
    {
        var bytes = text.Select(x => (byte)x).ToArray();
        return Fnv1a_64(bytes);
    }

    public static ulong Fnv1a_64(byte[] bytes)
    {
        return Fnv1a(bytes, FnvPrime64, OffsetBasis64);
    }

    private static ulong Fnv1a(byte[] data, ulong prime, ulong offset)
    {
        var hash = offset;
        foreach (var b in data)
        {
            hash ^= b;
            hash *= prime;
        }
        return hash;
    }
}
