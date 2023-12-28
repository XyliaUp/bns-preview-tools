using System.Xml.Linq;
using CUE4Parse.BNS;
using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.Engine;
using CUE4Parse.UE4.Objects.UObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Tests.Utils;

namespace Xylia.Preview.Tests.PakTests;

[TestClass]
public class SceneTest
{
	[TestMethod]
	public void SceneInformation()
	{
		var Output = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "scene");

		using GameFileProvider Provider = new(IniHelper.Instance.GameFolder);
		var AssetPath = "BNSR/Content/Art/UI/GameUI/Scene/Game_ToolTip/Game_ToolTipScene/Skill3ToolTipPanel_2.uasset";
		var Blueprint = Provider.LoadAllObjects(AssetPath).OfType<UWidgetBlueprintGeneratedClass>().First();

		var dump = new WidgetDump() { Output = Path.Combine(Output, Path.GetFileNameWithoutExtension(AssetPath)) };
		dump.LoadBlueprint(Blueprint);

		Console.WriteLine(dump.Root);
	}

	//[TestMethod]
	public void Create()
	{
		var VfsFolder = "BNSR/Content/Art/UI/GameUI/Scene/";
		var OutputDir = @"F:\Resources\文档\Programming\C#\Xylia\bns\bns-preview-tools\Preview.UI\Art\GameUI\Scene\新建文件夹";

		foreach (var _gamefile in FileCache.Provider.Files)
		{
			var package = _gamefile.Value.Path;
			if (package.Contains(".uasset") && package.StartsWith(VfsFolder, StringComparison.OrdinalIgnoreCase))
			{
				var temp = package.Replace(VfsFolder, null).Split('/');
				if (temp.Length == 2)
				{
					var current = OutputDir + "/" + temp[0];
					Directory.CreateDirectory(current);

					var name = temp[1].Replace(".uasset", null);
					if (name.Contains("_UIPanelList")) continue;
					else if (name.Contains("_Datatable"))
					{
						File.WriteAllText($"{current}/{name}", null);
					}
					else
					{
						//File.WriteAllText($"{current}/{name}.xaml.cs", $$"""
						//	namespace Xylia.Preview.UI.Art.GameUI.Scene.{{temp[0]}};
						//	public partial class {{name}} 
						//	{
						//		public {{name}}()
						//		{
						//	        DataContext = new {{name}}ViewModel();
						//			InitializeComponent();
						//		}
						//	}
						//	""");
					}
				}
			}
		}
	}
}
public class WidgetDump
{
	public string Output { get; set; }

	public bool ExtractImage = false;

	public XElement Root = new XElement("temp");



	public void LoadBlueprint(UWidgetBlueprintGeneratedClass blueprint, int level = 0)
	{
		var bAllowTemplate = blueprint.GetOrDefault<bool>("bAllowTemplate");
		var bValidTemplate = blueprint.GetOrDefault<bool>("bValidTemplate");
		var bClassRequiresNativeTick = blueprint.GetOrDefault<bool>("bClassRequiresNativeTick");
		var DefaultObject = blueprint.ClassDefaultObject;
		var TemplateAsset = blueprint.GetOrDefault<FSoftObjectPath>("TemplateAsset");
		var WidgetTree = blueprint.GetOrDefault<UWidgetTree>("WidgetTree");

		//WidgetTree = TemplateAsset.Load().GetOrDefault<UWidgetTree>("WidgetTree");  
		this.LoadWidget(WidgetTree.RootWidget.Load(), null, level);
	}

	public void LoadWidget(UObject obj, UBnsCustomBaseWidgetSlot widgetslot, int level)
	{
		WriteLine(level++, $"{obj.ExportType}  {obj.Name}");

		if (obj.Template != null)
		{
			LoadBlueprint(obj.Template.Class.Load<UWidgetBlueprintGeneratedClass>(), level);
			return;
		}

		var el = new XElement(obj.ExportType);
		el.Add(new XAttribute("name", obj.Name));
		Root.Add(el);


		if (widgetslot != null)
		{
			WriteLine(level, JsonConvert.SerializeObject(widgetslot.LayoutData));

			el.AddAttribute("AnchorPanel.Anchor", widgetslot.LayoutData.Anchors);
			el.AddAttribute("AnchorPanel.Offset", widgetslot.LayoutData.Offsets);
		}

		if (obj is UBnsCustomBaseWidget widget)
		{
			WriteLineIf(level, widget.MetaData);
			WriteLineIf(level, widget.StringProperty.LabelText?.Text);

			el.AddAttribute("MetaData", widget.MetaData);
			el.AddAttribute("String", widget.StringProperty.LabelText?.Text);

			if (ExtractImage)
			{
				Directory.CreateDirectory(Output);
				widget.BaseImageProperty.Image?.Save(Output + $"/{obj.Name}.png");
				widget.ExpansionComponentList?.ForEach(expansion =>
				{
					expansion.Image?.Save(Output + $"/{obj.Name}.{expansion.ExpansionName}.png");
				});
			}
		}


		// children
		var Slots = obj.GetOrDefault<UBnsCustomBaseWidgetSlot[]>("Slots");
		Slots?.Where(slot => slot != null).ForEach(slot => LoadWidget(slot.Content.Load(), slot, level));
	}


	private static void WriteLine(int level, string message)
	{
		Console.WriteLine(new string('\t', level) + message);
	}

	private static void WriteLineIf(int level, string message)
	{
		if (message != null) WriteLine(level, message);
	}
}