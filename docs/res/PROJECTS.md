Projects
====
Projects in Ynote Classic have 2 files. A project file, which contains information about the project and a  layout file which contains a list of open files, dock-windows and other user info.

Creating a Project
---
Creating a project is very simple. **Project->Add Folder To Project** shows the project manager with your chosen folder. Click **Project->Save Project** to save the project and you are good to go.

Editing a Project
---

A Ynote Project is a json file with the following parameters -
 
- Path(string) - The Path to the project's folder
- Name(string) - The name of the project
- ExcludeFileTypes(array)- The file types to exclude ( without * character, simple .exe,.txt etc)
- ExcludeDirectories(array) - The Directories to exclude
- Arguments(array) - Arguments of the Project, generally used by plugins

*Sample Project File*
```json
{
  "Path": "C:\\Users\\Guest\\Desktop\\PySripts",
  "Name": "Python Scripts",
  "ExcludeFileTypes": [".exe",".pyc"]
  "ExcludeDirectories": null,
  "Arguments": null
}
```