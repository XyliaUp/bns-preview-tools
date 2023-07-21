using Xylia.Extension;

namespace Xylia.Preview.Data.Models.Definition;
public class XList
{
    public ushort Version = 1;
    public ushort Type;

    public bool IsCompressed;
    public bool IsEncrypted;

    public long DataTime = 0;

    public string Title = "";

    public List<int> datas = new();


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
        DataTime = reader.ReadInt64();
        Title = reader.ReadString();

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
        writer.Write(DataTime);
        writer.Write(Title);

        writer.Write(datas.Count);
        foreach (var o in datas) writer.Write(o);
    }



    public static HashSet<int> LoadData()
    {
        OpenFileDialog fileDialog = new()
        {
            Filter = "|*.chv",
            RestoreDirectory = false
        };

        if (fileDialog.ShowDialog() != DialogResult.OK) return null;

        return LoadData(fileDialog.FileName);
    }

    public static HashSet<int> LoadData(string path, Action<string> action = null)
    {
        if (string.IsNullOrWhiteSpace(path)) return null;

        XList cache = new();
        cache.Load(path);

        string Msg = null;
        if (!string.IsNullOrWhiteSpace(cache.Title)) Msg += $"目前读取的是「{cache.Title}」";
        if (cache.DataTime != 0) Msg += $" 版本{cache.DataTime.GetDateTime()}";
        if (!string.IsNullOrEmpty(Msg)) action?.Invoke(Msg);


        if (!cache.datas.Any()) return null;

        var vs = new HashSet<int>();
        foreach (var item in cache.datas)
            vs.Add(item);

        action?.Invoke($"共读取到{vs.Count}个历史版本道具, 将会自动跳过");
        return vs;
    }
}