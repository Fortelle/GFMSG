namespace GFMSG.Pokemon
{
    public class LetsGoMsgFormatter : PokemonMsgFormatter
    {
        public LetsGoMsgFormatter() : base()
        {
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

            Chars.PlainMap.Add(0xE300, "$");
        }
    }

}
