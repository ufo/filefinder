@ECHO OFF

CALL SETUP_ENV.BAT
if ERRORLEVEL == 1 GOTO FINISH

MSBUILD /target:Rebuild /Property:Configuration=Release /p:Platform="Any CPU" FileFinder.sln

:FINISH
PAUSE
