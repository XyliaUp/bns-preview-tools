using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public abstract class DuelBotChallenge : ModelElement, IAttraction
{
	public Ref<Text> DungeonName2;
	public Ref<Text> DungeonDesc;

	public string Text => DungeonName2.GetText();
	public string Describe => DungeonDesc.GetText();


	public sealed class TimeAttackMode : ModelElement
	{
		public short TotalTimeoutDurationSecond;
	}

	public sealed class RoundMode : ModelElement
	{
		public sbyte TotalRound;
	}
}