using Xylia.Extension;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Common.Cast;
public static class Cast
{
	#region Seq
	public static object CastSeq(this string value, string name)
	{
		if (!name.TryParseToEnum<SeqType>(out var SeqType)) return null;
		else if (SeqType == SeqType.KeyCap) return KeyCap.Cast(KeyCap.GetKeyCode(value));
		else if (SeqType == SeqType.KeyCommand) return KeyCommand.Cast(value.ToEnum<KeyCommandSeq>());

		throw new InvalidCastException($"Cast Failed: {name} > {value}");
	}
	#endregion

	#region Object
	public static T CastObject<T>(this string alias, string table = null, BnsDatabase data = null) where T : Record
	{
		if (string.IsNullOrWhiteSpace(alias)) 
			return default;

		return (data ?? FileCache.Data).Get<T>(table)?[alias];
	}
	#endregion
}