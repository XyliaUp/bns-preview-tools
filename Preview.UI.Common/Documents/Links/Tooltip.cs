namespace Xylia.Preview.UI.Documents.Links;
internal sealed class Tooltip : LinkId
{
	public string alias;

	internal override void Load(ContentParams data)
	{
		alias = data[1] as string;
	}
}