using System.ComponentModel;
using CUE4Parse.Utils;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.Data.Engine.DatData;
public class DefaultProvider : Datafile, IDataProvider
{
	#region Fields
	public BNSDat XmlData;
	public BNSDat LocalData;
	public BNSDat ConfigData;
	public ITableParseType Detect;

	public virtual string Name { get; protected set; }
	public Locale Locale { get; protected set; }
	#endregion


	#region Methods
	public virtual Stream[] GetFiles(string pattern) => this.XmlData.EnumerateFiles(pattern).Select(x => new MemoryStream(x.Data)).ToArray();

	public virtual void LoadData(DatafileDefinition definitions)
	{
		#region Tables
		this.Tables = [];

		//ReadFrom(File.ReadAllBytes("C:\\Users\\10565\\Desktop\\datafile.bin"), true);
		ReadFrom(XmlData.EnumerateFiles(Is64Bit ? "datafile64.bin" : "datafile.bin").FirstOrDefault()?.Data, Is64Bit);
		ReadFrom(LocalData.EnumerateFiles(Is64Bit ? "localfile64.bin" : "localfile.bin").FirstOrDefault()?.Data, Is64Bit);

		Tables.Add(new() { Name = "quest", XmlPath = @"quest\questdata*.xml" });
		Tables.Add(new() { Name = "contextscript", XmlPath = @"skill3_contextscriptdata*.xml" });
		Tables.Add(new() { Name = "skill-training-sequence", XmlPath = @"skilltrainingsequencedata*.xml" });
		Tables.Add(new() { Name = "summoned-sequence", XmlPath = @"summonedsequencedata*.xml" });
		Tables.Add(new() { Name = "tutorialskillsequence", XmlPath = @"tutorialskillsequencedata*.xml" });

		// surveyquestions
		#endregion

		#region ParseType
		// Actually, it is directly defined in the game program, but we cannot get it.
		if (definitions.HasHeader) Detect = new DatafileDirect(definitions.Header);
		else Detect = new DatafileDetect(this);

		Detect?.ParseType(definitions);
		#endregion
	}

	public virtual void WriteData(string folder, bool is64bit)
	{
		// Rebuild alias map when full build
		bool FullBuild = !this.Tables.Any(x => x.Definition.IsDefault);
		if (FullBuild && false)
		{
			Serilog.Log.Information("Rebuilding alias map");
			var rebuilder = new NameTable.Rebuilder(this.NameTable);
			foreach (var table in this.Tables)
			{
				// get alias definition
				var aliasAttrDef = table.Definition.ElRecord["alias"];
				if (aliasAttrDef == null) continue;

				var tablePrefix = table.Name.ToLowerInvariant() + ":";
				foreach (var record in table.Records)
				{
					var alias = record.Attributes["alias"];
					if (alias == null) continue;

					rebuilder.AddAliasManually(tablePrefix + alias.ToLowerInvariant(), record.Ref);
				}
			}

			rebuilder.EndRebuilding();
		}


		// UserCommand remove at UE4
		var raw = this.Tables.Where(x => x.XmlPath != null);
		var local = this.Tables.Where(x => x.Name == "petition-faq-list" || x.Name == "survey" || x.Name == "text" || (true && x.Name == "user-command"));
		var xml = this.Tables.Except(local).Except(raw);

		File.WriteAllBytes(Path.Combine(folder, Is64Bit ? "datafile64.bin" : "datafile.bin"), WriteTo([.. xml], is64bit));
		File.WriteAllBytes(Path.Combine(folder, Is64Bit ? "localfile64.bin" : "localfile.bin"), WriteTo([.. local], is64bit));
	}

	public virtual void Dispose()
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


	#region Static Methods
	public static DefaultProvider Load(string FolderPath, ResultMode mode = ResultMode.SelectDat)
	{
		if (string.IsNullOrWhiteSpace(FolderPath) || !Directory.Exists(FolderPath))
			throw new WarningException("invalid game folder, please to set.");

		//get all
		var datas = new DataCollection(FolderPath);
		var xmls = datas.GetFiles(DatType.xml, mode);
		var locals = datas.GetFiles(DatType.local, mode);
		var configs = datas.GetFiles(DatType.config, mode);

		//get target
		DefaultProvider provider;
		if (xmls.Count == 0) throw new WarningException("invalid game data, possible specified directory incorrect");
		if (xmls.Count == 1 && locals.Count <= 1)
		{
			provider = new DefaultProvider() { XmlData = xmls.FirstOrDefault(), LocalData = locals.FirstOrDefault() };
		}
		else provider = IDatSelect.Default.Show(xmls, locals);

		// set info
		provider.Name = FolderPath.SubstringAfterLast('\\');
		provider.Is64Bit = provider.XmlData.Bit64;
		provider.Locale = new Locale(new DirectoryInfo(FolderPath));
		provider.ConfigData = configs.FirstOrDefault();

		return provider;
	}
	#endregion
}