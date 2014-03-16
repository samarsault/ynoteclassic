#region

using System;
using System.Collections.Generic;
using System.Linq;
using FastColoredTextBoxNS;
using Nini.Config;

#endregion

internal static class FileExtensions
{
    public static IDictionary<string[], Language> BuildDictionary()
    {
        var dic = new Dictionary<string[], Language>();
        IConfigSource source = new IniConfigSource(SettingsBase.SettingsDir + @"Extensions.ini");
        dic.Add(source.Configs["Extensions"].Get("CSharp").Split('|'), Language.CSharp);
        dic.Add(source.Configs["Extensions"].Get("VB").Split('|'), Language.VB);
        dic.Add(source.Configs["Extensions"].Get("Javascript").Split('|'), Language.Javascript);
        dic.Add(source.Configs["Extensions"].Get("Java").Split('|'), Language.Java);
        dic.Add(source.Configs["Extensions"].Get("HTML").Split('|'), Language.HTML);
        dic.Add(source.Configs["Extensions"].Get("CSS").Split('|'), Language.CSS);
        dic.Add(source.Configs["Extensions"].Get("CPP").Split('|'), Language.CPP);
        dic.Add(source.Configs["Extensions"].Get("PHP").Split('|'), Language.PHP);
        dic.Add(source.Configs["Extensions"].Get("Lua").Split('|'), Language.Lua);
        dic.Add(source.Configs["Extensions"].Get("Ruby").Split('|'), Language.Ruby);
        dic.Add(source.Configs["Extensions"].Get("Python").Split('|'), Language.Python);
        dic.Add(source.Configs["Extensions"].Get("Pascal").Split('|'), Language.Pascal);
        dic.Add(source.Configs["Extensions"].Get("Lisp").Split('|'), Language.Lisp);
        dic.Add(source.Configs["Extensions"].Get("Batch").Split('|'), Language.Batch);
        dic.Add(source.Configs["Extensions"].Get("C").Split('|'), Language.C);
        dic.Add(source.Configs["Extensions"].Get("Xml").Split('|'), Language.Xml);
        dic.Add(source.Configs["Extensions"].Get("ASP").Split('|'), Language.ASP);
        dic.Add(source.Configs["Extensions"].Get("Actionscript").Split('|'), Language.Actionscript);
        dic.Add(source.Configs["Extensions"].Get("Assembly").Split('|'), Language.Assembly);
        dic.Add(source.Configs["Extensions"].Get("Antlr").Split('|'), Language.Antlr);
        dic.Add(source.Configs["Extensions"].Get("Diff").Split('|'), Language.Diff);
        dic.Add(source.Configs["Extensions"].Get("D").Split('|'), Language.D);
        dic.Add(source.Configs["Extensions"].Get("FSharp").Split('|'), Language.FSharp);
        dic.Add(source.Configs["Extensions"].Get("JSON").Split('|'), Language.JSON);
        dic.Add(source.Configs["Extensions"].Get("MakeFile").Split('|'), Language.Makefile);
        dic.Add(source.Configs["Extensions"].Get("ObjectiveC").Split('|'), Language.Objective_C);
        dic.Add(source.Configs["Extensions"].Get("Perl").Split('|'), Language.Perl);
        dic.Add(source.Configs["Extensions"].Get("QBasic").Split('|'), Language.QBasic);
        dic.Add(source.Configs["Extensions"].Get("SQL").Split('|'), Language.SQL);
        dic.Add(source.Configs["Extensions"].Get("Shell").Split('|'), Language.Shell);
        dic.Add(source.Configs["Extensions"].Get("Scala").Split('|'), Language.Scala);
        dic.Add(source.Configs["Extensions"].Get("Scheme").Split('|'), Language.Scheme);
        dic.Add(source.Configs["Extensions"].Get("INI").Split('|'), Language.INI);
        dic.Add(source.Configs["Extensions"].Get("Yaml").Split('|'), Language.Yaml);
        return dic;
    }

    public static Language GetLanguage(IDictionary<string[], Language> dic, string extension)
    {
        string[] reqKey = default(string[]);
        foreach (var key in dic.Keys)
            if (key.Contains(extension))
                reqKey = key;
        try
        {
            Language lang;
            dic.TryGetValue(reqKey, out lang);
            return lang;
        }
        catch (Exception)
        {
            return Language.Text;
        }
    }
}