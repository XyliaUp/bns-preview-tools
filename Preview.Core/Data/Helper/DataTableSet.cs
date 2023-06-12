using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using BnsBinTool.Core.Models;

using Xylia.Extension;
using Xylia.Preview.Data.Definition;
using Xylia.Preview.Data.Models.BinData;
using Xylia.Preview.Data.Models.DatData;
using Xylia.Preview.Data.Models.DatData.DatDetect;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Properties;

using TableModel = BnsBinTool.Core.Models.Table;

namespace Xylia.Preview.Data.Helper;

public class DataTableSet : IDisposable
{
	#region Data
	public BNSDat XmlData;

	public BNSDat LocalData;

	public BNSDat ConfigData;

	public DateTime CreatedAt;

	public TableModel[] Tables;

	public DatafileToXmlConverterHelper datafileToXml;

	readonly Dictionary<string, TestHelper> TestHelpers = new(StringComparer.OrdinalIgnoreCase);
	public TestHelper GetHelper(string TableName, bool createIfNotExists = true)
	{
		if (string.IsNullOrWhiteSpace(TableName)) return null;


		TableName = TableName.Replace("-", null);

		if (!TestHelpers.ContainsKey(TableName))
			return createIfNotExists ? TestHelpers[TableName] = new() : null;
		return TestHelpers[TableName];
	}
	public short GetTableType(string TableName) => this.GetHelper(TableName, false)?.Type ?? 0;


	public virtual void LoadData(bool UseDB = true, string Folder = null)
	{
		#region File
		if (Tables is not null) return;
		var tableDef = DefinitionHelper.LoadTableDefinition();

		if (XmlData is null)
		{
			var getDataPath = new GetDataPath(Folder ?? CommonPath.GameFolder, true);
			XmlData = new BNSDat(getDataPath.TargetXml);
			LocalData = new BNSDat(getDataPath.TargetLocal);
			ConfigData = new BNSDat(getDataPath.TargetConfig);
		}
		#endregion

		#region bnsDB
		if (!UseDB) return;


		var is64Bit = true;
		var data = Datafile.ReadFromBytes(XmlData.ExtractBin(), is64Bit: is64Bit);
		var local = Datafile.ReadFromBytes(LocalData.ExtractBin(), is64Bit: is64Bit);

		this.Tables = data.Tables.Concat(local.Tables).ToArray();
		this.CreatedAt = data.CreatedAt;


		DateTime dt = DateTime.Now;

		//获取扩展信息
		var AliasTable = data.NameTable.Entries.CreateTable();
		Debug.WriteLine($"[2] rt {(DateTime.Now - dt).Seconds}s");

		this.SetType(this.Tables.DetectIndices(AliasTable));
		Debug.WriteLine($"[3] rt {(DateTime.Now - dt).Seconds}s");

		AliasTable.ForEach(table => GetHelper(table.Key).Aliases = table.Value);


		this.datafileToXml = new(DefinitionHelper.LoadDefinition(tableDef, this), this.Tables);
		if (CommonPath.Test == 2) Parallel.ForEach(this.Tables, table => datafileToXml.ProcessTable(table, null, CommonPath.DataFiles));
		#endregion
	}

	protected void SetType(Dictionary<short, string> info)
	{
		info.ForEach(o =>
		{
			if (o.Value is null)
				return;

			var helper = GetHelper(o.Value);
			helper.Type = o.Key;
			helper.TypeName = o.Value;
		});
	}
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
	public DataTable<Item> Item { get; } = new();
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
	public DataTable<MapInfo> MapInfo { get; } = new();
	public DataTable<MapGroup1> MapGroup1 { get; } = new();
	public DataTable<MapGroup1Expedition> MapGroup1Expedition { get; } = new();
	public DataTable<MapGroup2> MapGroup2 { get; } = new();
	public DataTable<MapUnit> MapUnit { get; } = new();
	public DataTable<Npc> Npc { get; } = new();
	public DataTable<NpcResponse> NpcResponse { get; } = new();
	public DataTable<NpcTalkMessage> NpcTalkMessage { get; } = new();
	public DataTable<PartyBattleFieldZone> PartyBattleFieldZone { get; } = new();
	public DataTable<PublicRaid> PublicRaid { get; } = new();
	public DataTable<Quest> Quest { get; } = new() { XmlDataPath = @"quest\questdata*.xml" };
	public DataTable<QuestBonusReward> QuestBonusReward { get; } = new();
	public DataTable<QuestBonusRewardSetting> QuestBonusRewardSetting { get; } = new();
	public DataTable<QuestReward> QuestReward { get; } = new();
	public DataTable<QuestSealedDungeonReward> QuestSealedDungeonReward { get; } = new();
	public DataTable<RaidDungeon> RaidDungeon { get; } = new();
	public DataTable<RandomStore> RandomStore { get; } = new();
	public DataTable<RandomStoreDrawReward> RandomStoreDrawReward { get; } = new();
	public DataTable<RandomStoreItem> RandomStoreItem { get; } = new();
	public DataTable<RandomStoreItemDisplay> RandomStoreItemDisplay { get; } = new();
	public DataTable<Reward> Reward { get; } = new();
	public DataTable<SetItem> SetItem { get; } = new();

