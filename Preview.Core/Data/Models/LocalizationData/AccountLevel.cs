using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed unsafe class AccountLevel : Record
{
	public Ref<Text> Name;

	public long Exp;
}