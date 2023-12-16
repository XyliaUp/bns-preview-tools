using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public sealed class Duel : ModelElement, IAttraction
{
	public Ref<Text> DuelName2;

	public Ref<Text> DuelDesc;

	public enum DuelType
	{
		None,

			DeathMatch1VS1,

			TagMatch3VS3,

			SuddenDeath3VS3,
	}


	#region Interface
	public string Text => this.DuelName2.GetText();

	public string Describe => this.DuelDesc.GetText();
	#endregion
}