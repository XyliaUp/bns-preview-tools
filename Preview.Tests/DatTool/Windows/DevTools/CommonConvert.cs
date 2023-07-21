namespace Xylia.Preview.Tests.DatTool.Windows.DevTools;

public class CommonConvert
{
    public CommonConvert(long LongVal) : this(BitConverter.GetBytes(LongVal)) { }

    public CommonConvert(int Int32Val) : this(BitConverter.GetBytes(Int32Val)) { }

    public CommonConvert(byte[] data) => Data = data;


    public readonly byte[] Data;


    public int? Int32 => Data.Length >= 4 ? BitConverter.ToInt32(Data, 0) : null;

    public long? Long => Data.Length >= 8 ? BitConverter.ToInt64(Data, 0) : null;

    public short? Short1 => Data.Length >= 2 ? BitConverter.ToInt16(Data, 0) : null;

    public short? Short2 => Data.Length >= 4 ? BitConverter.ToInt16(Data, 2) : null;

    public float? Float => Data.Length >= 4 ? BitConverter.ToSingle(Data, 0) : null;
}
