namespace Xylia.Preview.GameUI.Scene.Game_ItemStore.Game_ItemStore;
public class NodeInfo
{
    public NodeInfo(string alias, TreeNode node)
    {
        RecordAlias = alias;
        ParentNode = node.Parent;
    }


    public string RecordAlias;

    public TreeNode ParentNode;
}