using System;
using Xylia.Preview.UI.Controls.Primitives;

namespace Xylia.Preview.UI.Controls;
public class BnsCustomListCtrlWidget : BnsCustomSourceBaseWidget
{
	#region Constructors  		
	static BnsCustomListCtrlWidget()
	{

	}

	/// <summary>
	///     Default BnsCustomListCtrlWidget constructor
	/// </summary>
	public BnsCustomListCtrlWidget() : base()
	{

	}
	#endregion

	#region Override Methods
	protected override void OnInitialized(EventArgs e)
	{
		base.OnInitialized(e);

		//ListContainer = this.Children.OfType<BnsCustomImageWidget>().ToList();
		//ListContainer.ForEach(x =>
		//{
		//	x.Expansion = ["JobLabel"];
		//	x.MouseEnter += (_, _) => x.Expansion.Add("MouseOver");
		//	x.MouseLeave += (_, _) => x.Expansion.Remove("MouseOver");
		//	x.PreviewKeyDown += (_, _) => x.Expansion.Add("Pressed");
		//	x.PreviewKeyUp += (_, _) => x.Expansion.Remove("Pressed");
		//});
		//ListContainer[^1].ExpansionComponentList["JobLabel"]?.SetValue("灵剑士 5555555555555");
	}
	#endregion
}