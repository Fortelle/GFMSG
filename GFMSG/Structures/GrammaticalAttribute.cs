namespace GFMSG;

public struct GrammaticalAttribute
{
    public GrammaticalGender Gender;
    public GrammaticalInitialSound InitialSound;
    public bool IsUncountable;
    public bool IsAlwaysPlural;
    public ushort ExtraAttribute;
    public ushort ExtraAttribute2;

    public GrammaticalAttribute()
    {
        Gender = 0;
        InitialSound = 0;
        IsUncountable = false;
        IsAlwaysPlural = false;
        ExtraAttribute = 0;
        ExtraAttribute2 = 0;
    }

    public GrammaticalAttribute(ushort userparam)
    {
        Gender = (GrammaticalGender)(userparam & 0b_11);
        InitialSound = (GrammaticalInitialSound)(userparam >> 2 & 0b_11);
        IsUncountable = (userparam >> 4 & 0b_1) == 1;
        IsAlwaysPlural = (userparam >> 5 & 0b_1) == 1;
        ExtraAttribute = (ushort)(userparam >> 6 & 0b_11);
        ExtraAttribute2 = (ushort)(userparam >> 8 & 0b_11111111);
    }

    public ushort ToUshort()
    {
        return (ushort)(
            ((ushort)Gender & 0b_11)
            | (((ushort)InitialSound & 0b_11) << 2)
            | ((IsUncountable ? 1 : 0) << 4)
            | ((IsAlwaysPlural ? 1 : 0) << 5)
            | ((ExtraAttribute & 0b_11) << 6)
            | ((ExtraAttribute2 & 0b_11111111) << 8)
            );
    }
}

public enum GrammaticalGender
{
    Masculine,
    Feminine,
    Neuter,
    Unused,
}

public enum GrammaticalInitialSound
{
    Consonant,
    Vowel,
    Consonant2, // for Italian
    Unused,
}