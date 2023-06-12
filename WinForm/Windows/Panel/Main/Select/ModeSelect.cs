using System;

namespace Xylia.Match
{
	public partial class ModeSelect : HZH_Controls.Forms.FrmBack
	{
		#region Constructor
		public enum State
		{
			None,

			Text,
			Xlsx,
		}

		public State Result = State.None;

		public ModeSelect()
		{
			InitializeComponent();
			this.AllowTransparency = true;
		}
		#endregion

		#region Functions (UI)

		private void Select_Load(object sender, EventArgs e)
		{
			pictureBox1.MouseEnter += ((s, o) => { pictureBox1.BackColor = System.Drawing.Color.FromArgb(255, 224, 192); });
			pictureBox2.MouseEnter += ((s, o) => { pictureBox2.BackColor = System.Drawing.Color.FromArgb(255, 224, 192); });

			pictureBox1.MouseLeave += ((s, o) => { pictureBox1.BackColor = System.Drawing.Color.FromArgb(247, 247, 247); });
			pictureBox2.MouseLeave += ((s, o) => { pictureBox2.BackColor = System.Drawing.Color.FromArgb(247, 247, 247); });

		}

		private void PictureBox1_Click(object sender, EventArgs e)
		{
			Result = State.Text;
			this.Close();
		}

		private void PictureBox2_Click(object sender, EventArgs e)
		{
			Result = State.Xlsx;
			this.Close();
		}
		#endregion
	}
}