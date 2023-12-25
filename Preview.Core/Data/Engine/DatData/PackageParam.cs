using System.Security.Cryptography;

namespace Xylia.Preview.Data.Engine.DatData;
public class PackageParam(string path, bool? bit64 = null)
{
	public string FolderPath { get; set; }

	public string PackagePath { get; set; } = path;

	public bool Bit64 { get; set; } = bit64 ?? path.Judge64Bit();

	public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Normal;

	public byte[] AES_KEY { get; set; } = PackageKey.AES_2020_05;

	public byte[] XOR_KEY { get; set; } = PackageKey.XOR_KEY_2021;

	public RSAParameters RSA_KEY { get; set; } = PackageKey.RSA3;
}

public enum CompressionLevel
{
	Fastest,
	Fast,
	Normal,
	Maximum
}