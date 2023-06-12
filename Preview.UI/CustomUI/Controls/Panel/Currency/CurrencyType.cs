using System.Drawing;

using Xylia.Preview.Resources;

namespace Xylia.Preview.GameUI.Controls.Currency
{
	/// <summary>
	/// 货币类型
	/// </summary>
	public enum CurrencyType
	{
		/// <summary>
		/// 钱币
		/// </summary>
		Money,

		/// <summary>
		/// 神石
		/// </summary>
		GoodsStone,

		/// <summary>
		/// 绑定神石
		/// </summary>
		GoodsStone2,

		/// <summary>
		/// 珍珠
		/// </summary>
		Pearl,

		/// <summary>
		/// 龙果
		/// </summary>
		PartyBattlePoint,

		/// <summary>
		/// 仙桃
		/// </summary>
		FieldPlayPoint,

		/// <summary>
		/// 仙豆
		/// </summary>
		DuelPoint,

		/// <summary>
		/// 灵气
		/// </summary>
		FactionScore,
	}



	public static class CurrencyUtil
	{
		public static Bitmap GetCurrencyIcon(this CurrencyType Type) => Type switch
		{
			CurrencyType.DuelPoint => Resource_BNSR.DuelPoint,
			CurrencyType.FactionScore => Resource_BNSR.FactionScoreIcon,
			CurrencyType.FieldPlayPoint => Resource_BNSR.fieldplay,
			CurrencyType.GoodsStone => Resource_BNSR.GameUI_Coin_GoodsStone,
			CurrencyType.GoodsStone2 => Resource_BNSR.GameUI_Coin_GoodsStone_002,
			CurrencyType.PartyBattlePoint => Resource_BNSR.BattleFieldPoint,
			CurrencyType.Pearl => Resource_BNSR.Store_Pearl,

			_ => null,
		};
	}
}
