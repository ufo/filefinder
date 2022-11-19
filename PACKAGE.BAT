@ECHO off

SET VERSION=v0.2.0

SET ZIP="C:\Program Files\7-Zip\7z.exe"
SET ZIP_ARGS=a -mx9

SET TARGET=FileFinder.%VERSION%.bin

MD %TARGET%
MD %TARGET%\doc
MD %TARGET%\FileFinder

COPY "Notepad++\plugins\FileFinder\FileFinder.dll" "%TARGET%"
COPY "Notepad++\plugins\FileFinder\FileFinder\FolderSelectDialog.exe" "%TARGET%\FileFinder"
COPY "readme.txt" "%TARGET%\doc\FileFinder.readme.txt"

CD %TARGET%
%ZIP% %ZIP_ARGS% ..\%TARGET%.zip *
CD..
