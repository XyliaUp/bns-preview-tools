using CUE4Parse.BNS.Conversion;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse_Conversion.Textures;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Models;
using static Xylia.Preview.Data.Models.Item.Grocery;

namespace Xylia.Preview.UI.Helpers.Output.Textures;
public sealed class ItemIcon(string GameFolder, string OutputFolder) : IconOutBase(GameFolder, OutputFolder)
{
	#region Constructor

	public bool UseBackground = false;

	public bool isWhiteList = false;

	public string ChvPath = null;
	#endregion


	#region Methods
	protected override void AnalyseSourceData(DefaultFileProvider provider, string format, CancellationToken token)
	{
		var lst = XList.LoadData(ChvPath); 
		var Weapon_Lock_04 = provider.LoadObject<UTexture>("BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_Icon3_R/Weapon_Lock_04")?.Decode();

		Parallel.ForEach(set.Item, (x) =>
		{
			token.ThrowIfCancellationRequested();

			var Ref = (Ref)x.Source;
			if (isWhiteList && (lst is null || !lst.Contains(Ref.Id))) return;
			if (!isWhiteList && lst != null && lst.Contains(Ref.Id)) return;

			#region Get Data
			var alias = x.Attributes.Get<string>("alias");
			var ItemGrade = x.Attributes.Get<sbyte>("item-grade");
			var icon = x.Attributes["icon"]?.ToString();

			var Text = x.Attributes.Get<Record>("name2")?.Attributes["text"];	   
			var GroceryType = x.Source.SubclassType == 2 ? x.Attributes["grocery-type"]?.ToEnum<GroceryTypeSeq>() : null;

			x.Source.Dispose();
			#endregion


			try
			{
				#region process
				var bitmap = icon.GetIcon(set, provider) ?? throw new Exception($"get resouce failed ({icon})");

				if (UseBackground)
				{
					bitmap = ItemGrade.GetBackground(provider).Compose(bitmap);

					if (GroceryType == GroceryTypeSeq.Sealed) bitmap = bitmap.Compose(Weapon_Lock_04);
				}
				#endregion

				#region file name
				string MainId = Ref.Id.ToString();
				string OutName = format
					   .Replace("[alias]", alias)
					   .Replace("[id]", MainId)
					   .Replace("[name]", Text.ToString()).Replace("[name2]", Text.ToString());
				#endregion

				#region tags
				//var ProfileCopyright = bitmap.GetPropertyItem(0xc6fe);
				//ProfileCopyright.Value = Encoding.UTF8.GetBytes("blade & soul");
				//bitmap.SetPropertyItem(ProfileCopyright);
				#endregion


				Save(ref bitmap, OutName);
			}
			catch (Exception ee)
			{
				logger.Error($"id: {Ref} [{Text}]  " + ee.Message);
			}
		});
	}
	#endregion
}