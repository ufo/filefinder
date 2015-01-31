FileFinder
==========

A plug-in for N++ which helps quickly finding files by their name.
Additionally it offers an extended file history.

Functions:
----------

"Open from directory (greedy)...":
[T.b.d.]

"Search in directory (explicitly)...":
[T.b.d.]

"Open from file history...":
[T.b.d.]

"Open last closed file":
[T.b.d.]

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
Each for the recursive folder search and for the file history search
directories and file names can be excluded by search strings.
The commonly known syntax of these search strings is similar to the one used
by the Windows Explorer (e.g. in the file path text box of an Explorer window):
The strings can contain a combination of valid literal path, wildcard (* and ?)
characters and environment variables.

"Maximum history length":
Don't remember a higher amount of closed file paths than defined.
Don't set this value too high if you also enable the auto file name validation.

"Auto validate file names":
When accessing the file history check if all listed file paths exist and that
they don't match the defined file history exclusion list.

Plug-in developers:
-------------------

FileFinder exposes its functionality to other N++ plug-ins.
[T.b.d.]

Change log:
-----------

1.0 Release
