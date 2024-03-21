using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public sealed class Npc : ModelElement
{
	#region Properties
	public string Text => this.Attributes["name2"].GetText();

	public string Map
	{
		get
		{
			var alias = this.ToString();

			var MapUnit = FileCache.Data.Provider.GetTable<MapUnit>().Where(x => x.ToString() != null && x.ToString().Contains(alias, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
			return MapUnit is null ? null : FileCache.Data.Provider.GetTable<MapInfo>().FirstOrDefault(x => x.Id == MapUnit.Mapid)?.Text;
		}
	}
	#endregion

	#region Methods
	protected internal override void LoadHiddenField()
	{
		string alias = this.Attributes.Get<string>("alias");
		var comparer = StringComparison.OrdinalIgnoreCase;

		if (this.Attributes["brain"] is null)
		{
			string BrainInfo;
			if (this.Attributes["boss-npc"] != null) BrainInfo = "Boss";
			else if (alias.StartsWith("CH_", comparer) || alias.StartsWith("CE_", comparer)) BrainInfo = "Citizen";
			else if (alias.StartsWith("MH_", comparer) || alias.StartsWith("ME_", comparer)) BrainInfo = "Monster";
			else return;

			this.Attributes["brain"] = BrainInfo;
			this.Attributes["brain-parameters"] = alias + "_bp";
		}

		if (this.Attributes["formal-radius"] is null)
		{
			if (this.Attributes["radius"] is not null)
				this.Attributes["formal-radius"] = this.Attributes["radius"];
		}
	}
	#endregion


	// Baekcheong
	// Chicken_A
	// Daesamak
	// Dungeon
	// Jeryongrim
	// Pizza_A
	// Summer_A
	// Suweol
	// Winter_A
}