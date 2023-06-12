using System;
using System.Collections.Generic;
using System.Xml;

using Xylia.Preview.Data.Models.Util.Sort.Common;

namespace Xylia.Preview.Data.Models.Util.Sort
{
	public class XmlAttributeSort : IComparer<XmlAttribute>
    {
        /// <summary>
        /// 是否是序列化配置文件
        /// </summary>
        public bool IsConfig = false;

        public int Compare(XmlAttribute x, XmlAttribute y)
        {
            if (x is null || y is null) throw new ArgumentNullException();

            string AliasX = x.Name.NameConvert(IsConfig);
            string AliasY = y.Name.NameConvert(IsConfig);
            return Xylia.Sort.Method.StrCompare(AliasX, AliasY);
        }
    }
}
