namespace Xylia.Preview.Tests.DatTool.Utils.DevTools;
public class CommonConvert
{
	readonly byte[] data;

	public CommonConvert(long value) : this(BitConverter.GetBytes(value)) { }

    public CommonConvert(int value) : this(BitConverter.GetBytes(value)) { }

    public CommonConvert(byte[] value) => data = value;



    public int Length => data.Length;

	public int? Int32 => data.Length >= 4 ? BitConverter.ToInt32(data, 0) : null;

    public long? Long => data.Length >= 8 ? BitConverter.ToInt64(data, 0) : null;

    public short? Short1 => data.Length >= 2 ? BitConverter.ToInt16(data, 0) : null;

    public short? Short2 => data.Length >= 4 ? BitConverter.ToInt16(data, 2) : null;

    public float? Float => data.Length >= 4 ? BitConverter.ToSingle(data, 0) : null;
}
