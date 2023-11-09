using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class Effect : Record
{
	public string Alias;

	public Ref<Text> Name2;

	public Ref<Text> Name3;

	public Ref<Text> Description2;

	public Ref<Text> Description3;
}