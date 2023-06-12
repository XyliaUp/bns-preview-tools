using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Game_ItemGrowth2;

namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2
{
	[DesignTimeVisible(false)]
	public partial class EquipmentGuidePage : UserControl
	{
		#region Constructor
		public EquipmentGuidePage() => InitializeComponent();



		Item _myWeapon;

		/// <summary>
		/// 设置起始物品
		/// </summary>
		public Item MyWeapon
		{
			get => _myWeapon;
			set
			{
				if ((_myWeapon = value) is null) return;

				this.MyWeapon_Icon.Image = value.Icon();
				this.MyWeapon_Name.Text = value.Name2;
				this.MyWeapon_Name.ItemGrade = value.ItemGrade;
				this.MyWeapon_Name.Location = new Point(this.MyWeapon_Icon.Left + (this.MyWeapon_Icon.Width - this.MyWeapon_Name.Width) / 2, this.MyWeapon_Name.Top);
			}
		}
		#endregion


		public virtual void SetData()
		{

		}

		protected virtual void SubIngredientPreview_RecipeChanged(RecipeChangedEventArgs e)
		{
			
		}

		private void SubIngredientPreview_DataLoaded() => this.SubIngredientPreview.Location = new Point((this.Width - this.SubIngredientPreview.Width) / 2, this.SubIngredientPreview.Location.Y);

		private void FixedIngredientPreview_DataLoaded() => this.FixedIngredientPreview.Location = new Point((this.Width - this.FixedIngredientPreview.Width) / 2, this.FixedIngredientPreview.Location.Y);

		private void WarningPreview_TextChanged(object sender, EventArgs e) => this.WarningPreview.Location = new Point((this.Width - this.WarningPreview.Width) / 2, this.WarningPreview.Location.Y);
	}
}