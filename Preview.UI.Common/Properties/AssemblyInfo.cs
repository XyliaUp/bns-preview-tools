using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(
	ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
									 //(used if a resource is not found in the page,
									 // or application resource dictionaries)
	ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
											  //(used if a resource is not found in the page,
											  // app, or any theme specific resource dictionaries)
)]

[assembly: InternalsVisibleTo("Xylia.Preview.UI")]
[assembly: XmlnsPrefix("https://github.com/xyliaup/bns-preview-tools", "Preview")]
[assembly: XmlnsDefinition("https://github.com/xyliaup/bns-preview-tools", "Xylia.Preview.UI")]
[assembly: XmlnsDefinition("https://github.com/xyliaup/bns-preview-tools", "Xylia.Preview.UI.Controls")]
[assembly: XmlnsDefinition("https://github.com/xyliaup/bns-preview-tools", "Xylia.Preview.UI.Converters")]
[assembly: XmlnsDefinition("https://github.com/xyliaup/bns-preview-tools", "Xylia.Preview.UI.Documents")]
[assembly: XmlnsDefinition("https://github.com/xyliaup/bns-preview-tools", "Xylia.Preview.UI.Extensions")]