using System;
using System.IO;
using System.Windows.Forms;

using Xylia.Windows.CustomException;

namespace Xylia.Preview.Data.Models.DatData.DatDetect
{
	public class GetDataPath
	{
		public string TargetLocal;

		public string TargetXml;

		public string TargetConfig;


		public GetDataPath(string FolderPath, bool? is64 = null, bool? SelectBin = false)
		{
			if (string.IsNullOrWhiteSpace(FolderPath) || !Directory.Exists(FolderPath))
				throw new Exception("游戏目录未设置或并不存在，请先设置");


			var dataPathes = new DataPathes(FolderPath);
			var xmls = dataPathes.GetFiles(DatType.xml, SelectBin);
			var locals = dataPathes.GetFiles(DatType.local, SelectBin);
			var configs = dataPathes.GetFiles(DatType.config, SelectBin);

			if (xmls.Count == 1) TargetXml = xmls[0].FullName;
			if (locals.Count == 1) TargetLocal = locals[0].FullName;
			if (configs.Count == 1) TargetConfig = configs[0].FullName;


			if (TargetXml is null || TargetLocal is null)
			{
				using var select = new DatSelect(xmls, locals);
				if (select.ShowDialog() != DialogResult.OK)
					throw new UserExitException();

				TargetXml = select.XML_Select;
				TargetLocal = select.Local_Select;
			}
		}
	}
}