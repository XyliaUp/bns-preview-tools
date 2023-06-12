using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Xylia.Preview.Data.Models.BinData.AliasTable.Data
{
	public class TreeNode : IEnumerable
    {
        #region Constructor
        /// <summary>
        /// 用于创建根节点
        /// </summary>
        public TreeNode()
        {

        }

        public TreeNode(string Text, TreeNode Parent)
        {
            this.Text = Text;

            //赋值母对象
            this.Parent = Parent;
        }
        #endregion

        #region Fields
        /// <summary>
        /// 指示当前是根节点
        /// </summary>
        public bool RootNode => Parent == null;

        /// <summary>
        /// 母节点
        /// </summary>
        public TreeNode Parent;

        /// <summary>
        /// 子节点数量
        /// </summary>
        public int Count => Children.Count;

        /// <summary>
        /// 是否含有子节点
        /// </summary>
        public bool HasChildren => Count != 0;

        /// <summary>
        /// 第一个子节点
        /// </summary>
        public TreeNode FirstChild => Children[0];

        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<TreeNode> Children = new();

        /// <summary>
        /// 哈希表
        /// </summary>
        public Hashtable ht = new(StringComparer.Create(CultureInfo.InvariantCulture, true));



        /// <summary>
        /// 当前节点的文本
        /// </summary>
        public string Text;

        /// <summary>
        /// 用于对最终节点记录主信息
        /// </summary>
        public uint MainID;

        /// <summary>
        /// 用于对最终节点记录附信息
        /// </summary>
        public uint Variation;

        /// <summary>
        /// 返回从根节点到当前节点的合并文本
        /// </summary>
        public string CompleteText => Parent?.CompleteText + Text;
        #endregion

        #region Functions
        /// <summary>
        /// 增加子节点
        /// </summary>
        public void Add(TreeNode PathNode)
        {
            Children.Add(PathNode);


            //如果存在重复对象，依然允许插入对象
            //但是缓存用的哈希表不写入新对象
            if (!ht.ContainsKey(PathNode.Text)) ht.Add(PathNode.Text, PathNode);

            else if (!PathNode.CompleteText.StartsWith("text:"))
                System.Diagnostics.Trace.WriteLine($"存在重复对象 {PathNode.CompleteText}");
        }

        public bool ContainsKey(string Key) => ht.ContainsKey(Key);

        public TreeNode this[string alias] => (TreeNode)ht[alias];
		#endregion

		#region IEnumerable
		public IEnumerator<TreeNode> GetEnumerator()
        {
            foreach (var info in Children) yield return info;

            //结束迭代
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}