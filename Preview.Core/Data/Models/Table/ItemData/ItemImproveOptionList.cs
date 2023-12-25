using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public sealed class ItemImproveOptionList : ModelElement
{
	public int Id { get; set; }

	public JobSeq Job { get; set; }
}