@ECHO OFF

SET MAJORVER=0
SET MINORVER=1
SET BUILDNUM=0
SET REVISION=0

REPLEX ".\FileFinder\Properties\AssemblyInfo.cs" -e 2 ^
-s "Version\("[0-9]+[.][0-9]+[.][0-9]+[.][0-9]+"\)\]" ^
-r "Version("%MAJORVER%.%MINORVER%.%BUILDNUM%.%REVISION%")]"

REPLEX ".\FolderSelectDialog\Properties\AssemblyInfo.cs" -e 2 ^
-s "Version\("[0-9]+[.][0-9]+[.][0-9]+[.][0-9]+"\)\]" ^
-r "Version("%MAJORVER%.%MINORVER%.%BUILDNUM%.%REVISION%")]"

REPLEX ".\PACKAGE.BAT" -e 1 ^
-s "SET VERSION=v[0-9]+[.][0-9]+[.][0-9]+" ^
-r "SET VERSION=v%MAJORVER%.%MINORVER%.%BUILDNUM%"

PAUSE