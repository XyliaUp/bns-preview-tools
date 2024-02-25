using System.Windows;
using System.Windows.Controls;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Data.Models.Sequence;
using Xylia.Preview.UI.Controls;
using Xylia.Preview.UI.Documents;

namespace Xylia.Preview.UI.GameUI.Scene.Game_Tooltip;
public partial class ItemTooltipPanel
{
	public ItemTooltipPanel()
	{
		InitializeComponent();
		this.Loaded += OnLoaded;
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		if (DataContext is not Item record) return;

		#region Decompose 
		var pages = DecomposePage.LoadFrom(record.DecomposeInfo);
		if (pages.Count > 0)
		{
			var page = pages[0];
			page.Update(DecomposeDescription.Children);
		}
		#endregion
	}
}


internal sealed class DecomposePage
{
	#region Fields
	public JobSeq Job;

	public Reward DecomposeReward { get; private set; }

	public Tuple<Item, short> OpenItem { get; private set; }
	#endregion

	#region Methods
	public static List<DecomposePage> LoadFrom(ItemDecomposeInfo info)
	{
		var pages = new List<DecomposePage>();

		#region reward
		for (int index = 0; index < info.DecomposeReward.Length; index++)
		{
			var reward = info.DecomposeReward[index];
			var item2 = info.Decompose_By_Item2[index];
			if (reward is null) continue;

			pages.Add(new DecomposePage() { DecomposeReward = reward, OpenItem = item2 });
		}
		#endregion

		#region job reward
		var group_job = info.DecomposeJobRewards
			.Where(x => x.Value is not null)
			.Select(x => new DecomposePage() { Job = x.Key, DecomposeReward = x.Value, });

		if (group_job.Any())
		{
			// combine data according to cell num
			//int num = group_job.Sum(group => group.Preview.Count);
			//if (num >= 30) pages.AddRange(group_job);
			//else pages.Add(new DecomposePage()
			//{
			//	Job = JobSeq.JobNone,
			//	DecomposeReward = group_job.FirstOrDefault().DecomposeReward,
			//	OpenItem = info.Job_Decompose_By_Item2.FirstOrDefault(),
			//	Preview = group_job.SelectMany(group => group.Preview).ToList(),
			//});
		}
		#endregion

		return pages;
	}

	public void Update(UIElementCollection collection)
	{
		collection.Clear();

		DecomposeReward.FixedItem.ForEach(x => x.Instance, (item, i) =>
		{
			var min = DecomposeReward.FixedItemMin[i];
			var max = DecomposeReward.FixedItemMax[i];
			var param = new DataParams { [2] = item, [3] = min, [4] = max };

			collection.Add(new BnsCustomLabelWidget()
			{
				Params = param,
				Text = (min == max ? min == 1 ?
					"UI.ItemTooltip.RandomboxPreview.Fixed" :
					"UI.ItemTooltip.RandomboxPreview.Fixed.Min" :
					"UI.ItemTooltip.RandomboxPreview.Fixed.MinMax").GetText(),
			});
		});
		DecomposeReward.SelectedItem.ForEach(x => x.Instance, (item, i) =>
		{
			var count = DecomposeReward.SelectedItemCount[i];
			collection.Add(new BnsCustomLabelWidget()
			{
				Text = "UI.ItemTooltip.RandomboxPreview.Selected".GetText(),
				Params = new DataParams { [2] = item, [3] = count }
			});
		});
		DecomposeReward.RandomItem.ForEach(x => x.Instance, (item, i) =>
		{
			collection.Add(new BnsCustomLabelWidget()
			{
				Text = "UI.ItemTooltip.RandomboxPreview.Random".GetText(),
				Params = new DataParams { [2] = item }
			});
		});
	}
	#endregion
}