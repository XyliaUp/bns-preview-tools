using Serilog;
using Xylia.Preview.Common.Extension;

namespace Xylia.Preview.Data.Engine.BinData.Models;
public abstract class TableHeader
{
	/// <summary>
	/// name of table
	/// </summary>
	public string Name { get; set; }

	public byte ElementCount { get; set; }

	/// <summary>
	/// Identifier of table
	/// </summary>
	/// <remarks>generated automatically according to the sorting of the table name</remarks>
	public ushort Type { get; set; }

	/// <summary>
	/// major version of table
	/// </summary>
	public ushort MajorVersion { get; set; }

	/// <summary>
	/// minor version of table
	/// </summary>
	public ushort MinorVersion { get; set; }

	public int Size { get; set; }

	public bool IsCompressed { get; set; }


	internal void ReadHeaderFrom(DataArchive reader)
	{
		ElementCount = reader.Read<byte>();
		Type = reader.Read<ushort>();
		MajorVersion = reader.Read<ushort>();
		MinorVersion = reader.Read<ushort>();
		Size = reader.Read<int>();
		IsCompressed = reader.Read<bool>();
	}

	internal void WriteHeaderTo(BinaryWriter writer)
	{
		writer.Write(ElementCount);
		writer.Write(Type);
		writer.Write(MajorVersion);
		writer.Write(MinorVersion);
	}


	/// <summary>
	/// parse text version
	/// </summary>
	/// <returns></returns>
	public static (ushort, ushort) ParseVersion(string value)
	{
		var version = value.Split('.');
		var major = (ushort)version.ElementAtOrDefault(0).ToInt16();
		var minor = (ushort)version.ElementAtOrDefault(1).ToInt16();

		return (major, minor);
	}

	/// <summary>
	/// compare config version with game real version
	/// </summary>
	internal void CheckVersion((ushort, ushort) version)
	{
		if (this.MajorVersion == 0 && this.MinorVersion == 0)
		{
			MajorVersion = version.Item1;
			MinorVersion = version.Item2;
		}
		else if (this.MajorVersion != version.Item1 || this.MinorVersion != version.Item2)
		{
			Log.Warning($"check table `{this.Name}` version: {version.Item1}.{version.Item2} <> {this.MajorVersion}.{this.MinorVersion}");

			// non binary table
			if (this.Size == 0)
			{
				this.MajorVersion = version.Item1;
				this.MinorVersion = version.Item2;
			}
		}
	}
}