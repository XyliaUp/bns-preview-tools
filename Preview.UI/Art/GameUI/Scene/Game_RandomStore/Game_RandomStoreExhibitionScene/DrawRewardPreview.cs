using System.ComponentModel;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.GameUI.Scene.Game_RandomStore;
[DesignTimeVisible(false)]
public partial class DrawRewardPreview : UserControl
{
	public DrawRewardPreview() => InitializeComponent();

	protected override Point ScrollToControl(Control activeControl) => this.AutoScrollPosition;


	private void DrawRewardPreview_Load(object sender, EventArgs e) => this.LoadData();

	public void LoadData()
	{
		var RandomStore = FileCache.Data.RandomStore.FirstOrDefault(a => a.RandomStoreNumber == RandomStoreNumberSeq.RandomStore1);

		this.PromotionName.Text = "UI.RandomStore.PromotionName".GetText();
		this.label3.Text += $"（可以获得{ RandomStore?.AcquireDrawRewardSetRepeatCount }轮）";

		int LocY = 185;
		foreach (var Info in FileCache.Data.RandomStoreDrawReward.OrderBy(a => a.RequiredDrawCount))
		{
			var cell = new DrawRewardCell();
			cell.LoadData(Info);
			cell.Location = new Point(0, LocY);

			this.Controls.Add(cell);
			LocY = cell.Bottom + 20;
		}
	}
}