// Carbon cross-tabulation library sample
// https://github.com/redcentre
// Copyright © 2022-2025 Red Centre Software
// https://www.redcentresoftware.com/

// Ensure the latest tool is installed by:
// > dotnet tool update dotnet-script --global

// Run this script with the command:
// > dotnet-script GenTab-Basic.csx --isolated-load-context

#r "nuget:RCS.Carbon.Tables,9.1.39"
#r "nuget:RCS.Licensing.Provider.Shared,8.1.16"

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using RCS.Carbon.Variables;
using RCS.Carbon.Tables;
using RCS.Carbon.Shared;
using RCS.Licensing.Provider.Shared;
using static System.Console;

//------------------------------------------------------
// Create a crosstab engine instance and print the name,
// version, build info and current framework
//------------------------------------------------------
var engine = new CrossTabEngine();
var t = engine.GetType();
var asm = t.Assembly;
var ver = asm.GetName().Version;
var build = asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
var tfa = asm.GetCustomAttribute<TargetFrameworkAttribute>();
WriteLine($"Created {t.Name} {ver} Timestamp {build}");
WriteLine($"Framework {tfa.FrameworkName}");

//------------------------------------------------
// Get the 'free' licence to activate the engine,
// then print some of the licensing information.
//------------------------------------------------
LicenceInfo lic = await engine.GetFreeLicence();
WriteLine(lic.GetType().FullName);
WriteLine($"License Id ........... {lic.Id}");
WriteLine($"License Name ......... {lic.Name}");
string rolejoin = string.Join(",", lic.Roles);
WriteLine($"License Roles ........ {rolejoin}");
foreach (var cust in lic.Customers)
{
	WriteLine($"License | {cust.Name}");
	foreach (var job in cust.Jobs)
	{
		WriteLine($"License |  | {job.Name}");
	}
}

//------------------------------------------------
// Open one of the public cloud jobs and list the
// variable tree names defined in the job.
//------------------------------------------------
engine.OpenJob("rcsruby", "demo");
string[] vtnames = engine.ListVartreeNames().ToArray();
WriteLine($"Vartree count = {vtnames.Length}");
foreach (string vtname in vtnames)
{
	WriteLine($"VARTREE | {vtname}");
}

//--------------------------------------------------------------
// Generate a crosstab report in CSV format using the "Age" and
// "Region" variables on the top and side axes respectively.
//--------------------------------------------------------------
var sprops = new XSpecProperties();
var dprops = new XDisplayProperties();
dprops.Output.Format = XOutputFormat.CSV;
string body = engine.GenTab(null, "Age", "Region", null, null, sprops, dprops);
WriteLine(body);

bool closed = engine.CloseJob();
Console.WriteLine($"Job closed -> {closed}");
