using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public class UnlocatedStoreUi : Record
{
	public UnlocatedStore.UnlocatedStoreTypeSeq UnlocatedStoreType;

	public string TitleIcon;

	public Ref<Text> TitleText;

	public string ButtonIcon;

	public Ref<Text> ButtonText;
}