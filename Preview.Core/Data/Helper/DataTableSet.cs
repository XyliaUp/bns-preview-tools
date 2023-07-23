using System.Data;

using BnsBinTool.Core.Definitions;
using BnsBinTool.Core.Models;

using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData;
using Xylia.Preview.Data.Models.BinData.AliasTable;
using Xylia.Preview.Data.Models.BinData.Table;
using Xylia.Preview.Data.Models.BinData.Table.Config;
using Xylia.Preview.Data.Models.DatData;
using Xylia.Preview.Data.Models.DatData.DataProvider;
using Xylia.Preview.Data.Models.DatData.DatDetect;
using Xylia.Preview.Data.Models.Definition;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Properties;

using TableModel = BnsBinTool.Core.Models.Table;

namespace Xylia.Preview.Data.Helper;
public class DataTableSet : IDisposable
{
	#region Data
	public IDataProvider Provider;


	public GetDataPath DataPath;

	public DateTime CreatedAt;

	public TableModel[] Tables;
	#endregion

	#region Tables
	public DataTable<AccountLevel> AccountLevel { get; } = new();
	public DataTable<AccountPostCharge> AccountPostCharge { get; } = new();
	public DataTable<Achievement> Achievement { get; } = new();
	public DataTable<AttractionGroup> AttractionGroup { get; } = new();
	public DataTable<BossChallenge> BossChallenge { get; } = new();
	public DataTable<Collecting> Collecting { get; } = new();
	public DataTable<ChallengeList> ChallengeList { get; } = new();
	public DataTable<ChallengeListReward> ChallengeListReward { get; } = new();
	public DataTable<ClosetGroup> ClosetGroup { get; } = new();
	public DataTable<ContentsReset> ContentsReset { get; } = new();
	public DataTable<ContentQuota> ContentQuota { get; } = new();
	public DataTable<ContextScript> ContextScript { get; } = new() { XmlDataPath = "skill3_contextscriptdata*.xml" };
	public DataTable<Cave2> Cave2 { get; } = new();
	public DataTable<Cave> Cave { get; } = new();
	public DataTable<ClassicFieldZone> ClassicFieldZone { get; } = new();
	public DataTable<CreatureAppearance> CreatureAppearance { get; } = new();
	public DataTable<Duel> Duel { get; } = new();
	public DataTable<Dungeon> Dungeon { get; } = new();
	public DataTable<Effect> Effect { get; } = new();
	public DataTable<FactionBattleFieldZone> FactionBattleFieldZone { get; } = new();
	public DataTable<Faction> Faction { get; } = new();
	public DataTable<FactionLevel> FactionLevel { get; } = new();
	public DataTable<FieldZone> FieldZone { get; } = new();
	public DataTable<GoodsIcon> GoodsIcon { get; } = new();
	public DataTable<GuildBattleFieldZone> GuildBattleFieldZone { get; } = new();
	public DataTable<IconTexture> IconTexture { get; } = new();
	public DataTable<ItemBrand> ItemBrand { get; } = new();
	public DataTable<ItemBrandTooltip> ItemBrandTooltip { get; } = new();
	public DataTable<ItemBuyPrice> ItemBuyPrice { get; } = new();
	public DataTable<ItemCombat> ItemCombat { get; } = new();
	public DataTable<Item> Item { get; } = new() { };
	public DataTable<ItemExchange> ItemExchange { get; } = new();
	public DataTable<ItemEvent> ItemEvent { get; } = new();
	public DataTable<ItemImprove> ItemImprove { get; } = new();
	public DataTable<ItemImproveOption> ItemImproveOption { get; } = new();
	public DataTable<ItemImproveOptionList> ItemImproveOptionList { get; } = new();
	public DataTable<ItemImproveSuccession> ItemImproveSuccession { get; } = new();

