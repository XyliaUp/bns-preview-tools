using System.Diagnostics;
using System.Xml;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.Exceptions;

namespace Xylia.Preview.Data.Engine.Definitions;
public class SequenceDefinitionLoader
{
	private readonly Dictionary<string, SequenceDefinition> _duplicateSequences = [];


	#region Methods
	internal SequenceDefinition Load(XmlElement element, string name)
	{
		SequenceDefinition sequence = new(name);

		var nodes = element.ChildNodes.OfType<XmlElement>();
		if (nodes.Any())
		{
			short key = 0;
			foreach (var node in nodes)
			{
				#region key
				short _key;

				string Key = node.Attributes["key"]?.Value;
				if (string.IsNullOrWhiteSpace(Key)) _key = key++;
				else if (!short.TryParse(Key, out _key)) continue;

				key = (short)Math.Max(key, _key + 1);

				string text = node.Attributes["name"]?.Value?.Trim() ?? "unk" + key;
				#endregion

				sequence.Add(text);
				if ((node.Attributes["default"]?.Value).ToBool())
				{
					if (sequence.Default is not null)
						throw BnsDataException.InvalidSequence($"duplicate default value. (prev:{sequence.Default}, now:{text})", name);

					sequence.Default = text;
				}
			}
		}
		else
		{
			var SeqName = element.Attributes["seq"]?.Value?.Trim();
			if (string.IsNullOrWhiteSpace(SeqName)) return null;

			if (!_duplicateSequences.TryGetValue(SeqName, out var TSeq))
			{
				Trace.WriteLine($"seq `{SeqName}` not defined");
				return null;
			}

			sequence = TSeq.Clone();
			sequence.Name = name;
		}

		return sequence;
	}

	//public List<SequenceDefinition> LoadFor(IEnumerable<TableDefinition> tableDefs, bool mergeDuplicated)
	//{
	//	var allSequenceDefinitions = new List<SequenceDefinition>();

	//	foreach (var tableDef in tableDefs)
	//	{
	//		LoadForTable(tableDef.ElRecord, allSequenceDefinitions, mergeDuplicated);

	//		foreach (var subtableDef in tableDef.ElRecord.Subtables)
	//		{
	//			LoadForTable(subtableDef, allSequenceDefinitions, mergeDuplicated);
	//		}
	//	}

	//	return allSequenceDefinitions;
	//}

	//private void LoadForTable(ITableDefinition tableDef, List<SequenceDefinition> allSequenceDefinitions, bool mergeDuplicated)
	//{
	//	List<SequenceDefinition> sequenceDefList = null;

	//	foreach (var attrDef in tableDef.Attributes)
	//	{
	//		if (attrDef.Sequence.Count == 0)
	//			continue;

	//		var first = attrDef.Sequence[0];

	//		if (mergeDuplicated)
	//		{
	//			if (!_duplicateSequences.TryGetValue(first, out sequenceDefList))
	//			{
	//				sequenceDefList = new List<SequenceDefinition>();
	//				_duplicateSequences[first] = sequenceDefList;
	//			}

	//			foreach (var sequenceDef in sequenceDefList)
	//			{
	//				if (sequenceDef.Size != attrDef.Size)
	//					continue;

	//				if (!IsSequenceEqual(sequenceDef.Sequence, attrDef.Sequence, StringComparer.OrdinalIgnoreCase))
	//					continue;

	//				if (attrDef.Sequence.Count > sequenceDef.Sequence.Count)
	//				{
	//					for (var i = sequenceDef.Sequence.Count; i < attrDef.Sequence.Count; i++)
	//					{
	//						sequenceDef.Sequence.Add(attrDef.Sequence[i]);
	//						sequenceDef.OriginalSequence.Add(attrDef.Sequence[i]);
	//					}
	//				}

	//				// Assign sequence definition
	//				attrDef.SequenceDef = sequenceDef;

	//				goto FOUND_SEQUENCE;
	//			}
	//		}

	//		// Create new one if we didn't find existing one
	//		var newSequenceDef = new SequenceDefinition(attrDef.Name, attrDef.Size); // Use attribute name as sequence name
	//		newSequenceDef.Sequence.AddRange(attrDef.Sequence);
	//		newSequenceDef.OriginalSequence.AddRange(attrDef.Sequence);

	//		if (mergeDuplicated)
	//			sequenceDefList.Add(newSequenceDef);
	//		allSequenceDefinitions.Add(newSequenceDef);

	//		// Assign sequence definition
	//		attrDef.SequenceDef = newSequenceDef;

	//		FOUND_SEQUENCE:;
	//	}
	//}

	//private static bool IsSequenceEqual(IEnumerable<string> a, IEnumerable<string> b, StringComparer comparer)
	//{
	//	using var enumeratorA = a.GetEnumerator();
	//	using var enumeratorB = b.GetEnumerator();

	//	while (enumeratorA.MoveNext() & enumeratorB.MoveNext())
	//	{
	//		if (comparer.Equals(enumeratorA.Current, enumeratorB.Current))
	//			continue;

	//		return false;
	//	}

	//	return true;
	//}

	internal static SequenceDefinitionLoader LoadFrom(params string[] XmlContents)
	{
		var param = new SequenceDefinitionLoader();
		foreach (var content in XmlContents)
		{
			var xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(content);

			foreach (XmlElement record in xmlDoc.SelectNodes("table/record"))
			{
				string name = record.Attributes["name"]?.Value?.Trim();
				if (param._duplicateSequences.ContainsKey(name))
					throw BnsDataException.InvalidSequence($"has existed", name);

				var seq = param.Load(record, name);
				if (seq != null) param._duplicateSequences[name] = seq;
			}
		}

		return param;
	}
	#endregion
}