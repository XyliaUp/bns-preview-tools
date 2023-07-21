using System.Collections;
using System.Globalization;

using Xylia.Extension;

namespace Xylia.Preview.Data.Models.BinData.AliasTable;
public sealed class AliasCollection : List<AliasInfo>
{
    #region Fields
    /// <summary>
    /// 已经确认指向表
    /// </summary>
    public bool HasCheck = false;
    #endregion

    #region Functions
    private readonly Hashtable ht = new(StringComparer.Create(CultureInfo.InvariantCulture, true));

    public AliasInfo this[string alias] => ht.ContainsKey(alias) ? (AliasInfo)ht[alias] : null;

    public new void Add(AliasInfo aliasInfo)
    {
        base.Add(aliasInfo);

        //由于之前的处理会按表进行拆分，此处无需缓存完整文本
        ht[aliasInfo.Alias] = aliasInfo;
    }

    public void Sort(bool Mode = false) => Sort(new HNodeSort(Mode));
    #endregion
}

public class HNodeSort : IComparer<AliasInfo>
{
    #region Constructor
    /// <summary>
    /// 指示是否是序列化模式
    /// </summary>
    bool _mode = false;

    public HNodeSort(bool Mode) => _mode = Mode;
    #endregion


    public int Compare(AliasInfo x, AliasInfo y)
    {
        ArgumentNullException.ThrowIfNull(x);
		ArgumentNullException.ThrowIfNull(y);

		if (_mode) return Method.StrCompare(x.CompleteText, y.CompleteText, Method.SortRule.EveryChar);
        else return (int)(x.Ref.Id - y.Ref.Id);
    }
}