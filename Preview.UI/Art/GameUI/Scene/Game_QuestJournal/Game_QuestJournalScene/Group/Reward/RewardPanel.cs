using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;
using Xylia.Preview.GameUI.Controls.Currency;
using Xylia.Preview.GameUI.Scene.Game_QuestJournal.RewardCell;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	public partial class RewardPanel : GroupBase
	{
		#region Constructor
		readonly FixedItem FixedCommon = new();
		readonly OptionaItem OptionalCommon = new();
		readonly FixedItem Fixed = new() { Visible = false };
		readonly OptionaItem Optional = new() { Visible = false };

		readonly FixedItem SealedDungeonReward = new() { Visible = false };
		readonly BonusRewardPreview QuestBonusReward = new() { Visible = false };


		public RewardPanel()
		{
			InitializeComponent();

			//添加控件
			this.Controls.Add(FixedCommon);
			this.Controls.Add(OptionalCommon);
			this.Controls.Add(Fixed);
			this.Controls.Add(Optional);
			this.Controls.Add(SealedDungeonReward);
			this.Controls.Add(QuestBonusReward);
		}
		#endregion

		#region Fields
		private IEnumerable<QuestBonusRewardSetting> QuestBonusRewardSettings;

		private List<RewardCellBase> RewardCells;

		private readonly Dictionary<string, RewardGroupSet> RewardGroups = new();


		public void LoadData(Quest Quest)
		{
			#region 步骤奖励
			List<QuestReward> Rewards = new();

			var MissionStep = Quest.MissionStep.Value.SelectMany(s => s.Mission).Where(step => step.Reward1 != null || step.Reward2 != null);
			if (MissionStep.Any())
			{
				foreach (var step in MissionStep)
				{
					var Reward1 = FileCache.Data.QuestReward[step.Reward1];
					var Reward2 = FileCache.Data.QuestReward[step.Reward2];

					if (Reward1 != null)
					{
						Rewards.Add(Reward1);
					}

					if (Reward2 != null)
					{
						Rewards.Add(Reward2);
					}
				}
			}

			if (Rewards.Count == 0) this.Visible = false;
			else
			{
				QuestReward = Rewards.Last();
				System.Diagnostics.Trace.WriteLine("绑定的任务奖励数量: " + Rewards.Count);
			}
			#endregion

			#region 特别奖励
			QuestBonusReward.INVALID = true;
			QuestBonusRewardSettings = FileCache.Data.QuestBonusRewardSetting.Where(o => FileCache.Data.Quest[o.Quest] == Quest);
			if (QuestBonusRewardSettings.Any())
			{
				var setting = QuestBonusRewardSettings.FirstOrDefault(o => o.Type == QuestBonusRewardSetting.TypeSeq.IgnoreDifficulty);
				if (setting != null) QuestBonusReward.LoadData(setting);
			}
			#endregion

			#region End
			if (RewardSelect.Source.Count == 0)
			{
				RewardSelect.Visible = false;
				this.Refresh();
			}
			else
			{
				RewardSelect.Visible = true;
				RewardSelect.SelectedIndex = 0;
			}
			#endregion
		}

		/// <summary>
		/// 任务奖励
		/// </summary>
		public QuestReward QuestReward
		{
			set
			{
				if (value is null)
				{
					//没有奖励内容时, 隐藏界面
					this.Visible = false;
					return;
				}


				#region 货币型奖励
				//Load 货币类型奖励信息
				this.RewardCells = new List<RewardCellBase>();

				#region 钱币
				if (value.BasicMoney != 0)
				{
					var MoneyElement = new Money()
					{
						CurrencyType = CurrencyType.Money,
						CurrencyCount = value.BasicMoney,
					};

					int Ratio = 30;
					MoneyElement.SetToolTip($"此为基础金币, 应用{Ratio}%增益后为 {Money.ConvertInfo((long)(value.BasicMoney * (1 + (double)Ratio / 100)))}");
					RewardCells.Add(MoneyElement);
				}
				#endregion

				#region 经验
				if (value.BasicExp != 0)
				{
					RewardCells.Add(new HonorExp()
					{
						Title = "经验值",
						Text = value.BasicExp.ToString(),
					});
				}
				#endregion

				#region 声望
				if (value.BasicAccountExp != 0)
				{
					RewardCells.Add(new HonorExp()
					{
						Text = value.BasicAccountExp.ToString(),
					});
				}
				#endregion

				#region 仙豆
				if (value.BasicDuelPoint != 0)
				{
					RewardCells.Add(new PriceBase()
					{
						Title = "仙豆",
						CurrencyType = CurrencyType.DuelPoint,
						CurrencyCount = value.BasicDuelPoint,
					});
				}
				#endregion

				#region 龙果
				if (value.BasicPartyBattlePoint != 0)
				{
					RewardCells.Add(new PriceBase()
					{
						Title = "龙果",
						CurrencyType = CurrencyType.PartyBattlePoint,
						CurrencyCount = value.BasicPartyBattlePoint,
					});
				}
				#endregion

				#region 仙桃
				if (value.BasicFieldPlayPoint != 0)
				{
					RewardCells.Add(new PriceBase()
					{
						Title = "仙桃",
						CurrencyType = CurrencyType.FieldPlayPoint,
						CurrencyCount = value.BasicFieldPlayPoint,
					});
				}
				#endregion

				#region 势力贡献度
				if (value.BasicFieldPlayPoint != 0)
				{
					RewardCells.Add(new RewardCellBase()
					{
						Title = "势力贡献度",
						Text = value.BasicFieldPlayPoint.ToString(),
					});
				}
				#endregion

				#region 信誉度
				if (value.BasicProductionExp != 0)
				{
					RewardCells.Add(new RewardCellBase()
					{
						Title = "信誉度",
						Text = value.BasicProductionExp.ToString(),
					});
				}
				#endregion


				RewardCells.ForEach(c => this.Controls.Add(c));
				#endregion



				#region 固定奖励内容
				var FixedCommonObjs = new List<ItemIconCell>();
				for (int idx = 1; idx <= 4; idx++)
				{
					if (value.ContainsAttribute($"fixed-skill3-{idx}", out string Skill))
						FixedCommonObjs.AddItem(("skill: " + Skill).GetObjIcon());
				}

				for (int idx = 1; idx <= 4; idx++)
				{
					if (value.ContainsAttribute($"fixed-common-slot-{idx}", out string Slot))
						FixedCommonObjs.AddItem(Slot.GetObjIcon(value.Attributes[$"fixed-common-item-count-{idx}"]));
				}

				FixedCommon.Items = FixedCommonObjs;
				#endregion

				#region 随机奖励内容
				var OptionalCommonObjs = new List<ItemIconCell>();
				for (int i = 1; i <= 4; i++)
				{
					if (value.ContainsAttribute($"optional-common-slot-{i}", out string AttrVal))
						OptionalCommonObjs.AddItem(AttrVal.GetObjIcon(value.Attributes[$"optional-common-item-count-{i}"]));
				}

				OptionalCommon.Items = OptionalCommonObjs;
				#endregion


				#region	分组奖励内容
				void GetRewardGroup(QuestReward value, bool Fixed)
				{
					string Key = Fixed ? "fixed" : "optional";
					for (int i = 1; i <= 15; i++)
					{
						if (!value.ContainsAttribute($"{Key}-{i}-slot-1", out _)) break;

						#region 创建实例
						var f = new RewardGroup(value.Attributes, $"{Key}-{i}");
						var Name = f.GroupName ?? f.GroupKey;

						if (!RewardGroups.ContainsKey(Name))
						{
							RewardGroups.Add(Name, new RewardGroupSet());
							RewardSelect.Source.Add(Name);
						}
						#endregion

						if (Fixed) RewardGroups[Name].Fixed = f;
						else RewardGroups[Name].Optional = f;
					}
				}

				GetRewardGroup(value, true);
				GetRewardGroup(value, false);
				#endregion


				#region 封魔录奖励
				var SealedDugeonReward = value.Attributes["sealed-dungeon-reward"];
				if (SealedDugeonReward != null && int.TryParse(SealedDugeonReward.Replace(":0", null), out var id))
				{
					foreach (var reward in FileCache.Data.QuestSealedDungeonReward.Where(o => o.Key() == id))
					{
						var Name = reward.Level + "层";

						var GroupSet = new RewardGroupSet();
						GroupSet.QuestSealedDungeonReward = reward;

						RewardGroups.Add(Name, GroupSet);
						RewardSelect.Source.Add(Name);
					}
				}
				#endregion
			}
		}
		#endregion



		#region Get Icon
		private static List<ItemIconCell> GetObjIcon(RewardGroup group)
		{
			if (group is null) return null;

			var items = new List<ItemIconCell>();
			items.AddItem(group.Slot1.GetObjIcon(group.ItemCount1));
			items.AddItem(group.Slot2.GetObjIcon(group.ItemCount2));
			items.AddItem(group.Slot3.GetObjIcon(group.ItemCount3));
			items.AddItem(group.Slot4.GetObjIcon(group.ItemCount4));

			return items;
		}

		private static List<ItemIconCell> GetObjIcon(QuestSealedDungeonReward group)
		{
			if (group is null) return null;

			var items = new List<ItemIconCell>();
			items.AddItem(group.RewardItem1.GetObjIcon(group.RewardItemCount1));
			items.AddItem(group.RewardItem2.GetObjIcon(group.RewardItemCount2));
			items.AddItem(group.RewardItem3.GetObjIcon(group.RewardItemCount3));
			items.AddItem(group.RewardItem4.GetObjIcon(group.RewardItemCount4));

			return items;
		}
		#endregion

		#region Functions
		private void RewardSelect_SelectedChangedEvent(object sender, System.EventArgs e)
		{
			var CurGroup = RewardGroups[this.RewardSelect.TextValue];

			this.Fixed.Items = GetObjIcon(CurGroup.Fixed);
			this.Optional.Items = GetObjIcon(CurGroup.Optional);
			this.SealedDungeonReward.Items = GetObjIcon(CurGroup.QuestSealedDungeonReward);


			if (QuestBonusRewardSettings != null)
			{
				QuestBonusRewardSetting setting = null;
				if (CurGroup.QuestSealedDungeonReward != null) setting = QuestBonusRewardSettings.FirstOrDefault(o => o.Type == QuestBonusRewardSetting.TypeSeq.SealedLevel && o.SealedLevel == CurGroup.QuestSealedDungeonReward.Level);
				else setting = QuestBonusRewardSettings.FirstOrDefault(o => o.Type == QuestBonusRewardSetting.TypeSeq.DifficultyType && o.DifficultyType == CurGroup.Fixed.DifficultyType);

				QuestBonusReward.LoadData(setting);
			}

			this.Refresh();
			this.Parent.Refresh();
		}

		public override void Refresh()
		{
			base.Refresh();


			int ContentY = 40;

			#region 物品奖励 
			void RefreshItems(FixedItem c)
			{
				c.Visible = !c.INVALID;
				if (c.INVALID) return;

				c.Location = new Point(ContentStartX, ContentY);
				ContentY = c.Bottom + 10;
			}

			RefreshItems(FixedCommon);
			RefreshItems(Fixed);
			RefreshItems(OptionalCommon);
			RefreshItems(Optional);
			RefreshItems(SealedDungeonReward);
			#endregion

			#region 额外奖励
			if (QuestBonusReward.Visible = !QuestBonusReward.INVALID)
			{
				QuestBonusReward.Location = new Point(ContentStartX, ContentY);
				ContentY = QuestBonusReward.Bottom + 10;
			}
			#endregion


			if (RewardCells != null)
			{
				ContentY += 5;

				foreach (var cell in RewardCells)
				{
					cell.Location = new Point(ContentStartX, ContentY);
					ContentY = cell.Bottom;
				}
			}
		}
		#endregion
	}
}