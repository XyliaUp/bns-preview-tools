namespace Xylia.Preview.Data.Models.BinData.Table.Config;
public enum ErrorType
{
    /// <summary>
    /// 提醒 (执行时进程显示红字并记录日志)
    /// 是默认类型
    /// </summary>
    Warning,

    /// <summary>
    /// 一般 (执行时仅记录日志)
    /// </summary>
    Default,

    /// <summary>
    /// 建议 (执行完成后返回错误的统一提醒)
    /// </summary>
    Suggestion,

    /// <summary>
    /// 错误 (执行时抛出错误)
    /// </summary>
    Error,

    /// <summary>
    /// 仅在单文件序列时显示错误
    /// </summary>
    Single,

    /// <summary>
    /// 不作为错误
    /// </summary>
    Jump,


    /// <summary>
    /// 错误时销毁当前数据
    /// </summary>
    Dispose,
}
