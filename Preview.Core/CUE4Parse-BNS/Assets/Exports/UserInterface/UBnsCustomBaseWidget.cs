using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public abstract class UBnsCustomBaseWidget : UUserWidget
{	 
	public bool bApplyAlphaToBlur;
	public bool bEnableActivateEvent;
	public bool bEnableCustomize;
	public bool bEnableEscapeKey;
	public bool bEnableTopOrder;
	public bool bEnableLeftButton;
	public bool bEnableRightButton;
	public bool bEnableWheel;
	public bool bStartedVisible;
	public bool bUseLocalFont;
	public bool bVisibleBlend;
	public bool bVisibleScale;

	[UPROPERTY]
	public bool CanAssignWidgetID;

	[UPROPERTY]
	public string MetaData;


	public FPackageIndex ShowEvent;
	public FPackageIndex HideEvent;

	public string ScaleHorizontalAlignment;
	public string ScaleVerticalAlignment;
	public ResizeLink HorizontalResizeLink;
	public ResizeLink VerticalResizeLink;


	[UPROPERTY] public StringProperty StringProperty;
	[UPROPERTY] public ImageProperty BaseImageProperty;
	[UPROPERTY] public ExpansionComponent[] ExpansionComponentList;
}