namespace Xylia.Preview.Data.Models.ZoneData.TerrainData;
public class TerrainCell
{
    #region Functions
    public void Read(BinaryReader reader)
    {
        Type = (CellType)reader.ReadInt32();
        AreaIdx = reader.ReadInt32();
        Param2 = reader.ReadInt32();
    }

    public void Write(BinaryWriter writer)
    {
        writer.Write((int)Type);
        writer.Write(AreaIdx);
        writer.Write(Param2);
    }
    #endregion

    #region Fields
    public CellType Type;
    public int AreaIdx;
    public int Param2;
    #endregion
}

public enum CellType
{
    None,
    Unk1, //1 单元格
    Unk2, //2
    Unk3, //3 删除后缺失入场点，导致无法进入地图
    Unk4,
}

/// <summary>
/// 向量
/// </summary>
public struct Vector32
{
    #region Constructor
    public Vector32(BinaryReader reader)
    {
        X = reader.ReadInt16();
        Y = reader.ReadInt16();
        Z = reader.ReadInt16();
    }

    public Vector32(string Value)
    {
        var group = Value.Split(',');

        X = short.Parse(group[0]);
        Y = short.Parse(group[1]);
        Z = short.Parse(group[2]);
    }
    #endregion

    #region Fields
    public short X;
    public short Y;
    public short Z;
    #endregion


    #region Functions
    public override string ToString() => $"{X},{Y},{Z}";

    /// <summary>
    /// 存储数据
    /// </summary>
    /// <param name="writer"></param>
    public void Write(BinaryWriter writer)
    {
        writer.Write(X);
        writer.Write(Y);
        writer.Write(Z);
    }
    #endregion
}
