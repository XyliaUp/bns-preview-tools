namespace Xylia.Preview.Data.Models.BinData.Analyse.Struct.Condition.Base
{
    /// <summary>
    /// 条件类型
    /// </summary>
    public enum ConditionType
    {
        /// <summary>
        /// 无要求
        /// </summary>
        None,

        /// <summary>
        /// 不进行输出
        /// </summary>
        NonOut,

        /// <summary>
        /// 要求存在
        /// </summary>
        Exist,

        /// <summary>
        /// 要求不存在
        /// </summary>
        UnExist,



        /// <summary>
        /// 要求相等 （无视大小写）
        /// </summary>
        Equal,
        NotEqual,


        GreaterThan,
        GreaterThanOrEqualTo,

        LessThan,
        LessThanOrEqualTo,
    }
}
