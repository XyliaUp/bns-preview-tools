using System.Reflection;
using System.Windows.Markup;

[assembly: AssemblyTitle("Blade & Soul Preview Tool")]
[assembly: AssemblyProduct("Xylia.Preview")]
[assembly: AssemblyCompany("Xylia")]
[assembly: AssemblyCopyright("©2018-2023 Xylia, all rights reserved.")]
[assembly: AssemblyVersion("2.16.*")]

[assembly: ThemeInfo(
	ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
									 //(used if a resource is not found in the page,
									 // or application resource dictionaries)
	ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
											  //(used if a resource is not found in the page,
											  // app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsDefinition("https://github.com/xyliaup/bns-preview-tools", "Xylia.Preview.UI")]
[assembly: XmlnsDefinition("https://github.com/xyliaup/bns-preview-tools", "Xylia.Preview.UI.Resources")]
[assembly: XmlnsDefinition("https://github.com/xyliaup/bns-preview-tools", "Xylia.Preview.UI.Common.Controls")]
[assembly: XmlnsDefinition("https://github.com/xyliaup/bns-preview-tools", "Xylia.Preview.UI.Common.Converters")]
[assembly: XmlnsPrefix("https://github.com/xyliaup/bns-preview-tools", "UI")]