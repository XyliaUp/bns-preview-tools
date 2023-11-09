using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Assets.Objects.Properties;
using CUE4Parse.UE4.Assets.Readers;

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




	//public ResolvedObject WidgetPreset;
	public ImageProperty? BaseImageProperty;
	public ImageProperty? NormalImageProperty;
	public List<ExpansionComponent> ExpansionComponentList;

	public StringProperty? StringProperty;

	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);


		StringProperty = Objects.StringProperty.Load(GetOrDefault<FStructFallback>(nameof(StringProperty)));

		BaseImageProperty = ImageProperty.Load(GetOrDefault<FStructFallback>(nameof(BaseImageProperty)));
		NormalImageProperty = ImageProperty.Load(GetOrDefault<FStructFallback>(nameof(NormalImageProperty)));
		ExpansionComponentList = GetOrDefault<UScriptArray>(nameof(ExpansionComponentList))?.Properties
			.Select(p => (p as StructProperty).Value.StructType as FStructFallback)
			.Select(p => new ExpansionComponent(p))
			.ToList();


		MetaData = GetOrDefault<string>(nameof(MetaData));
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
	//public BaseImage BaseImageProperty;
	//public bool bEnableLeftButton;
	//public bool bEnableRightButton;
	//public bool CanAssignWidgetID;


	public bool bNeverActivate;
	public bool EnableResourceSize;
}

public class UBnsCustomLabelWidget : UBnsCustomWidget
{
	//public ImageProperty? NormalImageProperty;
	public ImageProperty? ActivatedImageProperty;
	public ImageProperty? PressedImageProperty;
	public ImageProperty? DisableImageProperty;
	public ImageProperty? ActiveOnlyImageProperty;

	//public FStructFallback StringProperty;
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