	public DataTable<_Skill> Skill { get; } = new();
	public DataTable<Skill> Skill3 { get; } = new();
	public DataTable<SkillByEquipment> SkillByEquipment { get; } = new();
	public DataTable<SkillCastCondition3> SkillCastCondition3 { get; } = new();
	public DataTable<SkillGatherRange3> SkillGatherRange3 { get; } = new();
	public DataTable<SkillModifyInfo> SkillModifyInfo { get; } = new();
	public DataTable<SkillModifyInfoGroup> SkillModifyInfoGroup { get; } = new();
	public DataTable<SkillTooltip> SkillTooltip { get; } = new();
	public DataTable<SkillTooltipAttribute> SkillTooltipAttribute { get; } = new();
	public DataTable<SkillTrainCategory> SkillTrainCategory { get; } = new();
	public DataTable<BaseRecord> SkillTrainingSequence { get; } = new() { XmlDataPath = "skilltrainingsequencedata*.xml" };
	public DataTable<SkillTrait> SkillTrait { get; } = new();
	public DataTable<SlateScroll> SlateScroll { get; } = new();
	public DataTable<SlateScrollStone> SlateScrollStone { get; } = new();
	public DataTable<SlateStone> SlateStone { get; } = new();
	public DataTable<Store2> Store2 { get; } = new();
	public DataTable<BaseRecord> SummonedSequence { get; } = new() { XmlDataPath = "summonedsequencedata*.xml" };
	public DataTable<BaseRecord> SurveyQuestions { get; } = new() { XmlDataPath = @"outsource\surveyquestions.x16" };
	public DataTable<TendencyField> TendencyField { get; } = new();
	public DataTable<Text> TextData { get; } = new() { ShowDebugInfo = false };
	public DataTable<BaseRecord> TutorialSkillSequence { get; } = new() { XmlDataPath = "tutorialskillsequencedata*.xml" };
	public DataTable<UnlocatedStore> UnlocatedStore { get; } = new();
	public DataTable<WantedMission> WantedMission { get; } = new();
	public DataTable<WorldAccountCard> WorldAccountCard { get; } = new();
	public DataTable<WorldAccountExpedition> WorldAccountExpedition { get; } = new();
	public DataTable<WorldAccountMuseum> WorldAccountMuseum { get; } = new();
	public DataTable<ZoneEnv2> ZoneEnv2 { get; } = new();
	#endregion

	public DataTableSet()
	{
		foreach (var finfo in this.GetType().GetMembers(ClassExtension.Flags))
		{
			if (finfo.HasImplementedRawGeneric(typeof(DataTable<>)))
			{
				var Table = finfo.GetValue(this);
				Table.GetInfo("DataTableSet").SetValue(Table, this);
			}
		}
	}


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

		var tableDef = DefinitionHelper.LoadTableDefinition();

		#region Load bnsDB
		LocalData = new BNSDat(datpath);

		var is64Bit = true;
		var local = Datafile.ReadFromBytes(LocalData.ExtractBin(), is64Bit: is64Bit);
		this.Tables = local.Tables.ToArray();
		this.SetType(this.Tables.DetectIndices());
		#endregion

		#region Load 定义数据
		this.datafileToXml = new(DefinitionHelper.LoadDefinition(tableDef, this));
		#endregion
	}
}

public sealed class TestSet : DataTableSet
{
	public override void LoadData(bool UseDB, string Folder)
	{
		#region Load 文件
		if (Tables is not null) return;

		var getDataPath = new GetDataPath(Folder ?? CommonPath.GameFolder, true);
		XmlData = new BNSDat(getDataPath.TargetXml);
		LocalData = new BNSDat(getDataPath.TargetLocal);
		#endregion

		#region Load  bnsDB
		var data = Datafile.ReadFromBytes(XmlData.ExtractBin());
		var local = Datafile.ReadFromBytes(LocalData.ExtractBin());
		this.Tables = data.Tables.Concat(local.Tables).ToArray();
		#endregion


		var definition = DefinitionHelper.LoadTableDefinition(@"F:\Resources\文档\Programming\C#\项目\Xylia\bns\Xylia.Match\Xylia.Preview\Properties\AnalyseSection\Data\117.xml");
		this.datafileToXml = new(DefinitionHelper.LoadDefinition(definition, this));
		Parallel.ForEach(definition, def =>
		{
			var table = Tables.FirstOrDefault(o => o.Type == def.Type);
			if (table is null) return;

			datafileToXml.ProcessTable(table, def, CommonPath.DataFiles);
		});
	}
}