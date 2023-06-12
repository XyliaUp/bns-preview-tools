using Xylia.Xml;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Struct.Seq.Type
{
    /// <summary>
    /// 类别记录器补充信息
    /// </summary>
    public sealed class TypeInfo : SeqInfo<TypeCell>
    {

    }


    public static partial class Extension
    {
        /// <summary>
        /// 获取类型信息
        /// </summary>
        /// <param name="seriData"></param>
        /// <param name="TypeInfo"></param>
        /// <returns></returns>
        public static TypeCell GetTypeCell(this XmlProperty seriData, TypeInfo TypeInfo)
        {
            //如果当前数据包含类型Fields名
            if (TypeInfo != null && !string.IsNullOrWhiteSpace(TypeInfo.Name) && seriData.Attributes.ContainsName(TypeInfo.Name, out string Value, true))
            {
                var Result = TypeInfo.GetCell(Value);
                if (Result != null) return Result;
            }

            //返回默认对象
            return TypeInfo[-1];
        }
    }
}
