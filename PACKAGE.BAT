SET VERSION=v0.2.0

SET ZIP="C:\Program Files\7-Zip\7z.exe"

SET TARGET=FileFinder.%VERSION%.bin

MD %TARGET%
MD %TARGET%\FileFinder
MD %TARGET%\FileFinder\doc
MD %TARGET%\FileFinder\FileFinder

COPY "Notepad++\plugins\FileFinder\FileFinder.dll" "%TARGET%\FileFinder"
COPY "Notepad++\plugins\FileFinder\FileFinder\FolderSelectDialog.exe" "%TARGET%\FileFinder\FileFinder"
COPY "readme.txt" "%TARGET%\FileFinder\doc\FileFinder.readme.txt"

CD %TARGET%
%ZIP% a -mx9 ..\%TARGET%.zip *
CD..
