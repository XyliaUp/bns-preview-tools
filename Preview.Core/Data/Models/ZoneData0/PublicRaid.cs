using Xylia.Preview.Data.Common.Interface;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class PublicRaid : Record, IAttraction
{
	public string Alias;

	public Ref<Text> PublicraidName2;
	public Ref<Text> PublicraidDesc;

	public Ref<AttractionRewardSummary> RewardSummary;

	public string PublicraidIcon;
	public string PublicraidImage;


	#region Interface Methdos
	public string Text => this.PublicraidName2.GetText();

	public string GetDescribe() => this.PublicraidDesc.GetText();
	#endregion
}