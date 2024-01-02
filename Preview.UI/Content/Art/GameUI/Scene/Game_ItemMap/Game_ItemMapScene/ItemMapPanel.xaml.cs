using System.IO;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Data.Models.Sequence;
using Xylia.Preview.UI.Extensions;

namespace Xylia.Preview.UI.GameUI.Scene.Game_ItemMap;
public partial class ItemMapPanel
{
	public ItemMapPanel()
	{
		this.DataContext = this;
		InitializeComponent();

		InitData();
	}

	[RelayCommand]
	private void EquipTypeChange(string value)
	{
		ItemMapPanel_MapField.Update(value, JobSeq.암살자);
		MapField_Scroller.ScrollToRightEnd();
	}

	private void ItemMapPanel_MapField_RoutesChanged(object sender, IEnumerable<ItemGraphRouteHelper> routes)
	{
		if (!routes.Any())
		{
			Growl.Info("没有可到达的成长路径");
			return;
		}

		foreach (var route in routes)
		{
			Growl.Warning(route.ToString() +
				route.Ingredients.Aggregate("", (a, n) => a + $"\n{n.Key.ItemNameOnly} {n.Value}"));
		}
	}

	protected override void OnPreviewKeyDown(KeyEventArgs e)
	{
		if (e.Key == Key.S)
		{
			File.WriteAllBytes(@"C:\Users\10565\Desktop\snap.png", ItemMapPanel_MapField.Snapshot());
		}

		if(e.Key == Key.F11)
		{
			ItemMapPanel_MapField.ShowLines = true;

			FileCache.Data.Get<ItemGraph>().Source.Clear();
			InitData();

			Growl.Success("reload");
		}
	}

	private void InitData()
	{
		var table = FileCache.Data.Get<ItemGraph>(reload: true);
		var seeds = table.Where(record => record is ItemGraph.Seed seed).Cast<ItemGraph.Seed>().ToArray();

		// NOTE: Custom helper
		for (int i = 0; i < seeds.Length; i++)
		{
			var seed = seeds[i];

			var item = seed.SeedItem.FirstOrDefault().Instance;
			if (item is null) continue;

			if (seed.UseImprove) ItemGraphRouteHelper.CreateEdge(item, table);
		}
	}
}