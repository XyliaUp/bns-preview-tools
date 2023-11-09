using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class SealedDungeonGimmick : Record
{
	public string Alias;


	public Ref<Text> Name;

	[Name("icon-name")]
	public Ref<Text> IconName;

	[Name("icon-tooltip")]
	public Ref<Text> IconTooltip;

	public string Icon;
}