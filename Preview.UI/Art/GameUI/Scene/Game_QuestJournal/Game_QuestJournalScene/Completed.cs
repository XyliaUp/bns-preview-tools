using Xylia.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls.Forms;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal;
public partial class Completed : PreviewFrm
{
	#region Constructor
	public Completed()
	{
		CheckForIllegalCrossThreadCalls = false;
		InitializeComponent();


		Completed_SizeChanged(null, null);

		this.Title = "UI.QuestJournal.CompletedTab".GetText();


		this.LoadData();
	}
	#endregion



	#region Functions (UI)
	private void Completed_SizeChanged(object sender, EventArgs e)
	{
		contentPanel1.Width = this.Width - TreeView.Right - 50;
		contentPanel1.Height = this.Height;
	}
	#endregion


	#region Functions
	readonly Dictionary<TreeNode, Quest> temp = new();

	public void LoadData()
	{
		List<Quest> CompletedQuest = new();
		Quest.GetEpic(CompletedQuest.Add);

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

		contentPanel1.Text = QuestData.CompletedDesc.GetText();
	}
	#endregion
}