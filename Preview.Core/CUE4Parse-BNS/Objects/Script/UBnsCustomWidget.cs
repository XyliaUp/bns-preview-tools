using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.BNS.Objects.Script;
public abstract class UBnsCustomWidget : UWidget
{
	public struct ResizeLink
	{
		public bool bEnable;
		public string Type;

		public float Offset1;
		public string LinkWidgetName1;
	}


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


	public bool CanAssignWidgetID;
	public string MetaData;


	public ResolvedObject ShowEvent;
	public ResolvedObject HideEvent;

	public string ScaleHorizontalAlignment;
	public string ScaleVerticalAlignment;
	public ResizeLink HorizontalResizeLink;
	public ResizeLink VerticalResizeLink;



	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);
	}
}




public class UBnsCustomCaptionWidget : UBnsCustomWidget
{
}

public class UBnsCustomEditBoxWidget : UBnsCustomWidget
{
}

public class UBnsCustomImageWidget : UBnsCustomWidget
{
	public struct BaseImage
	{
		public ResolvedObject BaseImageTexture;
		public FVector2D ImageUV;
		public FVector2D ImageUVSize;

		public object TintColor;

		public string HorizontalAlignment;
		public string VerticalAlignment;
		public string SperateImageType;
		public object[] CoordinatesArray;
	}

	


	public BaseImage BaseImageProperty;

	public bool bNeverActivate;
	public bool bIsVariable;

	public bool EnableResourceSize;
}

public class UBnsCustomLabelWidget : UBnsCustomWidget
{
}

public class UBnsCustomLabelButtonWidget : UBnsCustomWidget
{
}

public class UBnsCustomProgressBarWidget : UBnsCustomWidget
{
}

public class UBnsCustomSliderBarWidget : UBnsCustomWidget
{
}

public class UBnsCustomToggleButtonWidget : UBnsCustomWidget
{
}

public class UBnsCustomWindowWidget : UBnsCustomWidget
{

}






public struct ExpansionComponentList
{
	public bool bEnableSubState;
	public bool bPostExpansitonRender;
	public bool bVisibleFlag;
	public bool bShow;

	public string ExpansionType;
	public string ExpansionName;
	public string MetaData;
	public string WidgetState;
	public string WidgetSubState;


	public FStructFallback PublisherVisible;
	public FStructFallback MetaDataByPublisher;
	public FStructFallback ImageProperty; 
	public FStructFallback StringProperty;

	public ResolvedObject Owner;
	public string PresetFactor;
}