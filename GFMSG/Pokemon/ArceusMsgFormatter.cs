namespace GFMSG.Pokemon;

public class ArceusMsgFormatter : PokemonMsgFormatterV2
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
        [0x0F] = "POKETYPE_NAME",

        [0x10] = "ITEM_NAME_ACC_CLASSIFIED",
        [0x12] = "ITEM_POCKET_NAME",

        [0x25] = "PLAYER_RANK",
        [0x27] = "RIBBON_NAME",
        [0x2E] = "AREA_NAME",

        [0x89] = "TRAINER_NICKNAME",
        [0x9E] = "TRAINER_TYPE_AND_NAME",
        [0xA0] = "HAIR_NAME",
        [0xA2] = "HAIR_COLOR",
        [0xB0] = "ZWAZA_NAME",
        [0xB1] = "SICK_NAME",
        [0xC0] = "BOX_NAME",
        [0xC1] = "DRESSUP_NAME",
        [0xC2] = "DRESSUP_COLOR",
        [0xD3] = "PLACE_NAME_INDIRECT",
        [0xD4] = "MYSTERY_CARD_TITLE",
        [0xD6] = "IBOX_NAME",
        [0xD7] = "MISSION_NAME",
        [0xDC] = "RIVAL_NAME",
        [0xDD] = "ICHO_PROD_NAME",
    };

    public ArceusMsgFormatter() : base()
    {
        TrainerNameFieldFilename = @"common\namelist";


        Chars.PlainMap.Add(0xE300, "$");

        foreach (var (index, name) in WordTags)
        {
            AddWordSet(index, name);
        }

        AddConverter(new CharConverter(0xE301, 0xE329, StringFormat.Html | StringFormat.Plain)
        {
            ToText = (handler) => {
                return UnownCodes[handler.Code - handler.CodeStart].ToString();
            }
        });
    }

    private const string UnownCodes = "ABCDEFGHIJKLMNOPQRSTUVWXYZ!?";
}
