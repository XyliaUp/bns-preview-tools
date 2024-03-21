using CUE4Parse.BNS.Conversion;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse_Conversion.Textures;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Engine.BinData.Helpers;
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
    protected override void Output(DefaultFileProvider provider, string format, CancellationToken token)
    {
        var lst = new HashList(ChvPath);
        var Weapon_Lock_04 = provider.LoadObject<UTexture>("BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_Icon3_R/Weapon_Lock_04")?.Decode();

        Parallel.ForEach(db!.Provider.GetTable("Item"), (record) =>
        {
            token.ThrowIfCancellationRequested();

            #region Get Data
            var @ref = record.PrimaryKey;
            if (lst.CheckFailed(@ref, isWhiteList)) return;

            var alias = record.Attributes.Get<string>("alias");
            var ItemGrade = record.Attributes.Get<sbyte>("item-grade");
            var icon = record.Attributes["icon"]?.ToString();

            var Text = record.Attributes.Get<Record>("name2")?.Attributes["text"]?.ToString();
            var GroceryType = record.SubclassType == 2 ? record.Attributes["grocery-type"]?.ToEnum<GroceryTypeSeq>() : null;

            record.Dispose();
            #endregion


            try
            {
                #region process
                var bitmap = IconTexture.Parse(icon, db, provider)?.Image ??
                    throw new Exception($"get resouce failed ({icon})");

                if (UseBackground)
                {
                    bitmap = IconTexture.GetBackground(ItemGrade, provider)?.Image.Compose(bitmap);

                    if (GroceryType == GroceryTypeSeq.Sealed) bitmap = bitmap.Compose(Weapon_Lock_04);
                }
                #endregion

                #region file name
                string MainId = @ref.Id.ToString();
                string OutName = format.Replace("[alias]", alias).Replace("[id]", MainId).Replace("[name]", Text);
                #endregion

                #region tags
                //var ProfileCopyright = bitmap.GetPropertyItem(0xc6fe);
                //ProfileCopyright.Value = Encoding.UTF8.GetBytes("blade & soul");
                //bitmap.SetPropertyItem(ProfileCopyright);
                #endregion


                Save(bitmap, OutName);
            }
            catch (Exception ee)
            {
                logger.Error($"id: {@ref} [{Text}]  " + ee.Message);
            }
        });
    }
    #endregion
}