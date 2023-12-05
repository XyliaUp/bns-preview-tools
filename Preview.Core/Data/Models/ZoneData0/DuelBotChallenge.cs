using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public abstract class DuelBotChallenge : Record, IAttraction
{
	public Ref<Text> DungeonName2;
	public Ref<Text> DungeonDesc;

	public string Text => DungeonName2.GetText();
	public string Describe => DungeonDesc.GetText();


	public sealed class TimeAttackMode : Record
	{
		public short TotalTimeoutDurationSecond;
	}

	public sealed class RoundMode : Record
	{
		public sbyte TotalRound;
	}
}