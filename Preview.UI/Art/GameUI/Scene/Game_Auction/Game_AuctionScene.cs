using System.Collections.Concurrent;
using System.Data;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.Definition;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.GameUI.Scene.Game_Auction;
public partial class Game_AuctionScene : Form // PreviewFrm
{
	#region Constructor
	public Game_AuctionScene()
	{
		InitializeComponent();
		CheckForIllegalCrossThreadCalls = false;

		#region Category
		var child = ChildCategory();

		TreeView.Nodes.Add("all", "UI.Market.Category.All".GetText());
		TreeView.Nodes.Add("favorites", "UI.Market.Category.Favorites".GetText());
		TreeView.SelectedNode = TreeView.Nodes[0];

		foreach (var category2 in Enum.GetValues<Item.MarketCategory2Seq>())
		{
			if (category2 == Item.MarketCategory2Seq.None) continue;

			var node = TreeView.Nodes.Add(category2.ToString(), $"Name.item.game-category-2.{category2.GetSignal()}".GetText());

			if (!child.TryGetValue(category2, out var category3Seqs)) continue;
			category3Seqs.ForEach(category3 => node.Nodes.Add(category3.ToString(), $"Name.item.game-category-3.{category3.GetSignal()}".GetText(true)));
		}
		#endregion
	}

	public Game_AuctionScene(string rule) : this()
	{
		ItemPreview_Search.InputText = rule;
	}

