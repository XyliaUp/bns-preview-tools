using Xylia.Preview.Common.Arg;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Common.Tag.Link;
internal sealed class Tooltip : LinkId
{
	public string text;

	internal override void Load(Params<string> data)
	{
		text = data[1].GetText();

		//control.SetToolTip(text);
	}
}