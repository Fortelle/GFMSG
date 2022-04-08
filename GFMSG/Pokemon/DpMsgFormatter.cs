using System.Diagnostics;

namespace GFMSG.Pokemon
{
    public class DpMsgFormatter : PokemonMsgFormatterV1
    {
        private readonly Dictionary<byte, string> WordTags = new()
        {
            [0x00] = "pokemon",
            [0x01] = "nickname",
            [0x02] = "pokeclass",
            [0x03] = "trname",
            [0x04] = "mapname",
            [0x05] = "ability",
            [0x06] = "move",
            [0x07] = "nature",
            [0x08] = "item",
            [0x09] = "pokeblock",
            [0x0A] = "ugitem",
            [0x0B] = "box",
            //[0x0C] = "",
            [0x0D] = "stats",
            [0x0E] = "trtype",
            [0x0F] = "poketype",

            [0x10] = "taste",
            [0x11] = "condition",
            [0x12] = "pocket",
            [0x13] = "itemtext",
            //[0x14] = "",
            //[0x15] = "",
            //[0x16] = "",
            //[0x17] = "",
            [0x18] = "poketch",
            [0x19] = "goods",
            [0x1A] = "cupname",
            [0x1B] = "pocket_icon2",
            [0x1C] = "word",
            [0x1D] = "question",
            [0x1E] = "answer",
            [0x1F] = "accessory",

            [0x20] = "gym",
            [0x21] = "timezone",
            [0x22] = "contest",
            [0x23] = "conrank",
            [0x24] = "country",
            [0x25] = "area",
            [0x26] = "",
            [0x27] = "ribbon",
            [0x28] = "pokegender",
            [0x29] = "pokelevel",
            //[0x2A] = "",
            //[0x2B] = "",
            //[0x2C] = "",
            //[0x2D] = "",
            //[0x2E] = "",
            //[0x2F] = "",

            //[0x30] = "",
            //[0x31] = "",
            [0x32] = "num_1digit",
            [0x33] = "num_2digit",
            [0x34] = "num_3digit",
            [0x35] = "num_4digit",
            [0x36] = "num_5digit",
            [0x37] = "num_6digit",
            [0x38] = "num_7digit",
            [0x39] = "num_8digit",
            [0x3A] = "num_9digit",
            [0x3B] = "num_10digit",
            [0x3C] = "pocket_icon",
            [0x3D] = "group_name",
            [0x3E] = "item_def",
            [0x3F] = "item_ind",

            [0x40] = "goods_def",
            [0x41] = "goods_ind",
            [0x42] = "item_sysfont",
            [0x43] = "trname_sysfont",
            [0x44] = "nickname_sysfont",
            [0x45] = "pokemon_sysfont",
            [0x46] = "mapname_300",
            [0x47] = "mapname_301",
            [0x48] = "mapname_302",
            [0x49] = "mapname_303",
            [0x4A] = "month",
            [0x4B] = "trname_battle",
        };

        public DpMsgFormatter() : base()
        {
            foreach(var (index, name) in WordTags)
            {
                AddWordSet(index, name);
            }
        }
    }
}