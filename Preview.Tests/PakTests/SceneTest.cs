using System.Xml.Linq;
using CUE4Parse.BNS;
using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.Engine;
using CUE4Parse.UE4.Objects.UObject;
using CUE4Parse.UE4.Pak;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
		IPlatformFilePak.Signature = new byte[20];

		var Output = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "scene");

		using GameFileProvider Provider = new(IniHelper.Instance.GameFolder);
		var AssetPath = "BNSR/Content/Art/UI/GameUI/Scene/Game_ToolTip/Game_ToolTipScene/ItemTooltipPanel.uasset";
		var Blueprint = Provider.LoadAllObjects(AssetPath).OfType<UWidgetBlueprintGeneratedClass>().First();

		var dump = new WidgetDump() { Output = Path.Combine(Output, Path.GetFileNameWithoutExtension(AssetPath)) };
		dump.LoadBlueprint(Blueprint);

		Console.WriteLine(dump.Root);
	}

	//[TestMethod]
	public void Create()
	{
		var VfsFolder = "BNSR/Content/Art/UI/GameUI/Scene/";
		var OutFolder = @"F:\Resources\文档\Programming\C#\Xylia\bns\bns-preview-tools\Preview.UI\Art\GameUI\Scene\新建文件夹";

		foreach (var _gamefile in FileCache.Provider.Files)
		{
			var package = _gamefile.Value.Path;
			if (package.Contains(".uasset") && package.StartsWith(VfsFolder, StringComparison.OrdinalIgnoreCase))
			{
				var temp = package.Replace(VfsFolder, null).Split('/');
				if (temp.Length == 2)
				{
					var current = OutFolder + "/" + temp[0];
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

	public XElement Root = new("temp");



	public void LoadBlueprint(UWidgetBlueprintGeneratedClass blueprint, int level = 0, XElement parent = null)
	{
		var bAllowTemplate = blueprint.GetOrDefault<bool>("bAllowTemplate");
		var bValidTemplate = blueprint.GetOrDefault<bool>("bValidTemplate");
		var bClassRequiresNativeTick = blueprint.GetOrDefault<bool>("bClassRequiresNativeTick");
		var DefaultObject = blueprint.ClassDefaultObject;
		var TemplateAsset = blueprint.GetOrDefault<FSoftObjectPath>("TemplateAsset");
		var WidgetTree = blueprint.GetOrDefault<UWidgetTree>("WidgetTree");
		//WidgetTree = TemplateAsset.Load().GetOrDefault<UWidgetTree>("WidgetTree");  
		this.LoadWidget(WidgetTree.RootWidget.Load(), null, level, Root);

		// load widget which not on visual tree
		if (blueprint.Name.StartsWith("Legacy"))
		{
			//Console.WriteLine(JsonConvert.SerializeObject(blueprint, Formatting.Indented));

			parent ??= Root;
			foreach (var widget in blueprint.Owner.GetExports().Where(x => x.Outer == WidgetTree))
			{
				LoadWidget(widget, null, level, parent);
			}
		}
	}

	public void LoadWidget(UObject obj, UBnsCustomBaseWidgetSlot widgetslot, int level, XElement parent)
	{
		if (obj.Template != null)
		{
			LoadBlueprint(obj.Template.Class.Load<UWidgetBlueprintGeneratedClass>(), level, parent);
			return;
		}

		var el = new XElement(obj.ExportType);
		el.Add(new XAttribute("Name", obj.Name));
		parent.Add(el);

		if (widgetslot != null)
		{
			el.AddAttribute("Anchor.Anchors", widgetslot.LayoutData.Anchors);
			el.AddAttribute("Anchor.Offsets", widgetslot.LayoutData.Offsets);
			el.AddAttribute("Anchor.Alignments", widgetslot.LayoutData.Alignments);
		}

		if (obj is UBnsCustomBaseWidget widget)
		{
			el.AddAttribute("MetaData", widget.MetaData);
			el.AddAttribute("String", widget.StringProperty.LabelText?.Text);

			if (widget.BaseImageProperty != null)
			{
				var image = widget.BaseImageProperty!.Value;
				el.AddAttribute("BaseImageProperty", image.BaseImageTexture.ResolvedObject.GetPathName());
				el.AddAttribute("ImageUV", image.ImageUV);
				el.AddAttribute("ImageUVSize", image.ImageUVSize);
			}

			if (ExtractImage)
			{
				Directory.CreateDirectory(Output);
				widget.BaseImageProperty?.Image?.Save(Output + $"/{obj.Name}.png");
				widget.ExpansionComponentList?.ForEach(expansion => expansion.Image?.Save(Output + $"/{obj.Name}.{expansion.ExpansionName}.png"));
			}
		}


		// children
		var Slots = obj.GetOrDefault<UBnsCustomBaseWidgetSlot[]>("Slots");
		Slots?.Where(slot => slot != null).ForEach(slot => LoadWidget(slot.Content.Load(), slot, level, el));
	}


	private static void WriteLine(int level, string message)
	{
		if (message is null) return;

		Console.WriteLine(new string('\t', level) + message);
	}
}