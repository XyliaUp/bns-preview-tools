using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public sealed class Npc : ModelElement
{
	#region Fields
	public string Alias { get; set; }

	public Ref<Text> Name2 { get; set; }

	public Ref<Text> Title2 { get; set; }

	public Ref<Store2>[] Store2 { get; set; }

	public Ref<CreatureAppearance> Appearance { get; set; }

	public ObjectPath Animset { get; set; }
	#endregion


	#region Public Properties
	public string Text => this.Attributes["name2"].GetText();

	public string Map
	{
		get
		{
			var MapUnit = FileCache.Data.Get<MapUnit>().Where(x => x.ToString() != null && x.ToString().Contains(this.Alias, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
			return MapUnit is null ? null : FileCache.Data.Get<MapInfo>()[MapUnit.Mapid]?.Text;
		}
	}
	#endregion
}