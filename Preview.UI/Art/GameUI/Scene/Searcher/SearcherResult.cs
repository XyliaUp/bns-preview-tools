using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Xylia.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls.List;

namespace Xylia.Preview.GameUI.Scene.Searcher
{
	public partial class SearcherResult : Form
	{
		public SearcherResult(IEnumerable<BaseRecord> records)
		{
			InitializeComponent();

			var cells = new List<ListCell>();
			foreach (var record in records)
			{
				var cell = new InfoListCell();
				cells.Add(cell);

				string text = null;
				if (record is Npc npc)
				{
					text = $"{npc.Title2.GetText()} {npc.Name2.GetText()} ({ record.alias })";

					//get map info
					var MapUnit = FileCache.Data.MapUnit.Where(Info => Info.alias.MyContains(record.alias)).FirstOrDefault();
					if (MapUnit != null)
					{
						cell.ShowRightText = true;
						cell.RightText = FileCache.Data.MapInfo[MapUnit.Mapid]?.Name2.GetText();
					}
				}

				cell.LeftText = text ?? record.ToString();
			};

			this.storeListPreview1.Cells = cells;
		}
	}
}