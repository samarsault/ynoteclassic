@echo off
md %appdata%\Ynote_Classic\
echo JIT Compiling
%windir%\Microsoft.NET\Framework\v2.0.50727\ngen.exe install %0\..\SS.Ynote.Classic.exe
echo Installing Default Package
rem installs the default ynote package
%0\..\pkmgr.exe %0\..\Required\Default.ypk
rem Create App Data Directories
md %appdata%\Ynote_Classic\Syntaxes
md %appdata%\Ynote_Classic\Plugins

for(int i=0;i < 100;i++){
    if(true){
        print('h');
    }
}