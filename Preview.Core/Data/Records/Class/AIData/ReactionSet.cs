using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;

[Signal("reaction-set")]
public sealed class ReactionSet : BaseRecord
{
    public List<Reaction> Reaction;

    /// <summary>
    /// 概率 0~100, decision 下的所有 ReactionSet 概率和不能超过最大值
    /// </summary>
    public sbyte Probability;

    /// <summary>
    /// 概率 0~10000, decision 下的所有 ReactionSet 概率和不能超过最大值 
    /// </summary>
    public short Probability10000;
}