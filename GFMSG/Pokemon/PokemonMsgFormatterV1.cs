using System.Diagnostics;
using System.Drawing;

namespace GFMSG.Pokemon;

public class PokemonMsgFormatterV1 : MsgFormatterV1
{
    public PokemonMsgFormatterV1() : base()
    {
        AddLetters("", JapaneseLetters, 0x0001);
        AddLetters("ko", KoreanLetters, 0x0401);
        foreach(var li in SpecialLetters)
        {
            AddLetter("", li);
        }

        AddConverter(new TagConverter(0x02, 0x00, "NOTE_ICON")
        {
            ToText = (handler) =>
            {
                ushort iconIndex = handler.Parameters[0];
                return handler.Options switch
                {

                    { Format: StringFormat.Markup } => $@"{iconIndex}",
                    { Format: StringFormat.Html } => "",
                    { Format: StringFormat.Plain } => "",
                    _ => throw new NotSupportedException(),
                };
            },

            ToSymbol = (handler) =>
            {
                ushort iconIndex = Convert.ToUInt16(handler.Arguments[0]);
                return new[] { iconIndex };
            },
        });

        AddConverter(new TagConverter(0x02, 0x01, "FORCE_WAIT", StringFormat.Plain | StringFormat.Html)
        {
            ToText = (handler) => {
                handler.Queue.Insert(new CharSymbol('\n'));
                return "";
            },
        });

        AddConverter(new TagConverter(0xFF, 0x00, "FONT_COLOR")
        {
            ToText = (handler) =>
            {
                ushort colorIndex = handler.Parameters[0];
                return handler.Options switch
                {
                    { Format: StringFormat.Markup } => $@"{colorIndex}",
                    { Format: StringFormat.Html } when colorIndex == 0 => $@"</font>",
                    { Format: StringFormat.Html } when colorIndex > 0 => $@"<font color=""{ColorTranslator.ToHtml(FontColors[colorIndex])}"">",
                    { Format: StringFormat.Plain } => $@"",
                    _ => throw new NotSupportedException(),
                };
            },

            ToSymbol = (handler) =>
            {
                ushort colorIndex = Convert.ToUInt16(handler.Arguments[0]);
                return new[] { colorIndex };
            },
        });

        AddConverter(new TagConverter(0xFF, 0x01, "FONT_SIZE")
        {
            ToText = (handler) =>
            {
                ushort size = handler.Parameters[0];
                return handler.Options switch
                {

                    { Format: StringFormat.Markup } => $@"{size}",
                    { Format: StringFormat.Html } when size == 100 => $@"</font>",
                    { Format: StringFormat.Html } => $@"<font size=""{size}%"">", // illegal format
                    { Format: StringFormat.Plain } => $@"",
                    _ => throw new NotSupportedException(),
                };
            },

            ToSymbol = (handler) =>
            {
                ushort colorIndex = Convert.ToUInt16(handler.Arguments[0]);
                return new[] { colorIndex };
            },
        });

        // dp:
        // TAG_02_00 .. 02_03
        // hgss:
        // TAG_02_00 .. 02_08
        // TAG_03_00 .. 03_08
        // TAG_04_00 .. 04_03
        // TAG_FF_02
    }

    protected void AddLetters(string lang, string letters, ushort startIndex)
    {
        for (var i = 0; i < letters.Length; i++)
        {
            var c = letters[i];
            if (c == '\0') continue;
            AddLetter(lang, new((ushort)(startIndex + i), c.ToString(), c.ToString()));
        }
    }

    protected void AddWordSet(byte index, string name)
    {
        AddConverter(new TagConverter(0x01, index, name, StringFormat.Plain | StringFormat.Html)
        {
            ToText = WordToText,
        });
    }


