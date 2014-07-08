Runscripts (Build Systems)
===

Build Systems in ynote are called RunScripts. Runscripts are used for execution of code , external tool or anything.

Executing a RunScript
---
A Runscript can be executed using the **Commander** command *"Run:"*.

Writing a RunScript
---
A Runscript is a JSON file containing a name and a list of tasks to be executed one after the another when the RunScript is run.

**Example RunScript**
```JSON
{
    // name of the runscript
    "Name":["Notepad"],
    // executes Cmd.runtask with the given arguments
    "RunScripts\\Tasks\Cmd":[
        "%windir%\notepad.exe",
        "$source"
    ]
}
```
The above runscript when executed opens the current file in **notepad**. The abbreviations used by Runscripts are -

- **$source** - The Full Path of the active file
- **$source_dir** - The Directory of the active file.
- **$source_extension** - The extension of the active file.
- **$source_name** - The name of the active file(without extension)
- **$project_dir** - The Directory of the actve project
- **$project_name** - The name of the active project

What are RunScript Tasks ?
---
RunScript tasks are C# Scripts (YnoteScripts) which are invoked when a runscript containing a reference to the C# Script is invoked with arguments.

**Example Task**
```csharp
// Simple Run Task to create a directory
using System.IO;

// Cmd Run Script Task
public void RunTask(string[] arguments)
{
    if(arguments.Length == 0)
        return;
    string dir = arguments[0];
    if(!Directory.Exists(dir))
        Directory.CreateDirectory(dir);
}
```
The above task when invoked with **arguments** creates a directory.

Installed Tasks
---
Ynote contains some preinstalled RunTasks - 

- Cmd - Executes a program from the command prompt. Arguments - "process","args","working_dir"
- mkdir - Makes a directory. Arguments - "directory"
- scriptexec - Execute a YnoteScript. Arguments - "scriptFile","mainMethod"
- runexec - Execute another runscript. Arguments - "runscript_file"
