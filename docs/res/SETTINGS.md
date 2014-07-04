Settings
===
Ynote Settings can be changed using the Options Dialog or editing the User.ynotesettings file.

Common Settings
---
 - **TabLocation** - Tabs should be located on top or bottom
 - **DockStyle**   - Whether docking will be MDI(Multi-Document) or SDI(Single-Document)
 - **Show Same Words** - Highlights the same words as you enter them or move the cursor around
 - **Highlight ChangedLine** - Highlights the lines which have been changed from the last opened version
 - **WideCaret** - Show a Block/Wider Caret like that of a terminal
 - **Enable Virtaul Space** - User can place cursor anywhere and edit

File Extensions
---
You can add a file extension by editing the **Extensions.xml** file located in **$ynotedata**\ directory.

*File Preview (Extensions.xml)*
```xml
<?xml version="1.0"?>
<FileExtensions>
  <Key Language="Actionscript" Extensions=".as|.AS" />
  <Key Language="Antlr" Extensions=".g|.G" />
  <Key Language="ASP" Extensions=".asp|.aspx" />
  <Key Language="Assembly" Extensions=".asm|.s" />
  <Key Language="Batch" Extensions=".bat|.cmd|.BAT" />
  <Key Language="C" Extensions=".c|.C" />
  <Key Language="CPP" Extensions=".cpp|.h|.H|.CPP|.cc|.CC" />
  <Key Language="CSharp" Extensions=".cs|.CS|.ys" />
  <Key Language="CoffeeScript" Extensions=".coffee"/>
  <Key Language="CSS" Extensions=".css|.CSS" />
  <Key Language="D" Extensions=".d|.D" />
  <Key Language="Diff" Extensions=".diff" />
  <Key Language="FSharp" Extensions=".fs|.FS" />
  <Key Language="HTML" Extensions=".html|.htm|.HTML|.HTM" />
  <Key Language="Haskell" Extensions=".hs|.HS" />
  <Key Language="INI" Extensions=".ini|.inf|.INI" />
  <Key Language="Java" Extensions=".java|.JAVA" />
```
You can add an extension by adding an Extension to the **Extensions** seperated by the '**|**' character with the specefied language. eg -
```xml
<Key Language="Javascript" Extensions=".js|.JS|.json"
```
The above example adds **.json** to the list of javascript extensions. Thus, .json files are opened with Javascript Syntax instead of JSON.
You can also add extensions for an installed language by adding another Key specifying the Language and Extensions. For Example, we add the extension .testGrammar to **TestGrammar** syntax.
```xml
<Key Language="TestGrammar" Extensions=".testGrammar"/>
```
This overrides the default extensions. So you may add the default ones too.