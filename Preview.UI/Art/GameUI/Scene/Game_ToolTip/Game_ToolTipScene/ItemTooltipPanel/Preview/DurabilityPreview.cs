
using Xylia.Preview.Common.Interface;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	public partial class DurabilityPreview : PreviewControl
	{
		public DurabilityPreview() => InitializeComponent();

		#region Fields
		private int _durability;

		public int Durability
		{
			get => this._durability;
			set
			{
				this._durability = value;
				this.label1.Text = $"{ value } / { value }";
			}
		}
		#endregion
	}
}