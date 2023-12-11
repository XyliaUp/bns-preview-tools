using System.Runtime.CompilerServices;
using System.Text;

using Newtonsoft.Json;

using Xylia.Preview.Data.Common;
using Xylia.Preview.Data.Engine.BinData.Helpers;

namespace Xylia.Preview.Data.Engine.BinData.Models;

[JsonConverter(typeof(StringLookupConverter))]
public class StringLookup
{
	public byte[] Data { get; set; } = [];
	public bool IsPerTable { get; set; }


	#region Methods
	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public string GetString(int offset)
	{
		if (offset >= 0 && offset < Data.Length)
			return Data.GetNStringUTF16(offset);

		return null;
	}

	public string[] Strings
	{
		get => Encoding.Unicode.GetString(Data).Split('\0');
		set
		{
			StringBuilder _stringBuilder = new();
			foreach (var s in value)
			{
				_stringBuilder.Append(s);
				_stringBuilder.Append('\0');
			}

			Data = Encoding.Unicode.GetBytes(_stringBuilder.ToString());
		}
	}

	public int AppendString(string str, out int size)
	{
		ArgumentNullException.ThrowIfNull(Data, nameof(Data));

		str ??= "";
		var position = Data.Length;
		var data = Data;

		var strBytes = Encoding.Unicode.GetBytes(str + "\0");
		Array.Resize(ref data, Data.Length + strBytes.Length);
		Array.Copy(strBytes, 0, data, position, strBytes.Length);
		Data = data;

		size = strBytes.Length;
		return position;
	}
	#endregion
}