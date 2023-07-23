using CUE4Parse.BNS.Exports;
using CUE4Parse.UE4.Assets.Exports;

using Xylia.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom;
using Xylia.Preview.UI.FModel.Views;

namespace Xylia.Preview.GameUI.Scene.Searcher;
public partial class SearcherResult : Form
{
	IEnumerable<BaseRecord> Records;

	public SearcherResult(IEnumerable<BaseRecord> records)
	{
		InitializeComponent();

		this.Records = records;
		this.ListPreview.MaxItemNum = 0;
		this.ListPreview.Items = new(records);
	}

	private void ListPreview_DrawItem(object sender, PaintEventArgs e)
	{
		if (sender is Npc npc)
		{
			string Text = $"{npc.Title2.GetText()} {npc.Name2.GetText()} ({npc.alias})";

			var MapUnit = FileCache.Data.MapUnit.Where(Info => Info.alias.MyContains(npc.alias)).FirstOrDefault();
			string Text2 = MapUnit is null ? null : FileCache.Data.MapInfo[MapUnit.Mapid]?.Name2.GetText();

			e.Graphics.FillRectangle(new SolidBrush(this.ListPreview.BackColor), e.ClipRectangle);
			e.Graphics.DrawString(Text, this.ListPreview.Font, new SolidBrush(this.ListPreview.ForeColor), e.ClipRectangle, new StringFormat { LineAlignment = StringAlignment.Near });
		}
	}

	private void ListPreview_SelectItem(object sender, EventArgs e)
	{
		if (sender is Npc npc)
		{
			var appearance = npc?.Appearance;
			if (appearance is null) return;

			new ModelData()
			{
				Export = FileCache.PakData.LoadObject<UObject>(appearance.BodyMeshName),
				AnimSet = FileCache.PakData.LoadObject<UAnimSet>(npc.AnimSet),
				cols = new string[] { appearance.BodyMaterialName },
			}.Run();
		}
	}



	private void SearcherResult_Shown(object sender, EventArgs e)
	{
		this.ListPreview.RefreshList();
	}


	private void MenuItem_Filter_Click(object sender, EventArgs e)
	{
		var dialog = new SearcherDialog();
		if (dialog.ShowDialog() != DialogResult.OK) return;

		bool Contains(string text) => text?.Contains(dialog.Text, StringComparison.OrdinalIgnoreCase) ?? false;


		this.ListPreview.Items = new(Records.Where(reocrd =>
		{
			if (reocrd is Npc npc)
			{
				return Contains(npc.alias) || Contains(npc.Name2.GetText());
			}

			return false;
		}));
		this.ListPreview.RefreshList();
	}
}