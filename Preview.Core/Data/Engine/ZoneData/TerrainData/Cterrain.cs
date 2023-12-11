using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Engine.ZoneData.TerrainData;

//GeoZone::InitializeByRecordMap zone(1290) boundary (sector) [-16,153] - [28,197] but terrain(1290) is too small [-96,101] - [2,195]
//[geo-terran], FindCell failed; invalid z, zlow
//[geo-cube], GeoCube initialize failed, nearby


/// <summary>
/// 地形数据结构
/// </summary>
public class CterrainFile
{
	#region Fields
	public short Version = 23;

	public long FileSize;

	public int TerrainID;

	public Vector16 Vector1;
	public Vector16 Vector2;

	public short Xmin;
	public short Xmax;
	public short Ymin;
	public short Ymax;
	public short XRange;
	public short YRange;



	public TerrainCell[] AreaList;

	/// <summary>
	/// 高度区域起始偏移
	/// </summary>
	public long Height1_Offset = 0;

	public List<short> Heights1 = new();


	/// <summary>
	/// HeightOffset2 的对象数
	/// </summary>
	public long Height2_Count = 0;

	/// <summary>
	/// HeightOffset2偏移
	/// </summary>
	public long Height2_Offset = 0;

	public List<HeightParam> Heights2 = new();



	public long Unk4 = 0;    //删除后可以正常运行

	/// <summary>
	/// 组区域起始偏移
	/// </summary>
	public long GroupOffset = 0;

	/// <summary>
	/// 组区域数量
	/// </summary>
	public long GroupCount = 0;

	/// <summary>
	/// 删除后可以正常运行
	/// </summary>
	public short[] GroupMeta;


	public long UnkOffset = 0;

	public List<short> UnkData;
	#endregion



