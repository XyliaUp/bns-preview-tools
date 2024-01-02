using System.Runtime.CompilerServices;
using System.Text;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Engine.BinData.Serialization;
internal class AliasTableBuilder
{
	private readonly Node _rootNode = new Node();

	public AliasTableBuilder(AliasTable alias)
	{
		foreach (var record in alias.Table)
			AddAliasManually(record.Key, record.Value);
	}



	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddAliasManually(string fullAlias, Ref @ref)
	{
		Add(_rootNode, @ref, fullAlias);
	}

	public AliasTableArchive EndRebuilding()
	{
		var _target = new AliasTableArchive();
		_target.Entries = [];

		Optimize(_rootNode);
		Rebuild(_target, _rootNode);
		_target.RootEntry.Begin = _rootNode.Begin;
		_target.RootEntry.End = _rootNode.End;

		return _target;
	}


	private static void Optimize(Node currentNode, bool isRoot = true)
	{
		foreach (var (key, node) in currentNode.Children)
		{
			if (node.Children != null)
			{
				Optimize(node, false);

				if (node.Children.Count == 1)
				{
					var childNodePair = node.Children.First();

					if (!isRoot || !childNodePair.Value.IsLeaf)
					{
						var newKey = key + childNodePair.Key;
						currentNode.Children.Remove(key);
						currentNode.Children[newKey] = childNodePair.Value;
					}
				}
			}
		}
	}

	/// <summary>
	/// Only called on leafs
	/// </summary>
	private static void Rebuild(AliasTableArchive aliasTable, Node currentNode)
	{
		foreach (var node in currentNode.Children.Values)
		{
			if (node.IsLeaf)
				Rebuild(aliasTable, node);
		}

		var begin = (uint)aliasTable.Entries.Count;

		foreach (var (key, node) in currentNode.Children.OrderBy(x => x.Key, KoreanStringComparer.Instance))
		{
			var entry = new AliasTableArchiveEntry
			{
				Begin = node.Begin,
				End = node.End,
				String = key
			};
			aliasTable.Entries.Add(entry);
		}

		var end = (uint)aliasTable.Entries.Count - 1; // probably has to be - 1

		currentNode.Begin = begin << 1;
		currentNode.End = end;
	}

	private static void Add(Node currentNode, Ref @ref, ReadOnlySpan<char> alias)
	{
		while (true)
		{
			var index = alias.IndexOfAny('_', '.', ':') + 1;

			if (index == 0) // no more parts means it's value
			{
				currentNode.Children[alias.ToString()] = new Node(@ref);
			}
			else // just another leaf
			{
				var currentPart = alias[..index].ToString();
				if (!currentNode.Children.TryGetValue(currentPart, out var node))
				{
					node = new Node();
					currentNode.Children[currentPart] = node;
				}

				currentNode = node;
				alias = alias[index..];
				continue;
			}

			break;
		}
	}



	private class Node
	{
		public Dictionary<string, Node> Children { get; init; }
		public uint Begin { get; set; }
		public uint End { get; set; }
		public bool IsLeaf => (Begin & 1) == 0;

		public Node()
		{
			Children = new Dictionary<string, Node>();
		}

		public Node(Ref r)
		{
			var v = ((long)r << 1) | 1;
			Begin = (uint)v;
			End = (uint)(v >> 32);
		}

		public override string ToString() => $"{Begin >> 1}-{End} IsLeaf:{IsLeaf}";
	}

	private class KoreanStringComparer : IComparer<string>
	{
		public static readonly KoreanStringComparer Instance = new();
		private static readonly Encoding KoreanEncoding = CodePagesEncodingProvider.Instance.GetEncoding(949);

		private static unsafe int strcmp(byte* p1, byte* p2)
		{
			while (*p1 != 0 && *p1 == *p2)
			{
				++p1;
				++p2;
			}

			return (*p1 > *p2 ? 1 : 0) - (*p2 > *p1 ? 1 : 0);
		}

		public unsafe int Compare(string x, string y)
		{
			var b1 = KoreanEncoding.GetBytes(x + "\0");
			var b2 = KoreanEncoding.GetBytes(y + "\0");

			fixed (byte* p1 = b1)
			fixed (byte* p2 = b2)
				return strcmp(p1, p2);
		}
	}
}