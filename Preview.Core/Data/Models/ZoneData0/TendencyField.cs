using Xylia.Preview.Data.Common.Interface;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class TendencyField : Record, IAttraction
{
	public string Alias;

	public Ref<Text> TendencyFieldName2;
	public Ref<Text> TendencyFieldDesc;

	public Ref<AttractionRewardSummary> RewardSummary;

	public sbyte UiTextGrade;


	#region Interface Methdos
	public string Text => this.TendencyFieldName2.GetText();

	public string GetDescribe() => this.TendencyFieldDesc.GetText();
	#endregion
}