    public const string JapaneseLetters =
        "　ぁあぃいぅうぇえぉおかがきぎく" + //0x0001
        "ぐけげこごさざしじすずせぜそぞた" + //0x0011
        "だちぢっつづてでとどなにぬねのは" + //0x0021
        "ばぱひびぴふぶぷへべぺほぼぽまみ" + //0x0031
        "むめもゃやゅゆょよらりるれろわを" + //0x0041
        "んァアィイゥウェエォオカガキギク" + //0x0051
        "グケゲコゴサザシジスズセゼソゾタ" + //0x0061
        "ダチヂッツヅテデトドナニヌネノハ" + //0x0071
        "バパヒビピフブプヘベペホボポマミ" + //0x0081
        "ムメモャヤュユョヨラリルレロワヲ" + //0x0091
        "ン０１２３４５６７８９ＡＢＣＤＥ" + //0x00a1
        "ＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵ" + //0x00b1
        "ＶＷＸＹＺａｂｃｄｅｆｇｈｉｊｋ" + //0x00c1
        "ｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ_" + //0x00d1
        "！？、。…・／「」『』（）♂♀＋" + //0x00e1
        "ー×÷＝～：；．，♠♣♥♦★◎○" + //0x00f1
        "□△◇＠♪％☀☁☂☃\0\0\0\0\0\0" + //0x0101
        "\0円\0\0\0\0\0\0\0\0←↑↓→\0＆" + //0x0111
        "0123456789ABCDEF" + //0x0121
        "GHIJKLMNOPQRSTUV" + //0x0131
        "WXYZabcdefghijkl" + //0x0141
        "mnopqrstuvwxyzÀÁ" + //0x0151
        "ÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑ" + //0x0161
        "ÒÓÔÕÖ[ØÙÚÛÜÝÞßàá" + //0x0171
        "âãäåæçèéêëìíîïðñ" + //0x0181
        "òóôõö]øùúûüýþÿŒœ" + //0x0191
        "Şşªº\0\0\0$¡¿!?,.|･" + //0x01a1
        "/‘'“”„‹›()\0\0+-*#" + //0x01b1
        "=&~:;\0\0\0\0\0\0\0\0\0\0@" + //0x01c1
        "\0%\0\0\0\0\0\0\0\0\0\0\0 ᵉ\0" + //0x01d1
        "\0\0\0\0\0\0\0°\0\0\0\0\0\0\0\0"; //0x01e1

