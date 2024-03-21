using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public abstract class UBnsCustomBaseWidget : UUserWidget
{
	[UPROPERTY] public bool bApplyAlphaToBlur;
	[UPROPERTY] public bool bEnableActivateEvent;
	[UPROPERTY] public bool bEnableCustomize;
	[UPROPERTY] public bool bEnableEscapeKey;
	[UPROPERTY] public bool bEnableTopOrder;
	[UPROPERTY] public bool bEnableLeftButton;
	[UPROPERTY] public bool bEnableRightButton;
	[UPROPERTY] public bool bEnableWheel;
	[UPROPERTY] public bool bStartedVisible;
	[UPROPERTY] public bool bUseLocalFont;
	[UPROPERTY] public bool bVisibleBlend;
	[UPROPERTY] public bool bVisibleScale;
	[UPROPERTY] public bool CanAssignWidgetID;
	[UPROPERTY] public string MetaData;

	[UPROPERTY] public FPackageIndex ShowEvent;
	[UPROPERTY] public FPackageIndex HideEvent;

	[UPROPERTY] public string ScaleHorizontalAlignment;
	[UPROPERTY] public string ScaleVerticalAlignment;
	[UPROPERTY] public ResizeLink HorizontalResizeLink;
	[UPROPERTY] public ResizeLink VerticalResizeLink;

	[UPROPERTY] public StringProperty StringProperty;
	[UPROPERTY] public ImageProperty BaseImageProperty;
	[UPROPERTY] public ExpansionComponent[] ExpansionComponentList;
}

public abstract class UBnsCustomBaseWidgetProperty : USerializeObject
{

}