using System;
using System.Threading.Tasks;

using Xylia.Preview.Data;
using Xylia.Preview.Data.Models.BinData.Table.Attributes;
using Xylia.Preview.Data.Record;

using static Xylia.Preview.Data.Record.Item;

namespace Xylia.Match.Util.Paks.Textures
{
	public sealed class ItemIcon : IconOutBase
	{
		#region Define
		public static int GRADE => 221;

		public static int NAME2 => 2248;

		public static int ICON_ID => 2292;

		public static int GROCEERY_TYPE => 2844;
		#endregion



		#region Constructor
		public ItemIcon(string GameFolder) : base(GameFolder) { }


		/// <summary>
		/// 指示是否有背景
		/// </summary>
		public bool UseBackground = false;

		/// <summary>
		/// 指示白名单模式
		/// </summary>
		public bool isWhiteList = false;

		/// <summary>
		/// 缓存文件路径
		/// </summary>
		public string ChvPath = null;
		#endregion

		#region Functions
		protected override void AnalyseSourceData()
		{
			Action("正在分析物品数据...");

			//设置并发线程数量
			var pOptions = new ParallelOptions()
			{
				//MaxDegreeOfParallelism = 6,
			};


			var lst = XList.LoadData(ChvPath);
			Parallel.ForEach(set.Item, pOptions, (item) =>
			{
				var record = ((DbData)item.Attributes).record;

				int MainID = record.RecordId;
				if (isWhiteList && (lst is null || !lst.Contains(MainID))) return;
				if (!isWhiteList && (lst != null && lst.Contains(MainID))) return;


				#region Get data
				var Data = record.Data;

				var Grade = Data[@GRADE];
				var Name2 = set.TextData[BitConverter.ToInt32(Data, @NAME2)].GetText();
				var IconId = BitConverter.ToInt32(Data, @ICON_ID);
				var IconIdx = BitConverter.ToInt16(Data, @ICON_ID + 8);

				byte GroceryType = 0;
				if (record.SubclassType == 2) GroceryType = Data[@GROCEERY_TYPE];
				#endregion


				this.QuoteInfos.Add(new ItemQuoteInfo()
				{
					MainId = MainID,
					Alias = record.StringLookup.GetString(0),
					Name = Name2,
					Grade = Grade,
					GroceryType = (GroceryTypeSeq)GroceryType,

					TextureAlias = IconId.ToString(),
					IconIndex = IconIdx,

					NoBG = !this.UseBackground,
				});
			});
		}
		#endregion
	}
}