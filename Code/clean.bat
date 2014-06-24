@echo off
rem clear all unused pdb's and other auto generated files
cd %1
del Build\*.pdb
del Build\*.txt
del Build\*.config
del Build\*.xml
echo Cleared Build\
del FastColoredTextBox\bin\*.*
del FastColoredTextBox\obj\*.*
echo Cleared FastColoredTextBox
del Docking\bin\*.*
del Docking\obj\*.*
echo Cleared Docking\
del SS.Ynote.Classic\bin\*.*
del SS.Ynote.Classic\obj\*.*
echo Cleared SS.Ynote.Classic
del Utilities\bin\*.*
del Utilities\obj\*.*
echo Cleared Utilities\

echo Cleaning Successfull