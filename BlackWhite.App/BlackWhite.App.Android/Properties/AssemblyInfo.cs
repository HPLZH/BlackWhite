using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BlackWhite.App;
using Android.App;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("BlackWhite.App.Android")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("BlackWhite.App.Android")]
[assembly: AssemblyCopyright("Copyright © 李子晗 2022")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: MetaData("android:versionName", Value = AppVersion.MainVersion+"." + AppVersion.SubVersion + "." + AppVersion.Build)]
[assembly: MetaData("android:versionCode", Value = AppVersion.Build)]

#if DEBUG
[assembly: MetaData("package", Value = "cn.hplzh.blackwhite.app.debug")]
#else
[assembly: MetaData("package", Value = "cn.hplzh.blackwhite.app")]
#endif

// Add some common permissions, these can be removed if not needed
