using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public sealed class Duel : ModelElement, IAttraction
{
	public enum DuelType
	{
		None,
		DeathMatch1VS1,
		TagMatch3VS3,
		SuddenDeath3VS3,
	}


	#region IAttraction
	public string Text => this.Attributes["duel-name2"].GetText();

	public string Describe => this.Attributes["duel-desc"].GetText();
	#endregion
}