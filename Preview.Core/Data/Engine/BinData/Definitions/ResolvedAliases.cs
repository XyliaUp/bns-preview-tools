using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Engine.BinData.Definitions;
public class ResolvedAliases
{
	public Dictionary<int, Dictionary<Ref, string>> ByRef = new Dictionary<int, Dictionary<Ref, string>>();
	public Dictionary<int, Dictionary<string, Ref>> ByAlias = new Dictionary<int, Dictionary<string, Ref>>();
}