using HandyControl.Controls;
using HandyControl.Properties.Langs;

using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Helpers;

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

	public string ResolveDescription(AttributeDefinition attribute) => attribute.Name;

	public bool ResolveIsBrowsable(AttributeDefinition attribute) => !attribute.IsDeprecated;

	public bool ResolveIsReadOnly(AttributeDefinition attribute) => true;

	public object ResolveDefaultValue(AttributeDefinition attribute) => attribute.DefaultValue;

	public PropertyEditorBase ResolveEditor(AttributeDefinition attribute)
	{
		return attribute.Type switch
		{
			AttributeType.TNone => new ReadOnlyTextPropertyEditor(),
			AttributeType.TInt8 => new NumberAttributeEditor(sbyte.MinValue, sbyte.MaxValue),
			AttributeType.TInt16 => new NumberAttributeEditor(short.MinValue, short.MaxValue),
			AttributeType.TInt32 => new NumberAttributeEditor(int.MinValue, int.MaxValue),
			AttributeType.TInt64 => new NumberAttributeEditor(long.MinValue, long.MaxValue),
			AttributeType.TFloat32 => new NumberAttributeEditor(float.MinValue, float.MaxValue),
			AttributeType.TBool => new SwitchPropertyEditor(),
			AttributeType.TString => new ReadOnlyTextPropertyEditor(),
			AttributeType.TSeq => new SequenceAttributeEditor(attribute.Sequence),
			AttributeType.TSeq16 => new SequenceAttributeEditor(attribute.Sequence),
			AttributeType.TRef => new ReferenceAttributeEditor(attribute.ReferedTableName),
			AttributeType.TTRef => new ReadOnlyTextPropertyEditor(),
			AttributeType.TSub => new ReadOnlyTextPropertyEditor(),
			AttributeType.TSu => new ReadOnlyTextPropertyEditor(),
			AttributeType.TVector16 => new ReadOnlyTextPropertyEditor(),
			AttributeType.TVector32 => new ReadOnlyTextPropertyEditor(),
			AttributeType.TIColor => new ReadOnlyTextPropertyEditor(),
			AttributeType.TFColor => new ReadOnlyTextPropertyEditor(),
			AttributeType.TBox => new ReadOnlyTextPropertyEditor(),
			AttributeType.TAngle => new ReadOnlyTextPropertyEditor(),
			AttributeType.TMsec => new ReadOnlyTextPropertyEditor(),
			AttributeType.TDistance => new ReadOnlyTextPropertyEditor(),
			AttributeType.TVelocity => new ReadOnlyTextPropertyEditor(),
			AttributeType.TProp_seq => new SequenceAttributeEditor(attribute.Sequence),
			AttributeType.TProp_field => new SequenceAttributeEditor(attribute.Sequence),
			AttributeType.TScript_obj => new ReadOnlyTextPropertyEditor(),
			AttributeType.TNative => new ReadOnlyTextPropertyEditor(),
			AttributeType.TVersion => new ReadOnlyTextPropertyEditor(),
			AttributeType.TIcon => new ReadOnlyTextPropertyEditor(),
			AttributeType.TTime32 => new ReadOnlyTextPropertyEditor(),
			AttributeType.TTime64 => new DateTimePropertyEditor(),
			AttributeType.TXUnknown1 => new ReadOnlyTextPropertyEditor(),
			AttributeType.TXUnknown2 => new ReadOnlyTextPropertyEditor(),
			_ => new ReadOnlyTextPropertyEditor()
		};
	}
}