	public DataTable<ItemRandomAbilitySection> ItemRandomAbilitySection { get; } = new();
	public DataTable<ItemRandomAbilitySlot> ItemRandomAbilitySlot { get; } = new();
	public DataTable<ItemSkill> ItemSkill { get; } = new();
	public DataTable<ItemSpirit> ItemSpirit { get; } = new();
	public DataTable<ItemTransformRecipe> ItemTransformRecipe { get; } = new();
	public DataTable<Job> Job { get; } = new();
	public DataTable<JobStyle> JobStyle { get; } = new();
	public DataTable<KeyCap> KeyCap { get; } = new();
	public DataTable<KeyCommand> KeyCommand { get; } = new();
	public DataTable<BaseRecord> MapArea { get; } = new();
	public DataTable<MapInfo> MapInfo { get; } = new();
	public DataTable<MapGroup1> MapGroup1 { get; } = new();
	public DataTable<MapGroup1Expedition> MapGroup1Expedition { get; } = new();
	public DataTable<MapGroup2> MapGroup2 { get; } = new();
	public DataTable<MapUnit> MapUnit { get; } = new();
	public DataTable<Npc> Npc { get; } = new();
	public DataTable<NpcResponse> NpcResponse { get; } = new();
	public DataTable<NpcTalkMessage> NpcTalkMessage { get; } = new();
	public DataTable<PartyBattleFieldZone> PartyBattleFieldZone { get; } = new();
	public DataTable<Pet> Pet { get; } = new();
	public DataTable<PublicRaid> PublicRaid { get; } = new();
	public DataTable<Quest> Quest { get; } = new() { XmlDataPath = @"quest\questdata*.xml" };
	public DataTable<QuestBonusReward> QuestBonusReward { get; } = new();
	public DataTable<QuestBonusRewardSetting> QuestBonusRewardSetting { get; } = new();
	public DataTable<QuestReward> QuestReward { get; } = new();
	public DataTable<QuestSealedDungeonReward> QuestSealedDungeonReward { get; } = new();

	public DataTable<Race> Race { get; } = new();
	public DataTable<RaidDungeon> RaidDungeon { get; } = new();
	public DataTable<RandomStore> RandomStore { get; } = new();
	public DataTable<RandomStoreDrawReward> RandomStoreDrawReward { get; } = new();
	public DataTable<RandomStoreItem> RandomStoreItem { get; } = new();
	public DataTable<RandomStoreItemDisplay> RandomStoreItemDisplay { get; } = new();
	public DataTable<Reward> Reward { get; } = new();
	public DataTable<SetItem> SetItem { get; } = new();
	public DataTable<Skill> Skill { get; } = new();
	public DataTable<Skill3> Skill3 { get; } = new();
	public DataTable<SkillByEquipment> SkillByEquipment { get; } = new();
	public DataTable<SkillCastCondition3> SkillCastCondition3 { get; } = new();
	public DataTable<SkillGatherRange3> SkillGatherRange3 { get; } = new();
	public DataTable<SkillModifyInfo> SkillModifyInfo { get; } = new();
	public DataTable<SkillModifyInfoGroup> SkillModifyInfoGroup { get; } = new();
	public DataTable<SkillTooltip> SkillTooltip { get; } = new();
	public DataTable<SkillTooltipAttribute> SkillTooltipAttribute { get; } = new();
	public DataTable<SkillTrainCategory> SkillTrainCategory { get; } = new();
	public DataTable<SkillTrainingSequence> SkillTrainingSequence { get; } = new() { XmlDataPath = "skilltrainingsequencedata*.xml" };
	public DataTable<SkillTrait> SkillTrait { get; } = new();
	public DataTable<SlateScroll> SlateScroll { get; } = new();
	public DataTable<SlateScrollStone> SlateScrollStone { get; } = new();
	public DataTable<SlateStone> SlateStone { get; } = new();
	public DataTable<Store2> Store2 { get; } = new();
	public DataTable<SummonedSequence> SummonedSequence { get; } = new() { XmlDataPath = "summonedsequencedata*.xml" };
	public DataTable<SurveyQuestion> SurveyQuestions { get; } = new() { XmlDataPath = @"outsource\surveyquestions.x16" };
	public DataTable<TendencyField> TendencyField { get; } = new();
	public DataTable<Text> Text { get; } = new();
	public DataTable<TutorialSkillSequence> TutorialSkillSequence { get; } = new() { XmlDataPath = "tutorialskillsequencedata*.xml" };
	public DataTable<UnlocatedStore> UnlocatedStore { get; } = new();
	public DataTable<UnlocatedStoreUi> UnlocatedStoreUi { get; } = new();
	public DataTable<VehicleAppearance> VehicleAppearance { get; } = new();
	public DataTable<Vehicle> Vehicle { get; } = new();

	public DataTable<WantedMission> WantedMission { get; } = new();
	public DataTable<WorldAccountCard> WorldAccountCard { get; } = new();
	public DataTable<WorldAccountExpedition> WorldAccountExpedition { get; } = new();
	public DataTable<WorldAccountMuseum> WorldAccountMuseum { get; } = new();
	public DataTable<Zone> Zone { get; } = new();
	public DataTable<ZoneEnv2> ZoneEnv2 { get; } = new();
	#endregion


