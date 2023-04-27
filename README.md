:hand: Note that this repository contains documentation and sample scripts that are intended to be run using the standard **dotnet-script** tool, not Red Centre Software's **rcs.exe** script runner command which is customised for running scripts that call Carbon. The **rcsx.exe** command is discussed in detail in the ![PDF][pdf16] [Carbon Scripting][carbpdf] and ![PDF][pdf16] [Libraries and Applications][carbpdf] documents.

---

# Overview

The Carbon cross-tabulation libraries are standard .NET assemblies that can be called from modern scripting hosts. This repository contains sample scripts that exercise the data manipulation and reporting capabilities of Carbon.

The ability to call Carbon functionality from scripts allows data processing professionals to automate the preparation and generation of cross-tabulation reports.

C# scripting has a dependency upon the installtion of the [.NET][net] 6 SDK and the [dotnet-script][script] tool. The steps to install those components is discussed in detail in this Wiki page:

[.NET Prerequisites and Installation][prereq]

C# script files conventionally have the extension .csx. The following commands illustrate how to run a C# script.

```
# WINDOWS
%USERPROFILE%\.dotnet\tools\dotnet-script "Sample Script.csx"
```

```
# UNIX
$HOME\.dotnet\tools\dotnet-script "Sample Script.csx"
```

For convenience, the path to dotnet-script.exe may be added to the current user's PATH environment during login or at the beginning of a long script.

---

## NuGet Packages

Most csx scripts that call Carbon will begin with the statement:

```
#r "nuget:RCS.Carbon.Tables"
```

The scripting host recognises that the script has a dependency on the [RCS.Carbon.Tables][nugtab] package and follows the convention of downloading the package and all of its descendant dependency packages. The downloaded packages are cached under the `%HOMEPATH%\.nuget\packages` directory. There will be an unpredictable delay when the package is first downloaded, but using the cached copy will be fast for future script runs.

---

## using statements

Scripts that work with jobs, reports and variables will need the following statements before the code body to add namespaces for commonly used classes.

```
using RCS.Carbon.Tables
using RCS.Carbon.Variables
using RCS.Carbon.Shared
```

Scripts that perform import and export operations will need the following additional statements.

```
#r "nuget:RCS.Carbon.Import"
#r "nuget:RCS.Carbon.Export"

using RCS.Carbon.Import
using RCS.Carbon.Export
```

---

## Sample Scripts

### GenTab-Basic.csx

This script is a good basic test that the Carbon libraries are behaving correctly. Comments in the C# code explain what is happening in the different steps, but in summary they are the following.

- A CrossTabEngine class is created.
- A `GetFreeLicence` call activates the engine. Some information in the licence is printed, including the customers and jobs that are accessible.
- An `OpenJob` call opens a sample public Azure job published by Red Centre Software.
- A `GenTab` call generates a cross-tabulation report in CSV format.
- `CloseJob` releases job resources.

---

## Troubleshooting

:boom: The donet-script command can unpredictable produce an error like the following example:

<span style="color:red">System.IO.FileLoadException: Could not load file or assembly 'System.Text.Json, Version=7.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. Could not find or load a specific file. (0x80131621)</span>

In this case, the switch `--isolated-load-context` should be added to the command parameters. This issue is discussed in the article [Assembly Isolation Feature][asmiso].

[nugtab]: https://www.nuget.org/packages/RCS.Carbon.Tables/
[prereq]: https://github.com/redcentre/Documentation/wiki/.NET-Prerequisites-and-Installation
[net]: https://en.wikipedia.org/wiki/.NET
[script]: https://github.com/dotnet-script/dotnet-script
[carbpdf]: https://rcsapps.azurewebsites.net/doc/carbon/Carbon%20Scripting.pdf
[pdf16]: https://systemrcs.blob.core.windows.net/wiki-images/pdf16.png
[asmiso]: https://www.strathweb.com/2021/09/dotnet-script-1-2-is-out-with-assembly-isolation-feature/