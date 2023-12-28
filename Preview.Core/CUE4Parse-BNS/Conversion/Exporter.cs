using System.Diagnostics;
using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports.SkeletalMesh;
using CUE4Parse.UE4.Assets.Exports.Sound;
using CUE4Parse.UE4.Assets.Exports.StaticMesh;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse.Utils;
using CUE4Parse_Conversion;
using CUE4Parse_Conversion.Sounds;
using CUE4Parse_Conversion.Textures;
using Newtonsoft.Json;
using Xylia.Preview.Common.Extension;

namespace CUE4Parse.BNS.Conversion;
public class Exporter
{
	public string Folder = null;

	public Exporter(string OutputFolder)
	{
		Folder = OutputFolder;
	}

	public static string FixPath(string saveFolder, string path)
	{
		var fullPath = Path.Combine(saveFolder, path.SubstringBeforeLast('.'));
		Directory.CreateDirectory(fullPath.SubstringBeforeLast('/'));

		return fullPath;
	}

	public void Run(IPackage package, bool ContainType = true)
	{
		var objs = package.GetExports();
		if (objs.Count() == 1)
		{
			Run(objs.First(), ContainType);
		}
		else
		{
			File.WriteAllText($"{FixPath(Folder, package.Name)}.json", JsonConvert.SerializeObject(objs, Formatting.Indented));
		}

#if DEBUG
		objs.Where(o => o.GetType() == typeof(UObject))
			.ForEach(o => Debug.WriteLine("not supported class: " + o.Class.GetFullName()));
#endif
	}

	public void Run(UObject obj, bool ContainType = true)
	{
		var name = FixPath(Folder, obj.GetPathName());
		if (ContainType) name += $".{obj.ExportType}";

		switch (obj)
		{
			case USoundWave:
			{
				obj.Decode(true, out var audioFormat, out var data);
				File.WriteAllBytes($"{name}", data);
			}
			break;

			case UStaticMesh:
			case USkeletalMesh:
			//case UAnimSequence:
			{
				new CUE4Parse_Conversion.Exporter(obj, new ExporterOptions())
					.TryWriteToDir(new DirectoryInfo(Folder), out var label, out var savedFilePath);
			}
			break;

			case UImageSet ImageSet:
				ImageSet.GetImage()?.Save($"{name}.png");
				break;
			case UTexture texture:
				texture.Decode()?.Save($"{name}.png");
				break;

			default:
			{
				// keep class name in the case
				if (!ContainType) name += $".{obj.ExportType}";
				File.WriteAllText($"{name}", JsonConvert.SerializeObject(obj, Formatting.Indented));
				break;
			}
		}
	}
}