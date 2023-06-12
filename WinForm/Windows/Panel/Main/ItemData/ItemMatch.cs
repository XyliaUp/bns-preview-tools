using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Xylia.Extension;
using Xylia.Match.Util.ItemData;
using Xylia.Match.Util.ItemMatch.Util;
using Xylia.Preview.Data;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Attributes;
using Xylia.Preview.Properties;
using Xylia.Workbook;

namespace Xylia.Match.Util.ItemList
{
	public sealed class ItemMatch
	{
		#region Constructor
		private readonly Action<string> GetOutput = null;

		public ItemMatch(Action<string> action)
		{
			Application.DoEvents();
			this.GetOutput = action;
		}
		#endregion

		#region Cache
		/// <summary>
		/// 只输出新数据
		/// </summary>
		public bool Chk_OnlyNew;

		private HashSet<int> CacheList = null;

		public void LoadCache(string Path)
		{
			if (!this.Chk_OnlyNew) return;

			this.CacheList = XList.LoadData(Path, this.GetOutput);
		}
		#endregion

		#region Data
		DateTime CreatedAt;

		List<ItemDataInfo> ItemDatas;

		BlockingCollection<ItemDataInfo> Failures;


		public bool GetData()
		{
			#region Initialize
			DataTableSet set = new();
			set.LoadData();

			CreatedAt = set.CreatedAt;


			set.Item.TryLoad();
			set.TextData.TryLoad();

			var InfoGet = new InfoGet(set);
			this.Failures = new();
			#endregion

			#region Data
			var Result = new BlockingCollection<ItemDataInfo>();
			Parallel.ForEach(set.Item, item =>
			{
				var record = ((DbData)item.Attributes).record;

				var MainID = record.RecordId;
				if (CacheList != null && CacheList.Contains(MainID)) return;

				var alias = record.StringLookup.GetString(0).Replace("SEW_", "GB_");
				var name2 = InfoGet.GetName2(alias);
				var data = new ItemDataInfo()
				{
					id = MainID,
					Alias = alias,

					Name2 = name2,
					Info = InfoGet.GetInfo(alias),
					Desc = InfoGet.GetDesc(alias),
					Job = InfoGet.GetJob(alias),
				};

				Result.Add(data);
				if (name2 is null) Failures.Add(data);
			});
			#endregion


			set = null;
			this.ItemDatas = Result.OrderBy(o => o.id).ToList();
			return this.ItemDatas.Any();
		}
		#endregion


		#region Output
		public static string OUTDIR => Path.Combine(CommonPath.OutputFolder, "output");
		public static string OUTTIME(DateTime? dt = null) => $"{dt ?? DateTime.Now:yyyy年M月/d日 HH时mm分}"
			.Replace("/", "\\");



		public void Start(DateTime StartTime, bool UseExcel)
		{
			var time = CreatedAt == default ? StartTime : CreatedAt;
			var outdir = Path.Combine(OUTDIR, $@"item\{OUTTIME(time)}");
			Directory.CreateDirectory(outdir);


			FilePath File = new()
			{
				CacheList = Path.Combine(outdir, $@"{time:yyyy-MM-dd HH-mm}.chv"),
				Failure = Path.Combine(outdir, @"未汉化道具.txt"),
				PlainTXT = Path.Combine(outdir, @"output." + (UseExcel ? "xlsx" : "txt"))
			};


			XList cache = new();
			cache.DataTime = CreatedAt.GetTimeStamp();

			if (CacheList != null) cache.datas.AddRange(CacheList);
			cache.datas.AddRange(this.ItemDatas.Select(item => item.id));
			cache.Save(File.CacheList);



			#region Output
			if (UseExcel) CreateExcel(File, this.ItemDatas);
			else CreateText(File, this.ItemDatas);

			Application.DoEvents();

			if (this.Failures.Any())
			{
				using StreamWriter Out_Failure = new(File.Failure);
				foreach (var Item in this.Failures) Out_Failure.WriteLine($"{Item.id,-20}   {Item.Alias}");
			}
			#endregion

			#region End
			TimeSpan ts = DateTime.Now - StartTime;
			GetOutput($"本次拉取数据共计{this.ItemDatas.Count}条, 总耗{ts.Minutes}分{ts.Seconds}秒。");

			this.ItemDatas.Clear();
			this.ItemDatas = null;

			this.Failures = null;
			#endregion
		}




		private static void CreateExcel(FilePath File, IEnumerable<ItemDataInfo> Info)
		{
			#region Title
			var info = new ExcelInfo("道具数据");
			info.SetColumn("id", 10);
			info.SetColumn("key", 12);
			info.SetColumn("名称", 30);
			info.SetColumn("标识", 30);
			info.SetColumn("职业", 20);
			info.SetColumn("描述", 80);
			info.SetColumn("信息", 80);
			#endregion

			foreach (var Item in Info)
			{
				var row = info.CreateRow();
				row.AddCell(Item.id);
				row.AddCell(Item.Key);

				row.AddCell(Item.Name2);
				row.AddCell(Item.Alias);
				row.AddCell(Item.Job);
				row.AddCell(Item.Desc);
				row.AddCell(Item.Info);
			}

			info.Save(File.PlainTXT);
		}

		private static void CreateText(FilePath File, IEnumerable<ItemDataInfo> Info)
		{
			using var Out_Main = new StreamWriter(new FileStream(File.PlainTXT, FileMode.Create));

			foreach (var Item in Info)
			{
				string Message = null;
				string IdTxt = $"{Item.id,-15}";
				if (IdTxt.Length > 15) IdTxt += "    ";


				if (Item.Name2 is null) Message = $"{IdTxt}  暂无汉化 ({Item.Alias})";
				else
				{
					string ResultTxt = $"{Item.Name2,-20}";
					if (ResultTxt.Length > 15) ResultTxt += "    ";

					Message = $"{IdTxt}{ResultTxt}{"别名：" + Item.Alias}";
				}

				#region info
				var ExtraInfo = new List<KeyValuePair<string, string>>()
				{
					 new KeyValuePair<string, string>("职业" ,Item.Job),
					 new KeyValuePair<string, string>("描述" ,Item.Desc),
					 new KeyValuePair<string, string>("信息" ,Item.Info),
				};

				foreach (var t in ExtraInfo.Where(t => !string.IsNullOrWhiteSpace(t.Value)))
				{
					Message += $"\t{t.Key}：{t.Value}";
				}
				#endregion

				Out_Main.WriteLine(Message.Replace("<br>", "\r\n"));
			}
		}
		#endregion
	}
}