using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Helpers;
public class TextDiffPiece
{
	public ChangeType Type { get; set; }

	public string Alias { get; set; }
	public string Text { get; set; }
	public string TextOld { get; set; }

	public TextDiffPiece(Text model, ChangeType type)
	{
		this.Type = type;

		this.Alias = model.Alias;
		this.Text = model.text;
	}
}

public enum ChangeType
{
	Unchanged,
	Deleted,
	Inserted,
	Modified
}