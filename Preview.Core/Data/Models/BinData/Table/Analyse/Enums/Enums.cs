using System.ComponentModel;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Enums
{
    public enum ListRule
    {
        None,

        /// <summary>
        /// 完全的
        /// </summary>
        [Description("以完全重生成方式处理")]
        Complete,

        /// <summary>
        /// 额外
        /// </summary>
        [Description("在loose区域新增一个null值")]
        extra,


        [Description("不进行重新排序")]
        unsort,


        [Description("以部分重生成方式处理")]
        cover,

        /// <summary>
        /// 自动生成分支id
        /// </summary>
        [Description("自动生成分支id")]
        UseAutoVariation,

        /// <summary>
        /// 处理结束后，重新生成编号数据
        /// 为 ItemGraphData 定制的规则
        /// </summary>
        SortId,


        Simple,
    }

    /// <summary>
    /// 崩溃类型
    /// </summary>
    public enum CrashType
    {
        /// <summary>
        /// 未在类型记录器中包含的类型范围而发生的错误
        /// </summary>
        NotIncludedType,
    }

    /// <summary>
    /// 服务端应用规则方式
    /// </summary>
    public enum ApplyMode
    {
        /// <summary>
        /// 不使用
        /// </summary>
        None,

        /// <summary>
        /// 必须存在属性 （如果值不存在，先使用默认值。如果默认值仍不存在，抛出错误）
        /// </summary>
        Exist,

        /// <summary>
        /// 使用默认值方式 （值等于默认值时删除）
        /// </summary>
        HideDefault,

        /// <summary>
        /// 强制应用默认属性值
        /// </summary>
        Force,

        /// <summary>
        /// 自动编号，仅使用于ID类型
        /// </summary>
        Auto,
    }
}
