using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models;
public sealed class ItemImprove : Record
{
	public int Id;

	public sbyte Level;

	[Name("success-option-list-id")]
	public int SuccessOptionListId;
}