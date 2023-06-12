using System;
using System.Windows.Forms;

using Xylia.Configure;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.GameUI.Scene.Skill
{
	public partial class SkillFrm : Form
	{
		public SkillFrm()
		{
			InitializeComponent();
			this.textBox1.Text = Ini.ReadValue("Preview", "skill#searchrule");

			this.Controls.Add(this.SkillPreview);
			this.SkillPreview.Refresh();
		}

		public SkillFrm(Data.Record.Skill Skill)
		{
			InitializeComponent();
			this.Controls.Add(this.SkillPreview);
			this.textBox1.Visible = true;

			this.LoadData(Skill);
		}




		public SkillPreview SkillPreview = new();

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			var rule = this.textBox1.Text;
			Ini.WriteValue("Preview", "skill#searchrule", rule);

			this.LoadData(int.TryParse(rule, out var id) ? 
				FileCache.Data.Skill3[id, 1] : 
				FileCache.Data.Skill3[rule]);
		}

		public void LoadData(Data.Record.Skill Skill)
		{
			this.Text = "查看技能 " + Skill?.alias;

			this.SkillPreview.LoadData(Skill);
			this.SkillPreview.Refresh();
		}
	}
}