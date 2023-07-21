
using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record.Attributes;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal;
public sealed class RewardGroup
{
	#region Constructor
	public RewardGroup(IAttributeCollection Attributes, string Group)
	{
		this.Faction = Attributes[Group + "-faction"];
		this.DifficultyType = Attributes[Group + "-difficulty-type"].ToEnum<DifficultyType>();

		this.Slot1 = Attributes[Group + "-slot-1"];
		this.Slot2 = Attributes[Group + "-slot-2"];
		this.Slot3 = Attributes[Group + "-slot-3"];
		this.Slot4 = Attributes[Group + "-slot-4"];

		byte.TryParse(Attributes[Group + "-item-count-1"], out this.ItemCount1);
		byte.TryParse(Attributes[Group + "-item-count-2"], out this.ItemCount2);
		byte.TryParse(Attributes[Group + "-item-count-3"], out this.ItemCount3);
		byte.TryParse(Attributes[Group + "-item-count-4"], out this.ItemCount4);

		byte.TryParse(Attributes[Group + "-skill-var-idx-1"], out this.SkillVarIdx1);
		byte.TryParse(Attributes[Group + "-skill-var-idx-2"], out this.SkillVarIdx2);
		byte.TryParse(Attributes[Group + "-skill-var-idx-3"], out this.SkillVarIdx3);
		byte.TryParse(Attributes[Group + "-skill-var-idx-4"], out this.SkillVarIdx4);


		this.GroupKey = Group;
		this.GroupName = GetGroupName(Attributes);
	}
	#endregion

	#region Fields
	public string Faction;
	public DifficultyType DifficultyType;

	public string Slot1;
	public string Slot2;
	public string Slot3;
	public string Slot4;

	public byte ItemCount1;
	public byte ItemCount2;
	public byte ItemCount3;
	public byte ItemCount4;

	//optional组没有这个
	public byte SkillVarIdx1;
	public byte SkillVarIdx2;
	public byte SkillVarIdx3;
	public byte SkillVarIdx4;
	#endregion

	#region Functions
	/// <summary>
	/// 归属组
	/// </summary>
	public string GroupKey;

	public string GroupName;

	private string GetGroupName(IAttributeCollection Attributes)
	{
		string groupName = null;

		#region DifficultyType
		if (this.DifficultyType == DifficultyType.Easy) groupName = "入门";
		else if (this.DifficultyType == DifficultyType.Normal) groupName = "普通";
		else if (this.DifficultyType == DifficultyType.Hard) groupName = "熟练";
		#endregion

		#region Faction
		if (this.Faction != null)
		{
			var faction = FileCache.Data.Faction[this.Faction];
			groupName += faction?.Name2.GetText() ?? this.Faction;
		}
		#endregion

		#region Job
		for (int i = 1; i <= 20; i++)
		{
			if (!Attributes.ContainsKey($"{this.GroupKey}-job-{i}", out string job)) break;

			var seq = job.ToEnum<JobSeq>();
			if (seq == JobSeq.JobNone) break;

			groupName += seq.GetName();
		}
		#endregion

		#region Sex And Race
		for (int i = 1; i <= 4; i++)
		{
			if (!Attributes.ContainsKey($"{this.GroupKey}-sex-{i}", out string sex)) break;

			var seq = sex.ToEnum<SexSeq>();
			if (seq == SexSeq.SexNone) break;

			groupName += ((SexSeq2)seq).GetName();
		}

		for (int i = 1; i <= 4; i++)
		{
			if (!Attributes.ContainsKey($"{this.GroupKey}-race-{i}", out string race)) break;

			var seq = race.ToEnum<RaceSeq>();
			if (seq == RaceSeq.RaceNone) break;

			groupName += seq.GetName();
		}
		#endregion

		return groupName;
	}
	#endregion
}


public class RewardGroupSet
{
	public RewardGroup Fixed;

	public RewardGroup Optional;



	public QuestSealedDungeonReward QuestSealedDungeonReward;
}