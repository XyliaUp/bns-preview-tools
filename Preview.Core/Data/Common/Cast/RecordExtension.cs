using System.Reflection;

using Xylia.Extension;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.Cast;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Interface;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Common.Cast;
public static partial class RecordExtension
{
	#region GetParam
	internal static object GetParam<T>(this T Object, string ParamName) => Object.TryGetParam(ParamName, out object Value) ? Value : null;

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
			Value = obj is Ref<Text> text ? text.GetText() : obj;
			return true;
		}

		// record
		if (Object is Record record)
		{
			Value = record.Attributes[ParamName];
			return true;
		}

		Value = null;
		return false;
	}

	public static string GetName(this object Object) => Object.GetAttribute<Name>()?.Description ?? (Object is MemberInfo m ? m.Name : Object.ToString());

	public static T Get<T>(this T[] array, int num) => array.Length < num ? default : array[num - 1];
	#endregion

	#region GetName
	public static string GetName(this Record record)
	{
		if (record is null) return null;
		else if (record is IName IName) return IName.Text;

		var name =
			record.Attributes["name2"]?.ToString() ??
			record.Attributes["name"]?.ToString();

		if (record.GetType() != typeof(Record)) return record.ToString();

		return name;
	}

	/// <summary>
	/// temp
	/// </summary>
	/// <param name="record"></param>
	/// <returns></returns>
	public static string GetAttraction(this Record record)
	{
		var tooltip = $"{record.GetType().Name}: " + record.GetName();
		if (record is IAttraction attraction) tooltip += "\n" + attraction.GetDescribe();

		return tooltip;
	}
	#endregion
}