	#region Constructor
	public DataTableSet(bool unload = false)
	{
		if (unload)
			return;

		// simple valid same table
		tableDefinitions = DefinitionHelper.LoadTableDefinition();
		var tableDef = tableDefinitions.ToDictionary(def => def.Name.Replace("-", null).ToLower());

		foreach (var member in this.GetType().GetProperties(ClassExtension.Flags))
		{
			var type = member.PropertyType;
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(DataTable<>))
			{
				var table = member.GetValue(this);
				table.GetInfo("Owner").SetValue(table, this);
				table.GetInfo("Name").SetValue(table, member.Name);

				if (tableDef.TryGetValue(member.Name.ToLower(), out var def))
					table.GetInfo("TableDef").SetValue(table, def);
			}
		}
	}
	#endregion



	#region Helper
	protected List<DataTableDefinition> tableDefinitions;

	public DatafileConverter converter;

	public DatafileDetect detect = new();

	protected void LoadConverter(bool mergeDuplicatedSequences = false)
	{
		// convert ref table name to key
		foreach (var tableDef in tableDefinitions)
		{
			{
				if (tableDef.Type == 0 && detect.TryGetKey(tableDef.Name, out var type))
					tableDef.Type = type;
			}

			foreach (AttributeDef attr in tableDef.ExpandedAttributes.Where(o => o is AttributeDef))
			{
				var TypeName = attr.ReferedTableName;
				if (TypeName != null && detect.TryGetKey(TypeName, out var type))
					attr.ReferedTable = type;
			}

			foreach (var subtable in tableDef.Subtables)
			{
				foreach (AttributeDef attr in subtable.ExpandedAttributes.Where(o => o is AttributeDef))
				{
					var TypeName = attr.ReferedTableName;
					if (TypeName != null && detect.TryGetKey(TypeName, out var type))
						attr.ReferedTable = type;
				}
			}
		}


		// transfer to 
		var defs = tableDefinitions.DistinctBy(o => o.Type).Select(d => (TableDefinition)d).ToList();

		var datafileDeinition = new DatafileDefinition(defs) { Is64Bit = DataPath.is64Bit };
		datafileDeinition.SequenceDefinitions.AddRange(new SequenceDefinitionLoader().LoadFor(defs, mergeDuplicatedSequences));


		// set converter
		this.converter = new DatafileConverter(datafileDeinition, this.Tables);
	}

	public virtual void LoadData(bool UseDB = true, string Folder = null)
	{
		if (Tables is not null) return;


		DataPath = new GetDataPath(Folder ?? CommonPath.GameFolder);
		if (!UseDB) return;


		var data = Datafile.ReadFromBytes(DataPath.XmlData.ExtractBin(), is64Bit: DataPath.is64Bit);
		var local = Datafile.ReadFromBytes(DataPath.LocalData.ExtractBin(), is64Bit: DataPath.is64Bit);

		this.Tables = data.Tables.Concat(local.Tables).ToArray();
		this.CreatedAt = data.CreatedAt;

		detect.Detect(this.Tables, data.NameTable.Entries.CreateTable());
		this.LoadConverter();


		if (Settings.TestMode == DumpMode.Full)
		{
			Parallel.ForEach(this.Tables, table => converter.ProcessTable(table, null, CommonPath.DataFiles));
		}
	}
	#endregion


	#region IDispose
	private bool disposedValue;

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				// TODO: 释放托管状态(托管对象)
				Tables = null;
			}

			// TODO: 释放未托管的资源(未托管的对象)并重写终结器
			// TODO: 将大型Fields设置为 null
			disposedValue = true;
		}
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
	#endregion
}



public sealed class LocalDataTableSet : DataTableSet
{
	private readonly string datpath;
	public LocalDataTableSet(string DatPath) : base() => this.datpath = DatPath;


	public override void LoadData(bool UseDB, string Folder)
	{
		if (Tables is not null) return;


		var LocalData = new BNSDat(datpath);
		var local = Datafile.ReadFromBytes(LocalData.ExtractBin(), is64Bit: DataPath.is64Bit);

		this.Tables = local.Tables.ToArray();
		detect.Detect(this.Tables, null);


		this.LoadConverter();
	}
}