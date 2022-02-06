# FileComparer
****
**FileComparer is a console program created by Doopath. That helps you to see the current
(or other) directory containing files (like unix "ls" program). I often notice when I use
"ls" in linux that it could be pretty comfy if it shows a diagram of file sizes, because it's 
not awesome the you should go through a list of files and check their sizes to know that file is
the largest in a directory.**

**FileComparer by Doopath is also a cross-platform program, so you can run it on Windows, Linux, OSX and other
platforms supported by .NET 5.0 (and some others, because there is unoptimized portable assemblies). Current release (v1.0.1)
contains "Self-contained" assemblies. That means you don't need to install .NET 5.0 platform on your PC, it's already included
in a build (win-x64, linux-x64, osx-x64). Also, release contains "Framework-depended" portable version, it's supported on all
platforms supporting .NET 5.0, but it's not optimized.**

****
*Default FileComparer view*
![no image](https://raw.githubusercontent.com/doopath/FileComparer/develop/Screenshots/FileComparer.png)



## Usage

**FileComparer by Doopath is very simple to use, it has only 4 types of command-line arguments
(you can set color, width, directory and size format). For example, if you want to know sizes
of files in current directory, colorized in blue and formatted as KiBs, you should type:**
```shell
fcr -c blue -sf kib
# or fcr -c blue -sf kib ./
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
**To install FileComparer go to the release page and download an archive written with name of your platform
(for me it's FileComparer_v.v.v_linux-x64.tar.gz). Extract this archive and move target directory somewhere
(if you use linux it could be the $HOME/.local/bin directory, or C:/Program Files/ if you use Windows. Next step is to create a
shortcut to the "fcr" binary. I would like to recommend to create a bash script, if you use linux:**


```bash
# Create and open a file
touch /usr/local/bin/fcr
nano /usr/local/bin/fcr
```

**And put these lines in the /usr/local/bin/fcr file:**
```bash
#! /usr/bin/bash
$HOME/.local/bin/FileComparer/fcr
```

**Add the permissions to allow to execute the file**
```bash
chmod +x /usr/local/bin/fcr
```
<br/>

**On Windows you can just put the extracted archive in the C:/Program Files/ folder and add the path to the binary in the PATH env variable.**