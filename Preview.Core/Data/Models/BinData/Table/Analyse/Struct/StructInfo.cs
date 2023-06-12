using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Xylia.Preview.Data.Models.BinData.Analyse.Load;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Record;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Seq;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Struct
{
    /// <summary>
    /// 结构体信息
    /// </summary>
    public sealed class StructInfo : List<RecordDef>
    {
        #region Constructor
        public StructInfo() : base()
        {

        }

        public StructInfo(string Alias, IEnumerable<RecordDef> collection) : base(collection)
        {
            this.Alias = Alias;
        }
        #endregion

        #region Fields
        public string Alias;
        #endregion
    }


    public static class StructExtension
    {
        /// <summary>
        /// 创建结构体成员别名
        /// </summary>
        /// <param name="MetaName"></param>
        /// <param name="StructName"></param>
        /// <param name="OnlyReplace">只处理通配符</param>
        /// <returns></returns>
        public static string CreateStructMetaName(this string MetaName, string StructName, bool OnlyReplace = false)
        {
            //空文本返回
            if (string.IsNullOrWhiteSpace(MetaName)) return null;

            //通配符形式
            else if (MetaName.Contains("$")) return MetaName.Replace("$", StructName);

            //文本拼接形式
            else if (!OnlyReplace) return StructName + MetaName;

            else return MetaName;
        }

        /// <summary>
        /// 读取结构信息
        /// </summary>
        /// <param name="PublicStructs"></param>
        /// <param name="Element"></param>
        /// <param name="PublicSeq"></param>
        public static void ReadStructInfo(this XmlElement Element, Dictionary<string, StructInfo> PublicStructs, Dictionary<string, SeqInfo> PublicSeq)
        {
            //Initialize
            if (PublicStructs is null)
                throw new ArgumentNullException("结构体集合不存在，请先Initialize");



            string Alias = Element.Attributes["alias"]?.Value?.Trim();
            string Parent = Element.Attributes["parent"]?.Value?.Trim();

            //是否存在继承的母结构
            bool HasParent = !string.IsNullOrWhiteSpace(Parent);

            //加载record结构数据
            if (Element.HasChildNodes)
            {
                //读取记录器信息
                var StructInfo = new StructInfo(Alias, Element.ChildNodes.OfType<XmlElement>().GetRecordInfo("#struct", new ConfigParam()
                {
                    PublicSeq = PublicSeq,

                }).Records);

                if (HasParent)
                {
                    if (!PublicStructs.ContainsKey(Parent)) Console.WriteLine($"{Alias}的Parent:{Parent}不存在");
                    else StructInfo.InsertRange(0, PublicStructs[Parent]);
                }

                PublicStructs.Add(Alias, StructInfo);
            }
            else if (HasParent) Console.WriteLine($"{Alias}为空结构体");
        }
    }
}
