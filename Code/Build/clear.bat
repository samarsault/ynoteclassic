@echo off
rem clear all unused pdb's and other auto generated files
cd %1
del *.pdb
del *.txt
del *.config
del *.xml
echo Cleared