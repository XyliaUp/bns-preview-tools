namespace Xylia.Preview.Data.Models.BinData.Table.Config;
public class SeqCell
{
    #region Fields
    public short Key;

    public string Name;

    public string Desc;
    #endregion

    #region Constructor
    public SeqCell(short key, string name, string desc = null)
	{
		if (name == "unk-")
			name = key.ToString();


		this.Key = key;
		this.Name = name;
        this.Desc = desc ?? name;
    }
    #endregion


    public override string ToString() => Name;
}