using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
public class UnlocatedStoreUi : BaseRecord
{
	[Signal("unlocated-store-type")]
	public UnlocatedStore.UnlocatedStoreTypeSeq UnlocatedStoreType;

	[Signal("title-icon")]
	public string TitleIcon;

	[Signal("title-text")]
	public Text TitleText;

	[Signal("button-icon")]
	public string ButtonIcon;

	[Signal("button-text")]
	public Text ButtonText;
}