using Xylia.Extension;

namespace Xylia.Preview.UI.Custom.Controls;
public sealed class ItemNamePanel : ContentPanel
{
	#region Grade
	private sbyte _grade = 6;

	public sbyte ItemGrade
	{
		get => _grade;
		set
		{
			_grade = value;
			this.ForeColor = new ExecuteParam().GetFont("00008130.Program.Fontset_ItemGrade_" + _grade).ForeColor;
			this.Invalidate();
		}
	}
	#endregion


	#region Text
	private string _text;

	public override string Text { get => _text; set => base.Text = (_text = value) + "<image object='icon'/>"; }
	#endregion

	#region TagImage
	private Bitmap icon => tagImage?.ChangeColor(this.ForeColor);


	private Bitmap tagImage;
	public Bitmap TagImage
	{
		get => this.tagImage;
		set
		{
			this.tagImage = value;
			this.Invalidate();
		}
	}
	#endregion
}