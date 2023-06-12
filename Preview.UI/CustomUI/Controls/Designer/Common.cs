using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Xylia.Preview.GameUI.Controls.Designer
{
	/// <summary>
	/// 不允许调整控件的高度
	/// </summary>
	public class FixedHeightDesigner : ControlDesigner
	{
		public override SelectionRules SelectionRules => SelectionRules.Visible | SelectionRules.Moveable | SelectionRules.LeftSizeable | SelectionRules.RightSizeable;
	}

	/// <summary>
	/// 不允许调整控件的宽度
	/// </summary>
	public class FixedWidthDesigner : ControlDesigner
	{
		public override SelectionRules SelectionRules => SelectionRules.Visible | SelectionRules.Moveable | SelectionRules.TopSizeable | SelectionRules.BottomSizeable;
	}


	/// <summary>
	/// 不允许调整控件的宽度和高度
	/// </summary>
	public class FixedDesigner : ControlDesigner
	{
		public override SelectionRules SelectionRules
		{
			get
			{
				//获取标志值
				var OnlyAutoSize = this.Control.GetType().GetCustomAttribute<OnlyAutoSize>()?.Value ?? false;

				if (!OnlyAutoSize || this.Control.AutoSize) return SelectionRules.Visible | SelectionRules.Moveable;
				else return SelectionRules.Visible | SelectionRules.Moveable  |
						SelectionRules.TopSizeable | SelectionRules.BottomSizeable |
						SelectionRules.LeftSizeable | SelectionRules.RightSizeable;
			}
		}
	}

	/// <summary>
	/// 指示设计器规则是否只在 <see cref="Control.AutoSize"/> 为 <see langword="true"/> 时生效
	/// </summary>
	[System.AttributeUsage(System.AttributeTargets.Class)]
	public class OnlyAutoSize : System.Attribute
	{
		public OnlyAutoSize(bool Value = false)
		{
			this.Value = Value;
		}

		public readonly bool Value;
	}
}
