using System.ComponentModel;
using System.Windows.Forms;

using Xylia.Preview.Resources;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell
{
	[DesignTimeVisible(false)]
	public partial class SetItemEffect : UserControl
	{
		#region Constructor
		public SetItemEffect()
		{
			InitializeComponent();
			this.ContentPanel.FontName = "00008130.UI.Label_Green03_12";
		}
		#endregion


		#region Fields
		private int _count = 1;

		/// <summary>
		/// 套装数量设置
		/// </summary>
		[Category("Data"), Description("套装数量")]
		public int Count
		{
			get => _count;
			set
			{
				switch (_count = value)
				{
					case 2: this.CountTooltip.Image = Resource_BNSR.BNSR_Gem02_Image; break;
					case 3: this.CountTooltip.Image = Resource_BNSR.BNSR_Gem03_Image; break;
					case 4: this.CountTooltip.Image = Resource_BNSR.BNSR_Gem04_Image; break;
					case 5: this.CountTooltip.Image = Resource_BNSR.BNSR_Gem05_Image; break;
					case 6: this.CountTooltip.Image = Resource_BNSR.BNSR_Gem06_Image; break;
					case 7: this.CountTooltip.Image = Resource_BNSR.BNSR_Gem07_Image; break;
					case 8: this.CountTooltip.Image = Resource_BNSR.BNSR_Gem08_Image; break;
					case 9: this.CountTooltip.Image = Resource_BNSR.BNSR_Gem09_Image; break;
					case 10: this.CountTooltip.Image = Resource_BNSR.BNSR_Gem10_Image; break;

					case 1 or _: this.CountTooltip.Image = Resource_BNSR.BNSR_Gem01_Image; break;
				}
			}
		}


		/// <summary>
		/// 套装效果设置
		/// </summary>
		[Category("Data"), Description("套装效果")]
		public override string Text
		{
			get => this.ContentPanel.Text;
			set
			{
				this.ContentPanel.Text = value;
				this.Height = this.ContentPanel.Height + 5;
			}
		}
		#endregion
	}
}