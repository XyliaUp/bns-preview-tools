using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Helpers;
public class TextDiffPiece
{
	public ChangeType Type { get; set; }

	public string alias { get; set; }
	public string text { get; set; }
	public string oldtext { get; set; }

	public TextDiffPiece(Text model, ChangeType type)
	{
		this.Type = type;

		this.alias = model.Alias;
		this.text = model.text;
	}
}

public enum ChangeType
{
	Unchanged,
	Deleted,
	Inserted,
	Modified
}