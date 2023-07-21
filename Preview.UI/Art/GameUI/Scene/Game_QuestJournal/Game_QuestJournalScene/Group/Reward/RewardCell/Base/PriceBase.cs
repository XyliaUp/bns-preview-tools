using Xylia.Preview.UI.Custom.Controls.Currency;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal.RewardCell
{
	public partial class PriceBase : RewardCellBase
	{
		#region Initialize
		public PriceBase()
		{
			InitializeComponent();
		}
		#endregion


		#region Fields
		/// <summary>
		/// 货币数量
		/// </summary>
		public int CurrencyCount 
		{ 
			get => (int)this.priceCell1.CurrencyCount; 
			set => this.priceCell1.CurrencyCount = value; 
		}

		/// <summary>
		/// 货币类型
		/// </summary>
		public virtual CurrencyType CurrencyType 
		{ 
			get => this.priceCell1.CurrencyType; 
			set => this.priceCell1.CurrencyType = value;
		}
		#endregion
	}
}
