using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

using Xylia.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Searcher;

namespace Xylia.Preview.GameUI.Scene.Game_ItemStore
{
	public partial class StoreScene : Form
	{
		#region Constructor
		public StoreScene()
		{
			InitializeComponent();

			CheckForIllegalCrossThreadCalls = false;
			this.Frm_SizeChanged(null, null);
		}
		#endregion

		#region Fields
		internal Dictionary<TreeNode, NodeInfo> TreeNodeInfo = new();

		public readonly List<FilterInfo> _filter = new();

		public string StoreAlias;

		private Thread thread;
		#endregion


		#region Functions (UI)
		private void Store2Frm_Load(object sender, EventArgs e) => this.LoadData();

		public void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (TreeView.SelectedNode is null || !TreeNodeInfo.TryGetValue(TreeView.SelectedNode, out var info)) return;

			thread = new Thread(() => this.Show(this.StoreAlias = info.RecordAlias));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
		}

		private void Store2Frm_FormClosing(object sender, FormClosingEventArgs e) => thread?.Interrupt();

		public void Frm_SizeChanged(object sender, EventArgs e)
		{
			int TempWidth = this.Width - this.TreeView.Width - 30;
			this.ListPreview.Width = Math.Max(TempWidth, 315);
		}

		private void CancelFilter_Click(object sender, EventArgs e) => Filter(null);

		private void ModifyFilterRule_Click(object sender, EventArgs e)
		{
			var Searcher = new SearcherDialog(_filter);
			if (Searcher.ShowDialog() != DialogResult.OK) return;

			Filter(Searcher.textBox1.Text, Searcher.filters);
		}
		#endregion


		#region Functions
		protected virtual void LoadData() { }

		protected virtual void Show(string StoreAlias) { }



		private void Filter(string rule, IEnumerable<FilterInfo> filters = null)
		{
			foreach (TreeNode CurNode in this.TreeView.Nodes)
				CurNode.Nodes.Clear();

			var records = SearcherDialog.Filter(rule, filters);
			foreach (var (node, info) in this.TreeNodeInfo)
			{
				bool flag = false;
				if (string.IsNullOrWhiteSpace(rule)) flag = true;
				else if (node.Text.MyContains(rule)) flag = true;
				else if (records != null && Filter(info, records)) flag = true;


				if (flag) info.ParentNode.Nodes.Add(node);
			}

			this.TreeView.ExpandAll();
		}

		protected virtual bool Filter(NodeInfo NodeInfo, List<BaseRecord> FilterRule) => false;
		#endregion
	}
}