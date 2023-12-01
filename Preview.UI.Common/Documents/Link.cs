using System.Diagnostics;

using HtmlAgilityPack;

namespace Xylia.Preview.UI.Documents;
public class Link : Element
{
	public bool IgnoreInput;
	public string Id;

	protected override void Load(HtmlNode node)
	{
		base.Load(node);

		if (string.IsNullOrWhiteSpace(Id) || Id == "none")
			return;

		var tmp = Id.Split(':');
		var type = tmp[0]?.Trim();


		LinkId test;
		switch (type)
		{
			case "item-name": test = new Links.ItemName(); break;
			case "tooltip": test = new Links.Tooltip(); break;

			default: Debug.WriteLine($"link type `{type}` not supported!"); return;
		}

		test.tagData = this;
		test.Load(new ContentParams(tmp[1]?.Split('.')));
	}
}


public abstract class LinkId
{
	internal Link tagData;

	internal abstract void Load(ContentParams data);
}