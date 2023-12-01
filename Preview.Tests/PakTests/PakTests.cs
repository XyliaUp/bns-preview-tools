using System.Diagnostics;

using CUE4Parse.BNS;
using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.BNS.Pak;
using CUE4Parse.Compression;
using CUE4Parse.UE4.Assets.Exports.BuildData;
using CUE4Parse.UE4.Assets.Exports.Texture;

using CUE4Parse_Conversion.Textures;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using Xylia.Preview.Properties;

namespace Xylia.Preview.Tests.PakTests;

[TestClass]
public class Common
{
	internal static Settings Settings => new();

	[TestMethod]
	public void ObjectTest()
	{
		using GameFileProvider Provider = new(Settings.GameFolder);

		//var obj = Provider.LoadObject(@"BNSR/Content/Art/FX/01_Source/05_SF/FXUI_03/Particle/UI_BIMUTag_EscapeNo.UI_BIMUTag_EscapeNo");
		var obj = Provider.LoadObject(@"BNSR/Content/bns/Package/World/GameDesign/commonpackage/ShowData/indun/soc_etc_all_insdungeun/ME_ChungGakABoss_0005_soc_voice.ME_ChungGakABoss_0005_soc_voice");
		switch (obj)
		{
			case UTexture2D texture:
				var bitmap = texture.Decode(ETexturePlatform.DesktopMobile);
				break;

			case UShowObject ShowObject:
			{
				foreach (var a in ShowObject.EventKeys)
				{
					Trace.WriteLine(JsonConvert.SerializeObject(a.Load(), Formatting.Indented));
				}
			}
			break;

			case UBnsParticleSystem ParticleSystem:
			{
				//Trace.WriteLine(JsonConvert.SerializeObject(emitter, Formatting.Indented));
			}
			break;
		}
	}

	public void MapTest(string name)
	{
		using GameFileProvider Provider = new(Settings.GameFolder);
		var MapRegistry = Provider.LoadObject<UMapBuildDataRegistry>($"/Game/bns/Package/World/Area/{name}_BuiltData");

		throw new Exception(MapRegistry.ToString());
	}


	[TestMethod]
	public void RepackTest()
	{
		var outDir = @"D:\资源\Paks";

		var pak = new MyPakFileReader(@"BNSR\Content");
		//pak.Add(@"F:\NewTest\WindowsNoEditor\TestPack\Content\MyTest\intro2_texture.uasset", @"local\tencent\ChineseS\package\Art\UI\GameUI\Resource\GameUI_Loading\intro2_texture.uasset");
		//pak.Add(@"F:\NewTest\WindowsNoEditor\TestPack\Content\MyTest\intro2_texture.uexp", @"local\tencent\ChineseS\package\Art\UI\GameUI\Resource\GameUI_Loading\intro2_texture.uexp", CompressionMethod.Oodle);
		pak.Add(@"D:\资源\lisence.txt", null, CompressionMethod.Oodle);
		pak.Add(@"D:\资源\233.txt", null, CompressionMethod.None);
		pak.WriteToDir(outDir);

#if true
		var provider = new GameFileProvider(outDir);
		if (provider.TrySaveAsset(pak.MountPoint + @"\lisence.txt", out var data))
			Debug.WriteLine("data: " + BitConverter.ToString(data));
#endif
	}
}