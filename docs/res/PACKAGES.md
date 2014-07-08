Ynote Packages
===
Ynote Packages are archives which contains any kind of resource files used by Ynote like - 

- Plugins
- Color Schemes
- Snippets
- Commands(Used by Commander)
- RunScripts (Build System)
- Key Bindings
- Ynote Scripts
- Macros

Installing a Package
---
You can install a Package using the **Package Manager** included in Ynote. It is found in the **Tools->Package** Manager Menu. The *Available Packages* tab shows a list of packages which are available for download. Click "Install Selected" to install the package.

**Installing from File**

You can also install a package from a local ynotepackage file. By clicking on the *"Install from File"* button.

Creating a Package
---
User can share the ynote resource files by creating packages. Packages can be created using the **Package Manager**. The **Installed Packages** tab has the *Create Package* button. It will show the Package Maker.

![Ynote Package Maker)](images/PackageMaker.PNG "Package Maker")

**Walkthrough**

- Choose an output file
- Select the files to be added to your package - It will show another dialog, which asks for the input file and the output file. The **Output** is the directory where the file will go. The parameters are -
    - **$ynotedata** - The %appdata%\Ynote Classic Directory
    - **$ynotedir**  - The %programfiles%\Ynote Classic Directory
- Click "Create Package"

*eg*- **Input** - **C:\my\pySnippet.ynotesnippet** , **Output** - **$ynotedata\User\Snippets**
You can also specify the output as **$ynotedata\{Package Name}\Snippets
The non-existing directories will be automatically created