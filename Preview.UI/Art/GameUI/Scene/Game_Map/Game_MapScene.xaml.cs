using System.Windows.Forms;

using CUE4Parse.BNS.Conversion;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.Cast;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Interface;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Extension;

using static Xylia.Preview.Data.Models.MapUnit;

namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Map;
public partial class Game_MapScene : Window
{
	public Game_MapScene()
	{
		InitializeComponent();
		TreeView.ItemsSource = FileCache.Data.MapInfo;
	}

	#region Map
	private MapDepthSeq Depth;

	private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		if (e.OldValue == e.NewValue) return;

		LoadData(e.NewValue as MapInfo);
	}

	public void LoadData(MapInfo MapInfo)
	{
		if (MapInfo is null) return;

		if (MapInfo.ParentMapinfo.Instance != null)
		{
			this.OpenParentMap.Visibility = Visibility.Visible;
			//this.OpenParentMap.Click += new RoutedEventHandler((o, e) =>
			//	Task.Run(() => new Game_MapScene(MapInfo.ParentMapinfo.Alias).ShowDialog()));
		}

		// get current map depth
		Depth = MapInfo.GetMapDepth(MapInfo);

		//Trace.WriteLine(MapInfo.Attributes);

		this.Image.Source = FileCache.Provider.LoadObject(MapInfo.Imageset)?.GetImage()?.ToBitmap().ToImageSource();

		this.LoadMapUint(MapInfo);
	}


	private void LoadMapUint(MapInfo MapInfo, List<MapInfo> MapTree = null)
	{
		MapTree ??= new();
		MapTree.Add(MapInfo);

		//this.GetMapUnit(MapInfo, MapTree);

		if (MapInfo.Alias == "World") return;
		FileCache.Data.MapInfo
			.Where(o => o.ParentMapinfo.Instance == MapInfo)
			.ForEach(o => this.LoadMapUint(o, new(MapTree)));
	}

	private void GetMapUnit(MapInfo CurMapInfo, List<MapInfo> MapTree)
	{
		var MapUnits = FileCache.Data.MapUnit.Where(o => o.Mapid == CurMapInfo.Id && o.MapDepth <= Depth);
		foreach (var mapunit in MapUnits)
		{
			if (mapunit is MapUnit.Quest) continue;
			if (mapunit is MapUnit.Npc) continue;    //�����Ͱ���ȡ�����������ʾ

			var res = FileCache.Provider.LoadObject(mapunit.Imageset)?.GetImage().ToBitmap();
			if (res is null) continue;

			#region Get Pos
			//�������һ��, ����ת�� pos
			//var Pos = GetPoint(mapunit.PositionX, mapunit.PositionY, this._mapInfo);
			//if (MapTree.Count > 1)
			//{
			//	for (int idx = MapTree.Count; idx > 1; idx--)
			//	{
			//		var Map = MapTree[idx - 1];
			//		if (Map.UsePosInParent) Pos = new System.Drawing.Point((int)Map.PosInParentX, (int)Map.PosInParentY);
			//	}
			//}
			#endregion


			var temp = new PictureBox()
			{
				Name = mapunit.Alias,
				BackColor = System.Drawing.Color.Transparent,
				SizeMode = PictureBoxSizeMode.AutoSize,

				//Location = Pos,
				Image = res.GetThumbnailImage(mapunit.SizeX == 0 ? res.Width : mapunit.SizeX, mapunit.SizeY == 0 ? res.Height : mapunit.SizeY, null, IntPtr.Zero),
			};
			//this.Image.Add(temp);

			#region Event
			temp.Click += new EventHandler((sender, e) =>
			{
				//if (mapunit is MapUnit.Link) Task.Run(() => new Game_MapScene(mapunit.Attributes["link-mapid"]).ShowDialog());

				Debug.WriteLine(mapunit.Attributes);
			});
			#endregion

			#region Tooltip
			string tooltip = mapunit.Name2.GetText();
			if (mapunit is MapUnit.Attraction)
			{
				var obj = new Ref<Record>(mapunit.Attributes["attraction"]).Instance;
				if (obj != null) tooltip = obj.GetAttraction();
			}
			else if (mapunit is MapUnit.Npc or MapUnit.Boss)
			{
				var Npc = FileCache.Data.Npc[mapunit.Attributes["npc"]];
				if (Npc != null) tooltip = Npc.GetName();
			}

			//TestTooltip2.SetTooltip(temp, tooltip);
			#endregion
		}
	}

	public static System.Drawing.Point GetPoint(float PositionX, float PositionY, MapInfo ParentMapInfo)
	{
		float PointX = (PositionX - ParentMapInfo.LocalAxisX) / (ParentMapInfo.Scale);     //1050
		float PointY = (PositionY - ParentMapInfo.LocalAxisY) / (ParentMapInfo.Scale);     //1000

		return new System.Drawing.Point((int)PointY, (int)(ParentMapInfo.ImageSize - PointX));
	}
	#endregion
}