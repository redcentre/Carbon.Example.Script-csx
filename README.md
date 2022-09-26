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
#r "nuget:RCS.Carbon.Tables, 8.3.10"
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

---

## Sample Scripts

### GenTab-Basic.csx

This script is a good basic test that the Carbon libraries are behaving correctly. Comments in the C# code explain what is happening in the different steps, but in summary they are the following.

- A CrossTabEngine class is created.
- A `GetFreeLicence` call activates the engine. Some information in the licence is printed, including the customers and jobs that are accessible.
- An `OpenJob` call opens a sample public Azure job published by Red Centre Software.
- A `GenTab` call generates a cross-tabulation report in CSV format.
- `CloseJob` releases job resources.


[nugtab]: https://www.nuget.org/packages/RCS.Carbon.Tables/
[prereq]: https://github.com/redcentre/Documentation/wiki/.NET-Prerequisites-and-Installation
[net]: https://en.wikipedia.org/wiki/.NET
[script]: https://github.com/dotnet-script/dotnet-script
