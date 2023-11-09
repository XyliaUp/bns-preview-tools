using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Views.Selector;
public partial class SearcherDialog : Window
{
	private List<FilterInfo> filters;

	public SearcherDialog()
	{
		InitializeComponent();
		filters = new();
	}


	#region Dialog
	private void Confirm_Click(object sender, RoutedEventArgs e)
	{
		this.DialogResult = true;
	}

	public string Text => this.FilterRule.Text;

	public IEnumerable<FilterInfo> ActivateFilter => filters.Where(f => f.Checked);
	#endregion

	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		//int LocX = 30, LocY = this.Confirm.Bottom + 5;
		//foreach (var Filter in filters)
		//{
		//	var c = new CheckBox
		//	{
		//		Text = Filter.TagName,
		//		Checked = Filter.Checked,
		//		Location = new System.Drawing.Point(LocX, LocY),
		//	};

		//	c.CheckedChanged += new((o, e) => Filter.Checked = c.Checked);
		//	this.Controls.Add(c);


		//	if (this.Width > (LocX + 20)) LocX = c.Right;
		//	else
		//	{
		//		LocX = 30;
		//		LocY = c.Bottom + 2;
		//	}
		//}
	}

	public void Add(FilterInfo filter)
	{
		filters.Add(filter);
	}

	public static List<Record> Filter(string rule, IEnumerable<FilterInfo> filters)
	{
		if (string.IsNullOrWhiteSpace(rule) || !filters.Any())
			return null;


		bool Contains(Type type) => filters.FirstOrDefault(f => f.Tag == type) != null;

		List<Record> entity = new();
		//if (Contains(typeof(Item))) entity.AddItem(rule.GetItemInfo().FirstOrDefault());
		if (Contains(typeof(Npc))) entity.AddItem(FileCache.Data.Npc[rule]);

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
