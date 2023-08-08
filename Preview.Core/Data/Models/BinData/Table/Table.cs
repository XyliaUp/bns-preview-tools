using System.Collections;
using System.Configuration;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

using BnsBinTool.Core;
using BnsBinTool.Core.DataStructs;
using BnsBinTool.Core.Definitions;
using BnsBinTool.Core.Helpers;

using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record.Attributes;
using Xylia.Preview.Data.Models.DatData.DataProvider;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Properties;
using Xylia.Xml;

using RecordModel = BnsBinTool.Core.Models.Record;
using TableModel = BnsBinTool.Core.Models.Table;


namespace Xylia.Preview.Data.Models.BinData.Table;
public class Table<T> : IEnumerable<T>, ITable where T : BaseRecord, new()
{
	#region Constructor
	public string Name { get; set; }
	public TableSet Owner{ get; set; }
	public TableDefinition TableDef { get; set; }


	public IDataProvider DataProvider;

	public string XmlDataPath;
	#endregion

	#region Data
	protected readonly Dictionary<Ref, Lazy<T>> ByRef = new();

	protected readonly Dictionary<string, Ref> ByAlias = new(StringComparer.OrdinalIgnoreCase);

	protected void AddAlias(string Alias, Ref Ref)
	{
		if (Alias is not null && !this.ByAlias.ContainsKey(Alias))
			this.ByAlias[Alias] = Ref;
	}


	protected readonly Dictionary<short, Type> SubType = new();


	private Lazy<T>[] _data;

	public Lazy<T>[] Data
	{
		private set => this._data = value;
		get
		{
			this.Load();
			return this._data;
		}
	}
	#endregion


	#region Load Methods
	private bool InLoading = false;

	public void TryLoad() => this.Load();

	private void Load(bool Reload = false)
	{
		#region Initialize
		if (this.InLoading)
		{
			while (this.InLoading) Thread.Sleep(100);
			return;
		}

		if (this._data != null && !Reload) return;
		if (Program.IsDesignMode) return;


		this.InLoading = true;
		this._data = null;
		#endregion


		ArgumentNullException.ThrowIfNull(TableDef);

		short typeIndex = 0;
		foreach (var itemType in Assembly.GetExecutingAssembly().GetTypes())
		{
			if (!itemType.IsAbstract && typeof(T).IsAssignableFrom(itemType) && itemType != typeof(T))
			{
				SubType[typeIndex++] = itemType;
			}
		}


		lock (this)
		{
			var task = new Task(() =>
			{
				try
				{
					if (DataProvider is null && !Program.IsDebugMode) this.LoadData();
					else this.LoadXml();

					Trace.WriteLine($"[{DateTime.Now}] load table `{Name}` successful ({this._data.Length})");
				}
				catch (ConfigurationErrorsException)
				{
					throw;
				}
				catch (Exception ex)
				{
					this._data = Array.Empty<Lazy<T>>();
					Trace.WriteLine($"[{DateTime.Now}] load table `{Name}` failed: {ex}");
				}
				finally
				{
					InLoading = false;
				}
			});

			task.Start();
			task.Wait();
		}
	}

	private void LoadXml()
	{
		//get table
		var Files = (DataProvider ?? this.Owner.Provider).GetFiles(XmlDataPath ?? $"{Name}Data*");
		if (Files is null || !Files.Any()) throw new FileNotFoundException();

		LoadXml(Files.Select(o => o.FullName.GetXmlDocument()));
	}

	private void LoadXml(IEnumerable<XmlDocument> xmls)
	{
		var keys = TableDef.ExpandedAttributes.Where(attr => attr.IsKey);
		var builder = new RecordBuilder(null, null);

		var aliasAttrDef = TableDef["alias"];


		List<XmlElement> tables = new();
		foreach (var xml in xmls)
		{
			var root = xml.DocumentElement;
			tables.AddRange(root.SelectNodes($"./" + TableDef.Els[1].Name).OfType<XmlElement>());
		}


		this._data = new Lazy<T>[tables.Count];
		for (var x = 0; x < this._data.Length; x++)
		{
			var element = tables[x];

			// build ref
			var record = new RecordModel { Data = new byte[16] };
			if (TableDef.AutoKey) record.RecordId = x + 1;
			else keys.ForEach(attr => builder.SetAttribute(record, attr, element.Attributes[attr.Name]?.Value));


			// add
			var Ref = record.RecordRef;
			if (aliasAttrDef != null) AddAlias(element.Attributes["alias"]?.Value, Ref);

			this.ByRef[Ref] = this._data[x] = new(() =>
			{
				var Object = new T();
				Object.Ref = Ref;
				//Object.Type = ;
				Object.LoadData(element);

				return Object;
			});
		}
	}

