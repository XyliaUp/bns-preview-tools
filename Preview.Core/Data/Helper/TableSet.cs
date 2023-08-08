using BnsBinTool.Core.Definitions;

using Xylia.Extension.Class;
using Xylia.Preview.Data.Models.BinData;
using Xylia.Preview.Data.Models.BinData.AliasTable;
using Xylia.Preview.Data.Models.BinData.Table;
using Xylia.Preview.Data.Models.DatData;
using Xylia.Preview.Data.Models.DatData.DataProvider;
using Xylia.Preview.Data.Models.Definition;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Properties;

using TableModel = BnsBinTool.Core.Models.Table;

namespace Xylia.Preview.Data.Helper;
public class TableSet : IDisposable
{
	#region Data
	public IDataProvider Provider;

	public DateTime CreatedAt;

	public TableModel[] Tables;
	#endregion

	#region Tables
	public Table<AccountLevel> AccountLevel { get; } = new();
	public Table<AccountPostCharge> AccountPostCharge { get; } = new();
	public Table<Achievement> Achievement { get; } = new();
	public Table<AttractionGroup> AttractionGroup { get; } = new();
	public Table<BossChallenge> BossChallenge { get; } = new();
	public Table<Collecting> Collecting { get; } = new();
	public Table<ChallengeList> ChallengeList { get; } = new();
	public Table<ChallengeListReward> ChallengeListReward { get; } = new();
	public Table<ClosetGroup> ClosetGroup { get; } = new();
	public Table<ContentsReset> ContentsReset { get; } = new();
	public Table<ContentQuota> ContentQuota { get; } = new();
	public Table<ContextScript> ContextScript { get; } = new() { XmlDataPath = "skill3_contextscriptdata*.xml" };
	public Table<Cave2> Cave2 { get; } = new();
	public Table<Cave> Cave { get; } = new();
	public Table<ClassicFieldZone> ClassicFieldZone { get; } = new();
	public Table<CreatureAppearance> CreatureAppearance { get; } = new();
	public Table<Duel> Duel { get; } = new();
	public Table<Dungeon> Dungeon { get; } = new();
	public Table<Effect> Effect { get; } = new();
	public Table<FactionBattleFieldZone> FactionBattleFieldZone { get; } = new();
	public Table<Faction> Faction { get; } = new();
	public Table<FactionLevel> FactionLevel { get; } = new();
	public Table<FieldZone> FieldZone { get; } = new();
	public Table<GoodsIcon> GoodsIcon { get; } = new();
	public Table<GuildBattleFieldZone> GuildBattleFieldZone { get; } = new();
	public Table<IconTexture> IconTexture { get; } = new();
	public Table<ItemBrand> ItemBrand { get; } = new();
	public Table<ItemBrandTooltip> ItemBrandTooltip { get; } = new();
	public Table<ItemBuyPrice> ItemBuyPrice { get; } = new();
	public Table<ItemCombat> ItemCombat { get; } = new();
	public Table<Item> Item { get; } = new() { };
	public Table<ItemExchange> ItemExchange { get; } = new();
	public Table<ItemEvent> ItemEvent { get; } = new();
	public Table<ItemImprove> ItemImprove { get; } = new();
	public Table<ItemImproveOption> ItemImproveOption { get; } = new();
	public Table<ItemImproveOptionList> ItemImproveOptionList { get; } = new();
	public Table<ItemImproveSuccession> ItemImproveSuccession { get; } = new();

	public Table<ItemRandomAbilitySection> ItemRandomAbilitySection { get; } = new();
	public Table<ItemRandomAbilitySlot> ItemRandomAbilitySlot { get; } = new();
	public Table<ItemSkill> ItemSkill { get; } = new();
	public Table<ItemSpirit> ItemSpirit { get; } = new();
	public Table<ItemTransformRecipe> ItemTransformRecipe { get; } = new();
	public Table<Job> Job { get; } = new();
	public Table<JobStyle> JobStyle { get; } = new();
	public Table<KeyCap> KeyCap { get; } = new();
	public Table<KeyCommand> KeyCommand { get; } = new();
	public Table<BaseRecord> MapArea { get; } = new();
	public Table<MapInfo> MapInfo { get; } = new();
	public Table<MapGroup1> MapGroup1 { get; } = new();
	public Table<MapGroup1Expedition> MapGroup1Expedition { get; } = new();
	public Table<MapGroup2> MapGroup2 { get; } = new();
	public Table<MapUnit> MapUnit { get; } = new();
	public Table<Npc> Npc { get; } = new();
	public Table<NpcResponse> NpcResponse { get; } = new();
	public Table<NpcTalkMessage> NpcTalkMessage { get; } = new();
	public Table<PartyBattleFieldZone> PartyBattleFieldZone { get; } = new();
	public Table<Pet> Pet { get; } = new();
	public Table<PublicRaid> PublicRaid { get; } = new();
	public Table<Quest> Quest { get; } = new() { XmlDataPath = @"quest\questdata*.xml" };
	public Table<QuestBonusReward> QuestBonusReward { get; } = new();
	public Table<QuestBonusRewardSetting> QuestBonusRewardSetting { get; } = new();
	public Table<QuestReward> QuestReward { get; } = new();
	public Table<QuestSealedDungeonReward> QuestSealedDungeonReward { get; } = new();

