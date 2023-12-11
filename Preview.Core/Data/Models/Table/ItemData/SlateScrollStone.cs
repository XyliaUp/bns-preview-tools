using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class SlateScrollStone : ModelElement
{
	public Ref<SlateScroll> scroll;

	public Ref<SlateStone> stone; 

	public bool tooltip; 
}