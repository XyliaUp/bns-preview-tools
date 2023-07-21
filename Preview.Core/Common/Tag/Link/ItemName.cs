using Xylia.Preview.Common.Arg;
using Xylia.Preview.Data.Helper;

namespace Xylia.Preview.Common.Tag.Link;
internal sealed class ItemName : LinkId
{
	public override void Load(Params<string> data)
	{
		if(_mode == 0)
		{
			var Id = Convert.ToInt32(data[1], 16);
			var Variant = Convert.ToInt32(data[2], 16);
			var StackCount = Convert.ToInt16(data[3], 16);

			tagData.ClickEvent = new((o, e) => PreviewRegister.Preview(FileCache.Data.Item[Id, Variant]));
		}
		else
		{
			var Item = data[1];
			tagData.ClickEvent = new((o, e) => PreviewRegister.Preview(FileCache.Data.Item[Item]));
		}
	}
}