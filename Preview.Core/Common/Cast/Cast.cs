using Xylia.Extension;
using Xylia.Extension.Class;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Common.Cast;
public static class Cast
{
	#region Seq
	public static object CastSeq(this string value, string name)
	{
		// register enum class

		if (!name.TryParseToEnum<SeqType>(out var SeqType)) return null;
		else if (SeqType == SeqType.KeyCap) return KeyCap.Cast(KeyCap.GetKeyCode(value));
		else if (SeqType == SeqType.KeyCommand) return KeyCommand.Cast(value.ToEnum<KeyCommandSeq>());

		throw new InvalidCastException($"Cast Failed: {name} > {value}");
	}
	#endregion



	#region Object
	public static ITable CastTable(this string TableName, TableSet set = null)
	{
		set ??= FileCache.Data;

		if (string.IsNullOrWhiteSpace(TableName)) return default;
		if (TableName.Equals("skill", StringComparison.OrdinalIgnoreCase)) return set.Skill3;

		return (ITable)set.GetValue(TableName, true);
	}

	public static BaseRecord CastObject(this string DataInfo)
	{
		if (string.IsNullOrWhiteSpace(DataInfo)) return default;
		if (DataInfo.Contains(':'))
		{
			var tmp = DataInfo.Split(':');

			var obj = CastObject<BaseRecord>(tmp[1]?.Trim(), tmp[0]?.Trim());
			if (obj != null) return obj;
		}

		Debug.WriteLine($"Cast Failed: {DataInfo}");
		return default;
	}

	public static T CastObject<T>(this string DataKey, string TableName = null) where T : BaseRecord
	{
		if (string.IsNullOrWhiteSpace(DataKey)) return default;

		if (TableName is null && typeof(T) != typeof(BaseRecord))
			TableName = typeof(T).Name;

		return TableName.CastTable()?[DataKey] as T;
	}
	#endregion
}