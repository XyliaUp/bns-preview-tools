namespace Xylia.Preview.Tests.DatTool.Windows.DevTools;

public class BytesInfo
{
    public int Index;

    public List<byte> Bytes = new();

    public void Add(byte Byte, int Index = 0)
    {
        if (!Any()) this.Index = Index;
        Bytes.Add(Byte);
    }

    public void Clear() => Bytes.Clear();

    public bool Any() => Bytes.Any();

    public byte[] Data => Bytes.ToArray();
}
