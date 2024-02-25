namespace Xylia.Preview.UI.Documents.Links;
public class Skill : LinkId
{
	public string alias;

	internal override void Load(string text)
	{
		alias = text;
	}
}