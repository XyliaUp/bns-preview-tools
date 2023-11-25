using System.Diagnostics;

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

		#region definition
		var defs = TableDefinitionHelper.LoadTableDefinition();
		Provider.LoadData(defs);

		var Definitions = new DatafileDefinition(defs);
		#endregion

		#region Table
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
				// set definition
				table.Definition = Definitions[table.Type];
				if (table.Definition is null)
				{
					table.Definition = new TableDefinition() { Type = table.Type, Name = table.Type.ToString() };

					var autoIdAttr = new AttributeDefinition
					{
						Name = "auto-id",
						Size = 8,
						Offset = 8,
						Type = AttributeType.TInt64,
						IsKey = true,
						IsRequired = true,
						Repeat = 1
					};
					table.Definition.ElRecord = new();
					table.Definition.ElRecord.ExpandedAttributes.Add(autoIdAttr);
					table.Definition.ElRecord.Size = 16;
				}

				table.Name = table.Definition.Name;
				table.CheckVersion();
				table.CheckSize((msg) => Debug.WriteLine(msg));
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
	public ModelTable<AccountLevel> AccountLevel => Get<AccountLevel>();
	public ModelTable<AccountPostCharge> AccountPostCharge => Get<AccountPostCharge>();
	public ModelTable<Achievement> Achievement => Get<Achievement>();
	public ModelTable<AttractionGroup> AttractionGroup => Get<AttractionGroup>();
	public ModelTable<BossChallenge> BossChallenge => Get<BossChallenge>();
	public ModelTable<Collecting> Collecting => Get<Collecting>();
	public ModelTable<ChallengeList> ChallengeList => Get<ChallengeList>();
	public ModelTable<ChallengeListReward> ChallengeListReward => Get<ChallengeListReward>();
	public ModelTable<ClosetGroup> ClosetGroup => Get<ClosetGroup>();
	public ModelTable<ContentsReset> ContentsReset => Get<ContentsReset>();
	public ModelTable<ContentQuota> ContentQuota => Get<ContentQuota>();
	public ModelTable<ContextScript> ContextScript => Get<ContextScript>();
	public ModelTable<Cave2> Cave2 => Get<Cave2>();
	public ModelTable<Cave> Cave => Get<Cave>();
	public ModelTable<ClassicFieldZone> ClassicFieldZone => Get<ClassicFieldZone>();
	public ModelTable<CreatureAppearance> CreatureAppearance => Get<CreatureAppearance>();
	public ModelTable<Duel> Duel => Get<Duel>();
	public ModelTable<Dungeon> Dungeon => Get<Dungeon>();
	public ModelTable<Effect> Effect => Get<Effect>();
	public ModelTable<FactionBattleFieldZone> FactionBattleFieldZone => Get<FactionBattleFieldZone>();
	public ModelTable<Faction> Faction => Get<Faction>();
	public ModelTable<FactionLevel> FactionLevel => Get<FactionLevel>();
	public ModelTable<FieldZone> FieldZone => Get<FieldZone>();
	public ModelTable<GoodsIcon> GoodsIcon => Get<GoodsIcon>();
	public ModelTable<GuildBattleFieldZone> GuildBattleFieldZone => Get<GuildBattleFieldZone>();
	public ModelTable<IconTexture> IconTexture => Get<IconTexture>();
	public ModelTable<ItemBrand> ItemBrand => Get<ItemBrand>();
	public ModelTable<ItemBrandTooltip> ItemBrandTooltip => Get<ItemBrandTooltip>();
	public ModelTable<ItemBuyPrice> ItemBuyPrice => Get<ItemBuyPrice>();
	public ModelTable<ItemCombat> ItemCombat => Get<ItemCombat>();
	public ModelTable<Item> Item => Get<Item>();
	public ModelTable<ItemExchange> ItemExchange => Get<ItemExchange>();
	public ModelTable<ItemEvent> ItemEvent => Get<ItemEvent>();
	public ModelTable<ItemGraph> ItemGraph => Get<ItemGraph>();
	public ModelTable<ItemGraphSeedGroup> ItemGraphSeedGroup => Get<ItemGraphSeedGroup>();
	public ModelTable<ItemImprove> ItemImprove => Get<ItemImprove>();
	public ModelTable<ItemImproveOption> ItemImproveOption => Get<ItemImproveOption>();
	public ModelTable<ItemImproveOptionList> ItemImproveOptionList => Get<ItemImproveOptionList>();
	public ModelTable<ItemImproveSuccession> ItemImproveSuccession => Get<ItemImproveSuccession>();
	public ModelTable<ItemRandomAbilitySection> ItemRandomAbilitySection => Get<ItemRandomAbilitySection>();
	public ModelTable<ItemRandomAbilitySlot> ItemRandomAbilitySlot => Get<ItemRandomAbilitySlot>();
	public ModelTable<ItemSkill> ItemSkill => Get<ItemSkill>();
	public ModelTable<ItemSpirit> ItemSpirit => Get<ItemSpirit>();
	public ModelTable<ItemTransformRecipe> ItemTransformRecipe => Get<ItemTransformRecipe>();
	public ModelTable<Job> Job => Get<Job>();
	public ModelTable<JobStyle> JobStyle => Get<JobStyle>();
	public ModelTable<KeyCap> KeyCap => Get<KeyCap>();
	public ModelTable<KeyCommand> KeyCommand => Get<KeyCommand>();
	public ModelTable<LoadingImage> LoadingImage => Get<LoadingImage>();
	public ModelTable<MapInfo> MapInfo => Get<MapInfo>();
	public ModelTable<MapGroup1> MapGroup1 => Get<MapGroup1>();
	public ModelTable<MapGroup1Expedition> MapGroup1Expedition => Get<MapGroup1Expedition>();
	public ModelTable<MapGroup2> MapGroup2 => Get<MapGroup2>();
	public ModelTable<MapUnit> MapUnit => Get<MapUnit>();
	public ModelTable<Npc> Npc => Get<Npc>();
	public ModelTable<NpcResponse> NpcResponse => Get<NpcResponse>();
	public ModelTable<NpcTalkMessage> NpcTalkMessage => Get<NpcTalkMessage>();
	public ModelTable<PartyBattleFieldZone> PartyBattleFieldZone => Get<PartyBattleFieldZone>();
	public ModelTable<Pet> Pet => Get<Pet>();
	public ModelTable<PublicRaid> PublicRaid => Get<PublicRaid>();
	public ModelTable<Quest> Quest => Get<Quest>();
	public ModelTable<QuestBonusReward> QuestBonusReward => Get<QuestBonusReward>();
	public ModelTable<QuestBonusRewardSetting> QuestBonusRewardSetting => Get<QuestBonusRewardSetting>();
	public ModelTable<QuestReward> QuestReward => Get<QuestReward>();
	public ModelTable<QuestSealedDungeonReward> QuestSealedDungeonReward => Get<QuestSealedDungeonReward>();
	public ModelTable<Race> Race => Get<Race>();
	public ModelTable<RaidDungeon> RaidDungeon => Get<RaidDungeon>();
	public ModelTable<RandomStore> RandomStore => Get<RandomStore>();
	public ModelTable<RandomStoreDrawReward> RandomStoreDrawReward => Get<RandomStoreDrawReward>();
	public ModelTable<RandomStoreItem> RandomStoreItem => Get<RandomStoreItem>();
	public ModelTable<RandomStoreItemDisplay> RandomStoreItemDisplay => Get<RandomStoreItemDisplay>();
	public ModelTable<Reward> Reward => Get<Reward>();
	public ModelTable<SetItem> SetItem => Get<SetItem>();
	public ModelTable<Skill> Skill => Get<Skill>();
	public ModelTable<Skill3> Skill3 => Get<Skill3>();
	public ModelTable<SkillByEquipment> SkillByEquipment => Get<SkillByEquipment>();
	public ModelTable<SkillCastCondition3> SkillCastCondition3 => Get<SkillCastCondition3>();
	public ModelTable<SkillGatherRange3> SkillGatherRange3 => Get<SkillGatherRange3>();
	public ModelTable<SkillModifyInfo> SkillModifyInfo => Get<SkillModifyInfo>();
	public ModelTable<SkillModifyInfoGroup> SkillModifyInfoGroup => Get<SkillModifyInfoGroup>();
	public ModelTable<SkillTooltip> SkillTooltip => Get<SkillTooltip>();
	public ModelTable<SkillTooltipAttribute> SkillTooltipAttribute => Get<SkillTooltipAttribute>();
	public ModelTable<SkillTrainCategory> SkillTrainCategory => Get<SkillTrainCategory>();
	public ModelTable<SkillTrainingSequence> SkillTrainingSequence => Get<SkillTrainingSequence>();
	public ModelTable<SkillTrait> SkillTrait => Get<SkillTrait>();
	public ModelTable<SlateScroll> SlateScroll => Get<SlateScroll>();
	public ModelTable<SlateScrollStone> SlateScrollStone => Get<SlateScrollStone>();
	public ModelTable<SlateStone> SlateStone => Get<SlateStone>();
	public ModelTable<Store2> Store2 => Get<Store2>();
	public ModelTable<SummonedSequence> SummonedSequence => Get<SummonedSequence>();
	public ModelTable<SurveyQuestion> SurveyQuestions => Get<SurveyQuestion>();
	public ModelTable<TendencyField> TendencyField => Get<TendencyField>();
	public ModelTable<Text> Text => Get<Text>();
	public ModelTable<TutorialSkillSequence> TutorialSkillSequence => Get<TutorialSkillSequence>();
	public ModelTable<UnlocatedStore> UnlocatedStore => Get<UnlocatedStore>();
	public ModelTable<UnlocatedStoreUi> UnlocatedStoreUi => Get<UnlocatedStoreUi>();
	public ModelTable<VehicleAppearance> VehicleAppearance => Get<VehicleAppearance>();
	public ModelTable<Vehicle> Vehicle => Get<Vehicle>();
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