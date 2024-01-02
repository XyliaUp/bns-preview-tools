using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Helpers;
public class TextDiff
{
	/// <summary>
	/// Gets the inline textual diffs.
	/// </summary>
	/// <param name="oldText"></param>
	/// <param name="newText"></param>
	/// <returns></returns>
	public static List<TextDiffPiece> Diff(GameDataTable<Text> oldText, GameDataTable<Text> newText)
	{
		var model = new List<TextDiffPiece>();
		if (oldText != null && oldText != null)
			BuildDiffPieces(oldText, newText, model);

		return model;
	}


	private static void BuildDiffPieces(GameDataTable<Text> oldText, GameDataTable<Text> newText, List<TextDiffPiece> pieces)
	{
		ArgumentNullException.ThrowIfNull(oldText, nameof(oldText));
		ArgumentNullException.ThrowIfNull(newText, nameof(newText));

		var PiecesOld = BuildPieceHashes(oldText);
		var PiecesNew = BuildPieceHashes(newText);

		foreach (var pair in PiecesNew)
		{
			var piece = pair.Value;

			if (!PiecesOld.TryGetValue(pair.Key, out var piece2))
				pieces.Add(new TextDiffPiece(piece, ChangeType.Inserted));
			else if (piece2.text == piece.text)
				pieces.Add(new TextDiffPiece(piece, ChangeType.Unchanged));
			else
				pieces.Add(new TextDiffPiece(piece, ChangeType.Modified) { oldtext = piece2.text });
		}

		foreach (var pair in PiecesOld)
		{
			if (!PiecesNew.ContainsKey(pair.Key))
				pieces.Add(new TextDiffPiece(pair.Value, ChangeType.Deleted));
		}
	}

	private static Dictionary<string, Text> BuildPieceHashes(GameDataTable<Text> table)
	{
		return table.ToLookup(x => x.alias)
			.Where(x => x.Key != null)
			.ToDictionary(x => x.Key, x => x.First());
	}
}