namespace GFMSG.Pokemon
{
    public class SwordShieldMsgFormatter : PokemonMsgFormatter
    {
        public SwordShieldMsgFormatter() : base()
        {
            TrainerNameFieldFilename = @"common\namelist";


            AddTagIndex(0x01, 0x00, "TRAINER_NAME");
            AddTagIndex(0x01, 0x01, "POKE_NAME");
            AddTagIndex(0x01, 0x02, "POKE_NICKNAME");
            AddTagIndex(0x01, 0x03, "POKE_TYPE");
            AddTagIndex(0x01, 0x04, "POKE_SPECIES");
            AddTagIndex(0x01, 0x05, "PLACE_NAME");
            AddTagIndex(0x01, 0x06, "TOKUSEI_NAME");
            AddTagIndex(0x01, 0x07, "WAZA_NAME");
            AddTagIndex(0x01, 0x08, "SEIKAKU_NAME");
            AddTagIndex(0x01, 0x09, "ITEM_NAME");
            AddTagIndex(0x01, 0x0A, "ITEM_NAME_CLASSIFIED");
            AddTagIndex(0x01, 0x0B, "ITEM_NAME_ACC");
            AddTagIndex(0x01, 0x0C, "POKE_NICKNAME_TRUTH");
            AddTagIndex(0x01, 0x0D, "STAT_NAME");
            AddTagIndex(0x01, 0x0E, "TRAINER_TYPE");

            AddTagIndex(0x01, 0x10, "ITEM_NAME_ACC_CLASSIFIED");
            AddTagIndex(0x01, 0x12, "ITEM_POCKET_NAME");
            AddTagIndex(0x01, 0x14, "TRAINER_NAME_FIELD");

            AddTagIndex(0x01, 0x27, "RIBBON_NAME");
            AddTagIndex(0x01, 0x2D, "ID");

            AddTagIndex(0x01, 0x89, "TRAINER_NICKNAME");

            AddTagIndex(0x01, 0x94, "TORIMIAN_FORM");
            AddTagIndex(0x01, 0x9E, "TRAINER_TYPE_AND_NAME");

            AddTagIndex(0x01, 0xA0, "HAIR_NAME");
            AddTagIndex(0x01, 0xA2, "HAIR_COLOR");
            AddTagIndex(0x01, 0xAF, "TOURNAMENT_NAME");

            AddTagIndex(0x01, 0xB0, "ZWAZA_NAME");
            AddTagIndex(0x01, 0xB1, "SICK_NAME");
            AddTagIndex(0x01, 0xBF, "PARTY_NAME");

            AddTagIndex(0x01, 0xC0, "BOX_NAME");
            AddTagIndex(0x01, 0xC1, "DRESSUP_NAME");
            AddTagIndex(0x01, 0xC2, "DRESSUP_COLOR");
            AddTagIndex(0x01, 0xC3, "BGM_NAME");
            AddTagIndex(0x01, 0xC4, "PLAYER_NUMBER");
            AddTagIndex(0x01, 0xC5, "BIRTHDAY_MONTH");
            AddTagIndex(0x01, 0xC6, "BIRTHDAY_DAY");
            AddTagIndex(0x01, 0xCF, "COOKING_NAME");

            AddTagIndex(0x01, 0xD0, "BATTLE_RANK");
            AddTagIndex(0x01, 0xD1, "POKE_ANOTHERNAME");
            AddTagIndex(0x01, 0xD2, "WORK_NAME");
            AddTagIndex(0x01, 0xD3, "PLACE_NAME_INDIRECT");
            AddTagIndex(0x01, 0xD4, "MYSTERY_CARD_TITLE");
            AddTagIndex(0x01, 0xD5, "BATTLE_RULE");
            AddTagIndex(0x01, 0xD6, "POKE_MEMORY_PLACE");
            AddTagIndex(0x01, 0xD7, "POKE_MEMORY_FEELING");
            AddTagIndex(0x01, 0xD8, "POKE_MEMORY_RANK");


            Chars.PlainMap.Add(0xE300, "$");
        }
    }

}