	public Table<Race> Race { get; } = new();
	public Table<RaidDungeon> RaidDungeon { get; } = new();
	public Table<RandomStore> RandomStore { get; } = new();
	public Table<RandomStoreDrawReward> RandomStoreDrawReward { get; } = new();
	public Table<RandomStoreItem> RandomStoreItem { get; } = new();
	public Table<RandomStoreItemDisplay> RandomStoreItemDisplay { get; } = new();
	public Table<Reward> Reward { get; } = new();
	public Table<SetItem> SetItem { get; } = new();
	public Table<Skill> Skill { get; } = new();
	public Table<Skill3> Skill3 { get; } = new();
	public Table<SkillByEquipment> SkillByEquipment { get; } = new();
	public Table<SkillCastCondition3> SkillCastCondition3 { get; } = new();
	public Table<SkillGatherRange3> SkillGatherRange3 { get; } = new();
	public Table<SkillModifyInfo> SkillModifyInfo { get; } = new();
	public Table<SkillModifyInfoGroup> SkillModifyInfoGroup { get; } = new();
	public Table<SkillTooltip> SkillTooltip { get; } = new();
	public Table<SkillTooltipAttribute> SkillTooltipAttribute { get; } = new();
	public Table<SkillTrainCategory> SkillTrainCategory { get; } = new();
	public Table<SkillTrainingSequence> SkillTrainingSequence { get; } = new() { XmlDataPath = "skilltrainingsequencedata*.xml" };
	public Table<SkillTrait> SkillTrait { get; } = new();
	public Table<SlateScroll> SlateScroll { get; } = new();
	public Table<SlateScrollStone> SlateScrollStone { get; } = new();
	public Table<SlateStone> SlateStone { get; } = new();
	public Table<Store2> Store2 { get; } = new();
	public Table<SummonedSequence> SummonedSequence { get; } = new() { XmlDataPath = "summonedsequencedata*.xml" };
	public Table<SurveyQuestion> SurveyQuestions { get; } = new() { XmlDataPath = @"outsource\surveyquestions.x16" };
	public Table<TendencyField> TendencyField { get; } = new();
	public Table<Text> Text { get; } = new();
	public Table<TutorialSkillSequence> TutorialSkillSequence { get; } = new() { XmlDataPath = "tutorialskillsequencedata*.xml" };
	public Table<UnlocatedStore> UnlocatedStore { get; } = new();
	public Table<UnlocatedStoreUi> UnlocatedStoreUi { get; } = new();
	public Table<VehicleAppearance> VehicleAppearance { get; } = new();
	public Table<Vehicle> Vehicle { get; } = new();

	public Table<WantedMission> WantedMission { get; } = new();
	public Table<WorldAccountCard> WorldAccountCard { get; } = new();
	public Table<WorldAccountExpedition> WorldAccountExpedition { get; } = new();
	public Table<WorldAccountMuseum> WorldAccountMuseum { get; } = new();

	public Table<ZoneArea> ZoneArea { get; } = new();
	public Table<Zone> Zone { get; } = new();
	public Table<ZoneEnv2> ZoneEnv2 { get; } = new();
	#endregion


	#region Constructor
	public TableSet(bool unload = false)
	{
		if (unload)
			return;

		// simple valid same table
		tableDefinitions = DefinitionHelper.LoadTableDefinition();
		var tableDef = tableDefinitions.ToDictionary(def => def.Name.Replace("-", null).ToLower());

		foreach (var member in this.GetType().GetProperties(ClassExtension.Flags))
		{
			var type = member.PropertyType;
			if (typeof(ITable).IsAssignableFrom(type))
			{
				var table = (ITable)member.GetValue(this);
				table.Name = member.Name;
				table.Owner = this;
				table.TableDef = tableDef.GetValueOrDefault(member.Name.ToLower(), null);
			}
		}
	}
	#endregion

	#region Helper
	protected List<TableDefinition> tableDefinitions;

	private DatafileConverter _converter;
	public DatafileConverter converter
	{
		private set => _converter = value;
		get => _converter ?? new DatafileConverter(new DatafileDefinition(tableDefinitions.DistinctBy(o => o.Type).ToList()), null);
	}

	public DatafileDetect detect = new();


	protected void LoadConverter(bool mergeDuplicatedSequences = false)
	{
		// transfer to 
		detect.ConvertTableName(tableDefinitions);

		var defs = tableDefinitions.DistinctBy(o => o.Type).ToList();
		var datafileDeinition = new DatafileDefinition(defs) { Is64Bit = Provider?.is64Bit() ?? true };
		datafileDeinition.SequenceDefinitions.AddRange(new SequenceDefinitionLoader().LoadFor(defs, mergeDuplicatedSequences));

		// set converter
		this.converter = new DatafileConverter(datafileDeinition, this.Tables);
	}

	public virtual void LoadData(bool UseDB = true, string Folder = null)
	{
		if (Tables is not null) return;


		var _provider = new DefaultProvider(Folder ?? CommonPath.GameFolder);
		this.Provider = _provider;
		if (!UseDB) return;


		var data = _provider.XmlData.ExtractBin();
		var local = _provider.LocalData.ExtractBin();

		this.Tables = data.Tables.Concat(local.Tables).ToArray();
		this.CreatedAt = data.CreatedAt;

		detect.Detect(this.Tables, data.NameTable.CreateTable());
		this.LoadConverter();

		if (Settings.TestMode == DumpMode.Full)
			Parallel.ForEach(this.Tables, table => converter.ProcessTable(table, null, CommonPath.DataFiles));
	}
	#endregion


	#region IDispose
	public void Dispose()
	{
		Provider = null;
		Tables = null;

		GC.SuppressFinalize(this);
	}
	#endregion
}



public sealed class LocalDataTableSet : TableSet
{
	private readonly string datpath;
	public LocalDataTableSet(string DatPath) : base() => this.datpath = DatPath;


	public override void LoadData(bool UseDB, string Folder)
	{
		if (Tables is not null) return;


		var local = new BNSDat(datpath).ExtractBin();

		this.Tables = local.Tables.ToArray();
		detect.Detect(this.Tables, null);

		this.LoadConverter();
	}
}