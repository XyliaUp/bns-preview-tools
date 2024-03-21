using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Engine.BinData.Models;
internal class AliasTable
{
	#region Methods
	public void Add(Ref Ref, string text)
	{
		if (Table.ContainsKey(text))
		{
			//Log.Warning(string.Format("already contains alias {0}.", text));
			return;
		}

		Table[text] = Ref;
	}

	public void Add(Record record)
	{
		var alias = record.Attributes.Get<string>("alias");
		Add(record.PrimaryKey, MakeKey(record.Owner.Name, alias));
	}

	public Ref Find(string fullAlias)
	{
		if (!string.IsNullOrEmpty(fullAlias))
		{
			if (Table.TryGetValue(fullAlias, out var value)) return value;
			// Log.Warning($"cannot found alias map: {fullAlias}");
		}

		return default;
	}

	internal static string MakeKey(string tableDefName, string alias)
	{
		return string.Format("{0}:{1}", tableDefName, alias);
	}
	#endregion

	#region Data
	internal virtual Dictionary<string, Ref> Table { get; } = new(StringComparer.OrdinalIgnoreCase);
	#endregion
}