using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class SlateScrollStone : Record
{
	public Ref<SlateScroll> scroll;

	public Ref<SlateStone> stone; 

	public bool tooltip; 
}