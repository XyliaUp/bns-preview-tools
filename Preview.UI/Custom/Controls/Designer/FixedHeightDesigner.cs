using System.Windows.Forms.Design;

namespace Xylia.Preview.UI.Custom.Controls.Designer;  

public class FixedHeightDesigner : ControlDesigner
{
	public override SelectionRules SelectionRules => SelectionRules.Visible | SelectionRules.Moveable | SelectionRules.LeftSizeable | SelectionRules.RightSizeable;
}