using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public sealed class Npc : Record
{
	#region Fields
	public string Alias;

	public Ref<Text> Name2;

	public Ref<Text> Title2;

	[Repeat(6)]
	public Ref<Store2>[] Store2;

	public Ref<CreatureAppearance> Appearance;

	public ObjectPath Animset;
	#endregion


	#region Public Properties
	public override string GetText => Name2.GetText();

	public string Map
	{
		get
		{
			var MapUnit = FileCache.Data.MapUnit.Where(x => x.Alias != null && x.Alias.Contains(this.Alias, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
			return MapUnit is null ? null : FileCache.Data.MapInfo[MapUnit.Mapid]?.GetText;
		}
	}
	#endregion
}