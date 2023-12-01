using System.Reflection;

using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Assets.Utils;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.UE4.Assets.Exports;
public class USerializeObject : UObject
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
			var prop = src.Properties
				.ToLookup(property => property.Name.Text)
				.ToDictionary(property => property.Key, x => x.First());

			foreach (FieldInfo item in dst.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
			{
				if (src is UObject && item.GetCustomAttribute<UPROPERTY>() is null) continue;
				if (!prop.TryGetValue(item.Name, out var property)) continue;

				//Debug.WriteLine(item.FieldType);
				object obj = property.Tag?.GetValue(item.FieldType);
				item.SetValue(dst, obj);
			}
		}
	}
}