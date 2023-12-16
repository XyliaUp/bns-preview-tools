using System.Data;
using System.Text.RegularExpressions;

namespace Xylia.Preview.Data.Engine.DatData.Third;
public static class MySpport
{
	public static byte[] RSA2 => BnsCompression.GetRSAKeyBlob(Convert.FromBase64String("AQAB"),
			Convert.FromBase64String("6frEEJqRXEuy/ttKNKxRZZdvqAgeSi0yDwMzMu4lZhtq4/sbojbQH2zkcsEUz6PJ7Ab9Zty2EuBDO1ZJoYN2Y0i1Pvi+avGGJbwTuHuPag352hxHwVPbBXZ//koxlL4J1J9FQKtEWHBCRkDM7UYVBkCQb5I6k9fEtyJejrzdmgk="),
			Convert.FromBase64String("8mJvLTFUS3w15GT+//Ok7xSZlO2SRtypmhcCrtAFUGDbmjmIT9Wg8Ll353yDGzFxZIVmiMblgdMrRnc1d7pf6Q=="),
			Convert.FromBase64String("9x93N77SSk7vsZdzuS9eutc+zMKxk5fBYqndgK6gm8+mSfKWBm4CCKVWXNeuhIYtsgSOBeix5nYvjkymVpw1IQ=="));

	public static byte[] RSA3 => BnsCompression.GetRSAKeyBlob(Convert.FromBase64String("AQAB"),
			 Convert.FromBase64String("4n/9xPwCpn2/TGXY0bCc23xXKdU9iobCl2RCMWTDgz17uh+Jl8W+Jvci+apyTyXDYdQH8nh2SKkUpAYsQy8bA9v8k+ZbYDytp/DAcHKBfY/1ccknJQrWStbzxQwRXSGsmWmY0vwCW2K7iTkWGQbxo0qRG/L10/qDXQLxf7bmyE8="),
			 Convert.FromBase64String("6fXfeI2dhsT173QVhtOpYLqEQazrsf9opL8cps8j6XH5AzGpRDh0PePoXzhWTZA36nbyEJY0yqDrrBVBEQwRnQ=="),
			 Convert.FromBase64String("99Y0G0QkA62/hFBFmg5fI4vdsesCYGMZw+QwhdSJW87Z5fTZ8r8PamYzNudQPeiJhgQgAVjpFBG7K6Um6JRj2w=="));

	public class PackParam
	{
		public string FolderPath;

		public string PackagePath;

		public bool? Bit64;

		public byte[] Aes = KeyInfo.AES_2020_05;

		public BnsCompression.CompressionLevel CompressionLevel = BnsCompression.CompressionLevel.Normal;
	}



	public static void Extract(PackParam param)
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

		Parallel.ForEach(new BNSDat(param.PackagePath, param.Bit64).FileTable, file =>
		{
			string path = Path.Combine(param.FolderPath, file.FilePath);
			Directory.CreateDirectory(Path.GetDirectoryName(path));

			File.WriteAllBytes(path, file.Data);
		});

		Console.WriteLine("Extract completed");
	}

	public static void Pack(PackParam param, IReadOnlyDictionary<string, byte[]> replaces = null, bool IgnoreExist = false)
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
		if (replaces != null)
		{
			var FilePath = new BNSDat(param.PackagePath, param.Bit64).FileTable.Select(Fte => Fte.FilePath).ToList();
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

				if (Target.Count == 0) continue;
				#endregion

				Target.ForEach(x => File.WriteAllBytes(param.FolderPath + "\\" + x, replace.Value));
			}
		}
		#endregion


		#region Execute
		var rsa = RSA3;
		double value = BnsCompression.CreateFromDirectory(param.FolderPath, param.PackagePath, param.Bit64 ?? true, param.CompressionLevel,
			param.Aes, (uint)param.Aes.Length, rsa, (uint)rsa.Length,
			BnsCompression.BinaryXmlVersion.Version4,
			(string fileName, ulong fileSize) => BnsCompression.DelegateResult.Continue);
		#endregion
	}
}