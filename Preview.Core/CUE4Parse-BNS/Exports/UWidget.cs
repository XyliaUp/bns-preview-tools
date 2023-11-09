using CUE4Parse.UE4.Assets.Readers;

namespace CUE4Parse.UE4.Assets.Exports;
public class UWidget : UObject
{
	public bool bIsVariable;
	public string Visibility;

	public ResolvedObject Slot;     // this
	public ResolvedObject[] Slots;  // children
	
	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);

		Slot = GetOrDefault<ResolvedObject>(nameof(Slot));
		Slots = GetOrDefault(nameof(Slots) , Array.Empty<ResolvedObject>());
	}
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
