using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.GameUI.Scene.Searcher;

namespace Xylia.Preview.GameUI.Scene.Game_ItemStore;
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
	CancellationTokenSource cts = new();


	internal Dictionary<TreeNode, TreeNode> TreeNodes = new();

	public readonly List<FilterInfo> _filter = new();
	#endregion





	#region Functions (UI)
	private async void Frm_Load(object sender, EventArgs e)
	{
		await Task.Run(this.LoadData);
		if (TreeView.Nodes.Count > 0) TreeView.Nodes[0].ExpandAll();
	}
	private void Frm_FormClosing(object sender, FormClosingEventArgs e) => cts.Cancel();
	public void Frm_SizeChanged(object sender, EventArgs e)
	{
		int TempWidth = this.Width - this.TreeView.Width - 30;
		this.ListPreview.Width = Math.Max(TempWidth, 315);
	}


	protected async void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
	{
		if (TreeView.SelectedNode?.Tag is null) return;

		await Task.Run(() => this.Show((string)TreeView.SelectedNode.Tag));
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


	protected void AddNode(TreeNode parent, string alias, string text)
	{
		if (this.IsHandleCreated) this.Invoke(() =>
		{
			var node = parent.Nodes.Add(text);
			node.Tag = alias;

			TreeNodes.Add(node, parent);
		});
	}

	private void Filter(string rule, IEnumerable<FilterInfo> filters = null)
	{
		foreach (TreeNode CurNode in this.TreeView.Nodes)
			CurNode.Nodes.Clear();

		var records = SearcherDialog.Filter(rule, filters);
		foreach (var (node, parent) in this.TreeNodes)
		{
			bool flag = false;
			if (string.IsNullOrWhiteSpace(rule)) flag = true;
			else if (node.Text.MyContains(rule)) flag = true;
			else if (records != null && Filter((string)node.Tag, records)) flag = true;


			if (flag) parent.Nodes.Add(node);
		}

		this.TreeView.ExpandAll();
	}

	protected virtual bool Filter(string alias, List<BaseRecord> FilterRule) => false;
	#endregion
}