using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Models.BinData.Table.Record.Attributes;
using Xylia.Preview.Data.Models.Definition;
using Xylia.Preview.Data.Record;

using static Xylia.Preview.Data.Record.Item.Grocery;

namespace Xylia.Match.Util.Paks.Textures;
public sealed class ItemIcon : IconOutBase
{
	#region Constructor
	public ItemIcon(string GameFolder) : base(GameFolder) { }

	public bool UseBackground = false;

	public bool isWhiteList = false;


	public string ChvPath = null;
	#endregion


	#region Functions
	protected override void AnalyseSourceData()
	{
		var lst = XList.LoadData(ChvPath);

		var GRADE = set.Item.TableDef["item-grade"];
		var NAME2 = set.Item.TableDef["name2"];
		var ICON  = set.Item.TableDef["icon"];
		var GROCEERY_TYPE = set.Item.TableDef.Subtables[2]["grocery-type"];


		BaseRecord.TempSwitch = false;
		Parallel.ForEach(set.Item, (item) =>
		{
			if (isWhiteList && (lst is null || !lst.Contains(item.Ref.Id))) return;
			if (!isWhiteList && (lst != null && lst.Contains(item.Ref.Id))) return;

			#region Get data
			var record = ((DbData)item.Attributes).record;
			var Grade = record.Data[GRADE.Offset];
			var Name2 = set.Text[BitConverter.ToInt32(record.Data, NAME2.Offset)].GetText();
			var IconId = BitConverter.ToInt32(record.Data, ICON.Offset);
			var IconIdx = BitConverter.ToInt16(record.Data, ICON.Offset + 8);

			byte GroceryType = 0;
			if (record.SubclassType == 2) GroceryType = record.Data[GROCEERY_TYPE.Offset];
			#endregion


			this.QuoteInfos.Add(new ItemQuoteInfo()
			{
				MainId = item.Ref.Id,
				Alias = record.StringLookup.GetString(0),
				Name = Name2,
				Grade = Grade,
				GroceryType = (GroceryTypeSeq)GroceryType,
				Icon = $"{IconId},{IconIdx}",

				NoBG = !this.UseBackground,
			});

			item = null;
		});

		BaseRecord.TempSwitch = true;
		set.Item.Clear();
	}
	#endregion
}