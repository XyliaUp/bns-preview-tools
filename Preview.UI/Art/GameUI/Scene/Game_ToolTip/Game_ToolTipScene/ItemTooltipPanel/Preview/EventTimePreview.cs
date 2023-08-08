using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel;
public partial class EventTimePreview : TitlePanel
{
	#region Constructor
	public EventTimePreview() => InitializeComponent();

	public DateTime EventExpirationTime
	{
		set
		{
			//判断是否过期
			if (value < DateTime.Now)
			{
				this.pictureBox1.Visible = false;
				this.EventTime_FixedDate.Location = new Point(7, this.EventTime_FixedDate.Location.Y);

				this.EventTime_FixedDate.Text = $"过期 ({value:yyyy年M月d日} 截止)";
				this.EventTime_FixedDate.ForeColor = Color.FromArgb(255, 88, 66);
			}
			else
			{
				this.pictureBox1.Visible = true;
				this.EventTime_FixedDate.Location = new Point(30, this.EventTime_FixedDate.Location.Y);

				this.EventTime_FixedDate.Text = $"可在{value:yyyy年M月d日}定期维护前使用";
				this.EventTime_FixedDate.ForeColor = Color.White;
			}
		}
	}
	#endregion


	#region Functions
	public override void LoadData(BaseRecord record)
	{
		ItemEvent Record = record as ItemEvent;

		this.Title = Record.Name2.GetText();
		this.EventExpirationTime = Record.EventExpirationTime;
	}
	#endregion
}