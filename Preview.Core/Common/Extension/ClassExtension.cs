using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Common.Extension;
public static partial class ClassExtension
{
    public const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

	#region MemberInfo
	public static MemberInfo GetMember<T>(this T Case, object Name, bool IgnoreCase = false)
    {
        #region init
        if (Name is null) return null;
        var _Name = Name.ToString();

        BindingFlags MyFlags = Flags;
        if (IgnoreCase) MyFlags |= BindingFlags.IgnoreCase;    //判断是否需要忽略大小写

        var type = Case.GetType();
        #endregion

        var Members = type.GetMember(_Name.Replace("-", null), MyFlags);
        if (Members.Length == 0) Members = type.GetMember(_Name.Replace("-", "_"), MyFlags);

        return Members.Where(m => m is FieldInfo || m is PropertyInfo).FirstOrDefault();
    }

    public static Type GetMemberType(this MemberInfo MemberInfo)
    {
        if (MemberInfo is PropertyInfo property) return property.PropertyType;
        else if (MemberInfo is FieldInfo field) return field.FieldType;
        else throw new InvalidDataException();
    }

	public static string GetMemberName(this MemberInfo member)
	{
		return member.GetAttribute<NameAttribute>()?.Name ?? member.Name;
	}


	public static object GetValue<T>(this T Case, object Name, bool IgnoreCase = false) => Case.GetMember(Name, IgnoreCase)?.GetValue(Case);

    public static object GetValue(this MemberInfo member, object obj = null)
    {
        if (member is FieldInfo field) return field.GetValue(obj);
        else if (member is PropertyInfo property) return property.GetValue(obj, null);
        else throw new InvalidDataException();
    }

    public static void SetValue<T>(this MemberInfo member, T Case, object Value)
    {
        if (member is PropertyInfo property) property.SetValue(Case, Value);
        else if (member is FieldInfo field) field.SetValue(Case, Value);
        else throw new InvalidDataException();
    }
	#endregion

	#region Attribute
	public static T GetAttribute<T>(this object value) where T : Attribute
	{
		switch (value)
		{
			case null:
				return null;
			case Enum e:
				return value.GetType().GetField(e.ToString()).GetAttribute<T>();
			case MemberInfo m:
				var attributes = Attribute.GetCustomAttributes(m, typeof(T), false);
				if (attributes.Length == 0) return default;

				return (T)attributes[0];

			default:
				var t = value.GetType();
				return t.GetCustomAttribute<T>();
		}
	}

	public static bool ContainAttribute<T>(this object EnumItem) where T : Attribute => EnumItem.ContainAttribute(out T _);

	public static bool ContainAttribute<T>(this object EnumItemm, out T Target) where T : Attribute => (Target = EnumItemm.GetAttribute<T>()) != null;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetDescription(this object EnumItem, bool ReturnNull = false)
		=> EnumItem.GetAttribute<DescriptionAttribute>()?.Description ?? (ReturnNull ? null : EnumItem.ToString());
	#endregion
}