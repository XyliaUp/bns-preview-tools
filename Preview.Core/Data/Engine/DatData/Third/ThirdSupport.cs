using System.Data;
using System.Text.RegularExpressions;
using Xylia.Preview.Data.Engine.DatData.Third;

namespace Xylia.Preview.Data.Engine.DatData;
public static class ThirdSupport
{
	public static void Extract(PackageParam param)
	{
		#region Initialize
		if (string.IsNullOrWhiteSpace(param.FolderPath) && string.IsNullOrWhiteSpace(param.PackagePath))
		{
			throw new ArgumentException("invalid path");
		}
		else if (string.IsNullOrWhiteSpace(param.FolderPath))
		{
			param.FolderPath = Path.GetDirectoryName(param.PackagePath) + @"\Export\" + Path.GetFileNameWithoutExtension(param.PackagePath);
		}
		#endregion

		Parallel.ForEach(new BNSDat(param).FileTable, file =>
		{
			string path = Path.Combine(param.FolderPath, file.FilePath);
			Directory.CreateDirectory(Path.GetDirectoryName(path));

			File.WriteAllBytes(path, file.Data);
		});

		Console.WriteLine("Extract completed");
	}

	public static void Pack(PackageParam param, IReadOnlyDictionary<string, byte[]> replaces = null, bool IgnoreExist = false)
	{
		#region Initialize
		if (string.IsNullOrWhiteSpace(param.FolderPath) && string.IsNullOrWhiteSpace(param.PackagePath))
		{
			throw new ArgumentException("invalid path");
		}
		else if (string.IsNullOrWhiteSpace(param.PackagePath))
		{
			var dir = new DirectoryInfo(param.FolderPath);
			param.PackagePath = dir.Parent.FullName + "\\" + dir.Name + ".dat";
		}
		else if (string.IsNullOrWhiteSpace(param.FolderPath))
		{
			param.FolderPath = Path.GetDirectoryName(param.PackagePath) + @"\Export\" + Path.GetFileNameWithoutExtension(param.PackagePath);
		}
		#endregion

		#region Replace
		if (!Directory.Exists(param.FolderPath))
		{
			Extract(param);
		}

		if (replaces != null)
		{
			var FilePath = new BNSDat(param).FileTable.Select(Fte => Fte.FilePath).ToList();
			foreach (var replace in replaces)
			{
				#region target
				var Target = new List<string>();
				if (FilePath.Contains(replace.Key)) Target.Add(replace.Key);
				else if (replace.Key.Contains('*') || replace.Key.Contains('?'))
				{
					Target.AddRange(FilePath.Where(f => new Regex(replace.Key, RegexOptions.IgnoreCase).Match(f).Success));
				}
				else if (IgnoreExist) Target.Add(replace.Key);
				#endregion

				Target.ForEach(x => File.WriteAllBytes(param.FolderPath + "\\" + x, replace.Value));
			}
		}
		#endregion


		#region Execute
		var rsa = BnsCompression.GetRSAKeyBlob(param.RSA_KEY);
		double value = BnsCompression.CreateFromDirectory(param.FolderPath, param.PackagePath, param.Bit64, param.CompressionLevel,
			param.AES_KEY, (uint)param.AES_KEY.Length, rsa, (uint)rsa.Length,
			BnsCompression.BinaryXmlVersion.Version4, (string fileName, ulong fileSize) => BnsCompression.DelegateResult.Continue);
		#endregion
	}
}