using System.Reflection;

using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Interface;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Common.Extension;

public static partial class RecordExtension
{
	public static bool TryGetParam<T>(this T Object, string ParamName, out object Value)
	{
		if (ParamName == Object.GetType().Name)
		{
			Value = Object;
			return true;
		}

		//返回实例数值
		var Member = Object.GetInfo(ParamName, true);
		if (Member != null)
		{
			var obj = Member.GetValue(Object);
			Value = obj is Text text ? text.GetText() : obj;
			return true;
		}

		//
		if (Object is BaseRecord record)
		{
			Value = record.Attributes[ParamName];
			return true;
		}

		Value = null;
		return false;
	}


	public static object GetParam<T>(this T Object, string ParamName) => Object.TryGetParam(ParamName, out object Value) ? Value : null;

	public static string GetSignal(this object EnumItem) => EnumItem.GetAttribute<Signal>()?.Description ?? (EnumItem is MemberInfo m ? m.Name : EnumItem.ToString());




	#region GetName
	public static string GetName(this string ObjInfo) => ObjInfo.CastObject()?.GetName() ?? ObjInfo;

	public static string GetName(this BaseRecord Obj)
	{
		if (Obj is null) return null;
		else if (Obj is IName IName) return IName.GetName();
		else if (Obj is Attraction attraction) return attraction.GetName();

		return Obj.GetParam("Name2")?.ToString()
			?? Obj.GetParam("Name")?.ToString()
			?? Obj.alias;
	}
	#endregion
}