using System.Diagnostics;

using CUE4Parse.BNS;
using CUE4Parse.BNS.Conversion;
using CUE4Parse.BNS.Exports;
using CUE4Parse.BNS.Objects.Script;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.Engine;
using CUE4Parse.UE4.Objects.UObject;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using Xylia.Configure;

namespace Xylia.Preview.Tests;

[TestClass]
public class PakTests
{
	//[TestMethod]
	public void Test()
	{
		using GameFileProvider Provider = new();

		UObject obj = null;
		obj = Provider.LoadObject(@"BNSR/Content/bns/Package/World/GameDesign/commonpackage/ShowData/Heaven_Epic_Voice/q_1972_3_indi_1_voice");

		switch (obj)
		{
			case UTexture2D texture:
			{
				//var bitmap = texture.Decode(ETexturePlatform.DesktopMobile);
			}
			break;

			case UShowObject ShowObject:
			{
				foreach (var a in ShowObject.EventKeys)
				{
					Trace.WriteLine(JsonConvert.SerializeObject(a.Load(), Formatting.Indented));
				}
			}
			break;
		}
	}


	//[TestMethod]
	public void Test2()
	{
		using GameFileProvider Provider = new();

		var Blueprint = Provider.LoadObject<UWidgetBlueprintGeneratedClass>(@"BNSR/Content/Art/UI/GameUI/Scene/Game_Battle/Game_BattleScene/NovaSkillBar.NovaSkillBar_C");


		var WidgetTree = Blueprint.GetOrDefault<ResolvedObject>("WidgetTree").Load() as UWidgetTree;
		var TemplateAsset = Blueprint.GetOrDefault<FSoftObjectPath>("TemplateAsset");
		var DefaultObject = Blueprint.ClassDefaultObject;


		//LoadWidget(WidgetTree);
		LoadWidget(TemplateAsset.Load().GetOrDefault<ResolvedObject>("WidgetTree").Load() as UWidgetTree);


		//UMaterialInstanceConstant
		//UMaterial



		//var data = Provider.LoadObject(@"BNSR/Content/Art/UI/GameUI/Resource/GameUI_Window/GameUI_New_Scene_00.GameUI_New_Scene_00")?.GetImage();
		//data?.Clone(new Rectangle((int)649, (int)639, (int)25, (int)27), data.PixelFormat).Save($@"C:\Users\10565\Desktop\test53.png");
	}

	public static void LoadWidget(UWidgetTree tree)
	{
		Trace.WriteLine(tree.GetFullName());
		LoadWidget(tree.RootWidget.Load() as UWidget);
	}

	public static void LoadWidget(UObject widget, UBnsCustomBaseWidgetSlot widgetslot = null)
	{
		Trace.WriteLine($"\t{widget.ExportType}  {widget.Name}");
		if(widgetslot != null) Trace.WriteLine($"\t\t{JsonConvert.SerializeObject(widgetslot.LayoutData)}");


		{
			var Slot = (UBnsCustomBaseWidgetSlot)widget.GetOrDefault<ResolvedObject>("Slot")?.Load();
			//if (Slot != null) Trace.WriteLine(JsonConvert.SerializeObject(Slot.LayoutData, Formatting.Indented));
		}

		foreach (var Slot in widget.GetOrDefault("Slots", Array.Empty<ResolvedObject>()))
		{
			var slot = (UBnsCustomBaseWidgetSlot)Slot.Load();
			var content = slot.Content.Load();

			LoadWidget(content, slot);
		}


		if (widget.GetOrDefault<ResolvedObject>("WidgetTree")?.Load() is UWidgetTree WidgetTree)
		{
			LoadWidget(WidgetTree);
		}
	}




	//[TestMethod]
	public void GetScene()
	{
		string OutDir = Path.Combine(PathDefine.Desktop, "scene");

		var AssetPath = "BNSR/Content/Art/UI/GameUI/Scene/Game_ToolTip/Game_ToolTipScene.uasset";
		void Output(string Name, FStructFallback ImageProperty)
		{
			if (ImageProperty is null) return;

			var BaseImageTexture = ImageProperty.GetOrDefault<ResolvedObject>("BaseImageTexture");
			var ImageUV = ImageProperty.GetOrDefault<FVector2D>("ImageUV");
			var ImageUVSize = ImageProperty.GetOrDefault<FVector2D>("ImageUVSize");

			string DirPath = Path.Combine(OutDir, Path.GetFileNameWithoutExtension(AssetPath));
			Directory.CreateDirectory(DirPath);

			var data = BaseImageTexture.Load()?.GetImage();
			data?.Clone(new Rectangle((int)ImageUV.X, (int)ImageUV.Y, (int)ImageUVSize.X, (int)ImageUVSize.Y), data.PixelFormat)
				.Save(DirPath + $"\\{Name}.png");
		}

		GameFileProvider Provider = new();
		foreach (var o in Provider.LoadPackage(AssetPath).GetExports())
		{
			if (o.TryGetValue(out FStructFallback BaseImageProperty, "BaseImageProperty")) Output(o.Name, BaseImageProperty);

			else if (o.TryGetValue(out FStructFallback NormalImageProperty, "NormalImageProperty")) Output(o.Name, NormalImageProperty);

			else if (o.TryGetValue(out UScriptArray ExpansionComponentList, "ExpansionComponentList"))
			{
				foreach (var p in ExpansionComponentList.Properties)
				{
					if (p is StructProperty s && s.Value.StructType is FStructFallback fallback)
					{
						Output(o.Name, fallback.GetOrDefault<FStructFallback>("ImageProperty"));
					}
				}
			}
		}
	}
}