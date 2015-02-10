FileFinder
==========

A plug-in for N++ which helps quickly finding files by name.
Besides it serves an extended usage of N++'s file history.

Additional notes:
1) The plug-in features were inspired by Wing IDE's "Select
   Project File to Open" and Opera's "Reopen last closed tab".
2) The plug-in does not crawl the content of files.
   N++ already offers this functionality ("Find in Files").
3) The file history of the plug-in is not directly related to
   the one of N++.

Functions:
----------

"Open from directory (greedy)...":
Directly open the dialog with the match list in the context of the document's
folder, which is currently focused in N++. The context folder, which is always
the top level directory for the asynchronously starting file search, gets
highlighted in the full file path box. All further dialog interactions (e.g.
entering a filter string) can be immediately started while the asynchronous
file search is still active. The context can be navigated to the parent folder
by clicking the folder-up icon (or by pressing ALT + UP as in Windows Explorer),
which restarts the file search. In the left bottom corner on the one hand case
sensitive search can be toggled on and off, on the other hand all filtered
results can be made visible in the match list.
Note: The use case for this function is to quickly open a file, which resides
"nearby" or in the same recursive "workspace". For technical reasons this
approach of file search starts slowing down when the match list contains
thousands of file entries.

"Search in directory (explicitly)...":
Opens a folder browser dialog preselected with the folder of the document,
which is currently focused in N++. The browser is followed by a search pattern
dialog, which besides offers a history of all entered search strings. Once the
search pattern is confirmed the match list dialog opens while the file search
gets started asynchronously. The context folder, which was chosen in the folder
browser dialog gets highlighted in the full file path box. All further dialog
interactions (e.g. entering a filter string) can be immediately started while
the asynchronous file search is still active. The context can be navigated to
the parent folder by clicking the folder-up icon (or by pressing ALT + UP as in
Windows Explorer), which restarts the file search. In the left bottom corner
on the one hand case sensitive search can be toggled on and off, on the other
hand all filtered results can be made visible in the match list.
Note: The use case for this function is to search for a file "somewhere on disk"
or in "workspaces" with thousands of files.

"Open from file history...":
Show a filterable list of the last closed files. Select and open a file or
choose multiple files at once. In the left bottom corner case sensitive
search can be toggled on and off. If the auto file name validation is enabled,
which can also be toggled in the left bottom corner, then FileFinder starts
asynchronously removing non-existing or exclusion-matching files. The displayed
file path format can be configured in the options dialog.

"Open last closed file":
Open the last entry of the file history list. If the auto file name validation
is enabled, then all non-existing and exclusion-matching files are skipped until
a valid file can be opened.

Options:
--------

"Tool bar buttons":
Check the buttons to display in the tool bar. Only applies after a restart of N++.

"Case sensitive search filter":
Choose how to compare the search box string with the file paths in the list box.
This option can also be toggled in the left bottom corner of the search dialog.

"Displayed file path format":
Choose how to display the file paths in the list box. Displaying relative paths
means that the least common directory ancestor of all currently found entries
is the root directory for all relative paths.

"Excluded directories / file names":
For each "folder search" and "file history search" directories and file
names can be excluded by search patterns. The usual syntax of these search strings
is similar to the one used by the Windows Explorer (e.g. in the file path text
box of an Explorer window):
The strings can contain a combination of valid literal path, wildcard (* and ?)
characters and environment variables. Typically this syntax results in the
desired file name hit list, but sometimes it is required to use an advanced and
more precise query string to be used as an exclusion filter. For this purpose
please see the section below about regular expressions.

"Show filtered paths":
Sometimes you might wonder if any files are missing in the expected result list,
either due to inaccurate exclusion filters, "hidden" file attributes or insufficient
access rights. If you enable this option, which you can also toggle in the left
bottom corner of the directory search dialog, then all of these files will be shown
too (red = hidden or access denied, blue = excluded by filter).

"Bypass file system redirection":
If you ever tried on a 64 bit Windows navigating via a file dialog of a 32 bit
application (like N++) into the "native" system32 folder, then you might have
noticed that it is not possible. As for technical reasons this is not doable without
an expensive workaround, FileFinder provides an approach to do so by executing an
included small 64 bit binary (".../plugins/FileFinder/FolderSelectDialog.exe").
Since this sometimes results in a delay of some seconds, you shouldn't enable this
option if you don't intend to ever search anything in the system32 folder.
Note: As this is a 64 bit issue, the check box is disabled on a 32 bit windows.

"Maximum history length":
Don't remember a higher amount of closed file paths than defined.

"Auto validate file names":
When accessing the file history always check if all listed file paths exist and that
they don't match the defined file history exclusion list.

