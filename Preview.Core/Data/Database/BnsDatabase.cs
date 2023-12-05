using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Properties;

namespace Xylia.Preview.Data;
public class BnsDatabase : IDisposable
{
	#region Ctor
	public readonly IDataProvider Provider;

	/// <summary>
	/// starts database
	/// </summary>
	public BnsDatabase(IDataProvider provider = null)
	{
		this.Provider = provider ?? DefaultProvider.Load(Settings.Default.GameFolder);
		ArgumentNullException.ThrowIfNull(this.Provider);


		#region Definition
		var defs = TableDefinitionHelper.LoadTableDefinition();
		Provider.LoadData(defs);

		// Bind definitions to tables
		var Definitions = new DatafileDefinition(defs);
		foreach (var table in Provider.Tables)
		{
			table.Owner = this;

			// represents from xml
			if (table.Type == 0)
			{
				ArgumentException.ThrowIfNullOrEmpty(table.Name);
				table.Definition = Definitions[table.Name];
			}
			else
			{
				table.Definition = Definitions[table.Type];
				table.Name = table.Definition.Name;
			}
		}
		#endregion
	}
	#endregion


	#region Collections
	readonly Dictionary<Table, object> _tables = new();

	public ModelTable<T> Get<T>(string name = null) where T : Record => Get<T>(Provider.Tables[name ?? typeof(T).Name]);

	public ModelTable<T> Get<T>(Table table) where T : Record
	{
		if (table is null) return null;

		lock (_tables)
		{
			if (!_tables.TryGetValue(table, out var Models))
				 _tables[table] = Models = ModelTypeHelper.As<T>(table);

			return Models as ModelTable<T>;
		}
	}
	#endregion

	#region Tables
	public ModelTable<Achievement> Achievement => Get<Achievement>();
	public ModelTable<BossChallenge> BossChallenge => Get<BossChallenge>();
	public ModelTable<ClosetGroup> ClosetGroup => Get<ClosetGroup>();
	public ModelTable<ContextScript> ContextScript => Get<ContextScript>();
	public ModelTable<Cave2> Cave2 => Get<Cave2>();
	public ModelTable<Cave> Cave => Get<Cave>();
	public ModelTable<ClassicFieldZone> ClassicFieldZone => Get<ClassicFieldZone>();
	public ModelTable<Duel> Duel => Get<Duel>();
	public ModelTable<Dungeon> Dungeon => Get<Dungeon>();
	public ModelTable<FactionBattleFieldZone> FactionBattleFieldZone => Get<FactionBattleFieldZone>();
	public ModelTable<FieldZone> FieldZone => Get<FieldZone>();
	public ModelTable<GuildBattleFieldZone> GuildBattleFieldZone => Get<GuildBattleFieldZone>();
	public ModelTable<IconTexture> IconTexture => Get<IconTexture>();
	public ModelTable<ItemBrandTooltip> ItemBrandTooltip => Get<ItemBrandTooltip>();
	public ModelTable<ItemBuyPrice> ItemBuyPrice => Get<ItemBuyPrice>();
	public ModelTable<Item> Item => Get<Item>();
	public ModelTable<ItemGraph> ItemGraph => Get<ItemGraph>();
	public ModelTable<ItemImproveSuccession> ItemImproveSuccession => Get<ItemImproveSuccession>();
	public ModelTable<ItemTransformRecipe> ItemTransformRecipe => Get<ItemTransformRecipe>();
	public ModelTable<Job> Job => Get<Job>();
	public ModelTable<JobStyle> JobStyle => Get<JobStyle>();
	public ModelTable<KeyCap> KeyCap => Get<KeyCap>();
	public ModelTable<KeyCommand> KeyCommand => Get<KeyCommand>();
	public ModelTable<MapInfo> MapInfo => Get<MapInfo>();
	public ModelTable<MapUnit> MapUnit => Get<MapUnit>();
	public ModelTable<Npc> Npc => Get<Npc>();
	public ModelTable<PartyBattleFieldZone> PartyBattleFieldZone => Get<PartyBattleFieldZone>();

	public ModelTable<PublicRaid> PublicRaid => Get<PublicRaid>();
	public ModelTable<Quest> Quest => Get<Quest>();
	public ModelTable<Race> Race => Get<Race>();
	public ModelTable<RaidDungeon> RaidDungeon => Get<RaidDungeon>();

	public ModelTable<Skill3> Skill3 => Get<Skill3>();
	public ModelTable<Store2> Store2 => Get<Store2>();
	public ModelTable<Text> Text => Get<Text>();
	public ModelTable<TendencyField> TendencyField => Get<TendencyField>();
	public ModelTable<TutorialSkillSequence> TutorialSkillSequence => Get<TutorialSkillSequence>();
	public ModelTable<UnlocatedStore> UnlocatedStore => Get<UnlocatedStore>();
	public ModelTable<UnlocatedStoreUi> UnlocatedStoreUi => Get<UnlocatedStoreUi>();
	#endregion


	#region Interface
	public void Dispose()
	{
		Provider.Dispose();

		GC.SuppressFinalize(this);
		GC.Collect();
	}


	public static implicit operator BnsDatabase(DefaultProvider provider) => new(provider);

	public static implicit operator BnsDatabase(FolderProvider provider) => new(provider);
	#endregion
}