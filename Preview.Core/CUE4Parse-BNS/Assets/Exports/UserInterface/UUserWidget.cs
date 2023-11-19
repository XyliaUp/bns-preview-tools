using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.UE4.Assets.Exports;
public class UUserWidget : USerializeObject
{
	[UPROPERTY] public bool bNeverActivate;
	[UPROPERTY] public bool bIsVariable;
	[UPROPERTY] public string Visibility;

	[UPROPERTY] public FPackageIndex Slot;     // UBnsCustomBaseWidgetSlot → this
	[UPROPERTY] public FPackageIndex[] Slots;  // UBnsCustomBaseWidgetSlot → children
}

public enum ESlateVisibility : byte
{
	/** Default widget visibility - visible and can interact with the cursor */
	Visible,
	/** Not visible and takes up no space in the layout; can never be clicked on because it takes up no space. */
	Collapsed,
	/** Not visible, but occupies layout space. Not interactive for obvious reasons. */
	Hidden,
	/** Visible to the user, but only as art. The cursors hit tests will never see this widget. */
	HitTestInvisible,
	/** Same as HitTestInvisible, but doesn't apply to child widgets. */
	SelfHitTestInvisible
}