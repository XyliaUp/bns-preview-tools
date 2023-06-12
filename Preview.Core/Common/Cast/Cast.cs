using System;

using Xylia.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Common.Cast;

public static class Cast
{
	#region Seq
	public static T CastSeq<T>(this string value) where T : Enum => value.ToEnum<T>();

	public static object CastSeq(this string value, string name)
	{
		if (!name.TryParseToEnum<SeqType>(out var SeqType)) return null;
		else if (SeqType == SeqType.KeyCap) return KeyCap.Cast(KeyCap.GetKeyCode(value));
		else if (SeqType == SeqType.KeyCommand) return KeyCommand.Cast(CastSeq<KeyCommandSeq>(value));

		throw new InvalidCastException($"Cast Failed: {name} > {value}");
	}
	#endregion


	#region Object
	public static ITable CastTable(this string DataTableName)
	{
		if (string.IsNullOrWhiteSpace(DataTableName)) return default;
		if (DataTableName.MyEquals("skill")) return FileCache.Data.Skill3;
		if (DataTableName.MyEquals("tooltip")) return FileCache.Data.TextData;

		return (ITable)FileCache.Data.GetValue(DataTableName, true);
	}


	public static BaseRecord CastObject(this string ObjInfo)
	{
		if (string.IsNullOrWhiteSpace(ObjInfo)) return default;
		if (ObjInfo.Contains(':'))
		{
			var tmp = ObjInfo.Split(':');

			var obj = CastObject(tmp[1]?.Trim(), tmp[0]?.Trim());
			if (obj != null) return obj;
		}

		System.Diagnostics.Debug.WriteLine($"Cast Failed: {ObjInfo}");
		return default;
	}

	public static BaseRecord CastObject(this string DataKey, string DataTableName)
		=> (BaseRecord)DataTableName.CastTable()[DataKey];
	#endregion
}