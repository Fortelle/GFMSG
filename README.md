# GFMSG
A library for reading pokemon text format, with GUI.

## Preview
<img src="https://user-images.githubusercontent.com/38492315/156877251-9bea6cbf-b5a6-429f-9f49-7b85144d5d5c.png" width="320px" />
<img src="https://user-images.githubusercontent.com/38492315/156161325-31ab49fe-8bd6-4b03-a642-7aa364ebec4c.png" width="320px" />

## Usage
### Load
#### GenIV
``` csharp
var msgdata = new MsgDataV1("filename.dat");
var wrapper = new MsgWrapper(msgdata, "filename", "ja-Hrkt");
```

#### GenV-GenVII
``` csharp
var msgdata = new MsgDataV2("filename.dat");
var langcodes = new[] { "ja-Hrkt", "ja-Jpan" };
var version = FileVersion.GenV; // GenV, GenVI, GenVII
var wrapper = new MsgWrapper(msgdata, "filename", version, langcodes);
```

#### GenVIII
``` csharp
var msgdata = new MsgDataV2("filename.dat");
var ahtb = new AHTB("filename.tbl");
var langcodes = new[] { "ja-Hrkt" };
var version = FileVersion.GenVIII;
var wrapper = new MsgWrapper(msgdata, ahtb, "filename", FileVersion.GenVIII, langcodes);
```

### Format
``` csharp
var formatter = new MsgFormatter();
var options = new StringOptions() {
    Format = StringFormat.Plain,
    LanguageCode = "ja-Hrkt",
    RemoveLineBreaks = false,
};

int entryIndex = 0;
int langIndex = 0;
string text = formatter.Format(wrapper[entryIndex][langIndex], options);
```

## Configurations
``` csharp
var formatter = new MsgFormatter();

// add tag converter
formatter.AddConverter(new TagConverter(0xBE, 0x00, "LINE_FEED", StringFormat.Plain | StringFormat.Html)
{
    ToText = () => "\n";
});

// add letter(GenIV)
formatter.AddLetter("ja", new(0x0003, "あ"));
formatter.AddLetter("ko", new(0x0401, "가"));
formatter.AddLetter("", new(0xE000, "[LINE_FEED]", "\n"));

// add char(GenV+)
formatter.AddChar(0xE300, "$", StringFormat.Plain | StringFormat.Html);

// add char converter
formatter.AddConverter(new CharConverter(0xE301, 0xE329, StringFormat.Html | StringFormat.Plain)
{
    ToText = (code) => "ABCDEFGHIJKLMNOPQRSTUVWXYZ!?"[code - 0xE301].ToString(),
});
```

## Samples
### Format variables
| Format | Output |
| ---- | ---- |
| Raw | `{TAG_01_00:0x0000} sent out {TAG_01_02:0x0001}!` |
| Markup | `{TRAINER_NAME:0x0000} sent out {POKE_NICKNAME:0x0001}!` |
| Plain | `<trainer_name> sent out <poke_nickname>!` |
| Html | `<var>trainer_name</var> sent out <var>poke_nickname</var>!` |

### Format tags
| Format | Output |
| ----  | ---- |
| Raw | `Are you alive, my {TAG_11_00:0x00FF,0x0403}boygirl?!` |
| Markup | `Are you alive, my {BY_GENDER:255,boy,girl}?!` |
| Plain | `Are you alive, my boy/girl?!` |

### Format characters
| Format | Output |
| ---- | ---- |
| Raw | `[0xE301]   [0xE316][0xE309][0xE30C][0xE30C][0xE301][0xE307][0xE305]   [0xE307][0xE301][0xE314][0xE305][0xE317][0xE301][0xE319]` |
| Markup | `\uE301   \uE316\uE309\uE30C\uE30C\uE301\uE307\uE305   \uE307\uE301\uE314\uE305\uE317\uE301\uE319` |
| Plain | `A   VILLAGE   GATEWAY` |
