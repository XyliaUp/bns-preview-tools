using System.Text.RegularExpressions;
using System.Xml;

using Xylia.Extension;
using Xylia.Match.Properties;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table;
using Xylia.Preview.Data.Record;

namespace Xylia.Match.Util;
public class InfoGet
{
	readonly Table<Text> TextData;

	public InfoGet(TableSet set)
	{
		this.Special = GetSpecial();
		this.TextData = set.Text;
	}



	#region Load Functions
	public string GetName2(string ItemAlias)
	{
		string NameText = TextData[$"Item.Name2.{ItemAlias}"].GetText();

		//名称获取异常状态替换规则
		if (NameText is null)
		{
			if (Special is null || !Special.TryGetValue(ItemAlias, out NameText))
			{
				if (ItemAlias.StartsWith("NPC_")) return "哈迪斯";
				if (ItemAlias.StartsWith("Lobby_")) return "哈迪斯";

				if (ItemAlias.MyContains("Test")) return "";
			}
		}

		return NameText is null ? null : NameText + GetEquipGem(ItemAlias);
	}

	public string GetDesc(string ItemAlias)
	{
		string Desc = null;
		Desc += TextData[$"Item.Desc2.{ItemAlias}"].GetText();
		Desc += TextData[$"Item.Desc5.{ItemAlias}"].GetText();

		return BNS_Cut(Desc);
	}

	public string GetInfo(string ItemAlias)
	{
		string Info = null;
		Info += TextData[$"Item.MainInfo.{ItemAlias}"].GetText();
		Info += TextData[$"Item.IdentifyMain.{ItemAlias}"].GetText();
		Info += TextData[$"Item.IdentifySub.{ItemAlias}"].GetText();

		return BNS_Cut(Info);
	}
	#endregion


	#region Special
	Dictionary<string, string> Special;

	static Dictionary<string, string> GetSpecial()
	{
		var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		var xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(Resources.Resources.Game);
		foreach (var (alias, text) in from XmlNode xmlNode in xmlDocument.SelectNodes("Game/record")
									  let alias = xmlNode.Attributes["alias"]?.Value
									  let text = xmlNode.Attributes["name2"]?.Value
									  where alias != null
									  select (alias, text))
		{
			dictionary[alias] = text;
		}

		return dictionary;
	}
	#endregion


	#region BnSConvert
	public static string BNS_Cut(string Str)
	{
		if (string.IsNullOrWhiteSpace(Str)) return null;

		#region 替换标识符部分
		Regex Replace_Tag1 = new(@"<image.*?\.", RegexOptions.IgnoreCase);
		Regex Replace_Tag2 = new("scalerate=.*?/>", RegexOptions.IgnoreCase);


		string Tag = Replace_Tag1.Replace(Replace_Tag2.Replace(Str, ""), "")
			.Replace("Tag_", "").Replace("Tooltip_", "")
			.Replace("GetWhere", "[兑换]").Replace("Warning", "[提醒]").Replace("Random", "[随机]").Replace("GuildProduction", "[门派制作]").Replace("Job_", "")
			.Replace("FildDrop", "[区域]").Replace("GrowthExp", "[成长经验]").Replace("NormalKey", "[钥匙]").Replace("Roulette", "[转盘]").Replace("process", "加工")
			.Replace("CampfireName", "龙柱").Replace("Portrait_Alert", "[提示]").Replace("Map_Unit_PartyCamp", "[队伍]").Replace("Production_", "[")
			.Replace("Growth", "[成长]").Replace("Party", "[副本]").Replace("PowerBook", "[百科]").Replace("RankStar", "[关心任务]")
			.Replace("SpecialKey", "[钥匙]").Replace("Epic_start", "[主线]").Replace("side_episode_start", "[外传]").Replace("GemDecompose", "[分解]")
			.Replace("Faction_start", "[势力任务]").Replace("Faction", "[势力]").Replace("Normal_start", "[支线任务]").Replace("Repeat_start", "[每日任务]")
			.Replace("DoGiBang", "陶瓷坊]").Replace("ChulMuBang", "铁匠坊]").Replace("YackJeSa", "药王院]").Replace("SungDunDang", "圣君堂]")
			.Replace("Daeeobang", "大鱼坊]").Replace("ManGumDang", "万金堂]").Replace("BulMockDang", "木作坊]").Replace("SukGongBang", "石工坊]")
			.Replace("PungNyeonHoe", "丰年会]")
			.Replace("SuRyeopDang", "百猎庄]").Replace("YackChoBang", "药草坊]").Replace("IlMiMun", "风味门]").Replace("CheGulDang", "冶金庄]")
			.Replace("CharInfo_", null)
			.Replace("HP", "（生命）").Replace("Defend", "（防御）").Replace("BossAttackPower", "（降魔攻击力）")
			.Replace('"'.ToString(), "");


		Regex replace1 = new Regex(@"<.*?>", RegexOptions.IgnoreCase);
		string New = replace1.Replace(Tag, "");
		#endregion

		//执行引号替换
		return New.Replace("&quot;", '"'.ToString().Replace('”', '"')).Replace("enablescale=true", "");
	}

	public static string GetJob(string Alias)
	{
		if (string.IsNullOrEmpty(Alias)) return null;
		else if (Alias.MyContains("RynSword") || Alias.Contains("SW")) return "灵剑";
		else if (Alias.MyContains("GreatSword") || Alias.Contains("WA")) return "斗士";
		else if (Alias.MyContains("SoulGauntlet") || Alias.Contains("SF")) return "气宗";
		else if (Alias.MyContains("WarDagger") || Alias.Contains("WL")) return "咒术";
		else if (Alias.MyContains("_sw_") || Alias.MyContains("Sword") || Alias.Contains("BM_")) return "剑士";
		else if (Alias.MyContains("_gt_") || Alias.MyContains("Gauntlet") || Alias.Contains("KF")) return "拳师";
		else if (Alias.MyContains("_st_") || Alias.MyContains("Staff") || Alias.Contains("SU")) return "召唤";
		else if (Alias.MyContains("_ab_") || Alias.MyContains("Aura-bangle") || Alias.Contains("FM")) return "气功";
		else if (Alias.MyContains("_ta_") || Alias.MyContains("Axe") || Alias.Contains("DE")) return "力士";
		else if (Alias.MyContains("_dg_") || Alias.MyContains("Dagger") || Alias.Contains("AS")) return "刺客";
		else if (Alias.MyContains("Gun_") || Alias.Contains("PT")) return "枪手";
		else if (Alias.MyContains("LongBow") || Alias.Contains("AR")) return "弓手";
		else if (Alias.MyContains("Orb")) return "星术师";
		else if (Alias.MyContains("DualBlade")) return "双剑";
		else if (Alias.MyContains("Harp")) return "乐师";
		else if (Alias.MyContains("Spear")) return "矛手";

		return null;
	}

	public static string GetEquipGem(string Alias)
	{
		if (Alias.MyContains("Gam1")) return " ☵1";
		else if (Alias.MyContains("Gan2")) return " ☳2";
		else if (Alias.MyContains("Gin3")) return " ☶3";
		else if (Alias.MyContains("Son4")) return " ☱4";
		else if (Alias.MyContains("Lee5")) return " ☲5";
		else if (Alias.MyContains("Gon6")) return " ☷6";
		else if (Alias.MyContains("Tae7")) return " ☴7";
		else if (Alias.MyContains("Gun8")) return " ☰8";
		else if (Alias.MyContains("EquipGem_None")) return " ☰8";

		return null;
	}
	#endregion
}