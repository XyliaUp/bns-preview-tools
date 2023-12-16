using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Helpers;
public class TextDiffPiece
{
	public ChangeType Type { get;  }
	public string alias { get; }
	public string text { get; }
	public string oldtext { get; set; }

	public TextDiffPiece(Text model, ChangeType type)
	{
		this.Type = type;

		this.alias = model.alias;
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