Regular expressions:
--------------------

The strings in the search pattern dialog ("Search in directory (explicitly)")
and in the exclusion masks ("Excluded directories / file names") can optionally
contain regular expressions in order to exclude files and folders by a maximum
granularity. By starting a pattern string with a colon (":") you tell FileFinder
that it should handle the pattern as a regular expression.

Example folder exclusion patterns:
:\\bak\\.*\.cs$ (case sensitive exclusion of all "*.cs" files recursively
                 found in the folder structure of any "bak" folder)
:(?i)\\log\\    (case insensitive exclusion of all "log" folders without
                 excluding "log" files in general)

Example file search patterns:
:\.(cs|resx)$   (show all file names with the file extensions ".cs" and ".resx"
                 without including files with other file extensions but residing
				 in a folder e.g. like ".cs-files")
:\\F[^\\]*$     (show all file names starting with a "F" without including files
                 not starting with a "F" but residing in a folder starting with
				 a "F")

For details about the used regular expression engine please search the internet
for the "MSDN: Regular Expression Language - Quick Reference".

Plug-in developers:
-------------------

FileFinder exposes its functionality to other N++ plug-ins:

NPPM_FILEFINDER_OPEN_FROM_DIRECTORY_GREEDY = 0x0101;
// Evaluated NppmFileFinderInfo members:
//  -NppmFileFinderInfo.szDirPath
//  -NppmFileFinderInfo.bOpenFiles
// Filled NppmFileFinderInfo members:
//  -NppmFileFinderInfo.arrSelectedFilePaths
// If bOpenFiles if false, then all selected paths are returned in
// the form of a string array ("arrSelectedFilePaths") which must
// be freed by the caller.
NPPM_FILEFINDER_OPEN_FROM_STRINGLIST_GREEDY = 0x0102;
// Evaluated NppmFileFinderInfo members:
// -NppmFileFinderInfo.arrFilePaths
// -NppmFileFinderInfo.bOpenFiles
// Filled NppmFileFinderInfo members:
//  -NppmFileFinderInfo.arrSelectedFilePaths
// If bOpenFiles if false, then all selected paths are returned in
// the form of a string array ("arrSelectedFilePaths") which must
// be freed by the caller.
NPPM_FILEFINDER_SEARCH_IN_DIRECTORY_EXPLICITLY = 0x0103;
// Evaluated NppmFileFinderInfo members:
// -NppmFileFinderInfo.szDirPath
// -NppmFileFinderInfo.bShowFolderBrowser
// -NppmFileFinderInfo.szSearchPattern
// -NppmFileFinderInfo.bOpenFiles
// Filled NppmFileFinderInfo members:
//  -NppmFileFinderInfo.arrSelectedFilePaths
// If bOpenFiles if false, then all selected paths are returned in
// the form of a string array ("arrSelectedFilePaths") which must
// be freed by the caller.
NPPM_FILEFINDER_OPEN_FROM_HISTORY = 0x0104;
// Evaluated NppmFileFinderInfo members:
// -NppmFileFinderInfo.bOpenFiles
// Filled NppmFileFinderInfo members:
//  -NppmFileFinderInfo.arrSelectedFilePaths
// If bOpenFiles if false, then all selected paths are returned in
// the form of a string array ("arrSelectedFilePaths") which must
// be freed by the caller.
NPPM_FILEFINDER_OPEN_LAST_CLOSED_FILE = 0x0105;
// Evaluated NppmFileFinderInfo members:
// -NppmFileFinderInfo.bOpenFiles
// Filled NppmFileFinderInfo members:
//  -NppmFileFinderInfo.arrSelectedFilePaths
// If bOpenFiles if false, then all selected paths are returned in
// the form of a string array ("arrSelectedFilePaths") which must
// be freed by the caller.

Example call:
SendMessage(nppData._nppHandle, NPPM_MSGTOPLUGIN, "FileFinder.dll", communicationInfo)

Example CommunicationInfo:
communicationInfo.srcModuleName = "MyPlugin.dll";
communicationInfo.internalMsg = NPPM_FILEFINDER_OPEN_FROM_DIRECTORY_GREEDY;
communicationInfo.info = nppmFileFinderInfo;

Example NppmFileFinderInfo:
nppmFileFinderInfo.szDirPath = "C:\some\directory";
nppmFileFinderInfo.arrFilePaths = NULL;
nppmFileFinderInfo.bShowFolderBrowser = false;
nppmFileFinderInfo.szSearchPattern = NULL;
nppmFileFinderInfo.bOpenFiles = true;
nppmFileFinderInfo.arrSelectedFilePaths = NULL;

Change log:
-----------

1.0
    -release
