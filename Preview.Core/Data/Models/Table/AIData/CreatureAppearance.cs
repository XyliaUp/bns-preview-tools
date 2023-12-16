using System.Diagnostics;
using Xylia.Preview.Common.Extension;

namespace Xylia.Preview.Data.Models;
public sealed class CreatureAppearance : ModelElement
{
															
}

public class Param8
{
	#region Constructor
	const int SIZE = 92;
	public sbyte[] Data;

	public Param8(sbyte[] data) => this.Data = data;
	public Param8(string data) => this.Data = data.ToBytes().Select(b => (sbyte)b).ToArray();
	#endregion


	public static bool operator ==(Param8 a, Param8 b)
	{
		if (SIZE != a.Data.Length || SIZE != b.Data.Length)
			throw new InvalidDataException();

		var flag = true;
		for (int i = 0; i < SIZE; i++)
		{
			if (a.Data[i] != b.Data[i])
			{
				flag = false;
				Trace.WriteLine($"param-{i + 1} ({a.Data[i]} <> {b.Data[i]})");
			}
		}

		return flag;
	}
	public static bool operator !=(Param8 a, Param8 b) => !(a == b);

	public override bool Equals(object other) => other is Param8 param8 && this == param8;
	public override int GetHashCode() => Data.GetHashCode();

	public static implicit operator Param8(sbyte[] data) => new(data);

	public static implicit operator Param8(string data) => new(data);
}