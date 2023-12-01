using CUE4Parse.Utils;

using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Engine.BinData.Serialization;
using Xylia.Preview.Data.Engine.Readers;

namespace Xylia.Preview.Data.Engine.DatData;
public class DefaultProvider : Datafile, IDataProvider
{
	#region Fields
	public BNSDat XmlData;
	public BNSDat LocalData;
	public BNSDat ConfigData;
	public DatafileDetect Detect;

	public string Name { get; protected set; }
	public Locale Locale { get; protected set; }
	#endregion


	#region Methods
	public virtual Stream[] GetFiles(string pattern) => this.XmlData.EnumerateFiles(pattern).Select(x => new MemoryStream(x.Data)).ToArray();

	public virtual void LoadData(List<TableDefinition> definitions)
	{
		this.Tables = new();

		#region bin
		ReadFrom(XmlData.EnumerateFiles(is64Bit ? "datafile64.bin" : $"datafile.bin").FirstOrDefault()?.Data, is64Bit);
		ReadFrom(LocalData.EnumerateFiles(is64Bit ? "localfile64.bin" : "localfile.bin").FirstOrDefault()?.Data, is64Bit);
		#endregion

		#region xml
		Tables.Add(new() { Name = "quest", XmlPath = @"quest\questdata*.xml" });
		Tables.Add(new() { Name = "contextscript", XmlPath = @"skill3_contextscriptdata*.xml" });
		Tables.Add(new() { Name = "skilltrainingsequence", XmlPath = @"skilltrainingsequencedata*.xml" });
		Tables.Add(new() { Name = "summoned-sequence", XmlPath = @"summonedsequencedata*.xml" });
		Tables.Add(new() { Name = "tutorialskillsequence", XmlPath = @"tutorialskillsequencedata*.xml" });
		#endregion


		#region detect
		// auto detect type
		// Actually, it is directly defined in the game program, but we cannot get it.
		Detect = new DatafileDetect();
		Detect.Read(this);

		if (definitions != null) Detect.Convert(definitions);
		#endregion
	}

	public void Dispose()
	{
		Locale = null;

		XmlData?.Dispose();
		LocalData?.Dispose();
		ConfigData?.Dispose();
		XmlData = LocalData = ConfigData = null;

		Tables?.ForEach(x => x.Dispose());
		Tables?.Clear();
		Tables = null;

		GC.SuppressFinalize(this);
	}
	#endregion

	#region Protected Methods
	protected void ReadFrom(byte[] bytes, bool _is64bit)
	{
		using var reader = new DatafileArchive(bytes);

		var bin = new DatafileHeader();
		bin.ReadHeaderFrom(reader, _is64bit);

		if (bin.ReadTableCount > 10)
		{
			this.DatafileVersion = bin.DatafileVersion;
			this.ClientVersion = bin.ClientVersion;
			this.CreatedAt = bin.CreatedAt;

			var _nameTableReader = new NameTableReader(is64Bit: _is64bit) { };
			this.NameTable = _nameTableReader.ReadFrom(reader);
		}

		for (var tableId = 0; tableId < bin.ReadTableCount; tableId++)
		{
			this.Tables.Add(TableArchive.LazyLoad(reader, _is64bit));
		}
	}
	#endregion


	#region Static Methods
	public static DefaultProvider Load(string FolderPath, ResultMode mode = ResultMode.SelectDat)
	{
		if (string.IsNullOrWhiteSpace(FolderPath) || !Directory.Exists(FolderPath))
			throw new Exception("invalid game folder, please to set.");

		//get all
		var datas = new DataCollection(FolderPath);
		var xmls = datas.GetFiles(DatType.xml, mode);
		var locals = datas.GetFiles(DatType.local, mode);
		var configs = datas.GetFiles(DatType.config, mode);

		//get target
		DefaultProvider provider;
		if (xmls.Count == 0) throw new Exception("invalid game data, possible specified directory incorrect");
		if (xmls.Count == 1 && locals.Count <= 1)
		{
			provider = new DefaultProvider() { XmlData = xmls.FirstOrDefault(), LocalData = locals.FirstOrDefault() };
		}
		else provider = IDatSelect.Default.Show(xmls, locals);

		// set info
		provider.Name = FolderPath.SubstringAfterLast('\\');
		provider.is64Bit = provider.XmlData.Bit64;
		provider.Locale = new Locale(new DirectoryInfo(FolderPath));
		provider.ConfigData = configs.FirstOrDefault();

		return provider;
	}
	#endregion
}