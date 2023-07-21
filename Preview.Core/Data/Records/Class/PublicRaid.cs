using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Interface;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class PublicRaid : BaseRecord, IAttraction
{
	[Signal("publicraid-name2")]
	public Text PublicraidName2;

	[Signal("publicraid-desc")]
	public Text PublicraidDesc;

	[Signal("reward-summary")]
	public AttractionRewardSummary RewardSummary;

	[Signal("publicraid-icon")]
	public string PublicraidIcon;

	[Signal("publicraid-image")]
	public string PublicraidImage;


	#region Interface Functions
	public string GetName() => this.PublicraidName2.GetText();

	public string GetDescribe() => this.PublicraidDesc.GetText();
	#endregion
}