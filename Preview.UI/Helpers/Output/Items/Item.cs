using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Text;

using OfficeOpenXml;
using Xylia.Preview.Data.Client;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Helpers.Output;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Data.Models.Sequence;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Helpers.Output.Items;
public sealed class ItemOut : OutSet, IDisposable
{
	#region Path
	public string Path_ItemList;
	public string Path_Failure;
	public string Path_MainFile;
	#endregion

	#region Cache
	public bool OnlyUpdate;

	private HashSet<int> CacheList = null;

	public void LoadCache(string path)
	{
		if (!OnlyUpdate) return;

		CacheList = XList.LoadData(path);
	}
	#endregion


	#region Output
	public void Start(DateTime startTime, bool UseExcel)
	{
		var time = CreatedAt ?? startTime;
		var outdir = Path.Combine(
			UserSettings.Default.OutputFolder, "output", "item",
			time.ToString("yyyyMM"),
			time.ToString("dd HHmm"));

		Directory.CreateDirectory(outdir);
		Path_ItemList = Path.Combine(outdir, $@"{time:yyyy-MM-dd HH-mm}.chv");
		Path_Failure = Path.Combine(outdir, @"no_text.txt");
		Path_MainFile = Path.Combine(outdir, @"output." + (UseExcel ? "xlsx" : "txt"));


		XList cache = new() { TimeStamp = CreatedAt?.Ticks ?? 0 };
		if (CacheList != null) cache.datas.AddRange(CacheList);
		cache.datas.AddRange(ItemDatas.Select(item => item.Ref.Id));
		cache.Save(Path_ItemList);


		#region Output
		if (!UseExcel) CreateText();
		else
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			using var package = new ExcelPackage();
			var sheet = package.Workbook.Worksheets.Add("item");

			CreateData(sheet);
			package.SaveAs(Path_MainFile);
		}

		var Failures = ItemDatas.Where(o => o.Name2 is null);
		if (Failures.Any())
		{
			using StreamWriter Out_Failure = new(Path_Failure);
			foreach (var Item in Failures.OrderBy(o => o.Ref.Id))
				Out_Failure.WriteLine($"{Item.Ref.Id,-15} {Item.Alias}");
		}
		#endregion
	}


	protected override void CreateData(ExcelWorksheet sheet)
	{
		int Row = 1;
		int index = 1;
		sheet.SetColumn(index++, "id", 10);
		sheet.SetColumn(index++, "key", 12);
		sheet.SetColumn(index++, "name", 30);
		sheet.SetColumn(index++, "alias", 30);
		sheet.SetColumn(index++, "job", 20);
		sheet.SetColumn(index++, "desc", 80);
		sheet.SetColumn(index++, "info", 80);

		foreach (var Item in ItemDatas)
		{
			Row++;
			int column = 1;

			sheet.Cells[Row, column++].SetValue(Item.Ref.ToString());
			sheet.Cells[Row, column++].SetValue(Item.Key);
			sheet.Cells[Row, column++].SetValue(Item.Name2);
			sheet.Cells[Row, column++].SetValue(Item.Alias);
			sheet.Cells[Row, column++].SetValue(Item.Job.GetText());
			sheet.Cells[Row, column++].SetValue(Item.Description);
			sheet.Cells[Row, column++].SetValue(Item.Info);
		}
	}

	protected override void CreateText()
	{
		using var writer = new StreamWriter(new FileStream(Path_MainFile, FileMode.Create));
		foreach (var Item in ItemDatas)
		{
			writer.Write("{0,-10}", Item.Ref.Id);
			writer.Write("{0,-60}", "alias: " + Item.Alias);
			writer.Write("{0,-0}", "name: " + Item.Name2);
			writer.WriteLine();
		}
	}
	#endregion


	#region Data
	DateTime? CreatedAt;

	List<ItemSimple> ItemDatas;

	public int Count => ItemDatas?.Count ?? 0;

	public void GetData()
	{
		BnsDatabase set = new(DefaultProvider.Load(UserSettings.Default.GameFolder));
		CreatedAt = set.Provider.CreatedAt;

		var Result = new BlockingCollection<ItemSimple>();
		Parallel.ForEach(set.Item.Records, (record) =>
		{
			if (CacheList != null && CacheList.Contains(record.RecordId)) return;

			var data = new ItemSimple(record, set);
			Result.Add(data);
		});

		set.Item.Dispose();
		set.Text.Dispose();
		set.Dispose();

		ItemDatas = Result.OrderBy(o => o.Ref.Id).ToList();
	}

	public void Dispose()
	{
		ItemDatas?.Clear();
		ItemDatas = null;
		CacheList = null;

		GC.Collect();
	}
	#endregion
}

