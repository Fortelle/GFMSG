namespace GFMSG.Pokemon
{
    public class SunMoonMsgFormatter : PokemonMsgFormatterV2
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
            [0x12] = "ITEM_POCKET_NAME",
        };

        public SunMoonMsgFormatter() : base()
        {
            TrainerNameFieldFilename = @"message\58";

            foreach (var (index, name) in WordTags)
            {
                AddWordSet(index, name);
            }

            AddChar(0xE07F, "\u202F", StringFormat.Plain | StringFormat.Html); // narrow nonbreaking space, used before punctuations in French
            AddChar(0xE300, "$", StringFormat.Plain | StringFormat.Html); // poke dollar
            foreach(var (code, text) in SunMoonPrivateCharacters)
            {
                AddChar(code, text, StringFormat.Plain | StringFormat.Html);
            }

            // 0xE800-0xEE26: Chinese pokemon names
            // sm: message\55, usum: message\60
            AddConverter(new CharConverter(0xE800, 0xEE24, StringFormat.Html | StringFormat.Plain)
            {
                ToText = (handler) => {
                    return ChineseCodes[handler.Code - handler.CodeStart].ToString();
                }
            });
        }

        private static readonly Dictionary<ushort, string> SunMoonPrivateCharacters = new()
        {
            [0xE000] = "{A}",
            [0xE001] = "{B}",
            [0xE002] = "{X}",
            [0xE003] = "{Y}",
            [0xE004] = "{L}",
            [0xE005] = "{R}",

            [0xE081] = "🙂", // U+1F642
            [0xE082] = "😄", // U+1F604
            [0xE083] = "😣", // U+1F623
            [0xE084] = "😠", // U+1F620
            [0xE085] = "⮥", // U+2BA5
            [0xE086] = "⮧", // U+2BA7
            [0xE087] = "💤", // U+1F4A4
            [0xE088] = "×", // U+00D7
            [0xE089] = "÷", // U+00F7
            [0xE08A] = "ᵉʳ", // U+1D49 U+02B3
            [0xE08B] = "ʳᵉ",
            [0xE08C] = "ʳ",
            [0xE08D] = "…", // U+2026
            [0xE08E] = "♂", // U+2642
            [0xE08F] = "♀", // U+2640
            [0xE090] = "♠", // U+2660
            [0xE091] = "♣", // U+2663
            [0xE092] = "♥", // U+2665
            [0xE093] = "♦", // U+2666
            [0xE094] = "★", // U+2605
            [0xE095] = "◎", // U+25CE
            [0xE096] = "〇", // U+3007
            [0xE097] = "□", // U+25A1
            [0xE098] = "△", // U+25B3
            [0xE099] = "◇", // U+25C7
            [0xE09A] = "♪", // U+266A
            [0xE09B] = "☀", // U+2600
            [0xE09C] = "☁", // U+2601
            [0xE09D] = "☂", // U+2602
            [0xE09E] = "☃", // U+2603
            [0xE09F] = "🙂",
            [0xE0A0] = "😄",
            [0xE0A1] = "😣",
            [0xE0A2] = "😠",
            [0xE0A3] = "⮥",
            [0xE0A4] = "⮧",
            [0xE0A5] = "💤",
            [0xE0A6] = "ᵉ",
            [0xE0A7] = "PK",
            [0xE0A8] = "MN",

            [0x2227] = "🙂", // ∧
            [0x2228] = "😄", // ∨
            [0xFFE2] = "😣", // ￢
            [0x21D2] = "😠", // ⇒
            [0x21D4] = "⮥", // ⇔
            [0x2200] = "⮧", // ∀
            [0x2203] = "💤", // ∃
        };

        // generated from letsgo
        private const string ChineseCodes =
            // 0xE800-0xEB0E, sunmoon(zh-hans)
            "蛋妙蛙种子草花小火龙恐喷杰尼龟卡" +
            "咪水箭绿毛虫铁甲蛹巴大蝶独角壳针" +
            "蜂波比鸟拉达烈雀嘴阿柏蛇怪皮丘雷" +
            "穿山鼠王多兰娜后朗力诺可西六尾九" +
            "胖丁超音蝠走路臭霸派斯特球摩鲁蛾" +
            "地三喵猫老鸭哥猴暴蒂狗风速蚊香蝌" +
            "蚪君泳士凯勇基胡腕豪喇叭芽口呆食" +
            "玛瑙母毒刺拳石隆岩马焰兽磁合一葱" +
            "嘟利海狮白泥舌贝鬼通耿催眠貘引梦" +
            "人钳蟹巨霹雳电顽弹椰树嘎啦飞腿郎" +
            "快头瓦双犀牛钻吉蔓藤袋墨金鱼星宝" +
            "魔墙偶天螳螂迷唇姐击罗肯泰鲤普百" +
            "变伊布边菊化盔镰刀翼急冻闪你哈克" +
            "幻叶月桂竺葵锯鳄蓝立咕夜鹰芭瓢安" +
            "圆丝蛛叉字灯笼古然咩羊茸美丽露才" +
            "皇毽棉长手向日蜻蜓乌沼太阳亮黑暗" +
            "鸦妖未知图腾果翁麒麟奇榛佛托土弟" +
            "蝎钢千壶赫狃熊圈熔蜗猪珊瑚炮章桶" +
            "信使翅戴加象顿Ⅱ惊鹿犬无畏战舞娃" +
            "奶罐幸福公炎帝幼沙班洛亚凤时木守" +
            "宫森林蜥蜴稚鸡壮跃狼纹直冲茧狩猎" +
            "盾粉莲童帽乐河橡实鼻狡猾傲骨燕鸥" +
            "莉奈朵溜糖雨蘑菇斗笠懒獭过动猿请" +
            "假居忍面者脱妞吼爆幕下掌朝北优雅" +
            "勾魂眼那恰姆落正拍负萤甜蔷薇溶吞" +
            "牙鲨鲸驼煤炭跳噗晃斑颚蚁漠仙歌青" +
            "绵七夕鼬斩饭匙鳅鲶虾兵螯秤念触摇" +
            "篮羽丑纳飘浮泡隐怨影诅咒巡灵彷徨" +
            "热带铃勃梭雪冰护豹珍珠樱空棘爱心" +
            "哑属艾欧盖固坐祈代希苗台猛曼拿儿" +
            "狸法师箱蟀勒伦琴含羞苞槌城结贵妇" +
            "绅蜜女帕兹潜兔随卷耳魅东施铛响坦" +
            "铜镜钟盆聒噪陆尖咬不良骷荧光霓虹" +
            "自舔狂远Ｚ由卢席恩骑色霏莱谢米尔" +
            "宙提主暖炒武刃丸剑探步哨约扒酷冷" +
            "蚀豆鸽高雉幔庞滚蝙螺钉差搬运匠修" +
            "建蟾蜍投摔打包保足蜈蚣车轮精根裙" +
            "野蛮鲈混流氓红倒狒殿滑巾征哭具死" +
            "神棺原肋始祖破灰尘索沫栗德单卵细" +
            "胞造鹅倍四季萌哎呀败轻蜘坚齿组麻" +
            "鳗宇烛幽晶斧嚏几何敏捷功夫父赤驹" +
            "劈司令炸雄秃丫首恶燃烧毕云酋迪耶" +
            "塔赛里狐呱贺掘彩蓓洁能鞘芳芙妮好" +
            "鱿贼脚铠垃藻臂枪伞咚碎黏钥朽南瓜" +
            "嗡哲裴格枭狙射炽咆哮虎漾壬笃啄铳" +
            "少强锹农胜虻鬃弱坏驴仔重挽滴伪睡" +
            "罩盗着竹疗环智挥猩掷胆噬堡爷参性" +
            "：银伴陨枕戈谜拟Ｑ磨舵鳞杖璞·鸣" +
            "哞鳍科莫迦虚吾肌费束辉纸御机夏" +
            // 0xEB0F-0xEE1D, sunmoon(zh-hant)
            "蛋妙蛙種子草花小火龍恐噴傑尼龜卡" +
            "咪水箭綠毛蟲鐵甲蛹巴大蝶獨角殼針" +
            "蜂波比鳥拉達烈雀嘴阿柏蛇怪皮丘雷" +
            "穿山鼠王多蘭娜后朗力諾可西六尾九" +
            "胖丁超音蝠走路臭霸派斯特球摩魯蛾" +
            "地三喵貓老鴨哥猴爆蒂狗風速蚊香蝌" +
            "蚪君泳士凱勇基胡腕豪喇叭芽口呆食" +
            "瑪瑙母毒刺拳石隆岩馬焰獸磁合一蔥" +
            "嘟利海獅白泥舌貝鬼通耿催眠貘引夢" +
            "人鉗蟹巨霹靂電頑彈椰樹嘎啦飛腿郎" +
            "快頭瓦雙犀牛鑽吉蔓藤袋墨金魚星寶" +
            "魔牆偶天螳螂迷唇姐擊羅肯泰鯉暴普" +
            "百變伊布邊菊化盔鐮刀翼急凍閃你哈" +
            "克幻葉月桂竺葵鋸鱷藍立咕夜鷹芭瓢" +
            "安圓絲蛛叉字燈籠古然咩羊茸美麗露" +
            "才皇毽棉長手向日蜻蜓烏沼太陽亮黑" +
            "暗鴉妖未知圖騰果翁麒麟奇榛佛托土" +
            "弟蠍鋼千壺赫狃熊圈熔蝸豬珊瑚炮章" +
            "桶信使翅戴加象頓Ⅱ驚鹿犬無畏戰舞" +
            "娃奶罐幸福公炎帝幼沙班洛亞鳳時木" +
            "守宮森林蜥蜴稚雞壯躍狼紋直衝繭狩" +
            "獵盾粉蓮童帽樂河橡實鼻狡猾傲骨燕" +
            "鷗莉奈朵溜糖雨蘑菇斗笠懶獺過動猿" +
            "請假居忍面者脫妞吼幕下掌朝北優雅" +
            "勾魂眼那恰姆落正拍負螢甜薔薇溶吞" +
            "牙鯊鯨駝煤炭跳噗晃斑顎蟻漠仙歌青" +
            "綿七夕鼬斬飯匙鰍鯰蝦兵螯秤念觸搖" +
            "籃羽醜納飄浮泡隱怨影詛咒巡靈彷徨" +
            "熱帶鈴勃梭雪冰護豹珍珠櫻空棘愛心" +
            "啞屬艾歐蓋固坐祈代希苗台猛曼拿兒" +
            "狸法師箱蟀勒倫琴含羞苞槌城結貴婦" +
            "紳蜜女帕茲潛兔隨捲耳魅東施鐺響坦" +
            "銅鏡鐘盆聒噪陸尖咬不良骷光霓虹自" +
            "舔狂遠Ｚ由盧席恩騎色霏萊謝米爾宙" +
            "提主暖炒武刃丸劍探步哨約扒酷冷蝕" +
            "豆鴿高雉幔龐滾蝙螺釘差搬運匠修建" +
            "蟾蜍投摔打包保足蜈蚣車輪毬精根裙" +
            "野蠻鱸混流氓紅倒狒殿滑巾徵哭具死" +
            "神棺原肋始祖破灰塵索沫栗德單卵細" +
            "胞造鵝倍四季萌哎呀敗輕蜘堅齒組麻" +
            "鰻宇燭幽晶斧嚏幾何敏捷功夫父赤駒" +
            "劈司令炸雄禿丫首惡燃燒畢雲酋迪耶" +
            "塔賽里狐呱賀掘彩蓓潔能鞘芳芙妮好" +
            "魷賊腳鎧垃藻臂槍傘咚碎黏鑰朽南瓜" +
            "嗡哲裴格梟狙射熾咆哮虎漾壬篤啄銃" +
            "少強鍬農勝虻鬃弱壞驢仔重挽滴偽睡" +
            "罩盜著竹療環智揮猩擲膽噬堡爺參性" +
            "：銀伴隕枕戈謎擬Ｑ磨舵鱗杖璞・鳴" +
            "哞鰭科莫迦虛吾肌費束輝紙御機夏" +
            // 0xEE1E-0xEE21, usum(zh-hans)
            "垒磊砰奥" +
            // 0xEE22-0xEE26, usum(zh-hant)
            "壘磊砰丑奧";
    }

    public class UltraSunUltraMoonMsgFormatter : SunMoonMsgFormatter
    {
        public UltraSunUltraMoonMsgFormatter() : base()
        {
            TrainerNameFieldFilename = @"message\63";
        }
    }

}
