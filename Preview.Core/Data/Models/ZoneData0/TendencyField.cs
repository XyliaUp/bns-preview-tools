using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Interface;

namespace Xylia.Preview.Data.Models;
public sealed class TendencyField : Record, IAttraction
{
	#region Fields
	public string Alias;

	public Ref<Text> TendencyFieldName2;
	public Ref<Text> TendencyFieldDesc;

	public Ref<AttractionRewardSummary> RewardSummary;

	public sbyte UiTextGrade;
	#endregion

	#region Interface Methdos
	public override string GetText => this.TendencyFieldName2.GetText();

	public string GetDescribe() => this.TendencyFieldDesc.GetText();
	#endregion
}