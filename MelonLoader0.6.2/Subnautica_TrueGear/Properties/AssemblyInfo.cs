using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(Subnautica_TrueGear.BuildInfo.Description)]
[assembly: AssemblyDescription(Subnautica_TrueGear.BuildInfo.Description)]
[assembly: AssemblyCompany(Subnautica_TrueGear.BuildInfo.Company)]
[assembly: AssemblyProduct(Subnautica_TrueGear.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + Subnautica_TrueGear.BuildInfo.Author)]
[assembly: AssemblyTrademark(Subnautica_TrueGear.BuildInfo.Company)]
[assembly: AssemblyVersion(Subnautica_TrueGear.BuildInfo.Version)]
[assembly: AssemblyFileVersion(Subnautica_TrueGear.BuildInfo.Version)]
[assembly: MelonInfo(typeof(Subnautica_TrueGear.Subnautica_TrueGear), Subnautica_TrueGear.BuildInfo.Name, Subnautica_TrueGear.BuildInfo.Version, Subnautica_TrueGear.BuildInfo.Author, Subnautica_TrueGear.BuildInfo.DownloadLink)]
[assembly: MelonColor()]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]