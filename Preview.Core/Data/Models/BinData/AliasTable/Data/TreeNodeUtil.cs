using System;
using System.Collections.Generic;
using System.Linq;

using BnsBinTool.Core.Models;

using Xylia.Extension;

namespace Xylia.Preview.Data.Models.BinData.AliasTable.Data
{
	public static class TreeNodeUtil
    {
        /// <summary>
        /// 创建完整的树结构
        /// </summary>
        /// <param name="Nodes"></param>
        /// <returns></returns>
        public static TreeNode CreateTree(this List<AliasInfo> Nodes)
        {
            //对完整别名信息进行顺序排序
            Nodes.Sort(new HNodeSort(true));

            var Tree = new TreeNode();
            foreach (var Node in Nodes)
            {
                string INFO = Node.CompleteText;
                if (INFO is null) continue;

                //生成对象树
                INFO.CreateTree(Node, Tree);
            }

            //检查树 (必须)
            Tree.CheckTree();

            return Tree;
        }

        /// <summary>
        /// 循环创建树节点
        /// </summary>
        /// <param name="INFO"></param>
        /// <param name="Node"></param>
        /// <param name="Tree"></param>
        private static void CreateTree(this string INFO, AliasInfo Node, TreeNode Tree = null)
        {
            //拆分获取组和对象
            string Group = INFO.MyTake();
            if (!string.IsNullOrWhiteSpace(Group))
            {
                //创建对象
                TreeNode CurTree = null;
                if (Tree.ContainsKey(Group)) CurTree = Tree[Group];
                else Tree.Add(CurTree = new TreeNode(Group, Tree));

                INFO.RemovePrefixString(Group).CreateTree(Node, CurTree);
            }
            else
            {
                //树最终截止到此
                Tree.Add(new TreeNode(INFO, Tree)
                {
                    MainID = Node.MainID,
                    Variation = Node.Variation,
                });
            }
        }

        /// <summary>
        /// 检查树<br/>将其中无需分离的节点重新合并
        /// </summary>
        /// <param name="Tree"></param>
        public static void CheckTree(this TreeNode Tree)
        {
            if (!Tree.HasChildren) return;

            //如果当前树存在子节点
            //注意：最终的实例节点对于上级节点也是子节点，但是其自身不存在子节点

            //如果当前节点只存在一个子节点只有一个对象
            //则合并当前节点与下级节点
            else if (Tree.Count == 1/* && !Tree.Text.Contains(":")*/)
            {
                var OnlyChild = Tree.FirstChild;

                //开始进行对象合并
                Tree.Children = OnlyChild.Children;
                Tree.ht = OnlyChild.ht;

                Tree.Text += OnlyChild.Text;
                Tree.MainID = OnlyChild.MainID;
                Tree.Variation = OnlyChild.Variation;

                //再次校验树，直至不再需要判断
                Tree.CheckTree();
            }
            else
            {
                foreach (var Children in Tree) Children.CheckTree();
            }
        }

        public static List<NameTableEntry> CreatCases(this TreeNode TreeNode)
        {
            var Result = TreeNode.CreatCases(new List<int>());
            // Result.Sort(new HNodeSort(false));

            return Result;
        }

        /// <summary>
        /// 创建 HeadCase 实例
        /// 为解决顺序问题，设计了一个级数机制
        /// </summary>
        /// <param name="TreeNode"></param>
        /// <param name="Levels"></param>
        public static List<NameTableEntry> CreatCases(this TreeNode TreeNode, List<int> Levels)
        {
            throw new NotImplementedException();

            //if (TreeNode.HasChildren)
            //{
            //	var Result = new List<NameTableEntry>();
            //	for (int i = 0; i < TreeNode.Count; i++)
            //	{
            //		var ChildNode = TreeNode.Children[i];

            //		var CurLevel = new List<int>(Levels);
            //		CurLevel.Add(i);

            //		Result.AddRange(CreatCases(ChildNode, CurLevel));
            //	}

            //	//创建路径节点
            //	if (!TreeNode.RootNode)
            //	{
            //		//考虑到先插入再执行排序
            //		//因此使用动态查询机制
            //		var CurCase = new NameTableEntry() { String = TreeNode.Text };
            //		CurCase.UseAutoIndex = true;

            //		//筛选出所有子节点
            //		CurCase.Cases.AddRange(Result.Where(r => r.Levels.Count == Levels.Count + 1));
            //		CurCase.Levels = Levels;

            //		Result.Add(CurCase);
            //	}

            //	return Result;
            //}
            //else
            //{
            //	//创建实例节点
            //	var CurCase = new NameTableEntry()
            //	{
            //		Begin = TreeNode.MainID * 2 + 1,
            //		End = TreeNode.Variation * 2,
            //		String = TreeNode.Text,
            //	};
            //	CurCase.Levels = Levels;

            //	return new List<NameTableEntry>() { CurCase };
            //}
        }



        public static string MyTake(this string Info) => new(Info.MyTakeToChars());

        public static char[] MyTakeToChars(this string Info)
        {
            //拆分特殊符号: _ :
            int SplitTarget1 = Info.IndexOf(':');
            if (SplitTarget1 != -1) return Info.Take(SplitTarget1 + 1).ToArray();

            int SplitTarget2 = Info.IndexOf('_');
            int SplitTarget3 = Info.IndexOf('.');

            //返回最终结果
            if (SplitTarget2 != -1 && SplitTarget3 != -1) return Info.Take(Math.Min(SplitTarget2 + 1, SplitTarget3 + 1)).ToArray();
            else if (SplitTarget2 != -1) return Info.Take(SplitTarget2 + 1).ToArray();
            else if (SplitTarget3 != -1) return Info.Take(SplitTarget3 + 1).ToArray();

            return null;
        }
    }
}