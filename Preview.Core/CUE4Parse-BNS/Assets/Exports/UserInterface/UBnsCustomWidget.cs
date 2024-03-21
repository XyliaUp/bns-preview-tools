using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public class UBnsCustomCaptionWidget : UBnsCustomBaseWidget
{

}

public class UBnsCustomColumnListWidget : UBnsCustomBaseWidget
{
	[UPROPERTY] public int RowCount;
	[UPROPERTY] public int ColumnCount;
	[UPROPERTY] public float PixelScrollValue;
	[UPROPERTY] public int AutoHeightColumnIndex;
	[UPROPERTY] public float DragScrollOffsetLimit;
	[UPROPERTY] public float MinAutoHeight;
	[UPROPERTY] public bool AttachScrollBar;
}

public class UBnsCustomEditBoxWidget : UBnsCustomBaseWidget
{
	[UPROPERTY] public bool bEnableGlobalState;
	[UPROPERTY] public FPackageIndex KeyInputEvent;

	[UPROPERTY] public ImageProperty BackgroundImageProperty;
	[UPROPERTY] public ImageProperty FocusedBackgroundImageProperty;
}

public class UBnsCustomGraphMapWidget : UBnsCustomBaseWidget
{
	[UPROPERTY] public float ArrowLength;
	[UPROPERTY] public float ArrowWidth;
	[UPROPERTY] public FPackageIndex[] GraphEdgeImageArray;
	[UPROPERTY] public float LineSize;
	[UPROPERTY] public int MaxLineNumHorizontal;
	[UPROPERTY] public int MaxLineNumVertical;
	[UPROPERTY] public BnsAlignment CenterPos_AlignmentHorizontal;
	[UPROPERTY] public float ColumnGap;
	[UPROPERTY] public float RowGap;
	[UPROPERTY] public FVector2D Background_Padding;
	[UPROPERTY] public FVector2D RatioToAdjustCenterPosBound;
	[UPROPERTY] public float MinNodeSizeHorizontal;
	[UPROPERTY] public float MinNodeSizeVertical;
	[UPROPERTY] public float MinNodeNumHorizontal;
	[UPROPERTY] public float MinNodeNumVertical;
	[UPROPERTY] public FVector2D CenterPos;
	[UPROPERTY] public FStructFallback[] EdgeArray;
	[UPROPERTY] public FStructFallback[] NodeArray;
	[UPROPERTY] public FStructFallback[] HorizontalRulerInfoArray;
	[UPROPERTY] public FStructFallback[] VerticalRulerInfoArray;
	[UPROPERTY] public float MaxZoomRatio;
	[UPROPERTY] public float MinZoomRatio;	

	[UPROPERTY] public FPackageIndex NodePresedEvent;
	[UPROPERTY] public FPackageIndex NodeActiveEvent;
	[UPROPERTY] public FPackageIndex EdgePresedEvent;
	[UPROPERTY] public FPackageIndex EdgeActiveEvent;
}

public class UBnsCustomImageWidget : UBnsCustomBaseWidget
{
	public bool EnableResourceSize;
}

public class UBnsCustomLabelWidget : UBnsCustomBaseWidget
{
	
}

public class UBnsCustomLabelButtonWidget : UBnsCustomBaseWidget
{
	[UPROPERTY] public ImageProperty NormalImageProperty;
	[UPROPERTY] public ImageProperty ActivatedImageProperty;
	[UPROPERTY] public ImageProperty PressedImageProperty;
	[UPROPERTY] public ImageProperty DisableImageProperty;
	[UPROPERTY] public ImageProperty ActiveOnlyImageProperty;
	[UPROPERTY] public ImageProperty CheckedNormalImageProperty;
	[UPROPERTY] public ImageProperty CheckedActivatedImageProperty;
	[UPROPERTY] public ImageProperty CheckedPressedImageProperty;
	[UPROPERTY] public ImageProperty CheckedDisableImageProperty;
	[UPROPERTY] public ImageProperty CheckedActiveOnlyImageProperty;

	[UPROPERTY] public FPackageIndex PresedEvent;  // SOUND
}

public class UBnsCustomProgressBarWidget : UBnsCustomBaseWidget
{
}

public class UBnsCustomScrollBarWidget : UBnsCustomBaseWidget
{
	[UPROPERTY] public bool ScrollHide;
}

public class UBnsCustomSliderBarWidget : UBnsCustomBaseWidget
{
	[UPROPERTY] public ImageProperty BackgroundImageProperty;
	[UPROPERTY] public ImageProperty HighlightImageProperty;
	[UPROPERTY] public Orientation SliderOrientation;
	[UPROPERTY] public bool SliderSnap;
	[UPROPERTY] public float SliderStepValue;
	[UPROPERTY] public bool bReverseDirection;

}

public class UBnsCustomToggleButtonWidget : UBnsCustomLabelButtonWidget
{
	[UPROPERTY] public bool bIsChecked;
	[UPROPERTY] public string ToggleType;   //BNSCustomToggleType_Toggle
}

public class UBnsCustomUISceneWidget : UBnsCustomBaseWidget
{

}

public class UBnsCustomWindowWidget : UBnsCustomBaseWidget
{
	[UPROPERTY] public FPackageIndex WidgetPreset;
}

public class UBnsUISceneGroupUserWidget : USerializeObject
{

}

public class UBnsWebBrowser : UBnsCustomBaseWidget
{

}