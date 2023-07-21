using System.Drawing.Imaging;

using CUE4Parse.BNS.Conversion;

using Xylia.Configure;
using Xylia.Extension;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.DatData.DatDetect;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.Game_ToolTipScene;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.Game_ToolTipScene.ItemTooltipPanel;
using Xylia.Preview.UI.Custom.Controls;
using Xylia.Preview.UI.Resources;

using static Vanara.PInvoke.User32;
using static Xylia.Preview.Data.Record.Item;


namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel;
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


		if (bool.TryParse(Ini.ReadValue("Preview", "item#option_UseUserOperPanel"), out bool f1))
			this.MenuItem_SwitchUserOperPanel.Checked = f1;

		Loading = false;
		#endregion

		#region UserOperator
		this.UserOperScene = new UserOperPanel(this);
		this.FormClosing += new FormClosingEventHandler((s, o) => this.UserOperScene.Close());
		this.Activated += new EventHandler((s, o) => new Thread(t => this.UserOperScene?.BringToFront()).Start());
		this.Move += new EventHandler((s, o) =>
		{
			var ScreenPoint = this.PointToScreen(new Point(0, 0));

			UserOperScene.Left = ScreenPoint.X - UserOperScene.Width;
			UserOperScene.Top = ScreenPoint.Y;
		});
		#endregion


		this.LoadData();
	}


	protected override void WndProc(ref Message m)
	{
		var msg = (WindowMessage)m.Msg;
		if (msg == WindowMessage.WM_EXITSIZEMOVE)
		{
			if (this.UserOperScene != null) this.UserOperScene.TopMost = false;
		}

		base.WndProc(ref m);
	}
	#endregion

	#region Functions (UI)
	private void ItemFrm_Shown(object sender, EventArgs e)
	{
		this.UserOperScene.Refresh();
		if (this.UserOperScene.BtnCount == 0) this.MenuItem_SwitchUserOperPanel.Visible = false;
		else this.UserOperScene.Visible = this.MenuItem_SwitchUserOperPanel.Checked;


		if (this.UserOperScene != null && UserOperScene.Visible)
			this.UserOperScene.Show();


		this.Refresh();
		this.RefreshBackgroundImage();
	}

	private void Preview_SizeChanged(object sender, EventArgs e)
	{
		if (this.WindowState == FormWindowState.Minimized) return;

		this.Refresh();
	}

	private void ItemNamePanel_DoubleClick(object sender, EventArgs e) => sender.SetClipboard();

	private void MenuItem_IconSaveAs_Click(object sender, EventArgs e) => this.ItemInfo.Icon().SaveDialog(this.ItemInfo.Ref.Id.ToString());

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
						case 1: this.ItemNamePanel.Text = this.ItemInfo.alias; break;
						case 2: this.ItemNamePanel.Text = this.ItemInfo.Ref.Id.ToString(); break;
						default:
						{
							m_nametype = 0;
							this.ItemNamePanel.Text = this.ItemInfo.Name2;
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
					foreach (var c in PreviewList.OfType<RandomboxPreview>())
					{
						c.ShowGroup = !c.ShowGroup;
						c.ShowJob = !c.ShowJob;
					}
				}
				break;
			}
		}
		else
		{
#pragma warning disable CS1522
			switch (e.KeyCode)
			{
				//case Keys.Escape: MainFrm_FormClosing(null, null); break;
			}
#pragma warning restore CS1522
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
			this.ItemNamePanel.Location = new Point((this.Width - 15 - this.ItemNamePanel.Width) / 2, 20);
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

			#region Info
			List<MyInfo> ValidMainInfo = new(this.MainInfo);
			List<MyInfo> ValidSubInfo = new(this.SubInfo);

			foreach (var o in this.ItemAbility)
			{
				if (o.Value == 0) continue;
				var val = new MyInfo(o.Key.GetName(o.Value));

				// 实际上直接 MainAbility转为此枚举是不正确的
				if (o.Key == this.ItemInfo.MainAbility1 || o.Key == this.ItemInfo.MainAbility2) ValidMainInfo.Add(val);
				else ValidSubInfo.Add(val);
			}
			#endregion

			#region UI
			ValidMainInfo = ValidMainInfo.Where(o => !string.IsNullOrWhiteSpace(o.Text)).ToList();
			if (ValidMainInfo.Any())
			{
				this.lbl_MainInfo.Location = new Point(this.lbl_MainInfo.Left, Info_Top);
				this.lbl_MainInfo.Text = ValidMainInfo.Select(Info => Info.Text).Aggregate("<br/>");
				this.lbl_MainInfo.Visible = true;
				this.lbl_MainInfo.Refresh();

				Info_Top = this.lbl_MainInfo.Bottom + 2;
			}

			ValidSubInfo = ValidSubInfo.Where(o => !string.IsNullOrWhiteSpace(o.Text)).ToList();
			if (ValidSubInfo.Any())
			{
				this.lbl_SubInfo.Visible = true;
				this.lbl_SubInfo.Location = new Point(this.lbl_SubInfo.Left, Info_Top);
				this.lbl_SubInfo.Text = ValidSubInfo.Select(Info => Info.Text).Aggregate("<br/>");
				this.lbl_SubInfo.Refresh();

				Info_Top = this.lbl_SubInfo.Bottom;
			}
			#endregion
		}


		#region Load Controls
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

		#region Location
		this.lbl_Category.Location = new Point(Width - this.lbl_Category.Width, this.lbl_Category.Location.Y);
		this.PricePreview.Location = new Point(Width - this.PricePreview.Width, PosY - 20);
		this.ClientSize = this.ClientSize with { Height = PosY + 10 }; 
		#endregion
	}

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



	#region Controls
	readonly UserOperPanel UserOperScene;

	/// <summary>
	/// 控件组
	/// </summary>
	readonly List<Control> PreviewList = new();

	readonly List<Control> BottomControl = new();


	readonly AttributePreview AttributePreview = new();



	internal Bitmap CardImage;

	internal List<MyInfo> MainInfo { get; set; } = new();

	internal List<MyInfo> SubInfo { get; set; } = new();
	#endregion


	#region Load Control
	private static ContentPanel LoadDescription2(params string[] info)
	{
		var temp = info.Where(t => !string.IsNullOrEmpty(t));
		if (temp.Any()) return new ContentPanel(temp.Aggregate("<br/>"));

		return null;
	}

	private static TitlePanel LoadDescription(string Title, string Text) => (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Text)) ? null : new TitleContentPanel(Title, Text);

	private static ContentPanel LoadDescription7(string Text) => string.IsNullOrEmpty(Text) ? null : new ContentPanel(Text);

	public void LoadBottomControl(string Text, Color? ForeColor = null, Font Font = null)
	{
		var o = new ContentPanel()
		{
			BackColor = Color.Transparent,
			Font = Font ?? new Font("Microsoft YaHei UI", 10F),
			ForeColor = ForeColor ?? Color.FromArgb(255, 88, 66),
			Text = Text,
			Tag = Tag,

			AutoSize = true,
		};

		this.Controls.Add(o);
		this.BottomControl.Add(o);
	}

	private void LoadPreview(Control Preview)
	{
		if (Preview is null || !Preview.Visible) return;

		if (Preview is not ContentPanel)
			Preview.Width = this.Width;

		this.Controls.Add(Preview);
		this.PreviewList.Add(Preview);
	}

	private void LoadPreview(Type Preview)
	{
		if (Preview.IsAbstract || Preview.IsInterface)
			throw new InvalidOperationException();

		var instance = Activator.CreateInstance(Preview);
		if (instance is ItemTooltip tooltip)
		{
			tooltip.Data = this.ItemInfo;
			LoadPreview(tooltip.Load());
		}
	}



	public static Bitmap LoadCardImage(Bitmap CardImage, byte ItemGrade)
	{
		if (CardImage is null) return null;

		var bitmap = new Bitmap(500, 635 + 60);
		Graphics g = Graphics.FromImage(bitmap);

		#region 获取背景图
		var bg = ItemGrade switch
		{
			2 => Resource_BNSR.CollectionCard_Preview_2,
			3 => Resource_BNSR.CollectionCard_Preview_3,
			4 => Resource_BNSR.CollectionCard_Preview_4,
			5 => Resource_BNSR.CollectionCard_Preview_5,
			6 => Resource_BNSR.CollectionCard_Preview_6,
			7 => Resource_BNSR.CollectionCard_Preview_7,
			8 => Resource_BNSR.CollectionCard_Preview_8,
			9 => Resource_BNSR.CollectionCard_Preview_9,
			_ => Resource_BNSR.CollectionCard_Preview_1,
		};
		#endregion

		g.DrawImage(CardImage, 0, 60);
		g.DrawImage(bg, 0, 0, bitmap.Width, bitmap.Height);

		return bitmap;
	}
	#endregion

	#region Load Data
	private Dictionary<MainAbility, long> ItemAbility { get; set; } = new();

	private void LoadAbility()
	{
		Dictionary<MainAbility, long> result = new();

		#region AttackPower
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

		#region Else Power
		foreach (var ability in Enum.GetValues<MainAbility>())
		{
			if (ability == MainAbility.None) continue;

			var signal = ability.GetSignal();
			var Value = this.ItemInfo.Attributes[signal];
			var ValueEquip = this.ItemInfo.Attributes[signal + "-equip"];

			if (int.TryParse(Value, out var value)) result[ability] = value;
			else if (int.TryParse(ValueEquip, out value)) result[ability] = value;
		}
		#endregion

		this.ItemAbility = result;
	}

	private void LoadEffectInfo()
	{
		for (int i = 1; i <= 4; i++)
		{
			var EffectEquip = FileCache.Data.Effect[this.ItemInfo.Attributes[$"effect-equip-{i}"]];
			if (EffectEquip is not null)
			{
				if (EffectEquip.Name3 != null)
					this.MainInfo.Add(new MyInfo(EffectEquip.Name3.GetText()));

				if (EffectEquip.Description3 != null)
					this.SubInfo.Add(new MyInfo(EffectEquip.Description3.GetText()));
			}
		}
	}

	private ContentPanel LoadExtraInfo()
	{
		string ExtraInfo = null;

		#region badge
		if (this.ItemInfo is Grocery grocery)
		{
			var BadgeInfo = new List<string>();

			if (grocery.BadgeGearScore != 0) BadgeInfo.Add(new ContentParams(grocery.BadgeGearScore).Handle("UI.ItemTooltip.SetItem.BadgeGearScore"));
			if (grocery.BadgeSynthesisScore != 0) BadgeInfo.Add(new ContentParams(grocery.BadgeSynthesisScore).Handle("UI.ItemTooltip.SetItem.BadgeComposeScore"));

			if (BadgeInfo.Any()) this.LoadPreview(new ContentPanel(BadgeInfo.Aggregate("<br/>")));
		}
		#endregion



		#region post charge
		var DecomposeMoneyCost = this.ItemInfo.Attributes["decompose-money-cost"].ToInt();
		if (DecomposeMoneyCost != 0)
		{
			var param = new ContentParams(DecomposeMoneyCost);
			this.MainInfo.Insert(0, new MyInfo(param.Handle("UI.Common.Required.Money")));
		}

		if (this.ItemInfo.BaseFee > 5 && (this.ItemInfo.AccountUsed || !this.ItemInfo.CannotTrade))
		{
			ExtraInfo += $"{"UI.Guild.Title12".GetText()} {new Money(this.ItemInfo.BaseFee)}<br/>";
		}

		var AccountPostCharge = this.ItemInfo.AccountPostCharge;
		if (AccountPostCharge != null)
		{
			var param = new ContentParams();
			param[2] = AccountPostCharge.ChargeItem1;
			param[3] = AccountPostCharge.ChargeItemAmount1;
			param[4] = AccountPostCharge.ChargeItem2;
			param[5] = AccountPostCharge.ChargeItemAmount2;

			if (!AccountPostCharge.ChargeItem1.INVALID())
				ExtraInfo += param.Handle(AccountPostCharge.ChargeItem2.INVALID() ?
					"UI.ItemTooltip.DeliveryCharge1" :
					"UI.ItemTooltip.DeliveryCharge2");
		}
		#endregion

		if (this.ItemInfo.UseRecycleGroupDuration != 0)
			this.MainInfo.Add(new MyInfo("Name.Item.recycle-group-2-duration".GetText() + " " +
				TimeSpan.FromMilliseconds(this.ItemInfo.UseRecycleGroupDuration).ToMyString()));


		if (ExtraInfo != null) return new ContentPanel(ExtraInfo);
		return null;
	}

	private void LoadItemLimit()
	{
		if (ItemInfo.HiddenPowerAttach != 0)
			this.LoadBottomControl(new ContentParams(null, ItemInfo.HiddenPowerAttach).Handle("Name.Item.Potential"), Color.FromArgb(0xff, 0xE9, 0x58));

		if (ItemInfo.Contains("valid-attraction-name", out _))
		{
			//UI.ItemTooltip.AttractionFilter.Fail
			this.LoadBottomControl("UI.ItemTooltip.AttractionFilter".GetText());
		}




		{
			string Required = null;
			foreach (var job in ItemInfo.EquipJobCheck)
			{
				var param = new ContentParams();
				param[3] = job;

				Required += param.Handle(true ? "Name.Item.Required.Job2" : "Name.Item.Required.Job2.Fail");
			}

			if (ItemInfo.EquipRace != RaceSeq2.All)
				Required += (true ? "Name.Item.Required.Race" : "Name.Item.Required.Race.Fail").GetText();

			if (ItemInfo.EquipSex != SexSeq2.All)
				Required += (true ? "Name.Item.Required.Sex" : "Name.Item.Required.Sex.Fail").GetText();



			if (Required != null)
				this.LoadBottomControl(new ContentParams(Required).Handle(true ? "Name.Item.Required.Result" : "Name.Item.Required.Result.Fail"));
			else if (ItemInfo.EquipFactionLevel != 0)
				this.LoadBottomControl(new ContentParams(null, ItemInfo.EquipFactionLevel).Handle(true ? "Name.Item.Required.FactionRank" : "Name.Item.Required.FactionRank.Fail"));
			else if (false)
				this.LoadBottomControl(new ContentParams().Handle(true ? "Name.Item.Required.FactionReputation" : "Name.Item.Required.FactionReputation.Fail"));
			else if (ItemInfo is Costume or Accessory)
				this.LoadBottomControl(new ContentParams().Handle("Name.Item.Required.Everyone".GetText()));
		}






		if (this.ItemInfo.AccountUsed)
			this.LoadBottomControl("UI.ItemTooltip.AccountUsed.Acquire".GetText());


		// useless
		//Name.Item.Cannot.Seal
		//Name.Item.Cannot.Trade

		List<string> LimitInfo = new();
		if (ItemInfo.CannotTrade && ItemInfo.Auctionable)
			LimitInfo.Add("Name.Item.Cannot.Trade.Player");
		else if (ItemInfo.CannotTrade)
			LimitInfo.Add("Name.Item.Cannot.Trade.All");
		//else if (ItemInfo.EquipUsed && ItemInfo.Type != ItemType.Grocery)	 //这两种情况不显示在UI上，需要另外处理
		//	LimitInfo.Add("UI.ItemTooltip.EquipUsed");
		//else if (ItemInfo.AcquireUsed)
		//	LimitInfo.Add("UI.ItemTooltip.AcquireUsed");
		else if (!ItemInfo.Auctionable)
			LimitInfo.Add("Name.Item.Cannot.Trade.Auction");


		if (ItemInfo.CannotSell)
			LimitInfo.Add("Name.Item.Cannot.Sell");
		if (ItemInfo.CannotDispose)
			LimitInfo.Add("Name.Item.Cannot.Dispose");
		if (ItemInfo is Weapon && !ItemInfo.Attributes["repairable"].ToBool())
			LimitInfo.Add("Name.Item.Cannot.Repair");


		if (LimitInfo.Any())
		{
			var comma = "Name.Item.Cannot.Comma".GetText();
			this.LoadBottomControl(FileCache.Data.DataPath?.Locale?.Language switch
			{
				Language.Korean => LimitInfo.Aggregate(comma, now => (now).GetText()) + " " + "Name.Item.Cannot".GetText(),
				Language.ChineseS => LimitInfo.Aggregate(comma, now => (now + ".Tencent").GetText()),
				_ => LimitInfo.Aggregate(comma, now => (now + ".Global").GetText()),

			});
		}
	}
	#endregion


	private void LoadData()
	{
		#region load info
		this.Text = $"[{this.ItemInfo.Ref.Id}] {this.ItemInfo.Name2}";

		this.ItemIcon.Image = this.ItemInfo.IconExtra();
		this.ItemNamePanel.ItemGrade = this.ItemInfo.ItemGrade;
		this.ItemNamePanel.TagImage = this.ItemInfo.TagIconGrade;
		this.ItemNamePanel.Text = this.ItemInfo.Name2;

		this.lbl_Category.Text = $"Name.item.game-category-3.{this.ItemInfo.GameCategory3.GetSignal()}".GetText(true);
		this.PricePreview.CurrencyCount = this.ItemInfo.Attributes["price"].ToInt();

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
		#endregion

		#region load sub tooltip
		this.LoadAbility();
		this.LoadItemLimit();
		this.LoadPreview(new PieceTransformPreview().LoadInfo(this.ItemInfo));
		this.LoadEffectInfo();
		this.LoadPreview(LoadDescription7(this.ItemInfo.Description7));
		this.LoadPreview(new SkillTooltipPreview().LoadInfo(this.ItemInfo));
		this.LoadPreview(new SetItemTooltip().LoadInfo(this.ItemInfo));
		if (this.ItemInfo is Item.Weapon weapon) this.LoadPreview(new SkillChangedTooltip().LoadInfo(weapon.SkillByEquipment));
		this.LoadPreview(new RandomboxPreview().LoadInfo(this.ItemInfo));
		this.LoadPreview(new SealPreview().LoadInfo(this.ItemInfo));
		if (this.ItemInfo is Item.Grocery grocery) this.LoadPreview(new SlateScrollTooltip().LoadInfo(grocery.SlateScroll));
		this.LoadPreview(LoadDescription(this.ItemInfo.Description4Title, this.ItemInfo.Description4));
		this.LoadPreview(LoadDescription(this.ItemInfo.Description5Title, this.ItemInfo.Description5));
		this.LoadPreview(LoadDescription(this.ItemInfo.Description6Title, this.ItemInfo.Description6));
		this.LoadPreview(LoadDescription2(this.ItemInfo.Description2, this.ItemInfo.IdentifyDescription));
		this.LoadPreview(this.LoadExtraInfo());
		this.LoadPreview(new EventTimePreview().LoadInfo(this.ItemInfo.EventInfo));
		this.LoadPreview(typeof(Durability));
		this.AttributePreview.LoadInfo(this.ItemInfo);
		#endregion



		if (this.ItemInfo is Grocery Grocery)
		{
			var Skill3Data = Grocery.Skill3;
			if (Skill3Data != null) Trace.WriteLine($">> {Skill3Data.alias} ({Skill3Data.Name2.GetText()})");

			if (Grocery.Card != null)
				this.CardImage = LoadCardImage(Grocery.Card.CardImage.GetUObject().GetImage(), Grocery.ItemGrade);
		}
	}
}