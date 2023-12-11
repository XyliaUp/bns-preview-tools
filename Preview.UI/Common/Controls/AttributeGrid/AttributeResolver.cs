using HandyControl.Controls;
using HandyControl.Properties.Langs;

using Xylia.Preview.Data.Engine.BinData.Definitions;
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

	public bool ResolveIsBrowsable(AttributeDefinition attribute) => !attribute.IsDeprecated;

	public bool ResolveIsReadOnly(AttributeDefinition attribute) => true;

	public object ResolveDefaultValue(AttributeDefinition attribute) => attribute.DefaultValue;

	public PropertyEditorBase ResolveEditor(AttributeDefinition attribute) => attribute.Type switch
	{
		AttributeType.TInt8 => new NumberAttributeEditor(attribute),
		AttributeType.TInt16 => new NumberAttributeEditor(attribute),
		AttributeType.TInt32 => new NumberAttributeEditor(attribute),
		AttributeType.TInt64 => new NumberAttributeEditor(attribute),
		AttributeType.TFloat32 => new NumberAttributeEditor(attribute),
		AttributeType.TBool => new BooleanPropertyEditor(),
		AttributeType.TString => new PlainTextPropertyEditor(),
		AttributeType.TSeq => new SequenceAttributeEditor(attribute.Sequence),
		AttributeType.TSeq16 => new SequenceAttributeEditor(attribute.Sequence),
		AttributeType.TRef => new ReferenceAttributeEditor(attribute.ReferedTableName),
		AttributeType.TTRef => new ReferenceAttributeEditor(null),
		//AttributeType.TSub => new PlainTextPropertyEditor(),
		//AttributeType.TSu => new PlainTextPropertyEditor(),
		//AttributeType.TVector16 => new PlainTextPropertyEditor(),
		//AttributeType.TVector32 => new PlainTextPropertyEditor(),
		//AttributeType.TIColor => new PlainTextPropertyEditor(),
		//AttributeType.TFColor => new PlainTextPropertyEditor(),
		//AttributeType.TBox => new PlainTextPropertyEditor(),
		//AttributeType.TAngle => new PlainTextPropertyEditor(),
		//AttributeType.TMsec => new PlainTextPropertyEditor(),
		//AttributeType.TDistance => new PlainTextPropertyEditor(),
		//AttributeType.TVelocity => new PlainTextPropertyEditor(),
		AttributeType.TProp_seq => new SequenceAttributeEditor(attribute.Sequence),
		AttributeType.TProp_field => new SequenceAttributeEditor(attribute.Sequence),
		AttributeType.TScript_obj => new ReadOnlyTextPropertyEditor(),
		AttributeType.TNative => new PlainTextPropertyEditor(),
		//AttributeType.TVersion => new PlainTextPropertyEditor(),
		AttributeType.TIcon => new IconAttributeEditor(),
		//AttributeType.TTime32 => new PlainTextPropertyEditor(),
		AttributeType.TTime64 => new TimeAttributeEditor(),
		AttributeType.TXUnknown1 => new TimeAttributeEditor(),
		AttributeType.TXUnknown2 => new PlainTextPropertyEditor(),
		_ => new ReadOnlyTextPropertyEditor()
	};
}