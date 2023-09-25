namespace GFMSG.Pokemon;

public class LetsGoMsgFormatter : PokemonMsgFormatterV2
{
    private readonly Dictionary<byte, string> WordTags = new()
    {
        [0x00] = "TRAINER_NAME",
        [0x01] = "POKE_NAME",
        [0x02] = "POKE_NICKNAME",
        [0x03] = "POKE_TYPE",
        [0x04] = "POKE_SPECIES",
        [0x05] = "PLACE_NAME",
        [0x06] = "TOKUSEI_NAME",
        [0x07] = "WAZA_NAME",
        [0x08] = "SEIKAKU_NAME",
        [0x09] = "ITEM_NAME",
        [0x0A] = "ITEM_NAME_CLASSIFIED",
        [0x0B] = "ITEM_NAME_ACC",
        [0x0C] = "POKE_NICKNAME_TRUTH",
        [0x0D] = "STAT_NAME",
        [0x0E] = "TRAINER_TYPE",
    };

    public LetsGoMsgFormatter() : base()
    {
        foreach (var (index, name) in WordTags)
        {
            AddWordSet(index, name);
        }

        Chars.PlainMap.Add(0xE300, "$");
    }
}