    public const string KoreanLetters =
        "가각간갇갈갉갊감갑값갓갔강갖갗같" + //0x0401
        "갚갛개객갠갤갬갭갯갰갱갸갹갼걀걋" + //0x0411
        "걍걔걘걜거걱건걷걸걺검겁것겄겅겆" + //0x0421
        "겉겊겋게겐겔겜겝겟겠겡겨격겪견겯" + //0x0431
        "결겸겹겻겼경곁계곈곌곕곗고곡곤곧" + //0x0441
        "골곪곬곯곰곱곳공곶과곽관괄괆괌괍" + //0x0451
        "괏광괘괜괠괩괬괭괴괵괸괼굄굅굇굉" + //0x0461
        "교굔굘굡굣구국군굳굴굵굶굻굼굽굿" + //0x0471
        "궁궂궈궉권궐궜궝궤궷귀귁귄귈귐귑" + //0x0481
        "귓규균귤그극근귿글긁금급긋긍긔기" + //0x0491
        "긱긴긷길긺김깁깃깅깆깊까깍깎깐깔" + //0x04a1
        "깖깜깝깟깠깡깥깨깩깬깰깸깹깻깼깽" + //0x04b1
        "꺄꺅꺌꺼꺽꺾껀껄껌껍껏껐껑께껙껜" + //0x04c1
        "껨껫껭껴껸껼꼇꼈꼍꼐꼬꼭꼰꼲꼴꼼" + //0x04d1
        "꼽꼿꽁꽂꽃꽈꽉꽐꽜꽝꽤꽥꽹꾀꾄꾈" + //0x04e1
        "꾐꾑꾕꾜꾸꾹꾼꿀꿇꿈꿉꿋꿍꿎꿔꿜" + //0x04f1
        "꿨꿩꿰꿱꿴꿸뀀뀁뀄뀌뀐뀔뀜뀝뀨끄" + //0x0501
        "끅끈끊끌끎끓끔끕끗끙끝끼끽낀낄낌" + //0x0511
        "낍낏낑나낙낚난낟날낡낢남납낫났낭" + //0x0521
        "낮낯낱낳내낵낸낼냄냅냇냈냉냐냑냔" + //0x0531
        "냘냠냥너넉넋넌널넒넓넘넙넛넜넝넣" + //0x0541
        "네넥넨넬넴넵넷넸넹녀녁년녈념녑녔" + //0x0551
        "녕녘녜녠노녹논놀놂놈놉놋농높놓놔" + //0x0561
        "놘놜놨뇌뇐뇔뇜뇝뇟뇨뇩뇬뇰뇹뇻뇽" + //0x0571
        "누눅눈눋눌눔눕눗눙눠눴눼뉘뉜뉠뉨" + //0x0581
        "뉩뉴뉵뉼늄늅늉느늑는늘늙늚늠늡늣" + //0x0591
        "능늦늪늬늰늴니닉닌닐닒님닙닛닝닢" + //0x05a1
        "다닥닦단닫달닭닮닯닳담답닷닸당닺" + //0x05b1
        "닻닿대댁댄댈댐댑댓댔댕댜더덕덖던" + //0x05c1
        "덛덜덞덟덤덥덧덩덫덮데덱덴델뎀뎁" + //0x05d1
        "뎃뎄뎅뎌뎐뎔뎠뎡뎨뎬도독돈돋돌돎" + //0x05e1
        "돐돔돕돗동돛돝돠돤돨돼됐되된될됨" + //0x05f1
        "됩됫됴두둑둔둘둠둡둣둥둬뒀뒈뒝뒤" + //0x0601
        "뒨뒬뒵뒷뒹듀듄듈듐듕드득든듣들듦" + //0x0611
        "듬듭듯등듸디딕딘딛딜딤딥딧딨딩딪" + //0x0621
        "따딱딴딸땀땁땃땄땅땋때땍땐땔땜땝" + //0x0631
        "땟땠땡떠떡떤떨떪떫떰떱떳떴떵떻떼" + //0x0641
        "떽뗀뗄뗌뗍뗏뗐뗑뗘뗬또똑똔똘똥똬" + //0x0651
        "똴뙈뙤뙨뚜뚝뚠뚤뚫뚬뚱뛔뛰뛴뛸뜀" + //0x0661
        "뜁뜅뜨뜩뜬뜯뜰뜸뜹뜻띄띈띌띔띕띠" + //0x0671
        "띤띨띰띱띳띵라락란랄람랍랏랐랑랒" + //0x0681
        "랖랗래랙랜랠램랩랫랬랭랴략랸럇량" + //0x0691
        "러럭런럴럼럽럿렀렁렇레렉렌렐렘렙" + //0x06a1
        "렛렝려력련렬렴렵렷렸령례롄롑롓로" + //0x06b1
        "록론롤롬롭롯롱롸롼뢍뢨뢰뢴뢸룀룁" + //0x06c1
        "룃룅료룐룔룝룟룡루룩룬룰룸룹룻룽" + //0x06d1
        "뤄뤘뤠뤼뤽륀륄륌륏륑류륙륜률륨륩" + //0x06e1
        "륫륭르륵른를름릅릇릉릊릍릎리릭린" + //0x06f1
        "릴림립릿링마막만많맏말맑맒맘맙맛" + //0x0701
        "망맞맡맣매맥맨맬맴맵맷맸맹맺먀먁" + //0x0711
        "먈먕머먹먼멀멂멈멉멋멍멎멓메멕멘" + //0x0721
        "멜멤멥멧멨멩며멱면멸몃몄명몇몌모" + //0x0731
        "목몫몬몰몲몸몹못몽뫄뫈뫘뫙뫼묀묄" + //0x0741
        "묍묏묑묘묜묠묩묫무묵묶문묻물묽묾" + //0x0751
        "뭄뭅뭇뭉뭍뭏뭐뭔뭘뭡뭣뭬뮈뮌뮐뮤" + //0x0761
        "뮨뮬뮴뮷므믄믈믐믓미믹민믿밀밂밈" + //0x0771
        "밉밋밌밍및밑바박밖밗반받발밝밞밟" + //0x0781
        "밤밥밧방밭배백밴밸뱀뱁뱃뱄뱅뱉뱌" + //0x0791
        "뱍뱐뱝버벅번벋벌벎범법벗벙벚베벡" + //0x07a1
        "벤벧벨벰벱벳벴벵벼벽변별볍볏볐병" + //0x07b1
        "볕볘볜보복볶본볼봄봅봇봉봐봔봤봬" + //0x07c1
        "뵀뵈뵉뵌뵐뵘뵙뵤뵨부북분붇불붉붊" + //0x07d1
        "붐붑붓붕붙붚붜붤붰붸뷔뷕뷘뷜뷩뷰" + //0x07e1
        "뷴뷸븀븃븅브븍븐블븜븝븟비빅빈빌" + //0x07f1
        "빎빔빕빗빙빚빛빠빡빤빨빪빰빱빳빴" + //0x0801
        "빵빻빼빽뺀뺄뺌뺍뺏뺐뺑뺘뺙뺨뻐뻑" + //0x0811
        "뻔뻗뻘뻠뻣뻤뻥뻬뼁뼈뼉뼘뼙뼛뼜뼝" + //0x0821
        "뽀뽁뽄뽈뽐뽑뽕뾔뾰뿅뿌뿍뿐뿔뿜뿟" + //0x0831
        "뿡쀼쁑쁘쁜쁠쁨쁩삐삑삔삘삠삡삣삥" + //0x0841
        "사삭삯산삳살삵삶삼삽삿샀상샅새색" + //0x0851
        "샌샐샘샙샛샜생샤샥샨샬샴샵샷샹섀" + //0x0861
        "섄섈섐섕서석섞섟선섣설섦섧섬섭섯" + //0x0871
        "섰성섶세섹센셀셈셉셋셌셍셔셕션셜" + //0x0881
        "셤셥셧셨셩셰셴셸솅소속솎손솔솖솜" + //0x0891
        "솝솟송솥솨솩솬솰솽쇄쇈쇌쇔쇗쇘쇠" + //0x08a1
        "쇤쇨쇰쇱쇳쇼쇽숀숄숌숍숏숑수숙순" + //0x08b1
        "숟술숨숩숫숭숯숱숲숴쉈쉐쉑쉔쉘쉠" + //0x08c1
        "쉥쉬쉭쉰쉴쉼쉽쉿슁슈슉슐슘슛슝스" + //0x08d1
        "슥슨슬슭슴습슷승시식신싣실싫심십" + //0x08e1
        "싯싱싶싸싹싻싼쌀쌈쌉쌌쌍쌓쌔쌕쌘" + //0x08f1
        "쌜쌤쌥쌨쌩썅써썩썬썰썲썸썹썼썽쎄" + //0x0901
        "쎈쎌쏀쏘쏙쏜쏟쏠쏢쏨쏩쏭쏴쏵쏸쐈" + //0x0911
        "쐐쐤쐬쐰쐴쐼쐽쑈쑤쑥쑨쑬쑴쑵쑹쒀" + //0x0921
        "쒔쒜쒸쒼쓩쓰쓱쓴쓸쓺쓿씀씁씌씐씔" + //0x0931
        "씜씨씩씬씰씸씹씻씽아악안앉않알앍" + //0x0941
        "앎앓암압앗았앙앝앞애액앤앨앰앱앳" + //0x0951
        "앴앵야약얀얄얇얌얍얏양얕얗얘얜얠" + //0x0961
        "얩어억언얹얻얼얽얾엄업없엇었엉엊" + //0x0971
        "엌엎에엑엔엘엠엡엣엥여역엮연열엶" + //0x0981
        "엷염엽엾엿였영옅옆옇예옌옐옘옙옛" + //0x0991
        "옜오옥온올옭옮옰옳옴옵옷옹옻와왁" + //0x09a1
        "완왈왐왑왓왔왕왜왝왠왬왯왱외왹왼" + //0x09b1
        "욀욈욉욋욍요욕욘욜욤욥욧용우욱운" + //0x09c1
        "울욹욺움웁웃웅워웍원월웜웝웠웡웨" + //0x09d1
        "웩웬웰웸웹웽위윅윈윌윔윕윗윙유육" + //0x09e1
        "윤율윰윱윳융윷으윽은을읊음읍읏응" + //0x09f1
        "읒읓읔읕읖읗의읜읠읨읫이익인일읽" + //0x0a01
        "읾잃임입잇있잉잊잎자작잔잖잗잘잚" + //0x0a11
        "잠잡잣잤장잦재잭잰잴잼잽잿쟀쟁쟈" + //0x0a21
        "쟉쟌쟎쟐쟘쟝쟤쟨쟬저적전절젊점접" + //0x0a31
        "젓정젖제젝젠젤젬젭젯젱져젼졀졈졉" + //0x0a41
        "졌졍졔조족존졸졺좀좁좃종좆좇좋좌" + //0x0a51
        "좍좔좝좟좡좨좼좽죄죈죌죔죕죗죙죠" + //0x0a61
        "죡죤죵주죽준줄줅줆줌줍줏중줘줬줴" + //0x0a71
        "쥐쥑쥔쥘쥠쥡쥣쥬쥰쥴쥼즈즉즌즐즘" + //0x0a81
        "즙즛증지직진짇질짊짐집짓징짖짙짚" + //0x0a91
        "짜짝짠짢짤짧짬짭짯짰짱째짹짼쨀쨈" + //0x0aa1
        "쨉쨋쨌쨍쨔쨘쨩쩌쩍쩐쩔쩜쩝쩟쩠쩡" + //0x0ab1
        "쩨쩽쪄쪘쪼쪽쫀쫄쫌쫍쫏쫑쫓쫘쫙쫠" + //0x0ac1
        "쫬쫴쬈쬐쬔쬘쬠쬡쭁쭈쭉쭌쭐쭘쭙쭝" + //0x0ad1
        "쭤쭸쭹쮜쮸쯔쯤쯧쯩찌찍찐찔찜찝찡" + //0x0ae1
        "찢찧차착찬찮찰참찹찻찼창찾채책챈" + //0x0af1
        "챌챔챕챗챘챙챠챤챦챨챰챵처척천철" + //0x0b01
        "첨첩첫첬청체첵첸첼쳄쳅쳇쳉쳐쳔쳤" + //0x0b11
        "쳬쳰촁초촉촌촐촘촙촛총촤촨촬촹최" + //0x0b21
        "쵠쵤쵬쵭쵯쵱쵸춈추축춘출춤춥춧충" + //0x0b31
        "춰췄췌췐취췬췰췸췹췻췽츄츈츌츔츙" + //0x0b41
        "츠측츤츨츰츱츳층치칙친칟칠칡침칩" + //0x0b51
        "칫칭카칵칸칼캄캅캇캉캐캑캔캘캠캡" + //0x0b61
        "캣캤캥캬캭컁커컥컨컫컬컴컵컷컸컹" + //0x0b71
        "케켁켄켈켐켑켓켕켜켠켤켬켭켯켰켱" + //0x0b81
        "켸코콕콘콜콤콥콧콩콰콱콴콸쾀쾅쾌" + //0x0b91
        "쾡쾨쾰쿄쿠쿡쿤쿨쿰쿱쿳쿵쿼퀀퀄퀑" + //0x0ba1
        "퀘퀭퀴퀵퀸퀼큄큅큇큉큐큔큘큠크큭" + //0x0bb1
        "큰클큼큽킁키킥킨킬킴킵킷킹타탁탄" + //0x0bc1
        "탈탉탐탑탓탔탕태택탠탤탬탭탯탰탱" + //0x0bd1
        "탸턍터턱턴털턺텀텁텃텄텅테텍텐텔" + //0x0be1
        "템텝텟텡텨텬텼톄톈토톡톤톨톰톱톳" + //0x0bf1
        "통톺톼퇀퇘퇴퇸툇툉툐투툭툰툴툼툽" + //0x0c01
        "툿퉁퉈퉜퉤튀튁튄튈튐튑튕튜튠튤튬" + //0x0c11
        "튱트특튼튿틀틂틈틉틋틔틘틜틤틥티" + //0x0c21
        "틱틴틸팀팁팃팅파팍팎판팔팖팜팝팟" + //0x0c31
        "팠팡팥패팩팬팰팸팹팻팼팽퍄퍅퍼퍽" + //0x0c41
        "펀펄펌펍펏펐펑페펙펜펠펨펩펫펭펴" + //0x0c51
        "편펼폄폅폈평폐폘폡폣포폭폰폴폼폽" + //0x0c61
        "폿퐁퐈퐝푀푄표푠푤푭푯푸푹푼푿풀" + //0x0c71
        "풂품풉풋풍풔풩퓌퓐퓔퓜퓟퓨퓬퓰퓸" + //0x0c81
        "퓻퓽프픈플픔픕픗피픽핀필핌핍핏핑" + //0x0c91
        "하학한할핥함합핫항해핵핸핼햄햅햇" + //0x0ca1
        "했행햐향허헉헌헐헒험헙헛헝헤헥헨" + //0x0cb1
        "헬헴헵헷헹혀혁현혈혐협혓혔형혜혠" + //0x0cc1
        "혤혭호혹혼홀홅홈홉홋홍홑화확환활" + //0x0cd1
        "홧황홰홱홴횃횅회획횐횔횝횟횡효횬" + //0x0ce1
        "횰횹횻후훅훈훌훑훔훗훙훠훤훨훰훵" + //0x0cf1
        "훼훽휀휄휑휘휙휜휠휨휩휫휭휴휵휸" + //0x0d01
        "휼흄흇흉흐흑흔흖흗흘흙흠흡흣흥흩" + //0x0d11
        "희흰흴흼흽힁히힉힌힐힘힙힛힝\0\0" + //0x0d21
        "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋ" + //0x0d31
        "ㅌㅍㅎㅏㅐㅑㅒㅓㅔㅕㅖㅗㅛㅜㅠㅡ" + //0x0d41
        "ㅣ\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0" + //0xd51
        "뢔쌰쎼쓔쬬"; //0xd61

