using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.UE4.Objects.UObject;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Controls;

namespace Xylia.Preview.UI.GameUI.Scene.Game_Map;
public partial class Game_MapScene
{
	#region Constructors
	public Game_MapScene()
	{
		InitializeComponent();
		TreeView.ItemsSource = FileCache.Data.Provider.GetTable<MapInfo>();
	}

	private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		if (e.OldValue == e.NewValue) return;

		LoadData((MapInfo)e.NewValue);
	}
	#endregion

	#region Map
	public void LoadData(MapInfo MapInfo)
	{
		// get current map depth
		if (MapInfo is null) return;

		this.MapDepth = MapInfo.GetMapDepth(MapInfo);
		this.MapPanel.BaseImageProperty = new ImageProperty() { ImageSet = new MyFPackageIndex(MapInfo.Imageset) };
		this.LoadMapUint(MapInfo);
	}

	private void LoadMapUint(MapInfo MapInfo, List<MapInfo>? MapTree = null)
	{
		MapTree ??= [];
		MapTree.Add(MapInfo);

		this.GetMapUnit(MapInfo, MapTree);

		if (MapInfo.Alias == "World") return;
		FileCache.Data.Provider.GetTable<MapInfo>()
			.Where(x => x.ParentMapinfo.Instance == MapInfo)
			.ForEach(x => this.LoadMapUint(x, new(MapTree)));
	}

	private void GetMapUnit(MapInfo MapInfo, List<MapInfo> MapTree)
	{
		var MapUnits = FileCache.Data.Provider.GetTable<MapUnit>().Where(o => o.Mapid == MapInfo.Id && o.MapDepth <= this.MapDepth);
		foreach (var mapunit in MapUnits)
		{
			#region init
			if (mapunit is MapUnit.Quest) continue;
			if (mapunit is MapUnit.Npc) continue;

			var res = FileCache.Provider.LoadObject<UImageSet>(mapunit.Imageset)?.GetImage();
			if (res is null) continue;

			var temp = new BnsCustomImageWidget()
			{
				//Tag = mapunit,
				//BaseImageProperty = res,

				//Width = mapunit.SizeX,
				//Height = mapunit.SizeY,
			};
			#endregion

			#region Tooltip
			string tooltip = mapunit.Name2.GetText();
			if (mapunit is MapUnit.Attraction)
			{
				var obj = mapunit.Attributes.Get<Record>("attraction")?.As<ModelElement>();
				if (obj is IAttraction attraction)
				{
					tooltip = attraction.Text + "\n" + attraction.Describe;
				}
				else if (obj != null)
				{
					tooltip = obj.ToString();
				}
			}
			else if (mapunit is MapUnit.Npc or MapUnit.Boss)
			{
				var Npc = FileCache.Data.Provider.GetTable<Npc>()[mapunit.Attributes["npc"]?.ToString()];
				if (Npc != null) tooltip = Npc.Text;
			}
			else if (mapunit is MapUnit.Link)
			{
				temp.MouseLeftButtonDown += new((o, e) =>
				{
					var map = FileCache.Data.Provider.GetTable<MapInfo>()[mapunit.Attributes["link-mapid"]?.ToString()];
					LoadData(map);
				});
			}

			temp.ToolTip = tooltip;
			temp.MouseLeftButtonDown += new((o, e) => Debug.WriteLine(mapunit.Attributes));
			#endregion

			#region Pos
			float posX = (mapunit.PositionX - MapInfo.LocalAxisX) / (MapInfo.Scale);
			float posY = (mapunit.PositionY - MapInfo.LocalAxisY) / (MapInfo.Scale);

			if (MapTree.Count > 1)
			{
				for (int idx = MapTree.Count; idx > 1; idx--)
				{
					var Map = MapTree[idx - 1];
					if (Map.UsePosInParent)
					{
						posX = Map.PosInParentX;
						posY = Map.PosInParentY;
					}
				}
			}

			Canvas.SetLeft(temp, posY);
			Canvas.SetTop(temp, MapInfo.ImageSize - posX);
			MapPanel.Children.Add(temp);
			#endregion
		}
	}
	#endregion


	#region Private Fields
	private MapUnit.MapDepthSeq MapDepth;
	#endregion
}