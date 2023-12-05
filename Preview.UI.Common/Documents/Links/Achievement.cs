namespace Xylia.Preview.UI.Documents.Links;
/// <summary>
/// achievement:291_event_SoulEvent_Extreme_0004_step1:123.3.0.1.1.1.626f57f5.1.0.0.0.1
/// </summary>
public sealed class Achievement : LinkId
{
	public string alias;

	internal override void Load(string text)
	{
		var data = text.Split('.');

		alias = data[0];
	}
}