    public LetterInfo[] SpecialLetters = new[] {
        new LetterInfo(0x010B, "[FACE_NORMAL]", "🙂"),
        new LetterInfo(0x010C, "[FACE_SMILE]", "😄"),
        new LetterInfo(0x010D, "[FACE_CRY]", "😣"),
        new LetterInfo(0x010E, "[FACE_ANGRY]", "😠"),
        new LetterInfo(0x010F, "[UPPER]", "⮥"),
        new LetterInfo(0x0110, "[DOWNER]", "⮧"),
        new LetterInfo(0x0111, "[SLEEP]", "💤"),
        new LetterInfo(0x0113, "[POCKET_ITEM]", "[POCKET_ITEM]"),
        new LetterInfo(0x0114, "[POCKET_KEYITEM]", "[POCKET_KEYITEM]"),
        new LetterInfo(0x0115, "[POCKET_MACHINE]", "[POCKET_MACHINE]"),
        new LetterInfo(0x0116, "[POCKET_SEAL]", "[POCKET_SEAL]"),
        new LetterInfo(0x0117, "[POCKET_MEDICINE]", "[POCKET_MEDICINE]"),
        new LetterInfo(0x0118, "[POCKET_NUT]", "[POCKET_BERRY]"),
        new LetterInfo(0x0119, "[POCKET_BALL]", "[POCKET_BALL]"),
        new LetterInfo(0x011A, "[POCKET_BATTLE]", "[POCKET_BATTLEITEM]"),
        new LetterInfo(0x011F, "[CURSOR]", "►"),

        new LetterInfo(0x01A5, "[er]", "ᵉʳ"),
        new LetterInfo(0x01A6, "[re]", "ʳᵉ"),
        new LetterInfo(0x01A7, "[r]", "ʳ"),
        new LetterInfo(0x01BB, "[H_MALE]", "♂"),
        new LetterInfo(0x01BC, "[H_FEMALE]", "♀"),

        new LetterInfo(0x01E0, "[PK]", "PK"),
        new LetterInfo(0x01E1, "[MN]", "MN"),
        new LetterInfo(0x01E2, "[SPCNUM]", " "),

        new LetterInfo(0x25BC, "[WAIT_NORMAL]", "\n"),
        new LetterInfo(0x25BD, "[WAIT_SCROLL]", "\n"),
        new LetterInfo(0xE000, "[LINE_FEED]", "\n"),
    };

    public Color[] FontColors { get; set; } = new[]
    {
        ColorTranslator.FromHtml("#000000"),
        ColorTranslator.FromHtml("#FF0000"),
        ColorTranslator.FromHtml("#0000FF"),
        ColorTranslator.FromHtml("#008C00"),
        ColorTranslator.FromHtml("#FF00FF"),
    };

}