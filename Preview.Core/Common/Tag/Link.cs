using HtmlAgilityPack;

using Xylia.Extension;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Tag.Link;

namespace Xylia.Preview.Common.Tag;
public sealed class LinkTag	: ITag
{
	#region Fields
	public readonly bool IgnoreInput;

	public readonly string id;
	#endregion


	#region Constructor
	public LinkTag(HtmlNode node)
	{
		IgnoreInput = (node.Attributes["ignoreinput"]?.Value).ToBool();
		id = node.Attributes["id"]?.Value;

		if (string.IsNullOrWhiteSpace(id) || id == "none")
			return;


		var tmp = id.Split(':');
		var type = tmp[0]?.Trim();


		LinkId test;
		switch (type)
		{
			case "item-name": test = new ItemName(); break;
			case "tooltip": test = new Tooltip(); break;

			default: Debug.WriteLine($"link type `{type}` not supported!"); return;
		}

		test.tagData = this;
		test.Load(new Params<string>(tmp[1]?.Split('.')));
	}
	#endregion
}



//achievement:291_event_SoulEvent_Extreme_0004_step1:123.3.0.1.1.1.626f57f5.1.0.0.0.1
//skill:SRK_B1_DollQueen_AirBomb

public abstract class LinkId
{
	internal LinkTag tagData;

	internal abstract void Load(Params<string> data);
}