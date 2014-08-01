@echo off
%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild /p:Configuration=Release;TargetFrameworkVersion=v4.0 "Ynote Classic.sln"