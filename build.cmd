@echo off

SET VERSION=0.0.0
IF NOT [%1]==[] (set VERSION=%1)

dotnet test src/ViewModels.ApplicationState.Tests/ViewModels.ApplicationState.Tests.csproj
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet pack src/ViewModels.ApplicationState/ViewModels.ApplicationState.csproj -o ../../dist -p:Version=%version% -p:PackageVersion=%version% -c Release