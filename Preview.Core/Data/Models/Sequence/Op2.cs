using System.ComponentModel;

namespace Xylia.Preview.Data.Models.Sequence;

[DefaultValue(eq)]
public enum Op
{
    /// <summary>
    /// equal
    /// </summary>
    eq,

    /// <summary>
    /// not equal
    /// </summary>
    neq,

    /// <summary>
    /// greater than
    /// </summary>
    gt,

    /// <summary>
    /// greater than or equal
    /// </summary>
    ge,

    /// <summary>
    /// less than
    /// </summary>
    lt,

    /// <summary>
    /// less than or equal
    /// </summary>
    le,
}




[DefaultValue(and)]
public enum OpCheck
{
    and,

    or,
}

public enum OpCheck2
{
    or,

    and,
}