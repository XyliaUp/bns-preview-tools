using System.Diagnostics;

using CUE4Parse.BNS;
using CUE4Parse.BNS.Conversion;
using CUE4Parse.BNS.Exports;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports.SkeletalMesh;
using CUE4Parse.UE4.Assets.Exports.Sound;
using CUE4Parse.UE4.Assets.Exports.StaticMesh;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Objects.Core.Math;

using CUE4Parse_Conversion;
using CUE4Parse_Conversion.Sounds;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using Xylia.Configure;

namespace Xylia.Preview.Tests;

[TestClass]
public class PakTests
{
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

		PakData PakData = new();
		foreach (var o in PakData.Provider.LoadPackage(AssetPath).GetExports())
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

	//[TestMethod]
	public void Test()
	{
		using PakData pak = new(@"C:\腾讯游戏\Blade_and_Soul");

		var obj = pak.Provider.LoadObject(@"BNSR/Content/Art/Character/Monster/Beast/JunoTropicalBird/Mesh/JunoTropicalBird.JunoTropicalBird");
		obj = pak.Provider.LoadObject(@"BNSR/Content/bns/Package/World/GameDesign/commonpackage/ShowData/jeryoung/indi_jr_epic/q_303_m1_indi_1_voice_ShowObject.q_303_m1_indi_1_voice_ShowObject");

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


			case USoundWave:
			{
				obj.Decode(true, out var audioFormat, out var data);
			}
			break;


			case UStaticMesh:
			case USkeletalMesh:
			{
				var toSave = new Exporter(obj, new ExporterOptions()
				{

				});
				var success = toSave.TryWriteToDir(new DirectoryInfo(PathDefine.Desktop), out var label, out var savedFilePath);
			}
			break;
		}
	}
}