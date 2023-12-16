using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

using HtmlAgilityPack;

using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.UI.Documents;
public class Link : Element
{
	[Name("ignore-input")]
	public bool IgnoreInput;
	public LinkId Id;

	protected internal override void Load(HtmlNode node)
	{
		Children = node.ChildNodes.Select(TextDocument.ToElement).ToList();

		//IgnoreInput = (node.Attributes[nameof(IgnoreInput)]?.Value).ToBool();
		var data = node.Attributes[nameof(Id)]?.Value;
		if (string.IsNullOrWhiteSpace(data) || data == "none") return;


		// split
		var tmp = data.Split(':', 2);
		var type = tmp[0]?.Trim();
		switch (type)
		{
			case "item-name": Id = new Links.ItemName(); break;
			case "tooltip": Id = new Links.Tooltip(); break;

			default: Debug.WriteLine($"link type `{type}` not supported!"); return;
		}

		Id.Load(tmp[1]);

		this.MouseEnter += Id.OnMouseEnter;
		this.MouseLeave += Id.OnMouseLeave;
		this.MouseLeftButtonDown += Id.OnMouseLeftButtonDown;
		this.MouseRightButtonDown += Id.OnMouseRightButtonDown;
	}
}

public abstract class LinkId
{
	internal abstract void Load(string text);

	internal virtual void OnMouseEnter(object sender, MouseEventArgs e)
	{
		
	}

	internal virtual void OnMouseLeave(object sender, MouseEventArgs e)
	{
		
	}

	internal virtual void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		Debug.WriteLine("MouseLeftButtonDown: " + this);
	}

	internal virtual void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
	{
		
	}
}