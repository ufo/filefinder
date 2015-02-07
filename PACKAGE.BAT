SET VERSION=v0.1.0

SET ZIP="C:\Tools\7-Zip\7z.exe"

SET TARGET=FileFinder.%VERSION%.bin

MD %TARGET%
MD %TARGET%\doc
MD %TARGET%\FileFinder

COPY "Notepad++\plugins\FileFinder.dll" "%TARGET%"
COPY "Notepad++\plugins\FileFinder\FolderSelectDialog.exe" "%TARGET%\FileFinder"
COPY "readme.txt" "%TARGET%\doc\FileFinder.readme.txt"

CD %TARGET%
%ZIP% a -mx9 ..\%TARGET%.zip *
CD..
