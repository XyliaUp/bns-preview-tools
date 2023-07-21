using System.Collections;

namespace Xylia.Preview.Data.Models.BinData.Table.Record.Attributes;
public interface IAttributeCollection : IEnumerable, IEnumerable<KeyValuePair<string, string>>
{
	string this[string param, int index = 0, bool convert = false] { get; }


	bool ContainsKey(string Name, out string Value);

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}