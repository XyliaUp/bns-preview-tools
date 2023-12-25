using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public abstract class DuelBotChallenge : ModelElement, IAttraction
{
	#region IAttraction
	public string Text => this.Attributes["dungeon-name2"].GetText();
	public string Describe => this.Attributes["dungeon-desc"].GetText();
	#endregion

	public sealed class TimeAttackMode : ModelElement
	{
		public short TotalTimeoutDurationSecond;
	}

	public sealed class RoundMode : ModelElement
	{
		public sbyte TotalRound;
	}
}