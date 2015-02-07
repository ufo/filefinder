@ECHO OFF

CALL SETUP_ENV.BAT

MSBUILD /target:Rebuild /Property:Configuration=Release FileFinder.sln

PAUSE
