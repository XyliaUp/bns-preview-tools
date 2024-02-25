using System.ComponentModel;
using System.Globalization;
using CUE4Parse.UE4;
using CUE4Parse.UE4.Assets.Utils;
using CUE4Parse.UE4.Objects.Core.i18N;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;
namespace CUE4Parse.BNS.Assets.Exports;

[StructFallback]
[TypeConverter(typeof(StringPropertyConverter))]
public class StringProperty : IUStruct
{
	[TypeConverter(typeof(FPackageIndexTypeConverter))] public FPackageIndex fontset { get; set; }
	[TypeConverter(typeof(FTextTypeConverter))] public FText LabelText { get; set; }
	public float SpaceBetweenLines { get; set; }
	public HAlignment HorizontalAlignment { get; set; }
	public VAlignment VerticalAlignment { get; set; }
	[TypeConverter(typeof(Vector2DConverter))] public FVector2D ClippingBound { get; set; }
	public ClipMode ClipMode { get; set; }
	public int MaxCharacters { get; set; }

	[TypeConverter(typeof(Vector2DConverter))] public FVector2D Padding { get; set; }
	public string ClippingBoundFace_Horizontal { get; set; }  //WidgetFaceFace_Left
	public string ClippingBoundFace_Vertical { get; set; }    //WidgetFaceFace_Top
	public bool bJustification { get; set; }
	public string JustificationType { get; set; }             //BNSCustomJustification_Type_Normal
	public bool bWordWrap { get; set; }
	public bool bIgnoreDPIScale { get; set; }
	public float Opacity { get; set; }
	public string TextDirection { get; set; }             //BNS_UIORIENT_Horizontal
	public float TextScale { get; set; }
	public float AnimScale { get; set; }
	public float LastRenderWidth { get; set; }
	public float LastRenderHeight { get; set; }

	#region Methods
	public StringProperty Clone()
	{
		return (StringProperty)this.MemberwiseClone();
	}
	#endregion
}

internal class StringPropertyConverter : StrConverter
{
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value is string s)
		{
			return new StringProperty() { LabelText = new FText(s) };
		}

		return base.ConvertFrom(context, culture, value);
	}
}