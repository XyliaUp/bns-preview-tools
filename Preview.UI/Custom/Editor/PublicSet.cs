using Xylia.Preview.UI.Custom.Editor;

namespace Xylia.Preview.UI.Custom.Editor;
public partial class PublicSet : Form
{
	public static int Index = 0;
	public Action<string> GetAction = null;
	public static Settings ps = Settings.Default;


	public PublicSet(Action<string> action)
	{
		GetAction = action;

		InitializeComponent();
		this.TopMost = true;
	}

	public PublicSet()
	{
		InitializeComponent();
		this.TopMost = true;
	}


	/*Functions*/
	private void SetSettings(SetType type)
	{
		switch (type)
		{
			case SetType.Default:
			{
				FontColorPanel.BackColor = Color.FromArgb(228, 228, 228);
				BackgroundColorPanel.BackColor = Color.FromArgb(56, 56, 56);
				HighlightsColorPanel.BackColor = Color.FromArgb(101, 51, 6);
				FontNameComboBox.Text = "Consolas";
				FontSizeComboBox.Text = 11.ToString();
				Tip.Message("重置成功");
			}
			break;

			case SetType.Save:
			{
				ps.RichForeColor = this.FontColorPanel.BackColor;
				ps.RichBackColor = this.BackgroundColorPanel.BackColor;
				ps.HighlightsColor = this.HighlightsColorPanel.BackColor;
				ps.RichTextFont = this.FontNameComboBox.Text;


				if (int.TryParse(FontSizeComboBox.Text, out int FontSize))
				{
					ps.RichTextSize = FontSize;
				}
				else
				{
					ps.RichTextSize = 11;
					//FontSizeComboBox.Text = FontSize.ToString();
				}

				ps.Save();
				Tip.Message("保存成功");
			}
			break;
		}
	}


	public enum SetType
	{
		Default,
		Save,
	}

	/*Functions*/





	/*Form Events*/
	private void SettingsForm_Load(object sender, EventArgs e)
	{
		try { SettingsTabControl.SelectedIndex = Index; }
		catch { }



		foreach (FontFamily fonts in FontFamily.Families)
		{
			FontNameComboBox.Items.Add(fonts.Name);
		}

		FontNameComboBox.Text = ps.RichTextFont;
		FontSizeComboBox.Text = ps.RichTextSize.ToString();

		FontColorPanel.BackColor = ps.RichForeColor;
		BackgroundColorPanel.BackColor = ps.RichBackColor;
		HighlightsColorPanel.BackColor = ps.HighlightsColor;



		//Config.Checked_Read(chk_Exit);
		//Config.Checked_Read(chk_Write);
		//Config.Checked_Read(chk_Drag);
		//Config.Checked_Read(chk_AutoUp);
		//Config.Checked_Read(chk_TwiceExit);
	}
	/*Form Events*/


	/*Buttons*/
	private void ResetSettingsButton_Click(object sender, EventArgs e)
	{
		if (MessageBox.Show("确定需要重置吗?", "设置提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
		{
			SetSettings(SetType.Default);
		}
	}

	private void SaveSettingsButton_Click(object sender, EventArgs e)
	{
		SetSettings(SetType.Save);
	}
	/*Buttons*/


	/*Settings Section*/
	private void FontColorPanel_Click_1(object sender, EventArgs e)
	{
		colorDialog1.Color = FontColorPanel.BackColor;
		using (new CenterWinDialog(this))
		{
			colorDialog1.ShowDialog();
		}
		FontColorPanel.BackColor = colorDialog1.Color;
	}

	private void BackgroundColorPanel_Click(object sender, EventArgs e)
	{
		colorDialog1.Color = BackgroundColorPanel.BackColor;
		using (new CenterWinDialog(this))
		{
			colorDialog1.ShowDialog();
		}

		BackgroundColorPanel.BackColor = colorDialog1.Color;
	}

	private void HighlightsColorPanel_Click(object sender, EventArgs e)
	{
		colorDialog1.Color = HighlightsColorPanel.BackColor;
		//using (new CenterWinDialog(this))
		//{
		colorDialog1.ShowDialog();
		// }
		HighlightsColorPanel.BackColor = colorDialog1.Color;
	}


	private void SettingsForm_MouseEnter(object sender, EventArgs e)
	{
		this.TopMost = false;
	}


	private void SettingsTabControl_SelectedIndexChanged(object sender, EventArgs e)
	{
		var TabPage = SettingsTabControl.TabPages[SettingsTabControl.SelectedIndex];
	}


	private void PublicSet_FormClosed(object sender, FormClosedEventArgs e)
	{
		this.DialogResult = DialogResult.OK;
	}

	private void BackgroundColorPanel_Paint(object sender, PaintEventArgs e)
	{

	}
}
