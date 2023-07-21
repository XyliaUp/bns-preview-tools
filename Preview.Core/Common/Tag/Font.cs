using HtmlAgilityPack;

namespace Xylia.Preview.Common.Tag;
public sealed class Font : ITag
{
	#region Fields
	public readonly string name;
	#endregion

	#region Constructor
	public Font(HtmlNode node)
	{
		name = node.Attributes["name"]?.Value;
	}
	#endregion
}