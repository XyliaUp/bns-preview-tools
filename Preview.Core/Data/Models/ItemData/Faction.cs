using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class Faction : Record
{
	public string Alias;



	public Ref<Text> Name2;

	public Ref<Text> TagName;


	public string Icon;

	public Ref<Text> Text;
}