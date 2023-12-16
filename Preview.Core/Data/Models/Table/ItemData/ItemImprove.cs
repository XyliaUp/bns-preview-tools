using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
public sealed class ItemImprove : ModelElement
{
	public int Id;

	public sbyte Level;

	[Name("success-option-list-id")]
	public int SuccessOptionListId;
}