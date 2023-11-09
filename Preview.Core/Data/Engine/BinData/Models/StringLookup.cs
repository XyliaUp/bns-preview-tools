using System.Runtime.CompilerServices;
using System.Text;

using Newtonsoft.Json;

using Xylia.Preview.Data.Common;
using Xylia.Preview.Data.Engine.BinData.Helpers;

namespace Xylia.Preview.Data.Engine.BinData.Models;

[JsonConverter(typeof(StringLookupConverter))]
public class StringLookup
{
	public static StringLookup Empty => new StringLookup { Data = new byte[2] };

	public byte[] Data { get; set; }
	public bool IsPerTable { get; set; }

	public void ReadFrom(BinaryReader reader, int size)
	{
		Data = reader.ReadBytes(size);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public string GetString(int offset)
	{
		if (offset >= 0 && offset < Data.Length)
			return Data.GetNStringUTF16(offset);

		return null;
	}

	public int AppendString(string str)
	{
		str ??= "";

		var strBytes = Encoding.Unicode.GetBytes(str + "\0");

		if (Data == null)
			throw new InvalidOperationException("Attempted to append string on null string lookup data");

		var position = Data.Length;
		var data = Data;
		Array.Resize(ref data, Data.Length + strBytes.Length);
		Array.Copy(strBytes, 0, data, position, strBytes.Length);
		Data = data;

		return position;
	}

	public StringLookup Duplicate()
	{
		var data = new byte[Data.Length];
		Array.Copy(Data, data, data.Length);

		return new StringLookup
		{
			Data = data,
			IsPerTable = IsPerTable
		};
	}
}