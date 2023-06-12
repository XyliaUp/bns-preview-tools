using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

using Xylia.Configure;
using Xylia.CustomException;

namespace Xylia.Preview.Data.Models.DatData.DatDetect
{
	[Flags]
	public enum DatType
	{
		bit32 = 0x00000000,
		bit64 = 0x00000001,


		local = 0x10000000,
		local64 = local | bit64,

		xml = 0x20000000,
		xml64 = xml | bit64,

		config = 0x30000000,
		config64 = config | bit64,

	}

	public class DataPathes
	{
		#region Constructor
		private readonly Dictionary<DatType, List<FileInfo>> DataPathMenu;

		public DataPathes(string Folder)
		{
			DataPathMenu = new();
			InitPara(Folder);
		}
		#endregion

		#region Functions
		private void InitPara(string Folder)
		{
			List<FileInfo> files = new();

			var DirInfo = new DirectoryInfo(Folder);
			files.AddRange(DirInfo.GetFiles("*.dat", SearchOption.AllDirectories));
			files.AddRange(DirInfo.GetFiles("*.bin", SearchOption.AllDirectories));


			foreach (var file in files)
			{
				DatType datType = DatType.xml;
				switch (Path.GetFileNameWithoutExtension(file.Name).ToLower())
				{
					case "xml":
					case "datafile":
						datType = DatType.xml;
						break;

					case "xml64":
					case "datafile64":
						datType = DatType.xml64;
						break;

					case "config":
						datType = DatType.config; break;

					case "config64":
						datType = DatType.config64; break;

					case "local":
					case "localfile":
						datType = DatType.local; break;

					case "local64":
					case "localfile64":
						datType = DatType.local64; break;

					default: continue;
				}


				//add
				if (!DataPathMenu.ContainsKey(datType))
					DataPathMenu.Add(datType, new());

				DataPathMenu[datType].Add(file);
			}
		}

		/// <summary>
		/// 获取文件
		/// </summary>
		/// <param name="Type"></param>
		/// <param name="SelectBin"></param>
		/// <returns></returns>
		/// <exception cref="ReadException"></exception>
		public List<FileInfo> GetFiles(DatType Type, bool? SelectBin)
		{
			var result = new List<FileInfo>();
			if (Type == DatType.xml || Type == DatType.xml64)
			{
				if (DataPathMenu.ContainsKey(DatType.xml64)) result.AddRange(DataPathMenu[DatType.xml64]);
				if (DataPathMenu.ContainsKey(DatType.xml)) result.AddRange(DataPathMenu[DatType.xml]);
			}
			else if (Type == DatType.config || Type == DatType.config64)
			{
				if (DataPathMenu.ContainsKey(DatType.config64)) result.AddRange(DataPathMenu[DatType.config64]);
				if (DataPathMenu.ContainsKey(DatType.config)) result.AddRange(DataPathMenu[DatType.config]);
			}
			else if (Type == DatType.local || Type == DatType.local64)
			{
				if (DataPathMenu.ContainsKey(DatType.local64)) result.AddRange(DataPathMenu[DatType.local64]);
				if (DataPathMenu.ContainsKey(DatType.local)) result.AddRange(DataPathMenu[DatType.local]);
			}

			//True 只筛选bin, False 只筛选dat
			if (SelectBin == true) return result.Where(r => r.Extension == ".bin").ToList();
			if (SelectBin == false) return result.Where(r => r.Extension == ".dat").ToList();

			return result;
		}
		#endregion
	}


	public static partial class Extension
	{
		public static bool Judge64Bit(this FileInfo FileInfo) => FileInfo.Name.Judge64Bit();

		public static bool Judge64Bit(this string FilePath)
		{
			if (string.IsNullOrWhiteSpace(FilePath))
				return false;

			return Path.GetFileNameWithoutExtension(FilePath).Contains("64");
		}


		public static bool Has32bit(this IEnumerable<FileInfo> files) => files.FirstOrDefault(f => !f.Judge64Bit()) != null;

		public static bool Has64bit(this IEnumerable<FileInfo> files) => files.FirstOrDefault(f => f.Judge64Bit()) != null;


		public static IEnumerable<FileInfo> GetFiles(this IEnumerable<FileInfo> files, bool? is64 = null)
		{
			if (is64 is null) return files;
			else if (is64.Value) return files.Where(f => f.Judge64Bit());
			else return files.Where(f => !f.Judge64Bit());
		}




		public static string GetRegion(this string Folder)
		{
			var local = new DirectoryInfo(Folder).GetDirectories("Win64", SearchOption.AllDirectories).FirstOrDefault()?
				.GetFiles("local.ini").FirstOrDefault();
			if (local is not null)
			{
				var Publisher = Ini.ReadValue("Locale", "Publisher", local.FullName);
				var Language = Ini.ReadValue("Locale", "Language", local.FullName);
				var Universe = Ini.ReadValue("Locale", "Universe", local.FullName);

				return Language;
			}



			var temp = new DirectoryInfo(Folder).GetDirectories("Content", SearchOption.AllDirectories).FirstOrDefault()?
				.GetDirectories("local").FirstOrDefault()?
				.GetDirectories().FirstOrDefault();

			if (temp is not null)
			{
				var Publisher = temp.Name;
				var Language = temp.GetDirectories().Where(o => o.Name != "data").FirstOrDefault()?.Name;

				return Language;
			}

			return "unknown";
		}
	}
}