using System.ComponentModel;
using System.Text.RegularExpressions;

using Xylia.Net;

namespace Xylia.Match.Windows.Panel;

[DesignTimeVisible(false)]
public partial class Youdao : UserControl
{
	public Youdao()
	{
		InitializeComponent();
		CheckForIllegalCrossThreadCalls = false;
	}

	private void Btn_Start_Click(object sender, EventArgs e)
	{
		Regex re = new Regex(@"(?<url>http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?)");
		MatchCollection mc = re.Matches(textBox1.Text);

		if (mc.Count != 1)
		{
			Tip.Message("无效的链接信息");
			return;
		}


		var shareUrl = mc[0].Result("${url}");
		var downUrl = YDNoteShare.Resolve(shareUrl, out var info);


		label3.Text = $"文件名称：{info.tl}\n\n浏览次数：{info.pv}";
		if (!checkBox1.Checked) textBox3.Text = downUrl.ToString();
		else
		{
			textBox3.Text = info.key;
			textBox2.Text = info.p;
		}
	}

	private void Button1_Click(object sender, EventArgs e)
	{
		Clipboard.SetText(textBox3.Text);
		MessageBox.Show("粘贴至剪贴板成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}

	private void TextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		textBox1.SelectAll();
	}

	private void TextBox3_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		textBox3.SelectAll();
	}

	private void CheckBox1_CheckedChanged(object sender, EventArgs e)
	{
		textBox2.Visible = Copy2.Visible = checkBox1.Checked;
	}
}
