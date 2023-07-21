using Xylia.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom;

namespace Xylia.Preview.GameUI.Scene.Searcher;
public partial class SearcherResult : Form
{
	public SearcherResult(IEnumerable<BaseRecord> records)
	{
		InitializeComponent();

		this.ListPreview.MaxItemNum = 0;
		this.ListPreview.Items.AddRange(records);
	}

	private void SearcherResult_Shown(object sender, EventArgs e)
	{
		this.ListPreview.RefreshList();
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

			MyTest.TestMesh(appearance.BodyMeshName, npc.AnimSet, appearance.BodyMaterialName);
		}
	}
}