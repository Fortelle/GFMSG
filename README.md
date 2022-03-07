# GFMSG
A library for reading pokemon text format, with GUI.

## Preview
<img src="https://user-images.githubusercontent.com/38492315/156877251-9bea6cbf-b5a6-429f-9f49-7b85144d5d5c.png" width="320px" />
<img src="https://user-images.githubusercontent.com/38492315/156161325-31ab49fe-8bd6-4b03-a642-7aa364ebec4c.png" width="320px" />

## Usage
``` csharp
var wrapper = new MsgWrapper("filename.dat");
wrapper.Load();
```

``` csharp
var msgdata = new MsgData("filename.dat");
var ahtb = new AHTB("filename.tbl");
var wrapper = new MsgWrapper(msgdata, ahtb);
```

### Format
``` csharp
var formatter = new MsgFormatter();
var options = new StringOptions() {
    Format = StringFormat.Plain,
    LanguageCode = "ja-Jpan",
};

int entryIndex = 0;
string text = formatter.Format(wrapper[entryIndex][0], options);
```

## Configurations
``` csharp
var formatter = new MsgFormatter();

// add tag info
formatter.AddTagGroup(0x01, "WORD");
formatter.AddTagIndex(0x01, 0x00, "TRAINER_NAME");

formatter.AddTagGroup(0xBE, "STREAM_CTRL");
formatter.AddTagIndex(0xBE, 0x00, "LINE_FEED");

// add tag converter
formatter.AddConverter(new TagConverter("STREAM_CTRL", "LINE_FEED", StringFormat.Plain | StringFormat.Html)
{
    ToText = () => "\n";
});

// add char
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
| Markup | `{WORD:TRAINER_NAME:0x0000} sent out {WORD:POKE_NICKNAME:0x0001}!` |
| Plain | `<TRAINER_NAME> sent out <POKE_NICKNAME>!` |
| Html | `<var>TRAINER_NAME</var> sent out <var>POKE_NICKNAME</var>!` |

### Format tags
| Format | Output |
| ----  | ---- |
| Raw | `Are you alive, my {TAG_11_00:0x00FF,0x0403}boygirl?!` |
| Markup | `Are you alive, my {STRING_SELECT:BY_GENDER:255,boy,girl}?!` |
| Plain | `Are you alive, my boy/girl?!` |

### Format characters
| Format | Output |
| ---- | ---- |
| Raw | `[0xE301]   [0xE316][0xE309][0xE30C][0xE30C][0xE301][0xE307][0xE305]   [0xE307][0xE301][0xE314][0xE305][0xE317][0xE301][0xE319]` |
| Markup | `\uE301   \uE316\uE309\uE30C\uE30C\uE301\uE307\uE305   \uE307\uE301\uE314\uE305\uE317\uE301\uE319` |
| Plain | `A   VILLAGE   GATEWAY` |
