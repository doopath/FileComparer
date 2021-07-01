# FileComparer
****
**FileComparer is a console program created by Doopath. That helps you to see the current
(or other) directory containing files (like unix "ls" program). I often notice when I use
"ls" in linux that it could be pretty comfy if it shows a diagram of file sizes, because it's 
not awesome the you should go through a list of files and check their sizes to know that file is
the largest in a directory.**

**FileComparer by Doopath is also a cross-platform program, so you can run it on Windows, Linux, OSX and other
platforms supported by .NET 5.0 (and some others, because there is unoptimized portable assemblies).**


## Usage

**FileComparer by Doopath is very simple to use, it has only 4 types of command-line arguments
(you can set color, width, directory and size format). For example, if you want to know sizes
of files in current directory, colorized in blue and formatted as KiBs, you should type:**
```shell
fc -c blue -sf kib
# or fc -c blue -sf kib ./
# (if you didn't set path it's applies to current dir)
```

**Supported colors: *red, green, blue, black, white, yellow, purple*.**

**Supported size formats: *bytes, kb, kib, mb, mib, gb, gib*.** 

**Also, you can set a width value of the diagram with ```-w``` parameter.**
**For example: ```fc -w 200 -c red```**
**By default fc uses *width=100*, *color=purple* and *size format=bytes*.**

**Parameters' names:**
```None
-c : color
-w : width (of the diagram)
-sf : size format
<path-to-dir> : You know, path to a directory, containing files, whose sizes you want to know.
```


## Installation
**To install FileComparer go to the release page and download a zip archive written with name of your platform
(for me it's FileComparer_v_v_v_win-x64.zip). Extract this archive and move target directory somewhere
(if you use linux it could be the /opt/ directory, or C:/Program Files/ if you use Windows 10. Next step is create a
shortcut to the "fc" binary. I could advise create a bash script, if you use linux, containing something like:**
```bash
#! /usr/bin/bash
/opt/FileComparer/fc
```
**Name it as "fc" and put it in the /usr/bin/ directory. Also, give an execution permission for your user and start use
FileComparer. If you use Windows create a shortcut by right-clicking at the .exe file and choose "create shortcut"
option. Move it on your desktop or the taskbar.**