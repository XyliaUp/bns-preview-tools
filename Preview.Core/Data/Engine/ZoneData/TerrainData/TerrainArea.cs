namespace Xylia.Preview.Data.Engine.ZoneData.TerrainData;
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
