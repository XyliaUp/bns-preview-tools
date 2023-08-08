using Xylia.Windows.CustomException;

namespace Xylia.Preview.Data.Models.DatData.DatDetect;
public class DataDetector
{
	#region Fields
	public Locale Locale;

	public BNSDat XmlData;

	public BNSDat LocalData;

	public BNSDat ConfigData;


	public bool is64Bit = true;
	#endregion


	public DataDetector(string FolderPath, ResultMode mode = ResultMode.SelectDat)
	{
		if (string.IsNullOrWhiteSpace(FolderPath) || !Directory.Exists(FolderPath))
			throw new Exception("game folder is invalid, please to set.");

		this.Locale = new Locale(new DirectoryInfo(FolderPath));

		// *
		var dataPathes = new DataCollection(FolderPath);
		var xmls = dataPathes.GetFiles(DatType.xml, mode);
		var locals = dataPathes.GetFiles(DatType.local, mode);
		var configs = dataPathes.GetFiles(DatType.config, mode);

		//*
		if (xmls.Count == 1)
		{
			XmlData = new(xmls[0].FullName);
			is64Bit = XmlData.Bit64;
		}
		if (locals.Count == 1) LocalData = new(locals[0].FullName);
		if (configs.Count == 1) ConfigData = new(configs[0].FullName);


		if (XmlData is null || LocalData is null)
		{
			using var select = new DatSelect(xmls, locals);
			if (select.ShowDialog() != DialogResult.OK)
				throw new UserExitException();

			is64Bit = select.Chk_64bit.Checked;
			XmlData = new(select.XML_Select);
			LocalData = new(select.Local_Select);
		}
	}
}