class ItemSimple
{
	#region Field
	public Ref Ref;
	public string Alias;
	public string Name2;
	public string Description;
	public string Info;
	public JobSeq Job;

	public string Key => Convert.ToBase64String(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Ref.Id)));

	public ItemSimple(Record record, BnsDatabase tables = null)
	{
		Ref = record.Ref;
		Alias = record.StringLookup.GetString(0);

		var TextTable = tables?.Get<Text>();
		Name2 = GetName2(TextTable);
		Info = GetInfo(TextTable);
		Description = GetDesc(TextTable);
		Job = GetJob(Alias);
	}
	#endregion


	#region Text
	private string GetName2(ModelTable<Text> text)
	{
		string Text = text?[$"Item.Name2.{Alias}"]?.text;

		// replace rule when error status
		if (Text is null)
		{

		}

		return Text is null ? null : Text + GetEquipGem(Alias);
	}

	private string GetDesc(ModelTable<Text> text)
	{
		string Text = null;
		Text += text?[$"Item.Desc2.{Alias}"]?.text;
		Text += text?[$"Item.Desc5.{Alias}"]?.text;

		return BNS_Cut(Text);
	}

	private string GetInfo(ModelTable<Text> text)
	{
		string Text = null;
		Text += text?[$"Item.MainInfo.{Alias}"]?.text;
		Text += text?[$"Item.IdentifyMain.{Alias}"]?.text;
		Text += text?[$"Item.IdentifySub.{Alias}"]?.text;

		return BNS_Cut(Text);
	}


	public static string BNS_Cut(string text)
	{
		if (string.IsNullOrWhiteSpace(text)) return null;

		StringBuilder builder = new();

		var doc = new HtmlAgilityPack.HtmlDocument();
		doc.LoadHtml(text);

		foreach (var node in doc.DocumentNode.ChildNodes)
		{
			switch (node.Name)
			{
				case "#text": builder.Append(node.InnerText); break;
				case "image":
				{
					var ImagesetPath = node.Attributes["imagesetpath"]?.Value;

					builder.Append($"[{ImagesetPath}]");
					break;
				}

				default: builder.Append(node.InnerHtml); break;
			}
		}

		return builder.ToString();
	}

	public static JobSeq GetJob(string alias)
	{
		alias = alias?.ToLower();

		if (string.IsNullOrEmpty(alias)) return JobSeq.JobNone;
		else if (alias.Contains("RynSword") || alias.Contains("SW")) return JobSeq.귀검사;
		else if (alias.Contains("GreatSword") || alias.Contains("WA")) return JobSeq.투사;
		else if (alias.Contains("SoulGauntlet") || alias.Contains("SF")) return JobSeq.기권사;
		else if (alias.Contains("WarDagger") || alias.Contains("WL")) return JobSeq.주술사;
		else if (alias.Contains("_sw_") || alias.Contains("Sword") || alias.Contains("BM_")) return JobSeq.검사;
		else if (alias.Contains("_gt_") || alias.Contains("Gauntlet") || alias.Contains("KF")) return JobSeq.권사;
		else if (alias.Contains("_st_") || alias.Contains("Staff") || alias.Contains("SU")) return JobSeq.소환사;
		else if (alias.Contains("_ab_") || alias.Contains("Aura-bangle") || alias.Contains("FM")) return JobSeq.기공사;
		else if (alias.Contains("_ta_") || alias.Contains("Axe") || alias.Contains("DE")) return JobSeq.역사;
		else if (alias.Contains("_dg_") || alias.Contains("Dagger") || alias.Contains("AS")) return JobSeq.암살자;
		else if (alias.Contains("Gun_") || alias.Contains("PT")) return JobSeq.격사;
		else if (alias.Contains("LongBow") || alias.Contains("AR")) return JobSeq.궁사;
		else if (alias.Contains("Orb")) return JobSeq.뇌전술사;
		else if (alias.Contains("DualBlade")) return JobSeq.쌍검사;
		else if (alias.Contains("Harp")) return JobSeq.악사;
		else if (alias.Contains("Spear")) return JobSeq.궁사;

		return JobSeq.JobNone;
	}

	public static string GetEquipGem(string alias)
	{
		alias = alias?.ToLower();
		if (alias.Contains("Gam1")) return " ☵1";
		else if (alias.Contains("Gan2")) return " ☳2";
		else if (alias.Contains("Gin3")) return " ☶3";
		else if (alias.Contains("Son4")) return " ☱4";
		else if (alias.Contains("Lee5")) return " ☲5";
		else if (alias.Contains("Gon6")) return " ☷6";
		else if (alias.Contains("Tae7")) return " ☴7";
		else if (alias.Contains("Gun8")) return " ☰8";
		else if (alias.Contains("EquipGem_None")) return " ☰8";

		return null;
	}
	#endregion
}