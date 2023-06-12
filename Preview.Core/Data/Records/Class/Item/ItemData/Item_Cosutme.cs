
using Xylia.Preview.Common.Attribute;
using Xylia.Extension;


namespace Xylia.Preview.Data.Record;

public sealed partial class Item
{
	[Signal("custom-dress-design-state")]
	public CustomDressDesignStateSeq CustomDressDesignState => this.Attributes["custom-dress-design-state"].ToEnum<CustomDressDesignStateSeq>();
	public enum CustomDressDesignStateSeq
	{
		None,
		Disabled,
		Activated,
	}
}
