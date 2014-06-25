@echo off
rem clear all unused pdb's and other auto generated files
cd %1
del Build\*.pdb
del Build\*.txt
del Build\*.config
del Build\*.xml
del Build\Last.ynotelayout
echo Cleared Build\
del /F /Q FastColoredTextBox\bin
del /F /Q FastColoredTextBox\obj
echo Cleared FastColoredTextBox
del /F /Q Docking\bin
del /F /Q Docking\obj
echo Cleared Docking\
del /F /Q SS.Ynote.Classic\bin
del /F /Q SS.Ynote.Classic\obj
echo Cleared SS.Ynote.Classic
del /F /Q Utilities\bin
del /F /Q Utilities\obj
echo Cleared Utilities\

echo Cleaning Successfull