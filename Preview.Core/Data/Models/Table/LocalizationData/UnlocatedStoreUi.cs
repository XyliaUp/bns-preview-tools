namespace Xylia.Preview.Data.Models;
public class UnlocatedStoreUi : ModelElement
{
	public UnlocatedStore.UnlocatedStoreTypeSeq UnlocatedStoreType { get; set; }

	public string TitleIcon { get; set; }

	public Ref<Text> TitleText { get; set; }

	public string ButtonIcon { get; set; }

	public Ref<Text> ButtonText { get; set; }
}