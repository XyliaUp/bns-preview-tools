using Xylia.Preview.UI.Common.Documents.Args;

namespace Xylia.Preview.UI.Common.Documents.Links;
/// <summary>
/// skill:SRK_B1_DollQueen_AirBomb
/// </summary>
public sealed class Skill : LinkId
{
	internal override void Load(ContentParams data)
	{
		var alias = data[1];

		//tagData.ClickEvent = new((o, e) => PreviewRegister.Preview(FileCache.Data.Skill3[alias]));
	}
}