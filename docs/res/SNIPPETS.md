Snippets
===
Snippets are used to minimize the use of repeated code/text. Ynote has amazing support for snippets. Ynote stores snippets in a *.ynotesnippet* file which have the following properties -

- **description** - The description of the snippet
- **tabTrigger**  - The Text before pressing tab which will trigger the snippet
- **content**     - The content of the snippet.
- **scope** 	  - The scope of the snippet

Using Snippets
---
You can use snippets by activating them from their *tabTrigger* or using the Commander. For Example, if you press main + tab in a C file, it is expanded into - 

```C
int main(int argc, char const *argv[])
{
    
    return 0;
}
```
You can also access it from the command palette using the Snippet: command.

Downloading Snippets
---

You can download snippets from the Package Manager.

Writing Snippets
---
It is very easy to write your own snippets. A ynote snippet is an xml file with the extension .ynotesnippet, stored anywhere in the **$ynotedata** directory. Read the parameters above. The variables for the content are - 

- **^** - The position of the caret
- **$selection** - The text selected when the snippet was triggered
- **$current_line** - The text on the current line when the snippet was triggered
- **$file_name** - The name of the file being edited
- **$file_name_extension** - $file_name with extension
- **$clipboard** - The text on the clipboard when the snippet was triggered
- **$eol** - The new line of the file in ynote.
- **$choose_file** - Chooses a file and replaces it with the filename

Example Snippet -

```xml
<?xml version="1.0"?>
	<YnoteSnippet>
		<description>Python Function</description>
		<tabTrigger>def</tabTrigger>
		<content>def ^():$eol pass</content>
		<scope>Python</scope>
	</YnoteSnippet>
```
The above snippets is used when the syntax is python. **^** specifies the caret position. **$eol** inserts a newline. Snippets are automatically indented by ynote.