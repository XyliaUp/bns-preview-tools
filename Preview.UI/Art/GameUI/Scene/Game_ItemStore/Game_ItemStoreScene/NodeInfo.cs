namespace Xylia.Preview.GameUI.Scene.Game_ItemStore;
public class NodeInfo
{
	public NodeInfo(string alias, TreeNode node)
	{
		this.RecordAlias = alias;
		this.ParentNode = node.Parent;
	}


	public string RecordAlias;

	public TreeNode ParentNode;
}