	private void LoadData()
	{
		lock (Owner) Owner.LoadData(XmlDataPath is null);


		if (XmlDataPath is null)
		{
			//get table
			if (this.TableDef.Type == 0) throw new FileNotFoundException();

			var table = Owner.Tables.FirstOrDefault(table => table.Type == this.TableDef.Type);
			TableDefinitionEx.CheckVersion(TableDef, table);
			TableDefinitionEx.CheckSize(TableDef, table, (msg) => Debug.WriteLine(msg));


#pragma warning disable IDE0150 
			if (Settings.TestMode == DumpMode.Used && this.Owner is TableSet)
				ProcessTable(CommonPath.DataFiles);
#pragma warning restore IDE0150 


			var aliasAttrDef = TableDef["alias"];

			this._data = new Lazy<T>[table.Records.Count];
			for (var x = 0; x < this._data.Length; x++)
			{
				var record = table.Records[x];
				if (record is null) continue;

				var _ref = record.RecordRef;
				if (aliasAttrDef != null) AddAlias(record.StringLookup.GetString(record.Get<int>(aliasAttrDef.Offset)), _ref);


				this.ByRef[_ref] = this._data[x] = new(() =>
				{
					T GetSubType()
					{
						if (SubType.TryGetValue(record.SubclassType, out var type))
							return (T)Activator.CreateInstance(type);

						return new T();
					}


					var Object = GetSubType();

					Object.Ref = _ref;
					Object.Type = record.SubclassType;
					Object.LoadData(new DbData(Owner.converter, this.TableDef, record));

					return Object;
				});
			}
		}
		else LoadXml((Owner.Provider as DefaultProvider).XmlData.EnumerateFiles(XmlDataPath).Select(o => o.Xml.Nodes));
	}
	#endregion

	#region Get Methods
	public T this[string Alias] => this.GetLazyInfo(Alias)?.Value;

	public T this[int Id, int Variant = 0] => this.GetLazyInfo(new Ref(Id, Variant))?.Value;

	public T this[BaseRecord resolvedRecord] => this[resolvedRecord?.alias];


	protected Lazy<T> GetLazyInfo(string Alias)
	{
		this.TryLoad();

		if (string.IsNullOrWhiteSpace(Alias)) return null;
		else if (this.ByAlias.TryGetValue(Alias, out var item)) return GetLazyInfo(item);
		else if (int.TryParse(Alias, out var MainID)) return GetLazyInfo(new Ref(MainID));
		else if (Alias.Contains('.'))
		{
			var o = Alias.Split('.');
			if (o.Length == 2 && int.TryParse(o[0], out var id) && int.TryParse(o[1], out var variant))
				return GetLazyInfo(new Ref(id, variant));
		}

		if (this._data != null && this._data.Length != 0
			&& typeof(T) != typeof(Text))
			Debug.WriteLine($"[{Name}] get failed, alias: {Alias}");
		return null;
	}

	protected Lazy<T> GetLazyInfo(Ref Ref)
	{
		this.TryLoad();

		if (Ref.Id <= 0) return null;
		else if (this.ByRef.TryGetValue(Ref, out var item)) return item;
		else if (this._data != null && this._data.Length != 0)
			Debug.WriteLine($"[{Name}] get failed, id: {Ref.Id} variation: {Ref.Variant}");

		return null;
	}
	#endregion


	#region Process Methods
	public void ProcessTable(string outputPath)
	{
		var table = Owner.Tables.FirstOrDefault(table => table.Type == this.TableDef.Type);
		if (table is null) return;

		Owner.converter.ProcessTable(table, this.TableDef, outputPath);
	}
	#endregion

	#region Serialize Methods
	public void Serialize(out TableModel table)
	{
		table = new TableModel();


		var builder = Owner.converter.Builder;
		builder.InitializeTable(isCompressed: false);

		// Clear old records
		table.Records.Clear();

		foreach (var record in this.Data)
		{
			table.Records.Add(record.Value.Serialize(this));
		}

		builder.FinalizeTable();
	}

	public XDocument Serialize(ReleaseSide side = default)
	{
		var doc = new XDocument();

		var table = new XElement("table");
		doc.Add(table);

		table.SetAttributeValue("release-module", "");
		table.SetAttributeValue("release-side", side.ToString().ToLower());
		table.SetAttributeValue("type", null);
		table.SetAttributeValue("version", $"");

		foreach (var record in this.Data)
		{
			table.Add(record.Value.Serialize(side));
		}


		return doc;
	}
	#endregion


	#region Interface
	object ITable.this[string Alias] => this[Alias];

	public void Clear()
	{
		this._data = null;

		this.ByRef.Clear();
		this.ByAlias.Clear();
	}

	public IEnumerator<T> GetEnumerator()
	{
		if (this.Data is null)
			yield break;

		foreach (var info in this.Data)
			yield return info.Value;

		yield break;
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion
}


public interface ITable : IEnumerable
{
	/// <summary>
	/// table name
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// bns data provider
	/// </summary>
	public TableSet Owner { get; set; }

	/// <summary>
	/// data struct definition
	/// </summary>
	public TableDefinition TableDef { get; set; }




	object this[string Alias] { get; }

	void Clear();
}