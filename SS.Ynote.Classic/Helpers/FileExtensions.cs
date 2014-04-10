using System.Collections.Generic;
using System.Linq;
using FastColoredTextBoxNS;
using Nini.Config;
using SS.Ynote.Classic.Features.Syntax;

internal static class FileExtensions
{
    internal static IDictionary<IEnumerable<string>, Language> FileExtensionsDictionary { get; private set; }

    /// <summary>
    ///     Build Dictionary
    /// </summary>
    /// <returns></returns>
    internal static void BuildDictionary()
    {
        FileExtensionsDictionary = new Dictionary<IEnumerable<string>, Language>();
        IConfigSource source = new IniConfigSource(SettingsBase.SettingsDir + "Extensions.ini");
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("CSharp").Split('|'), Language.CSharp);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("VB").Split('|'), Language.VB);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Javascript").Split('|'), Language.Javascript);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Java").Split('|'), Language.Java);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("HTML").Split('|'), Language.HTML);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("CSS").Split('|'), Language.CSS);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("CPP").Split('|'), Language.CPP);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("PHP").Split('|'), Language.PHP);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Lua").Split('|'), Language.Lua);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Ruby").Split('|'), Language.Ruby);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Python").Split('|'), Language.Python);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Pascal").Split('|'), Language.Pascal);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Lisp").Split('|'), Language.Lisp);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Batch").Split('|'), Language.Batch);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("C").Split('|'), Language.C);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Xml").Split('|'), Language.Xml);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("ASP").Split('|'), Language.ASP);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Actionscript").Split('|'), Language.Actionscript);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Assembly").Split('|'), Language.Assembly);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Antlr").Split('|'), Language.Antlr);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Diff").Split('|'), Language.Diff);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("D").Split('|'), Language.D);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("FSharp").Split('|'), Language.FSharp);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("JSON").Split('|'), Language.JSON);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("MakeFile").Split('|'), Language.Makefile);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("ObjectiveC").Split('|'), Language.Objective_C);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Perl").Split('|'), Language.Perl);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("QBasic").Split('|'), Language.QBasic);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("SQL").Split('|'), Language.SQL);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Shell").Split('|'), Language.Shell);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Scala").Split('|'), Language.Scala);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Scheme").Split('|'), Language.Scheme);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("INI").Split('|'), Language.INI);
        FileExtensionsDictionary.Add(source.Configs["Extensions"].Get("Yaml").Split('|'), Language.Yaml);
    }

    internal static SyntaxDesc GetLanguage(IDictionary<IEnumerable<string>, Language> dic, string extension)
    {
        var desc = new SyntaxDesc();
        Language lang;
        foreach (var key in dic.Keys.Where(key => key.Contains(extension)))
            if (dic.TryGetValue(key, out lang))
                desc.Language = lang;
            else
                foreach (
                    var syntax in
                        SyntaxHighlighter.LoadedSyntaxes.Where(syntax => syntax.Extensions.Contains(extension)))
                    desc.SyntaxBase = syntax;
        return desc;
    }
}

internal class SyntaxDesc
{
    /// <summary>
    ///     if IsBase = false Value of Language
    /// </summary>
    internal Language Language;

    /// <summary>
    ///     Syntax Base
    /// </summary>
    internal SyntaxBase SyntaxBase;

    /// <summary>
    ///     Is a Syntax Base
    /// </summary>
    internal bool IsBase
    {
        get { return SyntaxBase != null; }
    }
}