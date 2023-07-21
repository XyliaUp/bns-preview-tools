using CUE4Parse.BNS.Conversion;

using Xylia.Extension;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Interface;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls;
using Xylia.Preview.UI.Custom.Controls.Forms;

using static Xylia.Preview.Data.Record.MapUnit;

namespace Xylia.Preview.GameUI.Scene.Game_Map;
public partial class Game_MapScene : PreviewFrm
{
	#region Constructor
	public Game_MapScene() : this("World") { }

	public Game_MapScene(string Rule)
	{
		InitializeComponent();

		this.LoadData(Rule);
	}
	#endregion


	#region Functions
	private MapInfo _mapInfo;

	private MapDepthSeq _mapDepth;

	public void LoadData(string Rule)
	{
		_mapInfo = FileCache.Data.MapInfo[Rule];
		if (_mapInfo is null) return;

		if (_mapInfo.ParentMapinfo != null)
		{
			this.OpenParentMap.Visible = true;
			this.OpenParentMap.Click += new EventHandler((o, e) => Task.Run(() => new Game_MapScene(_mapInfo.ParentMapinfo.alias).ShowDialog()));
		}

		var MapGroup1 = FileCache.Data.MapGroup1[_mapInfo.MapGroup1];
		var MapGroup2 = FileCache.Data.MapGroup2[_mapInfo.MapGroup2];


		// get current map depth
		_mapDepth = MapInfo.GetMapDepth(this._mapInfo);


		Trace.WriteLine(_mapInfo.Attributes);

		this.Text = $"[{_mapInfo.Name2.GetText()}]";
		this.pictureBox1.Image = _mapInfo.Imageset.GetUObject().GetImage();

		this.LoadMapUint(_mapInfo);
	}

	private void LoadMapUint(MapInfo MapInfo, List<MapInfo> MapTree = null)
	{
		MapTree ??= new();
		MapTree.Add(MapInfo);

		this.GetMapUnit(MapInfo, MapTree);


		if (MapInfo.alias == "World") return;
		FileCache.Data.MapInfo
			.Where(o => o.ParentMapinfo?.alias == MapInfo.alias)
			.ForEach(o => this.LoadMapUint(o, new(MapTree)));
	}

	/// <summary>
	/// 获取指定地图的单元
	/// </summary>
	/// <param name="CurMapInfo"></param>
	/// <param name="MapTree"></param>
	private void GetMapUnit(MapInfo CurMapInfo, List<MapInfo> MapTree)
	{
		var MapUnits = FileCache.Data.MapUnit.Where(o => o.Mapid == CurMapInfo.Id && o.MapDepth <= _mapDepth);
		foreach (var mapunit in MapUnits)
		{
			if (mapunit is MapUnit.Quest) continue;
			if (mapunit is MapUnit.Npc) continue;    //该类型按接取的任务进行显示


			var res = mapunit.Imageset.GetUObject().GetImage();
			if (res is null) continue;

			#region Get Pos
			//如果地形一致, 不用转换 pos
			var Pos = GetPoint(mapunit.PositionX, mapunit.PositionY, this._mapInfo);
			if (MapTree.Count > 1)
			{
				for (int idx = MapTree.Count; idx > 1; idx--)
				{
					var Map = MapTree[idx - 1];
					if (Map.UsePosInParent) Pos = new Point((int)Map.PosInParentX, (int)Map.PosInParentY);
				}
			}
			#endregion


			var temp = new PictureBox()
			{
				Name = mapunit.alias,
				BackColor = Color.Transparent,
				SizeMode = PictureBoxSizeMode.AutoSize,

				Location = Pos,
				Image = res.GetThumbnailImage(mapunit.SizeX == 0 ? res.Width : mapunit.SizeX, mapunit.SizeY == 0 ? res.Height : mapunit.SizeY, null, IntPtr.Zero),
			};
			this.pictureBox1.Controls.Add(temp);

			#region Event
			temp.Click += new EventHandler((sender, e) =>
			{
				if (mapunit is MapUnit.Link) Task.Run(() => new Game_MapScene(mapunit.Attributes["link-mapid"]).ShowDialog());

				Debug.WriteLine(mapunit.Attributes);
			});
			#endregion

			#region Tooltip
			string tooltip = mapunit.Name2.GetText();
			if (mapunit is MapUnit.Attraction)
			{
				var obj = mapunit.Attributes["attraction"].CastObject();
				if (obj is null) continue;

				tooltip = $"{obj.GetType().Name}: " + obj.GetName();
				if (obj is IAttraction attraction) tooltip += "\n" + attraction.GetDescribe();
			}
			else if (mapunit is MapUnit.Npc or MapUnit.Boss)
			{
				var Npc = FileCache.Data.Npc[mapunit.Attributes["npc"]];
				if (Npc != null) tooltip = Npc.GetName();
			}

			TestTooltip2.SetTooltip(temp, tooltip);
			#endregion
		}
	}

	/// <summary>
	/// 转换为当前坐标点
	/// </summary>
	/// <param name="PositionX"></param>
	/// <param name="PositionY"></param>
	/// <param name="ParentMapInfo"></param>
	/// <returns></returns>
	public static Point GetPoint(float PositionX, float PositionY, MapInfo ParentMapInfo)
	{
		float PointX = (PositionX - ParentMapInfo.LocalAxisX) / (ParentMapInfo.Scale);     //1050
		float PointY = (PositionY - ParentMapInfo.LocalAxisY) / (ParentMapInfo.Scale);     //1000

		//转换为当前坐标轴的位置
		return new Point((int)PointY, (int)(ParentMapInfo.ImageSize - PointX));
	}

	private void pictureBox1_DoubleClick(object sender, EventArgs e)
	{
		var me = e as MouseEventArgs;

		Debug.WriteLine(me.X + "  " + me.Y);
	}
	#endregion
}