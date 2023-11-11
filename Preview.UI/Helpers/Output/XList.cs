using System.IO;

namespace Xylia.Preview.UI.Helpers.Output;
/// <summary>
/// data list file
/// </summary>
public sealed class XList
{
	#region Fields
	public ushort Version = 1;
	public ushort Type;

	public bool IsCompressed;
	public bool IsEncrypted;

	public long TimeStamp = 0;
	public string Name = "";

	public List<int> datas = new();
	#endregion

	#region Methods
	public void Load(string path)
	{
		if (string.IsNullOrWhiteSpace(path))
			return;

		using var reader = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read));
		var magic = reader.ReadBytes(5);
		if (magic[0] != 'T' || magic[1] != 'S' || magic[2] != 'I' || magic[3] != 'L' || magic[4] != 'X')
		{
			using var rd = File.OpenText(path);

			string line;
			while ((line = rd.ReadLine()) != null)
			{
				if (int.TryParse(line.Replace("\"", ""), out int @int))
					datas.Add(@int);
			}

			return;
		}


		Version = reader.ReadUInt16();
		Type = reader.ReadUInt16();
		IsCompressed = reader.ReadBoolean();
		IsEncrypted = reader.ReadBoolean();
		TimeStamp = reader.ReadInt64();
		Name = reader.ReadString();

		var count = reader.ReadInt32();
		for (int i = 0; i < count; i++)
			datas.Add(reader.ReadInt32());
	}

	public void Save(string path)
	{
		using var writer = new BinaryWriter(new FileStream(path, FileMode.Create));

		writer.Write("TSILX"u8.ToArray());
		writer.Write(Version);
		writer.Write(Type);
		writer.Write(IsCompressed);
		writer.Write(IsEncrypted);
		writer.Write(TimeStamp);
		writer.Write(Name);

		writer.Write(datas.Count);
		foreach (var o in datas) writer.Write(o);
	}

	public static HashSet<int> LoadData(string path)
	{
		XList cache = new();
		cache.Load(path);

		if (!cache.datas.Any()) return null;
		return new HashSet<int>(cache.datas);
	}


	public static HashSet<int> LoadData()
	{
		var dialog = new Microsoft.Win32.OpenFileDialog() { Filter = @"|*.chv|All files|*.*" };
		return dialog.ShowDialog() == true ? LoadData(dialog.FileName) : null;
	}
	#endregion
}