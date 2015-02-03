FileFinder
==========

A plug-in for N++ which helps quickly finding files by the name.
Besides it serves an extended usage of N++'s file history.

The plug-in features were heavily inspired by Wing IDE's "Select
Project File to Open" and Opera's "Reopen last closed tab".

Note: This plug-in is not going to crawl the content of files.
N++ already perfectly offers this functionality. Also the
plug-in's own file history is not inherited by the one of N++.

Functions:
----------

"Open from directory (greedy)...":
Directly open the dialog with the match list in the context of the document's
folder, which is currently focused in N++. The context folder, which is always
the top level directory for the asynchronously starting file search, gets
highlighted in the full file path box. All further dialog interactions (e.g.
entering a filter string) can be immediately started while the asynchronous
file search is still active. The context can be navigated to the parent folder
by clicking the folder-up icon or by pressing ALT + UP, which restarts the file
search.
Note: The use case for this function is to quickly open a file, which resides
"nearby" or in the same "workspace". For technical reasons this approach of file
search starts slowing down when the match list contains hundreds or thousands
of file entries.

"Search in directory (explicitly)...":
Opens a folder browser dialog preselected with the folder of the document,
which is currently focused in N++. The browser is followed by a search pattern
dialog, which besides offers a history of all entered search strings. Once the
search pattern is confirmed the match list dialog opens while the file search
gets started asynchronously. The context folder, which was chosen in the folder
browser dialog gets highlighted in the full file path box. All further dialog
interactions (e.g. entering a filter string) can be immediately started while
the asynchronous file search is still active. The context can be navigated to
the parent folder by clicking the folder-up icon or by pressing ALT + UP, which
restarts the file search.
Note: The use case for this function is to search for a file "somewhere on disk"
or in workspaces with thousands of files.

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
Choose how to display the file paths in the list box.

"Excluded directories / file names":
For each "folder search" and "file history search" directories and file
names can be excluded by search patterns. The usual syntax of these search strings
is similar to the one used by the Windows Explorer (e.g. in the file path text
box of an Explorer window):
The strings can contain a combination of valid literal path, wildcard (* and ?)
characters and environment variables. Typically this syntax results in the
desired file name hit list, but sometimes it is required to use an advanced and
more precise query string as an exclusion filter. For this purpose please see the
section below about regular expressions.

"Show filtered paths":
Sometimes you might wonder if any files are missing in the expected result list,
either due to inaccurate exclusion filters, "hidden" file attribute or insufficient
access rights. If you enable this option, which you can also toggle in the left
bottom corner of the directory search dialog, then all of these files will be shown
too (red = hidden or access denied, blue = excluded by filter).

"Bypass file system redirection":
If you ever tried on a 64 bit Windows navigating via a file dialog of a 32 bit
application (like N++) into the "native" system32 folder, then you surely know that
you fail. As for technical reasons this is not possible without an expensive
workaround, FileFinder provides an approach to do so by executing an included small
64 bit binary (".../plugins/FileFinder/FileSelectDialog.exe"). So since this results
in a short delay of a second or two, you shouldn't really enable this options if
you don't intend to ever search anything in the system32 folder.
As this is a 64 bit issue, the check box is disabled on a 32 bit windows.

"Maximum history length":
Don't remember a higher amount of closed file paths than defined.

"Auto validate file names":
When accessing the file history check if all listed file paths exist and that
they don't match the defined file history exclusion list.

Regular expressions:
--------------------

The strings in the search pattern dialog ("Search in directory (explicitly)")
and in the exclusion masks ("Excluded directories / file names") can optionally
contain regular expressions in order to exclude files and folders by a maximum
granularity. By starting a pattern string with a colon (":") you tell FileFinder
that it should handle the pattern as a regular expression.
Examples:
:\\bak\\.*\.cs$ (case sensitive exclusion of all "*.cs" files recursively
                 found in the folder structure of any "bak" folder)
:(?i)\\log\\    (case insensitive exclusion of all "log" folders without
                 excluding "log" files in general)
For details about the used regular expression engine please search the internet
for the "MSDN: Regular Expression Language - Quick Reference".

Plug-in developers:
-------------------

FileFinder exposes its functionality to other N++ plug-ins.
[T.b.d.]

Change log:
-----------

1.0 Release
