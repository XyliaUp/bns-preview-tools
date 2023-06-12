using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;

using NPOI.SS.UserModel;

using Xylia.Workbook;
using Xylia.Preview.Data.Record;

namespace Xylia.Match.Windows.Panel.TextInfo
{
	/// <summary>
	/// 文本信息载入器
	/// </summary>
	public sealed class TextLoader
	{
		public bool NewCreat = false;

		public List<Text> Data = new();


		public void LoadDirectory(string DirectoryPath = @"F:\Build\server\2021_版本库2\client\text")
		{
			if (string.IsNullOrWhiteSpace(DirectoryPath) || !Directory.Exists(DirectoryPath))
				return;

			NewCreat = true;
			LoadDirectory(new DirectoryInfo(DirectoryPath));
		}

		private void LoadDirectory(DirectoryInfo DirInfo)
		{
			foreach (var FileInfo in DirInfo.GetFiles()) Load(FileInfo);
			foreach (var SubDirInfo in DirInfo.GetDirectories()) LoadDirectory(SubDirInfo);
		}

		public void Load(FileInfo file)
		{
			switch (file.Extension)
			{
				case ".xls":
				case ".xlsx":
				{
					#region 获取目标工作表
					List<string> DataName = new();

					var Sheets = file.FullName.GetSheetName(out IWorkbook Workbook);
					if (Sheets.Length == 1) DataName.Add(Sheets[0]);
					else
					{
						throw new Exception("ListSelect 失效");

						//var listSelect = new ListSelect(Sheets);
						//listSelect.ShowDialog();

						//DataName = listSelect.Result;
					}

					if (DataName == null) throw new ArgumentNullException("未成功获取工作表");
					else if (DataName.Count == 0) throw new ArgumentNullException("请先选择工作表");
					#endregion

					foreach (var SheetName in DataName)
					{
						var dataDt = Workbook.ExcelToDataTable(SheetName);
						foreach (DataRow row in dataDt.Rows)
						{
							string Alias = row["A"].ToString();
							string Text = row["B"].ToString();
							if (string.IsNullOrWhiteSpace(Alias)) continue;

							this.Data.Add(new Text() { alias = Alias, text = Text });
						}
					}
				}
				break;


				case ".xml" or ".x16":
				{
					var document = new XmlDocument();
					document.Load(file.FullName);

					foreach (XmlElement Node in document.SelectNodes("table/record").OfType<XmlElement>())
					{
						string Alias = Node.Attributes["alias"]?.Value;
						string Text = Node.Attributes["text"]?.Value ?? Node.InnerXml;

						//由于解析库不会保留空格, 只能通过特殊Functions转换
						Text = Text.Replace("&#160;", " ").Replace("&#38;", "&");
						this.Data.Add(new Text() { alias = Alias, text = Text });
					}
				}
				break;

				default: throw new Exception("导入类型暂不支持");
			}
		}



		public void Test()
		{
			uint MaxID = this.Data.Max(t => t.TableIndex);
			foreach (var text in this.Data.Where(t => t.TableIndex == 0))
			{
				text.TableIndex = ++MaxID;

				//修改编号后必须刷新缓存哈希
				//this.ht_id.Add(text.id, text);
			}

			System.Diagnostics.Trace.WriteLine("总Load 数量：" + this.Data.Count);
		}
	}
}