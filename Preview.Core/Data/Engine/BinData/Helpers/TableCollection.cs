using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Engine.BinData.Helpers;
public class TableCollection : List<Table>
{
	Dictionary<short, Table> _tableByType;
	Dictionary<string, Table> _tableByName;

	public Table this[short index]
	{
		get
		{
			lock(this)
			{
				return (_tableByType ??= this.Where(x => x.Type > 0).ToDictionary(o => o.Type))
					 .GetValueOrDefault(index);
			}
		}
	}

	public Table this[string index]
	{
		get
		{
			lock(this)
			{
				if (string.IsNullOrWhiteSpace(index)) return default;
				if (short.TryParse(index, out var type)) return this[type];
				if (index.Equals("skill", StringComparison.OrdinalIgnoreCase)) index = "skill3";

				return (_tableByName ??= this.ToDictionary(x => x.Name, new TableNameComparer()))
					.GetValueOrDefault(index);
			}
		}
	}
}