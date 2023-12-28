using System.Diagnostics;

using CUE4Parse.BNS;
using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.Compression;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports.BuildData;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse.UE4.Pak;
using CUE4Parse.UE4.Writers;
using CUE4Parse_Conversion.Textures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xylia.Preview.Tests.Utils;

namespace Xylia.Preview.Tests.PakTests;

[TestClass]
public partial class AssetExport
{
	[TestMethod]
	public void ObjectTest()
	{
		using GameFileProvider Provider = new(IniHelper.Instance.GameFolder);

		var package = Provider.LoadPackage(@"BNSR\Content\Art\FX\05_BM\EquipShow/SS_EquipShow_Wolf") as Package;

		var obj = package.GetExport("SS_EquipShow_Wolf");
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





		using var writer = new FArchiveWriter();
		package.Serialize(writer);

		writer.Flush();

		var path = @"F:\Resources\文档\Programming\C#\FModel2\Output\Exports\BNSR\Content\Art\FX\05_BM\EquipShow\a.uasset";
		File.WriteAllBytes(path , writer.GetBuffer());
	}

	[TestMethod]
	public void MapTest(string name)
	{
		using GameFileProvider Provider = new(IniHelper.Instance.GameFolder);
		var MapRegistry = Provider.LoadObject<UMapBuildDataRegistry>($"/Game/bns/Package/World/Area/{name}_BuiltData");

		throw new Exception(MapRegistry.ToString());
	}


	[TestMethod]
	[DataRow(@"C:\腾讯游戏\Blade_and_Soul\BNSR\Content\Paks")]
	public void RepackTest(string OutDir)
	{
		var pak = new MyPakFileReader(@"BNSR\Content");
		//pak.Add(@"F:\NewTest\WindowsNoEditor\TestPack\Content\MyTest\intro2_texture.uasset", @"local\tencent\ChineseS\package\Art\UI\GameUI\Resource\GameUI_Loading\intro2_texture.uasset");
		//pak.Add(@"F:\NewTest\WindowsNoEditor\TestPack\Content\MyTest\intro2_texture.uexp", @"local\tencent\ChineseS\package\Art\UI\GameUI\Resource\GameUI_Loading\intro2_texture.uexp", CompressionMethod.Oodle);
		pak.Add(@"C:\腾讯游戏\Blade_and_Soul\BNSR\Content\local\Tencent\data\xml64.dat", "local/Tencent/data/xml64.dat", CompressionMethod.None);
		pak.Add(@"C:\腾讯游戏\Blade_and_Soul\BNSR\Content\local\Tencent\data\config64.dat", "local/Tencent/data/config64.dat", CompressionMethod.None);

		pak.WriteToDir(OutDir , "Xylia_P.pak");

#if true
		var provider = new GameFileProvider(OutDir);
		if (provider.TrySaveAsset(pak.MountPoint + @"\lisence.txt", out var data))
			Console.WriteLine("data: " + BitConverter.ToString(data));
#endif
	}
}