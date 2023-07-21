using BnsBinTool.Core.DataStructs;

namespace Xylia.Preview.Data.Models.BinData.AliasTable;
public sealed class AliasInfo
{
    #region Constructor
    public AliasInfo(Ref Ref, string CompleteInfo)
    {
		this.Ref = Ref;
		this.CompleteText = CompleteInfo;
    }

    public AliasInfo(Ref Ref, string table, string alias)
    {
        this.Ref = Ref;
        this.Table = table;
		this.Alias = alias;
    }
    #endregion

    #region Fields
    public Ref Ref;

	public string Table;

    public string Alias;

    public string CompleteText
    {
        get => Table + ":" + Alias;
        set
        {
            if (!value.Contains(':')) throw new Exception("别名缓存区必须以列表+数据别名方式存储");
            else
            {
                var tmp = value.Split(':');

				Table = tmp[0];
                Alias = tmp[1];
            }
        }
    }


    public override string ToString() => $"{CompleteText}";
    #endregion
}