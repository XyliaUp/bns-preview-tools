
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Xylia.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls.Forms;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	public partial class Completed : PreviewFrm
	{
		#region Constructor
		public Completed()
		{
			CheckForIllegalCrossThreadCalls = false;
			InitializeComponent();
			richTextBox1.MouseWheel += new MouseEventHandler(richTextBox1_MouseWheel);


			this.Title = "UI.QuestJournal.CompletedTab".GetText();

			this.LoadData();
		}
		#endregion



		#region Functions (UI)
		bool UseControl = false;

		public void richTextBox1_MouseWheel(object sender, MouseEventArgs e)
		{
			if (!UseControl) return;

			float Num = (float)0.1;

			if (e.Delta > 0)  //上滚
				richTextBox1.ZoomFactor += Num;
			else              //下滚
				richTextBox1.ZoomFactor -= Num;

			richTextBox1.Refresh();
		}

		private void Completed_KeyDown(object sender, KeyEventArgs e)
		{
			UseControl = false;

			switch (e.KeyCode)
			{
				#region 控制快捷键
				case Keys.Control:
					UseControl = true;
					break;

				case Keys.Space:
					if (this.TreeView.SelectedNode == null)
						return;

					this.TreeView.SelectedNode.Expand();
					break;

				case Keys.Enter:
					if (this.TreeView.SelectedNode == null)
						return;

					this.TreeView.SelectedNode.Expand();

					break;
				#endregion

				#region 键盘上下
				case Keys.Up:
					if (this.TreeView.SelectedNode == null || sender == TreeView)
						return;

					var Node = this.TreeView.SelectedNode.PrevNode;

					if (Node != null)
						this.TreeView.SelectedNode = Node;
					break;

				case Keys.Down:
					if (this.TreeView.SelectedNode == null || sender == TreeView)
						return;

					Node = this.TreeView.SelectedNode.NextNode;

					if (Node != null)
						this.TreeView.SelectedNode = Node;
					break;
					#endregion
			}
		}

		private void Completed_SizeChanged(object sender, EventArgs e) => richTextBox1.Width = this.Width - TreeView.Right - 50;

		private void Completed_Shown(object sender, EventArgs e)
		{
			this.richTextBox1.ContextMenuStrip = contextMenu;
		}

		public static ContextMenuStrip contextMenu
		{
			get
			{
				ContextMenuStrip Menu = new ContextMenuStrip();
				ToolStripMenuItem copy = new ToolStripMenuItem() { Name = "copy", Text = "复制", /*Size = new Size(100, 22),*/ };


				#region  显示控制
				Menu.Opening += (o, a) =>
				{
					// 判断来源是否富文本控件
					if (Menu.SourceControl is RichTextBox box)
					{
						// 当选择内容为空时, 复制功能不可用
						copy.Enabled = !string.IsNullOrWhiteSpace(box.SelectedText);
					}
				};
				#endregion

				#region  功能控制
				// 绑定复制Event
				copy.Click += (o, a) =>
				{
					// 判断来源是否富文本控件
					if (Menu.SourceControl is RichTextBox box)
					{
						Clipboard.SetDataObject(box.SelectedText, true);
					}
				};



				#endregion

				Menu.Items.AddRange(new ToolStripItem[] { copy });
				return Menu;
			}
		}
		#endregion


		#region Functions
		Dictionary<TreeNode, Quest> temp = new();

		public void LoadData()
		{
			List<Quest> CompletedQuest = new();
			QuestExtension.GetEpicInfo(data => CompletedQuest.Add(data));

			var GroupNode = new Dictionary<string, TreeNode>();
			CompletedQuest.Select(q => q.Group2)
				.DistinctBy(group => group.alias)
				.ForEach(group => GroupNode[group.alias] = TreeView.Nodes.Add(group.GetText()));


			foreach (var q in CompletedQuest)
			{
				var node = GroupNode[q.Group2.alias].Nodes.Add(q.Name2.GetText());
				temp[node] = q;
			}
		}

		private void TreeView2_AfterSelect_1(object sender, TreeViewEventArgs e)
		{
			if (!temp.TryGetValue(TreeView.SelectedNode, out var QuestData)) return;

			//获得前世红尘的内容
			richTextBox1.Text = QuestData.CompletedDesc.GetText()?.Replace("<br/>", "\n");
		}
		#endregion
	}
}