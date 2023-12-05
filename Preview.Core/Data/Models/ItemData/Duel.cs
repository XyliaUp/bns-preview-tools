using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class Duel : Record, IAttraction
{
	public Ref<Text> DuelName2;

	public Ref<Text> DuelDesc;

	public enum DuelType
	{
		None,

		[Name("death-match-1vs1")]
		DeathMatch1VS1,

		[Name("tag-match-3vs3")]
		TagMatch3VS3,

		[Name("sudden-death-3vs3")]
		SuddenDeath3VS3,
	}


	#region Interface
	public string Text => this.DuelName2.GetText();

	public string Describe => this.DuelDesc.GetText();
	#endregion
}