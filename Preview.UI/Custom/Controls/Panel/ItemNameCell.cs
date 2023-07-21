using System.ComponentModel;

using Xylia.Extension;

namespace Xylia.Preview.UI.Custom.Controls;
public sealed class ItemNamePanel : ContentPanel
{
	#region Text
	private string _text;

	public override string Text { get => _text; set => base.Text = (_text = value) + "<image object='icon'/>"; }
	#endregion

	#region Grade
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override Color ForeColor => ExecuteParam.GetGradeColor(_grade);


	private byte _grade = 6;

	public byte ItemGrade
	{
		get => _grade;
		set
		{
			_grade = value;
			this.Invalidate();
		}
	}
	#endregion

	#region TagImage
	private Bitmap icon;

	public Bitmap TagImage
	{
		get => this.icon;
		set
		{
			this.icon = value?.ChangeColor(this.ForeColor);
			this.Invalidate();
		}
	}
	#endregion
}