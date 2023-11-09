using Xylia.Preview.Data.Engine.BinData.Serialization;
using Xylia.Preview.Data.Engine.Readers;

namespace Xylia.Preview.Data.Engine.BinData.Models;
public class LazyNameTable : NameTable
{
	private readonly INameTableReader _nameTableReader;
	private List<NameTableEntry> _entries;

	public LazyNameTable(INameTableReader nameTableReader)
	{
		_nameTableReader = nameTableReader;
	}
	public DatafileArchive Source { get; internal set; }
	public bool EntriesLoaded => _entries != null;

	public override List<NameTableEntry> Entries
	{
		get
		{
			if (_entries != null)
				return _entries;

			var nameTable = _nameTableReader.ReadFrom(Source);
			_entries = nameTable.Entries;

			return _entries;
		}
	}

	public override void Clear()
	{
		_entries = new List<NameTableEntry>();
	}
}