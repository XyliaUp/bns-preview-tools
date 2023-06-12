using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls.List;

using static Xylia.Preview.Data.Record.Item;

namespace Xylia.Preview.GameUI.Scene.Game_Auction
{
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

			foreach (var category2 in Enum.GetValues<MarketCategory2Seq>())
			{
				if (category2 == MarketCategory2Seq.None) continue;

				var node = TreeView.Nodes.Add(category2.ToString(), $"Name.item.game-category-2.{category2.GetSignal()}".GetText());

				if (!child.TryGetValue(category2, out var category3Seqs)) continue;
				category3Seqs.ForEach(category3 => node.Nodes.Add(category3.ToString(), $"Name.item.game-category-3.{category3.GetSignal()}".GetText(true)));
			}
			#endregion
		}

		public Game_AuctionScene(string rule) : this()
		{
			ItemPreview_Search.InputText = rule;

			FileCache.Data.Item.TryLoad();
			FileCache.PakData.Initialize();
		}

		private static Dictionary<MarketCategory2Seq, List<MarketCategory3Seq>> ChildCategory()
		{
			var data = new Dictionary<MarketCategory2Seq, List<MarketCategory3Seq>>();
			Enum.GetValues<MarketCategory2Seq>().ForEach(seq => data[seq] = new());

			#region Weapon
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Sword);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Gauntlet);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Axe);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Staff);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.AuraBangle);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Dagger);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.LynSword);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.WarlockDagger);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.SoulFighterGauntlet);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Gun);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.LongBow);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.GreatSword);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Orb);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.DualBlade);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Instrument);
			data[MarketCategory2Seq.Weapon].Add(MarketCategory3Seq.Spear);
			#endregion

			#region	EquipGem
			data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Gam1);
			data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Gan2);
			data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Jin3);
			data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Son4);
			data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Ri5);
			data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Gon6);
			data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Tae7);
			data[MarketCategory2Seq.EquipGem].Add(MarketCategory3Seq.Geon8);
			#endregion

			#region	Accessory
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Ring);
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Earring);
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Necklace);
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Belt);
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Bracelet);
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Soul);
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Soul2);
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Gloves);
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Pet1);
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Nova);
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Rune1);
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Rune2);
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.Vehicle);
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.NamePlate);
			data[MarketCategory2Seq.Accessory].Add(MarketCategory3Seq.SpeechBubble);
			#endregion

			#region	Dress
			data[MarketCategory2Seq.Dress].Add(MarketCategory3Seq.Costume);
			data[MarketCategory2Seq.Dress].Add(MarketCategory3Seq.HeadAttach);
			data[MarketCategory2Seq.Dress].Add(MarketCategory3Seq.FaceAttach);
			data[MarketCategory2Seq.Dress].Add(MarketCategory3Seq.CostumeAttach);
			data[MarketCategory2Seq.Dress].Add(MarketCategory3Seq.SummonedPetCostume);
			data[MarketCategory2Seq.Dress].Add(MarketCategory3Seq.SummonedPetHat);
			data[MarketCategory2Seq.Dress].Add(MarketCategory3Seq.SummonedPetAttach);
			#endregion

			#region	WeaponGem
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Ruby);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Topaz);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Sapphire);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Jade);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Amethyst);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Emerald);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Diamond);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Obsidian);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Amber);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Garnet);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Aquamarine);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.RubyDiamond);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.TopazDiamond);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.SapphireDiamond);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.JadeDiamond);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.AmethystDiamond);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.EmeraldDiamond);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.AquamarineDiamond);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.AmberDiamond);
			data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.ObsidianGarnet);
			#endregion

			#region	Medicine

			#endregion

			#region	Food

			#endregion

			#region	BuildUpStone
			data[MarketCategory2Seq.BuildUpStone].Add(MarketCategory3Seq.SkillStone1);
			data[MarketCategory2Seq.BuildUpStone].Add(MarketCategory3Seq.SkillStone2);
			#endregion

			#region	Talisman

			#endregion

			#region	Tool

			#endregion

			#region	EquipMaterial
			data[MarketCategory2Seq.EquipMaterial].Add(MarketCategory3Seq.GrowthMaterial);
			data[MarketCategory2Seq.EquipMaterial].Add(MarketCategory3Seq.HolyMaterial);
			#endregion

			#region	UnionMaterial
			data[MarketCategory2Seq.UnionMaterial].Add(MarketCategory3Seq.ProductionMaterial);
			data[MarketCategory2Seq.UnionMaterial].Add(MarketCategory3Seq.HypermoveMaterial);
			#endregion

			#region	DressMaterial
			data[MarketCategory2Seq.DressMaterial].Add(MarketCategory3Seq.ColorMaterial);
			data[MarketCategory2Seq.DressMaterial].Add(MarketCategory3Seq.Cloth);
			#endregion

			#region	EtcMaterial
			data[MarketCategory2Seq.EtcMaterial].Add(MarketCategory3Seq.SpecialMaterial);
			data[MarketCategory2Seq.EtcMaterial].Add(MarketCategory3Seq.NormalMaterial);
			#endregion

			#region	Coin

			#endregion

			#region	Deed
			data[MarketCategory2Seq.Deed].Add(MarketCategory3Seq.NormalDeed);
			data[MarketCategory2Seq.Deed].Add(MarketCategory3Seq.SkillTakeDeed);
			#endregion

			#region	Quest

			#endregion

			#region	EtcChange
			data[MarketCategory2Seq.EtcChange].Add(MarketCategory3Seq.SundryItem);
			data[MarketCategory2Seq.EtcChange].Add(MarketCategory3Seq.ChackItem);
			#endregion

			#region	EtcBox
			data[MarketCategory2Seq.EtcBox].Add(MarketCategory3Seq.NormalEtcBox);
			#endregion

			#region	Badge

			#endregion



			//data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Void10);
			//data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Void11);
			//data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Void12);
			//data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Void13);
			//data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Void14);
			//data[MarketCategory2Seq.WeaponGem].Add(MarketCategory3Seq.Void15);


			return data;
		}
		#endregion


		//Ini.ReadValue("Preview", "item#search_autoclose").ToBool();
		//Ini.ReadValue("Preview", "item#search_showextra").ToBool()



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

		private void LoadData(object sender, EventArgs e)
		{
			this.ItemList.Cells = null;

			var node = TreeView.SelectedNode;
			if (node.Name == "favorites") return;

			#region search rules
			string rule = ItemPreview_Search.InputText;
			bool empty = string.IsNullOrWhiteSpace(rule);

			var IsAll = node.Name == "all";
			if (IsAll && empty && !chk_compare.Checked) return;

			var category2 = IsAll ? default : (node.Level == 0 ? node : node.Parent).Name.ToEnum<MarketCategory2Seq>();
			var category3 = node.Level == 0 ? default : node.Name.ToEnum<MarketCategory3Seq>();

			var auctionable = chk_Auctionable.Checked;
			#endregion


			new Thread(t =>
			{
				IEnumerable<Item> datas = FileCache.Data.Item;
				if (lst != null && lst.Any()) datas = datas.Where(item => !lst.Contains(item.Key()));

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


				IEnumerable<Item> temp;
				if (checkBox1.Checked) temp = result.OrderByDescending(o => o.Key());
				else temp = result.OrderBy(o => o.Key());
				this.ItemList.Invoke(() => this.ItemList.Cells = temp.Select(o => new ItemData(o)));
			}).Start();
		}
	}
}