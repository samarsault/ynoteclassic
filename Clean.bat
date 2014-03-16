set msBuildDir=%WINDIR%\Microsoft.NET\Framework\v3.5
call %MSBuildDir%\msbuild SS.Ynote.Classic.sln /t:clean
@IF %ERRORLEVEL% NEQ 0 PAUSE