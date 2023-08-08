using CUE4Parse.BNS.Exports;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports.SkeletalMesh;
using CUE4Parse.UE4.Assets.Exports.Sound;
using CUE4Parse.UE4.Assets.Exports.StaticMesh;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse.Utils;

using CUE4Parse_Conversion;
using CUE4Parse_Conversion.Sounds;

using Newtonsoft.Json;

using Xylia.Extension;
using Xylia.Preview.Properties;

namespace CUE4Parse.BNS.Conversion;
public static class Common
{
	private static string FixPath(string path)
	{
		var fullPath = Path.Combine(CommonPath.OutputFolder_Resource, path);
		Directory.CreateDirectory(fullPath.SubstringBeforeLast('/'));

		return fullPath;
	}


	public static void Output(IPackage package, bool ContainType = true)
	{
		var objs = package.GetExports();
		if (objs.Count() == 1)
		{
			Output(objs.First(), ContainType);
		}
		else
		{
			File.WriteAllText($"{FixPath(package.Name)}.json", JsonConvert.SerializeObject(objs, Formatting.Indented));
		}

#if (DEBUG)
		objs.Where(o => o.GetType() == typeof(UObject))
			.ForEach(o => Debug.WriteLine("not supported class: " + o.Class.GetFullName()));
#endif
	}

	public static void Output(UObject obj, bool ContainType = true)
	{
		var name = FixPath(obj.GetPathName()).SubstringBeforeLast('.');
		if (ContainType) name += $".{obj.ExportType}";

		switch (obj)
		{
			case USoundWave:
			{
				obj.Decode(true, out var audioFormat, out var data);
			}
			break;

			case UStaticMesh:
			case USkeletalMesh:
			//case UAnimSequence:
			{
				new Exporter(obj, new ExporterOptions())
					.TryWriteToDir(new DirectoryInfo(CommonPath.OutputFolder_Resource), out var label, out var savedFilePath);
			}
			break;

			case UImageSet ImageSet:
				ImageSet.GetImage()?.Save($"{name}.png");
				break;

			default:
			{
				if (obj is UTexture) obj.GetImage()?.Save($"{name}.png");
				else
				{
					if (!ContainType) name += $".{obj.ExportType}";
					File.WriteAllText($"{name}", JsonConvert.SerializeObject(obj, Formatting.Indented));
				}
			}
			break;
		}
	}
}