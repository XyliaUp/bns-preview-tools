using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Engine.BinData.Helpers;
public sealed class HashList
{
	#region Constructors
	public HashList(string path)
	{
		this.Load(path);
	}

	public HashList(IEnumerable<Ref> refs)
	{
		HashMap = new HashSet<Ref>(refs);
	}
	#endregion

	#region Fields
	public ushort Version = 2;
	public ushort Type;

	public bool IsCompressed;
	public bool IsEncrypted;

	public long TimeStamp = 0;
	public string Name = "";

	public HashSet<Ref> HashMap { private set; get; }
	#endregion


	#region Methods
	public void Load(string path)
	{
		if (string.IsNullOrWhiteSpace(path))
			return;

		using var reader = new DataArchive(File.ReadAllBytes(path));
		var Magic = reader.ReadBytes(5);

		Version = reader.Read<UInt16>();
		Type = reader.Read<UInt16>();
		IsCompressed = reader.Read<Boolean>();
		IsEncrypted = reader.Read<Boolean>();
		TimeStamp = reader.Read<Int64>();
		Name = reader.ReadString();

		var datas = new Ref[reader.Read<int>()];
		for (int i = 0; i < datas.Length; i++)
		{
			if (Version < 2) datas[i] = new Ref(reader.Read<int>(), 1);
			else datas[i] = reader.Read<Ref>();
		}

		// create hash map
		HashMap = new HashSet<Ref>(datas);
	}

	public void Save(string path)
	{
		using var writer = new DataArchiveWriter();

		writer.Write("TSILX"u8.ToArray());
		writer.Write(Version);
		writer.Write(Type);
		writer.Write(IsCompressed);
		writer.Write(IsEncrypted);
		writer.Write(TimeStamp);
		writer.WriteString(Name);

		writer.Write(HashMap.Count);
		foreach (var o in HashMap) writer.Write(o);

		writer.Flush();
		File.WriteAllBytes(path, writer.ToArray());
	}

	/// <summary>
	/// valid data key
	/// </summary>
	/// <param name="key"></param>
	/// <param name="mode">is WhiteList</param>
	/// <returns></returns>
	public bool CheckFailed(Ref key, bool mode = false)
	{
		if (mode) return HashMap is null || !HashMap.Contains(key);
		else return HashMap != null && HashMap.Contains(key);
	}
	#endregion
}