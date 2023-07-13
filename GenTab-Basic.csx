// Carbon cross-tabulation library sample
// https://github.com/redcentre
// Copyright © 2022-2023 Red Centre Software
// https://www.redcentresoftware.com/

#r "nuget:RCS.Carbon.Tables, 8.5.9"			// Why is the version number needed sometimes to force the latest packge to be used?

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using RCS.Carbon.Variables;
using RCS.Carbon.Tables;
using RCS.Carbon.Shared;
using RCS.Carbon.Licensing.Shared;
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
LicenceInfo licinfo = await engine.GetFreeLicence();
Console.WriteLine(licinfo.GetType().FullName);
WriteLine($"License Id ........... {licinfo.Id}");
WriteLine($"License Name ......... {licinfo.Name}");
string rolejoin = string.Join(",", licinfo.Roles);
WriteLine($"License Roles ........ {rolejoin}");
foreach (var cust in licinfo.Customers)
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
