using System.Windows.Forms;

namespace Xylia.Preview.GameUI.Scene.Game_ItemStore
{
	public enum Store2Type
	{
		Normal,

		UnlocatedStore,

		AccountStore,

		SoulBoostStore,
	}

	public class StoreInfo
	{
		public NodeInfo Node;

		public Store2Type StoreType;

		public string Alias;
		public string Name;

		public int Order;
	}


	public class NodeInfo
	{
		public NodeInfo(string alias, TreeNode node)
		{
			this.RecordAlias = alias;
			this.TreeNode = node;
			this.ParentNode = node.Parent;
		}


		public string RecordAlias;


		public TreeNode ParentNode;

		public TreeNode TreeNode;
	}
}