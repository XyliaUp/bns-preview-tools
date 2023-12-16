using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public class UBnsCustomColumnListWidget : UBnsCustomBaseWidget
{

}

public class UBnsCustomEditBoxWidget : UBnsCustomBaseWidget
{
}

public class UBnsCustomGraphMapWidget : UBnsCustomBaseWidget
{
}

public class UBnsCustomImageWidget : UBnsCustomBaseWidget
{
	//public BaseImage BaseImageProperty;
	//public bool bEnableLeftButton;
	//public bool bEnableRightButton;


	public bool bNeverActivate;
	public bool EnableResourceSize;
}

public class UBnsCustomLabelWidget : UBnsCustomBaseWidget
{
	//[UPROPERTY] public ImageProperty? NormalImageProperty;
	[UPROPERTY] public ImageProperty? ActivatedImageProperty;
	[UPROPERTY] public ImageProperty? PressedImageProperty;
	[UPROPERTY] public ImageProperty? DisableImageProperty;
	[UPROPERTY] public ImageProperty? ActiveOnlyImageProperty;
}

public class UBnsCustomLabelButtonWidget : UBnsCustomBaseWidget
{
}

public class UBnsCustomProgressBarWidget : UBnsCustomBaseWidget
{
}

public class UBnsCustomScrollBarWidget : UBnsCustomBaseWidget
{
}

public class UBnsCustomSliderBarWidget : UBnsCustomBaseWidget
{
	//SliderOrientation
	//SliderStepValue
}

public class UBnsCustomToggleButtonWidget : UBnsCustomBaseWidget
{
	[UPROPERTY] public bool bIsChecked;
	[UPROPERTY] public string ToggleType;	//BNSCustomToggleType_Toggle
	[UPROPERTY] public ImageProperty CheckedActivatedImageProperty;
	[UPROPERTY] public ImageProperty CheckedPressedImageProperty;
	[UPROPERTY] public ImageProperty CheckedDisableImageProperty;
	[UPROPERTY] public ImageProperty CheckedActiveOnlyImageProperty;
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