	#region Methods
	public void Read(string FilePath) => Read(new BinaryReader(new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)));

	public void Write(string SavePath) => Write(new BinaryWriter(new FileStream(SavePath, FileMode.Create)));

	public void Read(BinaryReader br)
	{
		#region Initialize
		Version = br.ReadInt16();
		FileSize = br.ReadInt64();
		TerrainID = br.ReadInt32();

		//  Pos
		Vector1 = br.Read<Vector16>();
		Vector2 = br.Read<Vector16>();

		//  Cell
		//读取边界，这里的数据可能是区块
		//比例关系  1区块 = 64坐标系单位  (1 cell = 64 pos)
		Xmin = br.ReadInt16();   //terrain-start-x
		Xmax = br.ReadInt16();
		Ymin = br.ReadInt16();   //terrain-start-y
		Ymax = br.ReadInt16();
		XRange = br.ReadInt16();  //count-x
		YRange = br.ReadInt16();  //count-y

		var Color1 = br.ReadInt32();
		var Color2 = br.ReadInt32();
		var Color3 = br.ReadInt32();
		if (Color1 != 0) Console.WriteLine("#cred# Color1不为0: " + Color1);
		if (Color2 != 112) Console.WriteLine("#cred# Color2不为112: " + Color2);
		if (Color3 != 0) Console.WriteLine("#cred# Color3不为0: " + Color3);


		Console.WriteLine($"#cblue# 区域范围 {Vector1} ~ {Vector2}");
		Console.WriteLine($"Sector  [{Xmin},{Ymin}] ~ [{Xmax},{Ymax}]  ({XRange},{YRange})");

		long MaxIndex = br.ReadInt64();       //最大区块索引
		Height1_Offset = br.ReadInt64(); //区域位置信息偏移1
		Height2_Count = br.ReadInt64();  //区域位置2 对象数量
		Height2_Offset = br.ReadInt64(); //区域位置2 信息偏移
		Unk4 = br.ReadInt64();           //地形 7430 不为0
		GroupOffset = br.ReadInt64();    //组信息位置偏移
		GroupCount = br.ReadInt64();     //
		UnkOffset = br.ReadInt64();      //未知区域偏移
		#endregion

		#region 处理单元格区域
		AreaList = new TerrainCell[XRange * YRange];
		for (int i = 0; i < AreaList.Length; i++)
		{
			var CurArea = AreaList[i] = new TerrainCell();
			CurArea.Read(br);
		}

		//对于类型3，Param2 指类型分区的数量    而其他类型则常为0，未知作用
		Console.WriteLine($"类型1 {AreaList.Where(a => a.Type == CellType.Unk1).Count()}    类型2 {AreaList.Where(a => a.Type == CellType.Unk2).Count()}");
		Console.WriteLine($"类型3 {AreaList.Where(a => a.Type == CellType.Unk3).Count()}    类型4 {AreaList.Where(a => a.Type == CellType.Unk4).Count()}");
		#endregion

		#region 高度区域偏移
		Heights1 = new List<short>();
		Heights2 = new List<HeightParam>();

		br.BaseStream.Position = Height1_Offset + 2;
		while (br.BaseStream.Position < Height2_Offset + 2) Heights1.Add(br.ReadInt16());

		while (br.BaseStream.Position < GroupOffset + 2) Heights2.Add(new HeightParam(br.ReadInt16(), br.ReadInt16()));


		Console.WriteLine($"unk4: {Unk4}  Heights: {Heights1.Count},{Heights2.Count}  GroupOffset:{GroupOffset} UnkOffset: {UnkOffset}");
		#endregion


		#region 未知集合
		br.BaseStream.Position = GroupOffset + 2;

		GroupMeta = new short[GroupCount];
		for (int i = 0; i < GroupMeta.Length; i++) GroupMeta[i] = br.ReadInt16();
		#endregion

		#region 未知集合2
		if (GroupOffset != UnkOffset)
		{
			//throw new Exception("模式未支持");

			System.Diagnostics.Trace.WriteLine($"{GroupOffset} ({GroupCount})   " + UnkOffset);
			System.Diagnostics.Trace.WriteLine(br.BaseStream.Position + "  " + br.BaseStream.Length);
		}
		#endregion

		br.Close();
		br.Dispose();
	}

	public void Write(BinaryWriter bw)
	{
		#region Initialize
		bw.Write(Version);
		bw.Write(FileSize);
		bw.Write(TerrainID);
		bw.Write(Vector1);
		bw.Write(Vector2);

		//写入边界
		bw.Write(Xmin);
		bw.Write(Xmax);
		bw.Write(Ymin);
		bw.Write(Ymax);
		bw.Write(XRange);
		bw.Write(YRange);

		bw.Write(0);
		bw.Write(112);
		bw.Write(0);

		//MaxIndex
		bw.Write((long)(AreaList.Max(a => a.Type == CellType.Unk1 || a.Type == CellType.Unk2 ? a.AreaIdx : 0) + 1));

		//偏移数据到最后重写
		bw.Write(0L);   //Height1_Offset
		bw.Write((long)Heights2.Count);
		bw.Write(0L);   //Height2_Offset
		bw.Write(Unk4);
		bw.Write(0L);
		bw.Write((long)(GroupMeta?.Length ?? 0));
		bw.Write(0L);
		#endregion

		#region 处理单元格区域
		foreach (var CurArea in AreaList)
		{
			CurArea.Write(bw);
		}
		#endregion

		// 高度集合
		var heigthOffset1 = bw.BaseStream.Position;
		Heights1.ForEach(o => bw.Write(o));

		var heigthOffset2 = bw.BaseStream.Position;
		Heights2.ForEach(o =>
		{
			bw.Write(o.Min);
			bw.Write(o.Max);
		});

		// 未知集合
		var groupOffset = bw.BaseStream.Position;
		foreach (var c in GroupMeta) bw.Write(c);


		#region 最后处理
		//重写长度
		bw.BaseStream.Position = 2;
		bw.Write(bw.BaseStream.Length - 2);

		//heigthOffset1
		bw.BaseStream.Position = 0x3A;
		bw.Write(heigthOffset1 - 2);

		//heigthOffset2
		bw.BaseStream.Position = 0x4A;
		bw.Write(heigthOffset2 - 2);

		//groupOffset
		bw.BaseStream.Position = 0x5A;
		bw.Write(groupOffset - 2);

		//groupOffset2
		bw.BaseStream.Position = 0x6A;
		bw.Write(groupOffset - 2);

		bw.Flush();
		bw.Close();
		bw.Dispose();
		#endregion
	}

	public void Save(string FilePath)
	{
		var ms = new MemoryStream();
		var bw = new BinaryWriter(ms);
		Write(bw);

		bw.BaseStream.Position = 2;
		bw.Write(FileSize = bw.BaseStream.Length - 2);

		Console.WriteLine("数据合并完成，开始执行最后封包。");


		BinaryWriter fw = new(new FileStream(FilePath, FileMode.Create));
		fw.Write(ms.ToArray());

		ms.Close();
		fw.Close();

		Console.WriteLine("执行全部结束");
	}
	#endregion
}