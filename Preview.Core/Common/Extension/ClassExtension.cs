using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Xylia.Preview.Common.Extension;
public static partial class ClassExtension
{
	#region PropertyInfo
	public static PropertyInfo GetProperty<T>(this T instance, string name)
    {
		var MyFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.IgnoreCase;

		var type = instance.GetType();
		var Members = type.GetProperty(name.Replace("-", null), MyFlags);
        Members ??= type.GetProperty(name.Replace("-", "_"), MyFlags);

        return Members;
    }
	#endregion

	#region Attribute
	public static T GetAttribute<T>(this object value) where T : System.Attribute
	{
		switch (value)
		{
			case null:
				return null;
			case Enum e:
				return value.GetType().GetField(e.ToString()).GetAttribute<T>();
			case MemberInfo m:
				var attributes = System.Attribute.GetCustomAttributes(m, typeof(T), false);
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


	public static bool IsNullOrDefault<T>(this T argument)
	{
		// deal with normal scenarios
		if (argument == null) return true;
		if (object.Equals(argument, default(T))) return true;

		// deal with non-null nullables
		Type methodType = typeof(T);
		if (Nullable.GetUnderlyingType(methodType) != null) return false;

		// deal with boxed value types
		Type argumentType = argument.GetType();
		if (argumentType.IsValueType && argumentType != methodType)
		{
			object obj = Activator.CreateInstance(argument.GetType());
			return obj.Equals(argument);
		}

		return false;
	}
}