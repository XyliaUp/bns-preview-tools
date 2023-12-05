using System.Reflection;

using Xylia.Extension;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Common.Cast;
public static partial class RecordExtension
{
	public static string GetName(this object Object)
	{
		if (Object is Enum value)
		{
			if (value == default) return null;
			else if (value is EquipType EquipType) return EquipType.GetName();
			else if (value is ConditionType ConditionType) return ConditionType.GetName();
			else if (value is RaceSeq RaceSeq) return Race.Get(RaceSeq).GetName();
			else if (value is SexSeq SexSeq) return ((Item.SexSeq2)SexSeq).GetName();
			else if (value is Item.SexSeq2 SexSeq2) return SexSeq2.GetName();
		}

		return Object.GetAttribute<Name>()?.Description ?? (Object is MemberInfo m ? m.Name : Object.ToString());
	}

	public static T Get<T>(this T[] array, int num) => array.Length < num ? default : array[num - 1];
}