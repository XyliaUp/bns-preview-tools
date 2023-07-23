using CUE4Parse.BNS.Conversion;
using CUE4Parse.BNS.Exports;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports.SkeletalMesh;
using CUE4Parse.UE4.Assets.Exports.Sound;
using CUE4Parse.UE4.Assets.Exports.StaticMesh;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse.Utils;

using CUE4Parse_Conversion;
using CUE4Parse_Conversion.Sounds;

using Xylia.Preview.Properties;

namespace Xylia.Preview.CUE4Parse_BNS.Conversion;
public static class Common
{
	public static void Output(UObject obj, bool ContainType = true)
	{
		var name = obj.Name + (ContainType ? "." + obj.ExportType : null);

		var directory = Path.Combine(CommonPath.OutputFolder_Resource, obj.GetPathName().SubstringBeforeLast('/'));
		Directory.CreateDirectory(directory);


		switch (obj)
		{
			case UTexture2D:
			case UImageSet:
				obj.GetImage()?.Save(directory + $"\\{name}.png");
				break;

			case USoundWave:
			{
				obj.Decode(true, out var audioFormat, out var data);
			}
			break;

			case UStaticMesh:
			case USkeletalMesh:
			//case UAnimSequence:
			{
				var toSave = new Exporter(obj, new ExporterOptions()
				{
					    
				});
				var success = toSave.TryWriteToDir(new DirectoryInfo(CommonPath.OutputFolder_Resource), out var label, out var savedFilePath);
			}
			break;
		}
	}
}
