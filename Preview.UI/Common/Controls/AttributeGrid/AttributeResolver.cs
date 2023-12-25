using HandyControl.Controls;
using HandyControl.Properties.Langs;
using Xylia.Preview.Data.Engine.Definitions;
using Xylia.Preview.UI.Common.Controls.AttributeGrid.Editor;

namespace Xylia.Preview.UI.Controls;
public class AttributeResolver
{
	public string ResolveCategory(AttributeDefinition attribute)
	{
		//var categoryAttribute = propertyDescriptor.Attributes.OfType<CategoryAttribute>().FirstOrDefault();

		//return categoryAttribute == null ?
		//	Lang.Miscellaneous :
		//	string.IsNullOrEmpty(categoryAttribute.Category) ?
		//		Lang.Miscellaneous :
		//		categoryAttribute.Category;

		return Lang.Miscellaneous;
	}

	public string ResolveDisplayName(AttributeDefinition attribute) => attribute.Name;

	public string ResolveDescription(AttributeDefinition attribute) => $"{attribute.Type}";

	public bool ResolveIsBrowsable(AttributeDefinition attribute) => !attribute.IsDeprecated && attribute.CanInput;

	public object ResolveDefaultValue(AttributeDefinition attribute) => attribute.DefaultValue;

	public PropertyEditorBase ResolveEditor(AttributeDefinition attribute) => attribute.Type switch
	{
		AttributeType.TInt8 => new NumberAttributeEditor(attribute),
		AttributeType.TInt16 => new NumberAttributeEditor(attribute),
		AttributeType.TInt32 => new NumberAttributeEditor(attribute),
		AttributeType.TInt64 => new NumberAttributeEditor(attribute),
		AttributeType.TFloat32 => new NumberAttributeEditor(attribute),
		AttributeType.TBool => new BooleanPropertyEditor(),
		AttributeType.TString => new StringPropertyEditor(),
		AttributeType.TSeq => new SequenceAttributeEditor(attribute.Sequence),
		AttributeType.TSeq16 => new SequenceAttributeEditor(attribute.Sequence),
		AttributeType.TRef => new ReferenceAttributeEditor(attribute.ReferedTableName),
		AttributeType.TTRef => new ReferenceAttributeEditor(null),
		//AttributeType.TSub => new tex(),
		//AttributeType.TSu => new TextPropertyEditor(),
		//AttributeType.TVector16 => new TextPropertyEditor(),
		//AttributeType.TVector32 => new TextPropertyEditor(),
		//AttributeType.TIColor => new TextPropertyEditor(),
		//AttributeType.TFColor => new TextPropertyEditor(),
		//AttributeType.TBox => new TextPropertyEditor(),
		//AttributeType.TAngle => new TextPropertyEditor(),
		//AttributeType.TMsec => new TextPropertyEditor(),
		//AttributeType.TDistance => new TextPropertyEditor(),
		//AttributeType.TVelocity => new TextPropertyEditor(),
		AttributeType.TProp_seq => new SequenceAttributeEditor(attribute.Sequence),
		AttributeType.TProp_field => new SequenceAttributeEditor(attribute.Sequence),
		AttributeType.TScript_obj => new ReadOnlyTextPropertyEditor(),
		AttributeType.TNative => new StringPropertyEditor(),
		//AttributeType.TVersion => new TextPropertyEditor(),
		AttributeType.TIcon => new IconAttributeEditor(),
		//AttributeType.TTime32 => new TextPropertyEditor(),
		AttributeType.TTime64 => new TimeAttributeEditor(),
		AttributeType.TXUnknown1 => new TimeAttributeEditor(),
		AttributeType.TXUnknown2 => new StringPropertyEditor(),
		_ => new ReadOnlyTextPropertyEditor()
	};
}