using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Interface;

namespace Xylia.Preview.Data.Models;
public abstract class DuelBotChallenge : Record, IAttraction
{
	public string Alias;
	public Ref<AttractionGroup> Group;
	public sbyte RequiredLevel;
	public sbyte RequiredMasteryLevel;

	public Ref<Text> Name2;
	public Ref<Text> DungeonName2;
	public Ref<Text> DungeonDesc;
	public string ArenaMinimap;
	public Ref<AttractionRewardSummary> RewardSummary;
	public sbyte UiTextGrade;
	public sbyte RecommandLevelMin;
	public sbyte RecommandLevelMax;
	public sbyte RecommandMasteryLevelMin;
	public sbyte RecommandMasteryLevelMax;

	public override string GetText => DungeonName2.GetText();
	public string GetDescribe() => DungeonDesc.GetText();


	public sealed class TimeAttackMode : Record
	{
		public short TotalTimeoutDurationSecond;
	}

	public sealed class RoundMode : Record
	{
		public sbyte TotalRound;
	}
}