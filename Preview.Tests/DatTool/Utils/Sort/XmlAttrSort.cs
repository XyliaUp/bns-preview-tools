using System.Xml;

using Xylia.Extension;
using Xylia.Preview.Data.Models.Util.Sort.Common;

namespace Xylia.Preview.Data.Models.Util.Sort;

public class XmlAttributeSort : IComparer<XmlAttribute>
{
	/// <summary>
	/// 是否是序列化配置文件
	/// </summary>
	public bool IsConfig = false;

	public int Compare(XmlAttribute x, XmlAttribute y)
	{
		ArgumentNullException.ThrowIfNull(x);
		ArgumentNullException.ThrowIfNull(y);

		return Method.StrCompare(
			x.Name.NameConvert(IsConfig),
			y.Name.NameConvert(IsConfig));
	}




	public static void Sort(XmlNode xmlNode, bool recurse = true)
	{
		if (xmlNode.NodeType != XmlNodeType.Element) return;
		if (xmlNode.Attributes.Count > 0)
		{
			XmlElement Element = (XmlElement)xmlNode;

			var attrs = new List<XmlAttribute>();
			foreach (XmlAttribute attr in xmlNode.Attributes) attrs.Add(attr);

			//进行自定义排序
			attrs.Sort(new XmlAttributeSort() { IsConfig = false });

			//原节点移除所有
			Element.Attributes.RemoveAll();

			//设置数值
			attrs.ForEach(a => Element.SetAttribute(a.Name, a.Value));
		}

		//递归
		if (recurse && xmlNode.HasChildNodes)
			xmlNode.ChildNodes.OfType<XmlElement>().ForEach(node => Sort(node, true));
	}
}