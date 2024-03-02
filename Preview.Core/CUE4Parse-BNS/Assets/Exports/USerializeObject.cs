using System.Reflection;

using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Assets.Utils;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.UE4.Assets.Exports;
public abstract class USerializeObject : UObject
{
	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);

		StructFallbackUtil.ObjectMapper.Map(this, this);
	}


	static USerializeObject()
	{
		StructFallbackUtil.ObjectMapper = new DefaultObjectMapper();
	}

	public class DefaultObjectMapper : ObjectMapper
	{
		public override void Map(IPropertyHolder src, object dst)
		{
			var props = src.Properties
				.ToLookup(property => property.Name.Text)
				.ToDictionary(property => property.Key, x => x.First(), StringComparer.OrdinalIgnoreCase);

			foreach (var member in dst.GetType().GetMembers(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
			{
				if (src is UObject && member.GetCustomAttribute<UPROPERTY>() is null) continue;
				if (!props.TryGetValue(member.Name, out var property)) continue;

				if (member is FieldInfo item)
				{
					object obj = property.Tag?.GetValue(item.FieldType);
					item.SetValue(dst, obj);
				}
				else if (member is PropertyInfo prop && prop.CanWrite)
				{
					object obj = property.Tag?.GetValue(prop.PropertyType);
					prop.SetValue(dst, obj);
				}
			}
		}
	}
}