using System.Drawing.Imaging;

using CUE4Parse.BNS;

using Xylia.Configure;
using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;
using Xylia.Preview.GameUI.Controls.Currency;
using Xylia.Preview.Resources;
using Xylia.Windows.Framework.Enum;

using static Xylia.Preview.Data.Record.Item;


namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	public partial class ItemTooltipPanel : Form
	{
		#region Constructor 
		public readonly Item ItemInfo;

		private readonly bool Loading;

		public ItemTooltipPanel(Item ItemInfo)
		{
			#region Initialize
			Loading = true;

			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;

			this.ItemInfo = ItemInfo;


			//Initialize可配置内容
			if (bool.TryParse(Ini.ReadValue("Preview", "item#option_UseUserOperPanel"), out bool f1))
				this.MenuItem_SwitchUserOperPanel.Checked = f1;

			Loading = false;
			#endregion

			#region 实例化操作按钮
			this.UserOperScene = new UserOperPanel(this);
			this.FormClosing += new FormClosingEventHandler((s, o) => this.UserOperScene.Close());
			this.Activated += new EventHandler((s, o) => new Thread(t => this.UserOperScene?.BringToFront()).Start());
			this.Move += new EventHandler((s, o) =>
			{
				var ScreenPoint = this.PointToScreen(new Point(0, 0));

				//必须等开始Load 了才能进行定位
				UserOperScene.Left = ScreenPoint.X - UserOperScene.Width;
				UserOperScene.Top = ScreenPoint.Y;
			});
			#endregion


			this.LoadData();
		}

		private void Preview_Load(object sender, EventArgs e)
		{
			this.Refresh();
			this.RefreshBackgroundImage();
		}

		protected override void WndProc(ref Message m)
		{
			if ((wMsg)m.Msg == wMsg.WM_EXITSIZEMOVE)
			{
				if (this.UserOperScene != null) this.UserOperScene.TopMost = false;
			}

			base.WndProc(ref m);
		}
		#endregion

		#region Functions (UI)
		private void ItemFrm_Shown(object sender, EventArgs e)
		{
			//Initialize显示状态
			this.UserOperScene.Refresh();
			if (this.UserOperScene.BtnCount == 0) this.MenuItem_SwitchUserOperPanel.Visible = false;
			else this.UserOperScene.Visible = this.MenuItem_SwitchUserOperPanel.Checked;


			//操作按钮
			if (this.UserOperScene != null && UserOperScene.Visible)
				this.UserOperScene.Show();
		}

		private void Preview_SizeChanged(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized) return;

			this.Refresh();
		}

		/// <summary>
		/// 目录保持右浮动
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lbl_Category_TextChanged(object sender, EventArgs e)
		{
			lbl_Category.Location = new Point(this.Width - lbl_Category.Width - 90, lbl_Category.Location.Y);
		}

		private void ItemNameCell_DoubleClick(object sender, EventArgs e) => sender.SetClipboard();

		/// <summary>
		/// 存储图标
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MenuItem_IconSaveAs_Click(object sender, EventArgs e) => this.ItemInfo.Icon().SaveDialog(this.ItemInfo.Key().ToString());

		private void MenuItem_SaveAsImage_Click(object sender, EventArgs e) => this.DrawMeToBitmap(true).SaveDialog(this.ItemInfo.alias + "_ScreenShot");

		/// <summary>
		/// 启用用户操作板
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MenuItem_SwitchUserOperPanel_CheckedChanged(object sender, EventArgs e)
		{
			if (this.UserOperScene != null)
			{
				this.UserOperScene.Visible = this.MenuItem_SwitchUserOperPanel.Checked;

				//保证焦点不会转移
				this.Activate();

				Ini.WriteValue("Preview", "item#option_UseUserOperPanel", this.MenuItem_SwitchUserOperPanel.Checked);
			}
		}

		private void lbl_Category_VisibleChanged(object sender, EventArgs e)
		{
			if (Loading) return;

			Ini.WriteValue("Preview", "item#option_ShowCategory3", this.lbl_Category.Visible);
		}


		private int m_nametype;

		/// <summary>
		/// 界面快捷键设置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Preview_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Modifiers == Keys.Control)      // Ctrl + 
			{
				switch (e.KeyCode)
				{
					//保存图片快捷键
					case Keys.A: MenuItem_IconSaveAs_Click(null, null); break;

					//快速切换物品名称与物品别名
					case Keys.F:
					{
						m_nametype++;

						switch (m_nametype)
						{
							case 1: this.ItemNameCell.Text = this.ItemInfo.alias; break;
							case 2: this.ItemNameCell.Text = this.ItemInfo.Key().ToString(); break;
							default:
							{
								m_nametype = 0;
								this.ItemNameCell.Text = this.ItemInfo.Name2;
							}
							break;
						}
					}
					break;

					//是否显示用户操作板
					case Keys.G: this.MenuItem_SwitchUserOperPanel.Checked = !this.MenuItem_SwitchUserOperPanel.Checked; break;

					//隐藏/显示奖励额外信息 快捷键
					case Keys.X:
					{
						foreach (var c in PreviewList.OfType<RewardPreview>())
						{
							c.ShowGroup = !c.ShowGroup;
							c.ShowJob = !c.ShowJob;

							c.ExecuteExtra();
						}
					}
					break;
				}
			}
			else
			{
				switch (e.KeyCode)
				{
					//case Keys.Escape: MainFrm_FormClosing(null, null); break;
				}
			}
		}


		public override void Refresh()
		{
			var param = new ContentParams();
			param.Add(ItemInfo);
			this.Controls.OfType<ContentPanel>().ForEach(c => c.Params = param);



			int Info_Top = 30;
			var Width = this.Width - 15;

			if (this.CardImage != null)
			{
				this.ItemNameCell.Location = new Point((this.Width - 15 - this.ItemNameCell.Width) / 2, 20);
				this.ItemIcon.Visible = false;
				this.lbl_Category.Visible = false;
				this.BackgroundImage = null;


				var CardPic = new PictureBox()
				{
					Size = new Size(Width, Width * this.CardImage.Height / this.CardImage.Width),
					SizeMode = PictureBoxSizeMode.StretchImage,

					Image = this.CardImage,
				};
				this.Controls.Add(CardPic);

				Info_Top = CardPic.Bottom;
			}
			else
			{
				#region Attribute Section
				Info_Top = 30;
				if (AttributePreview.Visible)
				{
					if (!this.Controls.Contains(AttributePreview)) this.Controls.Add(AttributePreview);

					this.AttributePreview.Location = new Point(this.lbl_SubInfo.Left, Info_Top);
					this.AttributePreview.Width = Width - this.AttributePreview.Left;

					Info_Top = this.AttributePreview.Bottom;
				}
				#endregion

				#region 获取Property
				List<MyInfo> ValidMainInfo = new(this.MainInfo);
				List<MyInfo> ValidSubInfo = new(this.SubInfo);

				foreach (var o in this.ItemAbility)
				{
					if (o.Value == 0) continue;
					var val = new MyInfo(o.Key.GetName(o.Value));

					//实际上直接 MainAbility转为此枚举是不正确的, 只是简便Functions
					if (o.Key == this.ItemInfo.MainAbility1 || o.Key == this.ItemInfo.MainAbility2) ValidMainInfo.Add(val);
					else ValidSubInfo.Add(val);
				}
				#endregion

				#region 处理信息
				ValidMainInfo = ValidMainInfo.Where(o => !string.IsNullOrWhiteSpace(o.Text)).ToList();
				if (ValidMainInfo.Any())
				{
					this.lbl_MainInfo.Location = new Point(this.lbl_MainInfo.Left, Info_Top);
					this.lbl_MainInfo.Text = ValidMainInfo.Select(Info => Info.Text).Aggregate((sum, now) => sum + "<br/>" + now);
					this.lbl_MainInfo.Visible = true;
					this.lbl_MainInfo.Refresh();

					Info_Top = this.lbl_MainInfo.Bottom + 2;
				}

				ValidSubInfo = ValidSubInfo.Where(o => !string.IsNullOrWhiteSpace(o.Text)).ToList();
				if (ValidSubInfo.Any())
				{
					this.lbl_SubInfo.Visible = true;
					this.lbl_SubInfo.Location = new Point(this.lbl_SubInfo.Left, Info_Top);
					this.lbl_SubInfo.Text = ValidSubInfo.Select(Info => Info.Text).Aggregate((sum, now) => sum + "<br/>" + now);
					this.lbl_SubInfo.Refresh();

					Info_Top = this.lbl_SubInfo.Bottom;
				}
				#endregion
			}


			#region Load 控件列表
			int PosY = Math.Max(ItemIcon.Bottom, Info_Top);
			foreach (var c in PreviewList)
			{
				c.Location = new Point(c is ContentPanel ? 5 : 0, PosY);
				c.Refresh();

				PosY = c.Bottom + 2;
			}

			foreach (var c in BottomControl)
			{
				c.Location = new Point(2, PosY);
				PosY = c.Bottom;
			}
			#endregion

			#region 其他处理
			this.Height = PosY + 90;
			this.lbl_Category.Location = new Point(Width - this.lbl_Category.Width, this.lbl_Category.Location.Y);
			this.PricePreview.Location = new Point(Width - this.PricePreview.Width, this.Height - 68);
			#endregion
		}

		/// <summary>
		/// 刷新背景图
		/// </summary>
		private void RefreshBackgroundImage()
		{
			if (this.CardImage != null) return;
			if (this.WindowState == FormWindowState.Maximized) return;


			Bitmap res = ItemInfo.LegendGradeBackgroundParticleType switch
			{
				LegendGradeBackgroundParticleTypeSeq.TypeGold => Resource_BNSR.T_tooltip_legend2_texture_cn_NEW,
				LegendGradeBackgroundParticleTypeSeq.TypeRedup => Resource_BNSR.T_tooltip_legend3_RedUp,
				LegendGradeBackgroundParticleTypeSeq.TypeGoldup => Resource_BNSR.T_tooltip_legend3_GoldUp,
				_ => ItemInfo.ItemGrade >= 7 ? Resource_BNSR.T_tooltip_legend_texture_cn : null,
			};

			if (res is null) return;

			var b = new Bitmap(this.Width, this.Height);
			var g = Graphics.FromImage(b);
			g.DrawImage(res.Clone(new Rectangle(81, 81, 355, 430), PixelFormat.Format64bppArgb),
				  new Rectangle(0, 0, b.Width, res.Height * b.Width / res.Width));

			this.BackgroundImage = b;
		}
		#endregion



		#region 控件
		/// <summary>
		/// 操作按钮
		/// </summary>
		readonly UserOperPanel UserOperScene;

		/// <summary>
		/// 控件组
		/// </summary>
		readonly List<Control> PreviewList = new();

		readonly List<Control> BottomControl = new();


		readonly AttributePreview AttributePreview = new();
		#endregion

		#region 控件Property
		public Bitmap CardImage;

		private List<MyInfo> MainInfo { get; set; } = new();

		private List<MyInfo> SubInfo { get; set; } = new();
		#endregion

		#region 载入控件
		/// <summary>
		/// Load 描述
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public static ContentPanel LoadDescription2(params string[] info)
		{
			var temp = info.Where(t => t != null);
			if (temp.Any()) return new ContentPanel(temp.Aggregate((sum, now) => sum + "<br/>" + now));

			return null;
		}

		public static TitlePanel LoadDescription(string Title, string Text) => (Title is null || Text is null) ? null : new TitleContentPanel(Title, Text);

		public static ContentPanel LoadDescription7(string Text) => Text is null ? null : new ContentPanel(Text);



		/// <summary>
		/// 载入底部控件
		/// </summary>
		/// <param name="Tag"></param>
		/// <param name="Text"></param>
		/// <param name="ForeColor"></param>
		/// <param name="Font"></param>
		public void LoadBottomControl(string Tag, string Text, Color? ForeColor = null, Font Font = null)
		{
			if (Font is null) Font = new Font("Microsoft YaHei UI", 10F);
			if (ForeColor is null) ForeColor = Color.FromArgb(255, 88, 66);

			//目标控件
			var tc = this.BottomControl.Find(c => (string)c.Tag == Tag);
			if (tc != null) return;



			var o = new Label()
			{
				BackColor = Color.Transparent,
				Font = new Font("Microsoft YaHei UI", 10F),
				ForeColor = ForeColor.Value,
				Text = Text,
				Tag = Tag,

				AutoSize = true,
			};

			this.Controls.Add(o);
			this.BottomControl.Add(o);
		}

		/// <summary>
		/// 控件控制Functions
		/// </summary>
		/// <param name="Preview"></param>
		public void LoadPreview(Control Preview)
		{
			if (Preview is null || !Preview.Visible) return;

			if (Preview is not ContentPanel)
				Preview.Width = this.Width;

			this.Controls.Add(Preview);
			this.PreviewList.Add(Preview);
		}
		#endregion

		#region Load Data
		private Dictionary<MainAbility, long> ItemAbility { get; set; } = new();

		/// <summary>
		/// 白字Property部分
		/// </summary>
		private void LoadAbility()
		{
			Dictionary<MainAbility, long> result = new();

			#region Load 攻击力
			var AttackPowerEquipMin = this.ItemInfo.Attributes["attack-power-equip-min"].ToInt();
			var AttackPowerEquipMax = this.ItemInfo.Attributes["attack-power-equip-max"].ToInt();
			result[MainAbility.AttackPowerEquipMinAndMax] = (AttackPowerEquipMin + AttackPowerEquipMax) / 2;

			var PveBossLevelNpcAttackPowerEquipMin = this.ItemInfo.Attributes["pve-boss-level-npc-attack-power-equip-min"].ToInt();
			var PveBossLevelNpcAttackPowerEquipMax = this.ItemInfo.Attributes["pve-boss-level-npc-attack-power-equip-max"].ToInt();
			result[MainAbility.PveBossLevelNpcAttackPowerEquipMinAndMax] = (PveBossLevelNpcAttackPowerEquipMin + PveBossLevelNpcAttackPowerEquipMax) / 2;

			var PvpAttackPowerEquipMin = this.ItemInfo.Attributes["pvp-attack-power-equip-min"].ToInt();
			var PvpAttackPowerEquipMax = this.ItemInfo.Attributes["pvp-attack-power-equip-max"].ToInt();
			result[MainAbility.PvpAttackPowerEquipMinAndMax] = (PvpAttackPowerEquipMin + PvpAttackPowerEquipMax) / 2;
			#endregion

			#region Load 其他Property
			foreach (var ability in Enum.GetValues<MainAbility>())
			{
				if (ability == MainAbility.None) continue;

				var signal = ability.GetSignal();
				var Value = this.ItemInfo.Attributes[signal];
				var ValueEquip = this.ItemInfo.Attributes[signal + "-equip"];

				if (Value != null) result[ability] = int.Parse(Value);
				else if (ValueEquip != null) result[ability] = int.Parse(ValueEquip);
			}
			#endregion

			this.ItemAbility = result;
		}

		/// <summary>
		/// Load 宝石类的BuffProperty信息
		/// </summary>
		private void LoadEffectInfo()
		{
			for (int i = 1; i <= 4; i++)
			{
				var EffectEquip = FileCache.Data.Effect[this.ItemInfo.Attributes[$"effect-equip-{i}"]];
				if (EffectEquip is not null)
				{
					//显示附加效果文本提示信息
					if (EffectEquip.Name3 != null)
						this.MainInfo.Add(new MyInfo(EffectEquip.Name3.GetText()));

					if (EffectEquip.Description3 != null)
						this.SubInfo.Add(new MyInfo(EffectEquip.Description3.GetText()));
				}
			}
		}

		/// <summary>
		/// Load 奖励信息
		/// </summary>
		private void LoadReward()
		{
			//判断是否为封印物品
			var title = (this.ItemInfo.Type == ItemType.Grocery && this.ItemInfo.UnsealAcquireItem1 is null) ? "奖励" : "分解";
			var preview = new RewardPreview(title);

			#region 绑定修改奖励分页Event
			preview.SelRewardChanged += (o, s) =>
			{
				//清理先前提示
				this.MainInfo.RemoveAll(Info => Info.Tag == "RewardPreview");

				//如果存在开启物品
				if (s.Page.HasOpenItem2)
				{
					var OpenItem = FileCache.Data.Item[s.Page.OpenItem2.Item];
					var OpenItemCount = s.Page.OpenItem2.StackCount;

					string Info = "需要";
					if (OpenItemCount != 1 && OpenItemCount != 0) Info += OpenItemCount + "个";
					Info += OpenItem?.ItemName;

					this.MainInfo.Insert(0, new MyInfo(Info, "RewardPreview"));
				}



				var SelectedItemAssuredCount = s.Page.RewardInfo.DecomposeReward?.Attributes["selected-item-assured-count"].ToInt() ?? 0;
				if (SelectedItemAssuredCount != 0) this.MainInfo.Add(new MyInfo($"可选择{ SelectedItemAssuredCount }项", "RewardPreview"));

				this.Refresh();
			};
			#endregion

			this.LoadPreview(preview.LoadInfo(this.ItemInfo));
		}

		/// <summary>
		/// Load 额外信息
		/// </summary>
		private ContentPanel LoadExtraInfo()
		{
			string ExtraInfo = null;

			#region badge
			var BadgeInfo = new List<string>();

			if (this.ItemInfo.BadgeGearScore != 0) BadgeInfo.Add("徽章套装点数 " + this.ItemInfo.BadgeGearScore);
			if (this.ItemInfo.BadgeSynthesisScore != 0) BadgeInfo.Add("徽章合成点数 " + this.ItemInfo.BadgeSynthesisScore);

			if (BadgeInfo.Any()) this.LoadPreview(new ContentPanel(BadgeInfo.Aggregate((sum, now) => sum + "<br/>" + now)));
			#endregion

			#region post charge
			var DecomposeMoneyCost = this.ItemInfo.Attributes["decompose-money-cost"].ToInt();
			if (DecomposeMoneyCost != 0) this.MainInfo.Insert(0, new MyInfo("需要" + new MoneyConvert(DecomposeMoneyCost)));

			//判断是否可以邮寄, 限制5铜以上邮费价格道具才能显示追加信息
			if (this.ItemInfo.BaseFee > 5 && (this.ItemInfo.AccountUsed || !this.ItemInfo.CannotTrade))
			{
				string Info = $"邮寄时, 每个需要 { new MoneyConvert(this.ItemInfo.BaseFee) }<br/>";
				ExtraInfo += Info;
			}

			var AccountPostCharge = this.ItemInfo.AccountPostCharge;
			if (AccountPostCharge != null)
			{
				var ChargeItem1 = FileCache.Data.Item[AccountPostCharge.ChargeItem1];
				var ChargeItem2 = FileCache.Data.Item[AccountPostCharge.ChargeItem2];

				if (ChargeItem1 != null || ChargeItem2 != null)
				{
					if (ChargeItem1 != null && ChargeItem2 != null)
					{
						ExtraInfo += $"账号交易时, 需要" +
						 $"{ ChargeItem1.ItemName }<ga/> { AccountPostCharge.ChargeItemAmount1 }个、<wa/> " +
						 $"{ ChargeItem2.ItemName }<ga/> { AccountPostCharge.ChargeItemAmount2 }个。";
					}
					else
					{
						var ChargeItem = ChargeItem1;
						int ItemCount;
						if (ChargeItem != null) ItemCount = AccountPostCharge.ChargeItemAmount1;
						else
						{
							ChargeItem = ChargeItem2;
							ItemCount = AccountPostCharge.ChargeItemAmount2;
						}

						ExtraInfo += $"账号交易时, 需要{ ChargeItem.ItemName }</font><ga/> { ItemCount }个。";
					}
				}
			}
			#endregion


			if (this.ItemInfo.UseRecycleGroupDuration != 0)
				this.MainInfo.Add(new MyInfo("冷却时间 " + TimeSpan.FromMilliseconds(this.ItemInfo.UseRecycleGroupDuration).ToMyString()));


			if (ExtraInfo != null) return new ContentPanel(ExtraInfo);
			return null;
		}

		/// <summary>
		/// 获取交易类别
		/// </summary>
		/// <returns></returns>
		private void LoadTrade()
		{
			if (this.ItemInfo.AccountUsed)
				this.LoadBottomControl("TradeInfo_Account", "账号专用");

			#region 处理前端文本
			string TradeInfo;

			//交易Property显示
			if (this.ItemInfo.CannotTrade)
			{
				if (this.ItemInfo.Auctionable) TradeInfo = "无法个人交易";
				else TradeInfo = "无法交易";
			}
			else if (this.ItemInfo.EquipUsed && ItemInfo.Type != ItemType.Grocery)
			{
				//支持无目标封印状态
				TradeInfo = this.ItemInfo.CannotTrade ? "封印状态时, 解除封印后无法交易" : "装备后无法交易";
			}
			else if (this.ItemInfo.AcquireUsed) TradeInfo = "无法交易";  //拾取后无法交易
			else if (this.ItemInfo.Auctionable) TradeInfo = null;
			else TradeInfo = "无法拍卖行交易";
			#endregion

			if (TradeInfo != null) this.LoadBottomControl("TradeInfo", TradeInfo);
		}

		public static Bitmap LoadCardImage(Bitmap CardImage, byte ItemGrade)
		{
			if (CardImage is null) return null;

			var bitmap = new Bitmap(500, 635 + 60);
			Graphics g = Graphics.FromImage(bitmap);

			#region 获取背景图
			var bg = ItemGrade switch
			{
				2 => Resources.Resource_BNSR.CollectionCard_Preview_2,
				3 => Resources.Resource_BNSR.CollectionCard_Preview_3,
				4 => Resources.Resource_BNSR.CollectionCard_Preview_4,
				5 => Resources.Resource_BNSR.CollectionCard_Preview_5,
				6 => Resources.Resource_BNSR.CollectionCard_Preview_6,
				7 => Resources.Resource_BNSR.CollectionCard_Preview_7,
				8 => Resources.Resource_BNSR.CollectionCard_Preview_8,
				9 => Resources.Resource_BNSR.CollectionCard_Preview_9,
				_ => Resources.Resource_BNSR.CollectionCard_Preview_1,
			};
			#endregion

			g.DrawImage(CardImage, 0, 60);
			g.DrawImage(bg, 0, 0, bitmap.Width, bitmap.Height);

			return bitmap;
		}

		private void LoadCardImage()
		{
			if (this.ItemInfo.Card is null) return;

			this.CardImage = LoadCardImage(this.ItemInfo.Card.CardImage.GetUObject().GetImage(), this.ItemInfo.ItemGrade);
		}
		#endregion


		private void LoadData()
		{
			#region Load 信息
			this.Text = $"[{ this.ItemInfo.Key() }] { this.ItemInfo.Name2 }";

			this.ItemIcon.Image = this.ItemInfo.IconExtra();
			this.ItemNameCell.ItemGrade = this.ItemInfo.ItemGrade;
			this.ItemNameCell.TagImage = this.ItemInfo.TagIconGrade;
			this.lbl_Category.Text = $"Name.item.game-category-3.{ this.ItemInfo.GameCategory3.GetSignal() }".GetText(true);
			this.PricePreview.CurrencyCount = this.ItemInfo.Attributes["price"].ToInt();
			this.ItemNameCell.Text = this.ItemInfo.Name2;


			this.MainInfo = new List<MyInfo>()
			{
				new MyInfo(this.ItemInfo.MainInfo),
				new MyInfo(this.ItemInfo.IdentifyMainInfo),
			};

			this.SubInfo = new List<MyInfo>()
			{
				new MyInfo(this.ItemInfo.SubInfo),
				new MyInfo(this.ItemInfo.IdentifySubInfo),
			};

			this.LoadAbility();


			//潜力值
			if (this.ItemInfo.ContainsAttribute("hidden-power-attach", out string HiddenPower))
				this.LoadBottomControl("hidden-power-attach", "潜力" + HiddenPower, Color.FromArgb(0xff, 0xE9, 0x58));

			//限制使用区域
			if (this.ItemInfo.ContainsAttribute("valid-attraction-name", out string ValidAttractionName))
				this.LoadBottomControl("valid-attraction-name", ValidAttractionName.GetText() + "专用");

			//显示职业信息
			var JobInfo = this.ItemInfo.JobInfo;
			if (JobInfo != null) this.LoadBottomControl("JobLimit", JobInfo + ", 专用");

			//Load 交易信息
			this.LoadTrade();
			#endregion

			#region 载入提示工具
			this.LoadPreview(new ExchangePreview().LoadInfo(this.ItemInfo));
			this.LoadEffectInfo();

			//Load 绿字部分信息
			this.LoadPreview(LoadDescription7(this.ItemInfo.Description7));
			this.LoadPreview(new SkillTooltipPreview().LoadInfo(this.ItemInfo));

			//Load 套装信息
			this.LoadPreview(new SetItemPreview().LoadInfo(this.ItemInfo));

			//Load 技能变更信息
			this.LoadPreview(new SkillByEquipmentTooltip().LoadInfo(this.ItemInfo.SkillByEquipment));

			//Load 奖励信息
			this.LoadReward();

			//Load 封印&解印信息
			this.LoadPreview(new SealPreview().LoadInfo(this.ItemInfo));

			//Load 刻印书信息
			this.LoadPreview(new SlateScrollTooltip().LoadInfo(this.ItemInfo.SlateScroll));

			//Load 描述4~6
			this.LoadPreview(LoadDescription(this.ItemInfo.Description4Title, this.ItemInfo.Description4));
			this.LoadPreview(LoadDescription(this.ItemInfo.Description5Title, this.ItemInfo.Description5));
			this.LoadPreview(LoadDescription(this.ItemInfo.Description6Title, this.ItemInfo.Description6));

			//Load 描述信息
			this.LoadPreview(LoadDescription2(this.ItemInfo.Description2, this.ItemInfo.IdentifyDescription));

			//Load 额外信息
			this.LoadPreview(this.LoadExtraInfo());

			//Load Event信息
			this.LoadPreview(new EventTimePreview().LoadInfo(this.ItemInfo.EventInfo));

			//Load 可成长八卦牌Property信息
			this.AttributePreview.LoadInfo(this.ItemInfo);
			#endregion



			if (this.ItemInfo.Type == ItemType.Grocery)
			{
				#region 可用技能测试
				var Skill3Data = this.ItemInfo.Skill3;
				if (Skill3Data != null) System.Diagnostics.Trace.WriteLine($"发动技能 => { Skill3Data.alias } ({ Skill3Data.Name2.GetText() })");
				#endregion

				if (this.ItemInfo.Card != null) this.LoadCardImage();
			}
		}
	}

	/// <summary>
	/// 自定义信息
	/// </summary>
	public class MyInfo
	{
		public string Text;

		public string Tag;

		public MyInfo(string Text, string Tag = null)
		{
			this.Text = Text;
			this.Tag = Tag;
		}
	}
}