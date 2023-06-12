using System.ComponentModel;
using System.Windows.Forms;

using Xylia.Preview.Data.Record;

namespace Xylia.Preview.GameUI.Scene.Skill
{
	[DesignTimeVisible(false)]
	public partial class TraitTierCell : UserControl
	{
		public TraitTierCell() => InitializeComponent();



		private SkillTrait _skillTrait;

		public SkillTrait SkillTrait
		{
			get => this._skillTrait;
			set
			{
				this._skillTrait = value;
				if (value is null) return;

				this.Visible = value.Enable;
				this.TraitName.Text = value.Name2.GetText();
				this.TraitIcon.Image = value.Icon.GetIcon();
			}
		}
	}
}
