using System.ComponentModel;
using CUE4Parse.UE4;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Assets.Utils;
using CUE4Parse.UE4.Objects.Core.i18N;
using CUE4Parse.UE4.Objects.UObject;
namespace CUE4Parse.BNS.Assets.Exports;

[StructFallback]
public class ExpansionComponent : IUStruct
{
	public bool bEnableSubState { get; set; }
	public bool bPostExpansitonRender { get; set; }
	public bool bShow { get; set; }
	public bool bVisibleFlag { get; set; }
	[TypeConverter(typeof(FNameConverter))] public FName ExpansionType { get; set; }
	[TypeConverter(typeof(FNameConverter))] public FName ExpansionName { get; set; }
	public string MetaData { get; set; }
	public string WidgetState { get; set; }    //BNSCustomWidgetState_None
	public string WidgetSubState { get; set; } //Expansion_WidgetSubState_Normal
	public FStructFallback PublisherVisible { get; set; }
	public FStructFallback MetaDataByPublisher { get; set; }
	public ImageProperty ImageProperty { get; set; }
	public StringProperty StringProperty { get; set; }


	#region Methods
	public const string Type_IMAGE = "IMAGE";
	public const string Type_STRING = "STRING";

	public ExpansionComponent Clone()
	{
		var component = (ExpansionComponent)this.MemberwiseClone();
		component.ImageProperty = ImageProperty?.Clone();
		component.StringProperty = StringProperty?.Clone();

		return component;
	}

	public void SetValue(object value)
	{
		if (ExpansionType == Type_STRING)
		{
			if (value is StringProperty p) StringProperty = p;
			else StringProperty.LabelText = new FText(value?.ToString());
		}
		else if (ExpansionType == Type_IMAGE)
		{
			if (value is ImageProperty p) ImageProperty = p;
			else ImageProperty.BaseImageTexture = (FPackageIndex)value;
		}
		else throw new NotSupportedException();
	}
	#endregion
}