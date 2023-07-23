using System.Reflection;
using System.Xml.Linq;

using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Interface;
using Xylia.Preview.Data.Models.BinData.Table.Record.Attributes;
using Xylia.Preview.Data.Record;
using Xylia.Xml;

namespace Xylia.Preview.Common.Extension;
public static partial class RecordExtension
{
	public static object GetParam<T>(this T Object, string ParamName) => Object.TryGetParam(ParamName, out object Value) ? Value : null;

	public static bool TryGetParam<T>(this T Object, string ParamName, out object Value)
	{
		if (ParamName == Object.GetType().Name)
		{
			Value = Object;
			return true;
		}

		// instance
		var Member = Object.GetInfo(ParamName, true);
		if (Member != null)
		{
			var obj = Member.GetValue(Object);
			Value = obj is Text text ? text.GetText() : obj;
			return true;
		}

		// record
		if (Object is BaseRecord record)
		{
			Value = record.Attributes[ParamName, convert: true];
			return true;
		}

		Value = null;
		return false;
	}


	public static string GetSignal(this object EnumItem) => EnumItem.GetAttribute<Signal>()?.Description ?? (EnumItem is MemberInfo m ? m.Name : EnumItem.ToString());

	public static bool INVALID(this BaseRecord record)
	{
		if (record is null) return true;

		if (record.ContainAttribute<AliasRecord>())
			return string.IsNullOrWhiteSpace(record.alias);

		return false;
	}


	#region GetName
	public static string GetName(this string ObjInfo) => ObjInfo.CastObject()?.GetName() ?? ObjInfo;

	public static string GetName(this BaseRecord Obj)
	{
		if (Obj is null) return null;
		else if (Obj is IName IName) return IName.GetName();
		else if (Obj is IAttraction attraction) return attraction.GetName();

		return Obj.GetParam("Name2")?.ToString()
			?? Obj.GetParam("Name")?.ToString()
			?? Obj.alias;
	}
	#endregion


	public static T CreateNew<T>(params IAttribute[] attrs) where T : BaseRecord, new()
	{
		var xe = new XElement("record");
		attrs.ForEach(attr => xe.SetAttributeValue(attr.Key, attr.Value));

		var item = new T();
		item.LoadData(new XElementData(xe.LinqTo()));
		return item;
	}
}