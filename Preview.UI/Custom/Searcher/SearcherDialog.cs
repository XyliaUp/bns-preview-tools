using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.GameUI.Scene.Searcher;
public partial class SearcherDialog : Form
{
	#region Constructor
	private List<FilterInfo> filters;

	public SearcherDialog()
	{
		InitializeComponent();
		filters = new();
	}
	#endregion

	#region Dialog
	private void Confirm_BtnClick(object sender, EventArgs e)
	{
		this.DialogResult = DialogResult.OK;
	}

	public override string Text => this.textBox1?.Text;

	public IEnumerable<FilterInfo> ActivateFilter => filters.Where(f => f.Checked);
	#endregion




	private void SearcherDialog_Shown(object sender, EventArgs e)
	{
		int LocX = 30, LocY = this.Confirm.Bottom + 5;
		foreach (var Filter in filters)
		{
			var c = new CheckBox
			{
				Text = Filter.TagName,
				Checked = Filter.Checked,
				Location = new Point(LocX, LocY),
			};

			c.CheckedChanged += new((o, e) => Filter.Checked = c.Checked);
			this.Controls.Add(c);


			if (this.Width > (LocX + 20)) LocX = c.Right;
			else
			{
				LocX = 30;
				LocY = c.Bottom + 2;
			}
		}
	}

	public void Add(FilterInfo filter)
	{
		filters.Add(filter);
	}


	public static List<BaseRecord> Filter(string rule, IEnumerable<FilterInfo> filters)
	{
		if (string.IsNullOrWhiteSpace(rule) || !filters.Any())
			return null;


		bool Contains(Type type) => filters.FirstOrDefault(f => f.Tag == type) != null;

		List<BaseRecord> entity = new();
		if (Contains(typeof(Item))) entity.Add(rule.GetItemInfo().First());
		if (Contains(typeof(Npc))) entity.Add(FileCache.Data.Npc[rule]);

		return entity;
	}
}

public class FilterInfo
{
	public FilterInfo(Type tag, string tagName, bool defaultValue = true)
	{
		this.Tag = tag;
		this.TagName = tagName;
		this.Checked = defaultValue;
	}

	public Type Tag;

	public string TagName;

	public bool Checked = true;
}