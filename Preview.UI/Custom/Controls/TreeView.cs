using Xylia.Preview.UI.Resources;

namespace Xylia.Windows.Controls;
public partial class TreeView : System.Windows.Forms.TreeView
{
	public bool ShowCount = true;


	#region Field
	Font foreFont = new Font("微软雅黑", 9F, FontStyle.Bold);
	Brush recBrush = new SolidBrush(Color.FromArgb(82, 218, 163));
	Brush recSelectedBrush = new SolidBrush(Color.FromArgb(248, 248, 255));
	Pen recPen = new Pen(new SolidBrush(Color.FromArgb(226, 226, 226)));
	Pen recHoverPen = new Pen(new SolidBrush(Color.FromArgb(82, 218, 163)));
	Pen linePen = new Pen(Color.FromArgb(226, 226, 226), 1);
	Image icon;


	bool NormalState = false;
	bool DoubleState = false;
	#endregion


	public TreeView()
	{
		this.DrawMode = TreeViewDrawMode.OwnerDrawAll;
		this.ItemHeight = 30;//节点行高          
		this.ShowLines = false;
		this.HotTracking = true;
		this.Indent = 20;//节点X值缩进量
		this.Scrollable = true;
		this.BorderStyle = BorderStyle.None;
		this.Font = foreFont;
	}

	protected override void OnDrawNode(DrawTreeNodeEventArgs e)
	{
		if (e.Node.Level == 0)//根节点
		{
			//设置根节点的背景色, 可以防止字体图标重绘叠加
			e.Graphics.FillRectangle(new SolidBrush(Color.Transparent), e.Bounds);

			SolidBrush ForeBrush = new SolidBrush(Color.FromArgb(124, 169, 200));


			if (e.Node.Nodes.Count > 0)
			{
				if (!e.Node.IsExpanded) icon = Resource_Common.tree_Close;
				else icon = Resource_Common.tree_Open;

				e.Graphics.DrawImage(icon, e.Node.Bounds.X - 17, e.Node.Bounds.Y);

				if (ShowCount) e.Graphics.DrawString($"[{e.Node.Nodes.Count}]", foreFont, ForeBrush, this.Right - 55, e.Node.Bounds.Top + 4);
			}

			e.Graphics.DrawString(e.Node.Text, foreFont, ForeBrush, e.Node.Bounds.Left + 16, e.Node.Bounds.Top + 4);
		}
		else
		{
			if (!e.Bounds.IsEmpty)
			{
				e.Graphics.FillRectangle(Brushes.White, e.Bounds);

				//绘制连接线,30 = 20 + 10,10为图标宽的一半, 保证topEnd点在图标中心, 15为行高一半
				//Start定义了横线长度

				int Shift = -20;

				int Shifts = Shift * (1 - e.Node.Level);

				Point start = new Point(e.Node.Bounds.X, e.Node.Bounds.Y + 15);

				// Console.WriteLine(e.Node.Bounds.X + Shifts);

				Point middle = new Point(e.Node.Bounds.X - 30, e.Node.Bounds.Y + 15);
				Point topEnd = new Point(e.Node.Bounds.X - 30, e.Node.Bounds.Y);
				Point bottomEnd = new Point(e.Node.Bounds.X - 30, e.Node.Bounds.Y + 30);

				e.Graphics.DrawLine(linePen, start, middle);
				e.Graphics.DrawLine(linePen, middle, topEnd);

				if (null != e.Node.NextNode) e.Graphics.DrawLine(linePen, middle, bottomEnd);


				//绘制文本框, 宽145px可容纳十个10.5pt的字, 55 = 23 + 20 + 12, 文本框距离上下边界4px
				Rectangle box = new Rectangle(e.Bounds.Left + 43 + Shifts, e.Bounds.Top + 4, this.Width - 55 - 20 - Shifts, e.Bounds.Height - 8);
				if (e.Node.IsSelected)//二级节点被选中
				{
					e.Graphics.FillRectangle(recBrush, box);
					e.Graphics.DrawString(e.Node.Text, foreFont, recSelectedBrush, e.Node.Bounds.Left + 12 + Shifts, e.Node.Bounds.Top + 4);
				}
				else
				{
					//鼠标指针在二级节点上
					if ((e.State & TreeNodeStates.Hot) != 0) e.Graphics.DrawRectangle(recHoverPen, box);
					else e.Graphics.DrawRectangle(recPen, box);
					e.Graphics.DrawString(e.Node.Text, foreFont, new SolidBrush(Color.FromArgb(81, 81, 81)), e.Node.Bounds.Left + 12 + Shift * (1 - e.Node.Level), e.Node.Bounds.Top + 4);
				}
			}
		}
	}

	protected override void OnMouseClick(MouseEventArgs e)
	{
		base.OnMouseClick(e);

		TreeNode tn = this.GetNodeAt(e.Location);
		if (tn is null) return;

		this.SelectedNode = tn;
		if (e.Button == MouseButtons.Left)
		{
			if (NormalState && !DoubleState)
			{
				Rectangle bounds = new Rectangle(tn.Bounds.Left - 17, tn.Bounds.Y, tn.Bounds.Width - 16, tn.Bounds.Height);
				if (tn != null && bounds.Contains(e.Location) == false)
				{
					if (tn.IsExpanded == false) tn.Expand();
					else tn.Collapse();
				}
			}
			else NormalState = !NormalState;
		}
	}

	protected override void OnMouseDoubleClick(MouseEventArgs e)
	{
		DoubleState = true;
		base.OnMouseDoubleClick(e);


		TreeNode tn = this.GetNodeAt(e.Location);
		Rectangle bounds = new Rectangle(tn.Bounds.Left - 17, tn.Bounds.Y, tn.Bounds.Width - 16, tn.Bounds.Height);
		if (tn != null && bounds.Contains(e.Location) == false)
		{
			if (tn.IsExpanded == false) tn.Expand();
			else tn.Collapse();
		}

		NormalState = DoubleState = false;
	}
}