	private static Dictionary<Item.MarketCategory2Seq, List<Item.MarketCategory3Seq>> ChildCategory()
	{
		var data = new Dictionary<Item.MarketCategory2Seq, List<Item.MarketCategory3Seq>>();
		Enum.GetValues<Item.MarketCategory2Seq>().ForEach(seq => data[seq] = new());

		#region Weapon
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.Sword);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.Gauntlet);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.Axe);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.Staff);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.AuraBangle);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.Dagger);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.LynSword);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.WarlockDagger);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.SoulFighterGauntlet);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.Gun);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.LongBow);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.GreatSword);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.Orb);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.DualBlade);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.Instrument);
		data[Item.MarketCategory2Seq.Weapon].Add(Item.MarketCategory3Seq.Spear);
		#endregion

		#region	EquipGem
		data[Item.MarketCategory2Seq.EquipGem].Add(Item.MarketCategory3Seq.Gam1);
		data[Item.MarketCategory2Seq.EquipGem].Add(Item.MarketCategory3Seq.Gan2);
		data[Item.MarketCategory2Seq.EquipGem].Add(Item.MarketCategory3Seq.Jin3);
		data[Item.MarketCategory2Seq.EquipGem].Add(Item.MarketCategory3Seq.Son4);
		data[Item.MarketCategory2Seq.EquipGem].Add(Item.MarketCategory3Seq.Ri5);
		data[Item.MarketCategory2Seq.EquipGem].Add(Item.MarketCategory3Seq.Gon6);
		data[Item.MarketCategory2Seq.EquipGem].Add(Item.MarketCategory3Seq.Tae7);
		data[Item.MarketCategory2Seq.EquipGem].Add(Item.MarketCategory3Seq.Geon8);
		#endregion

		#region	Accessory
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.Ring);
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.Earring);
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.Necklace);
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.Belt);
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.Bracelet);
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.Soul);
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.Soul2);
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.Gloves);
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.Pet1);
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.Nova);
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.Rune1);
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.Rune2);
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.Vehicle);
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.NamePlate);
		data[Item.MarketCategory2Seq.Accessory].Add(Item.MarketCategory3Seq.SpeechBubble);
		#endregion

		#region	Dress
		data[Item.MarketCategory2Seq.Dress].Add(Item.MarketCategory3Seq.Costume);
		data[Item.MarketCategory2Seq.Dress].Add(Item.MarketCategory3Seq.HeadAttach);
		data[Item.MarketCategory2Seq.Dress].Add(Item.MarketCategory3Seq.FaceAttach);
		data[Item.MarketCategory2Seq.Dress].Add(Item.MarketCategory3Seq.CostumeAttach);
		data[Item.MarketCategory2Seq.Dress].Add(Item.MarketCategory3Seq.SummonedPetCostume);
		data[Item.MarketCategory2Seq.Dress].Add(Item.MarketCategory3Seq.SummonedPetHat);
		data[Item.MarketCategory2Seq.Dress].Add(Item.MarketCategory3Seq.SummonedPetAttach);
		#endregion

		#region	WeaponGem
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Ruby);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Topaz);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Sapphire);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Jade);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Amethyst);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Emerald);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Diamond);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Obsidian);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Amber);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Garnet);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Aquamarine);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.RubyDiamond);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.TopazDiamond);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.SapphireDiamond);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.JadeDiamond);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.AmethystDiamond);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.EmeraldDiamond);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.AquamarineDiamond);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.AmberDiamond);
		data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.ObsidianGarnet);
		#endregion

		#region	Medicine

		#endregion

		#region	Food

		#endregion

		#region	BuildUpStone
		data[Item.MarketCategory2Seq.BuildUpStone].Add(Item.MarketCategory3Seq.SkillStone1);
		data[Item.MarketCategory2Seq.BuildUpStone].Add(Item.MarketCategory3Seq.SkillStone2);
		#endregion

		#region	Talisman

		#endregion

		#region	Tool

		#endregion

		#region	EquipMaterial
		data[Item.MarketCategory2Seq.EquipMaterial].Add(Item.MarketCategory3Seq.GrowthMaterial);
		data[Item.MarketCategory2Seq.EquipMaterial].Add(Item.MarketCategory3Seq.HolyMaterial);
		#endregion

		#region	UnionMaterial
		data[Item.MarketCategory2Seq.UnionMaterial].Add(Item.MarketCategory3Seq.ProductionMaterial);
		data[Item.MarketCategory2Seq.UnionMaterial].Add(Item.MarketCategory3Seq.HypermoveMaterial);
		#endregion

		#region	DressMaterial
		data[Item.MarketCategory2Seq.DressMaterial].Add(Item.MarketCategory3Seq.ColorMaterial);
		data[Item.MarketCategory2Seq.DressMaterial].Add(Item.MarketCategory3Seq.Cloth);
		#endregion

		#region	EtcMaterial
		data[Item.MarketCategory2Seq.EtcMaterial].Add(Item.MarketCategory3Seq.SpecialMaterial);
		data[Item.MarketCategory2Seq.EtcMaterial].Add(Item.MarketCategory3Seq.NormalMaterial);
		#endregion

		#region	Coin

		#endregion

		#region	Deed
		data[Item.MarketCategory2Seq.Deed].Add(Item.MarketCategory3Seq.NormalDeed);
		data[Item.MarketCategory2Seq.Deed].Add(Item.MarketCategory3Seq.SkillTakeDeed);
		#endregion

		#region	Quest

		#endregion

		#region	EtcChange
		data[Item.MarketCategory2Seq.EtcChange].Add(Item.MarketCategory3Seq.SundryItem);
		data[Item.MarketCategory2Seq.EtcChange].Add(Item.MarketCategory3Seq.ChackItem);
		#endregion

		#region	EtcBox
		data[Item.MarketCategory2Seq.EtcBox].Add(Item.MarketCategory3Seq.NormalEtcBox);
		#endregion

		#region	Badge

		#endregion



		//data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Void10);
		//data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Void11);
		//data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Void12);
		//data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Void13);
		//data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Void14);
		//data[Item.MarketCategory2Seq.WeaponGem].Add(Item.MarketCategory3Seq.Void15);


		return data;
	}


	//Ini.ReadValue("Preview", "item#search_autoclose").ToBool();
	//Ini.ReadValue("Preview", "item#search_showextra").ToBool()
	#endregion


	HashSet<int> lst;

	private void chk_compare_CheckedChanged(object sender, EventArgs e)
	{
		if (!chk_compare.Enabled)
		{
			chk_compare.Enabled = true;
			return;
		}
		else if (!chk_compare.Checked)
		{
			lst = null;
		}
		else
		{
			lst = XList.LoadData();
			if (lst is null)
			{
				chk_compare.Enabled = false;
				chk_compare.Checked = false;
				return;
			}
		}

		LoadData(sender, e);
	}

	private void Game_AuctionScene_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
			LoadData(sender, e);
	}



	CancellationTokenSource cts = new();

	private async void LoadData(object sender, EventArgs e)
	{
		cts.Cancel();
		loader.Visible = true;
		await Task.Run(() =>
		{
			#region search rules
			var node = TreeView.SelectedNode;
			if (node.Name == "favorites") return;

			string rule = ItemPreview_Search.InputText;
			bool empty = string.IsNullOrWhiteSpace(rule);

			var IsAll = node.Name == "all";
			if (IsAll && empty && !chk_compare.Checked) return;

			var category2 = IsAll ? default : (node.Level == 0 ? node : node.Parent).Name.ToEnum<Item.MarketCategory2Seq>();
			var category3 = node.Level == 0 ? default : node.Name.ToEnum<Item.MarketCategory3Seq>();

			var auctionable = chk_Auctionable.Checked;
			#endregion


			IEnumerable<Item> datas = FileCache.Data.Item;
			if (lst != null && lst.Any()) datas = datas.Where(item => !lst.Contains(item.Ref.Id));

			BlockingCollection<Item> result = new();
			Parallel.ForEach(datas, o =>
			{
				if (!IsAll)
				{
					if (category3 != default && category3 != o.MarketCategory3) return;
					else if (category2 != default && category2 != o.MarketCategory2) return;
				}

				if (!empty)
				{
					string ItemName = o.Name2;
					if (ItemName is null || ItemName.IndexOf(rule, StringComparison.OrdinalIgnoreCase) < 0) return;
				}


				if (auctionable && !o.Auctionable && !o.SealRenewalAuctionable) return;

				result.Add(o);
			});

			lock (this.ItemList.Items)
			{
				this.ItemList.Items.Clear();
				this.ItemList.Items.AddRange(checkBox1.Checked ?
					result.OrderByDescending(o => o.Ref.Id) :
					result.OrderBy(o => o.Ref.Id));

				this.ItemList.RefreshList();
			}
		});

		loader.Visible = false;
	}

}