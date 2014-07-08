Search and Replace
===
Ynote has powerful search and replace capabilities with the use of Regular Expressions

Quick Find
---
Use **Ctrl+F** to perform a quick search of text. It's various options are -

- Match Case - Match the Case of the text
- Match Whole Word - Match whole word
- Regex - Use Regular Expressions in search

Find Next
---
Use F3 to Find Next occurence of the word. This can be changed using the **Hotkeys Editor**

Find Character
---
You can use **Alt+F+{char}** key to find a character in the document. It is fully compatible with macros. eg - 'Alt+F+(' finds the nearest **(** character in the active document.

Incremental Search
---
Use **Ctrl+I** or Edit->Search->Incremental Search to show the Incremental Searcher.


![Ynote Classic Incremental Search](images/IncrementalSearcher.PNG "Incremental Searcher")

*Searching for string values with regex Incrementally in ynote*
Replace
---
You can use **Ctrl+H** or **Edit->Search->Replace** to call the replace dialog. It has the same options as the Find Dialog.
** Replace In Selection ** - If you want to replace text in Selection, select some text and then call the Replace Dialog.

Find/Replace In Files
---
You can also Find and Replace in Multiple Files using Ynote. Use the **Edit->Search->Find In Files menu or **Ctrl+Shift+F** keys to show up the Find In Files Dialog. It's various fields are -

- **Directory** - The Directory to search for the text. Use **_$docs_** to search within the open Files
- **Filter** - The File Filter. eg - \*.\* , *.html etc.
- **Sub Folder** - Search in Sub Folders too.

Regex
---
Ynote uses the .NET Regular Expressions Engine. For a quick reference of the language see http://msdn.microsoft.com/en-us/library/az24scfc(v=vs.110).aspx (Regex Language Reference by MSDN)