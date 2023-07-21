using System.Collections.ObjectModel;
using System.Collections.Specialized;

using Xylia.Windows.Controls;

namespace Xylia.Preview.UI;
public partial class ListSelect : HZH_Controls.Forms.FrmBack
{
	#region Constructor
	public ListSelect(IEnumerable<string> list)
	{
		InitializeComponent();
		this.AllowTransparency = true;

		this.comboBox1.Items.AddRange(list.ToArray());

		if (this.comboBox1.Items.Count != 0)
			this.comboBox1.Text = this.comboBox1.Items[0].ToString();


		//注册集合修改事件
		BlockTags.CollectionChanged += new NotifyCollectionChangedEventHandler((o, a) =>
		{
			this.ReferBlockTags();
		});
	}

	public ListSelect(params string[] list) : this(list.ToList())
	{

	}
	#endregion

	#region Fields
	/// <summary>
	/// 选择结果
	/// </summary>
	public List<string> Result => this.BlockTags.Select(tag => tag.Text).ToList();

	readonly private ObservableCollection<BlockTag> BlockTags = new();
	#endregion


	#region Functions
	private void Select3_FormClosing(object sender, FormClosingEventArgs e)
	{

	}

	private void ucBtnFillet3_BtnClick(object sender, System.EventArgs e)
	{
		this.DialogResult = DialogResult.OK;
		this.Close();
	}

	private void ucBtnFillet1_BtnClick(object sender, System.EventArgs e)
	{
		foreach(var tag in this.BlockTags)
		{
			if(tag.Text == this.comboBox1.Text)
			{
				HZH_Controls.Forms.FrmTips.ShowTipsWarning("已选择过此内容");
				return;
			}	
		}


		var Tag = new BlockTag(this.comboBox1.Text);
		Tag.CancelClicked += new System.ComponentModel.HandledEventHandler((s, e) =>
		{
			this.BlockTags.Remove(Tag);
			this.Controls.Remove(Tag);
		});

		this.BlockTags.Add(Tag);
	}

	/// <summary>
	/// 刷新标签位置
	/// </summary>
	private void ReferBlockTags()
	{
		//横坐标起始位置
		const int StartLeft = 15;

		int Left = StartLeft;
		int Top = comboBox1.Bottom + 10;
		foreach (var tag in this.BlockTags)
		{
			if(!this.Controls.Contains(tag)) 
				this.Controls.Add(tag);

			//如果超出长度，进行换行处理
			if(Left + tag.Width > this.Width - StartLeft - 10)
			{
				Left = StartLeft;
				Top += tag.Height + 7;
			}


			tag.Location = new Point(Left, Top);

			//计算下一个控件的横坐标起始位置
			Left += tag.Width + 15;
		}
	}
	#endregion
}