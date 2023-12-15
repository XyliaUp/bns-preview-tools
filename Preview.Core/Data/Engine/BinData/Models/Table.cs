using System.Data;
using System.Diagnostics;
using System.Xml;
using K4os.Hash.xxHash;
using Newtonsoft.Json;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Engine.Definitions;
using Xylia.Preview.Data.Engine.Readers;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Engine.BinData.Models;
/// <summary>
/// bns data table
/// </summary>
[JsonConverter(typeof(TableConverter))]
public class Table : TableHeader, IDisposable
{
	#region Constructor
	private TableDefinition definition;


	/// <summary>
	/// table name
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// table owner
	/// </summary>
	public IDataProvider Owner { get; set; }

	/// <summary>
	/// data struct definition
	/// </summary>
	public TableDefinition Definition
	{
		// create default def if null
		get => definition ??= TableDefinition.CreateDefault(this.Type);
		set
		{
			definition = value;
			this.CheckVersion(definition);
		}
	}


	public string XmlPath { get; set; }
	#endregion


	#region Data
	internal TableArchive Archive { get; set; }

	/// <summary>
	/// TODO: Hack because the table seems to offset it randomly?
	/// </summary>
	internal int RecordCountOffset { get; set; }

	/// <summary>
	/// TODO: Hack because idk where this padding is coming from
	/// </summary>
	internal byte[] Padding { get; set; }

	internal StringLookup GlobalString { get; set; }





	internal Dictionary<Ref, Record> ByRef = new();

	internal Dictionary<AttributeDefinition, Dictionary<string, Record[]>> ByRequired = new();


	/// <summary>
	/// the table index
	/// should use hashmap
	/// </summary>
	/// <param name="attribute"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public Record[] Search(AttributeDefinition attribute, string value)
	{
		if (attribute != null /*&& attribute.IsRequired*/)
		{
			lock (ByRequired)
			{
				// find group
				if (!ByRequired.TryGetValue(attribute, out var index))
				{
					var comparer = StringComparer.OrdinalIgnoreCase;
					index = _records
						.ToLookup(record => record.Attributes.Get(attribute.Name)?.ToString(), comparer)
						.Where(record => record.Key is not null)
						.ToDictionary(record => record.Key, record => record.ToArray(), comparer);

					ByRequired[attribute] = index;
				}

				// search item
				if (value != null && index.TryGetValue(value, out var result))
					return result;
			}
		}

		return [];
	}


	protected List<Record> _records;

	/// <summary>
	/// element collection
	/// </summary>
	public List<Record> Records
	{
		internal set => _records = value;
		get
		{
			LoadAsync().Wait();
			return _records;
		}
	}
	#endregion


	#region Load Methods
	public virtual Task LoadAsync() => Task.Run(() =>
	{
		lock (this)
		{
			if (_records != null) return;

			if (XmlPath is null) LoadData();
			else LoadXml(Owner.GetFiles(XmlPath));
		}
	});


	private void LoadData()
	{
		Archive.ReadFrom(this);
		Archive = null;

		foreach (var record in _records)
			ByRef[record.Ref] = record;
	}

	/// <summary>
	/// load data from xml
	/// </summary>
	/// <param name="streams"></param>
	/// <returns>data build actions</returns>
	public List<Action> LoadXml(params Stream[] streams)
	{
		this.Clear();

		var actions = new List<Action>();
		foreach (var stream in streams)
		{
			XmlDocument xml = new();
			xml.Load(stream);
			stream.Close();

			LoadElement(xml.DocumentElement, actions);
		}

		return actions;
	}

	internal void LoadElement(XmlElement parent, ICollection<Action> actions)
	{
		_records ??= [];	 

		var elements = parent.SelectNodes($"./" + Definition.ElRecord.Name).OfType<XmlElement>();
		foreach (var element in elements)
		{
			// get definition
			var definition = Definition.ElRecord.SubtableByName(element.GetAttribute(AttributeCollection.s_type));
			var record = new Record
			{
				Owner = this,
				Data = new byte[definition.Size],
				DataSize = definition.Size,
				XmlNodeType = 1,
				SubclassType = definition.SubclassType,
				StringLookup = IsCompressed ? new StringLookup() : GlobalString,
			};

			// create attributes
			record.Attributes = new(record, element, Definition.ElRecord, _records.Count + 1);
			record.Attributes.CreateData(definition , true);

			// create primary key
			_records.Add(record);
			ByRef[record.Ref] = record;

			// The ref is not determined at this time
			actions?.Add(new Action(() => record.Attributes.CreateData(definition)));
		}
	}
	#endregion



	#region Get Methods
	public Record this[string alias]
	{
		get
		{
			if (_records == null) LoadAsync().Wait();

			if (string.IsNullOrEmpty(alias)) return null;

			var objs = Search(Definition.ElRecord["alias"], alias);
			if (objs.Length > 0) return objs.FirstOrDefault();
			else if (int.TryParse(alias, out var MainID)) return this[new Ref(MainID)];
			else if (alias.Contains('.'))
			{
				var o = alias.Split('.');
				if (o.Length == 2 && int.TryParse(o[0], out var id) && int.TryParse(o[1], out var variant))
					return this[new Ref(id, variant)];
			}

			if (Name != "text") Serilog.Log.Warning($"[{Name}] get failed, alias: {alias}");
			return null;
		}
	}

	public Record this[Ref Ref, bool message = true]
	{
		get
		{
			if (Ref == default) return null;
			if (_records == null) LoadAsync().Wait();

			if (ByRef.TryGetValue(Ref, out var item)) return item;
			else if (_records.Any() && message)
				Debug.WriteLine($"[{Name}] get failed, id: {Ref.Id} variation: {Ref.Variant}");

			return null;
		}
	}
	#endregion

	#region Serialize Methods
	public List<HashInfo> WriteXml(string folder, TableWriterSettings settings = null)
	{
		var hash = new List<HashInfo>();

		var name = $"{Name.TitleCase()}Data.xml";
		var path = Path.Combine(folder, name);
		Directory.CreateDirectory(Path.GetDirectoryName(path));

		settings ??= TableWriterSettings.DefaultSettings;
		var data = WriteXml(settings);
		File.WriteAllBytes(path, data);

		hash.Add(new HashInfo(name, XXH64.DigestOf(data)));
		return hash;
	}

	public byte[] WriteXml(TableWriterSettings settings, params Record[] records)
	{
		ArgumentNullException.ThrowIfNull(settings);

		using var ms = new MemoryStream();
		using var writer = XmlWriter.Create(ms, new XmlWriterSettings() { Indent = true, IndentChars = "\t", Encoding = settings.Encoding });

		writer.WriteStartDocument();
		writer.WriteStartElement(Definition.ElRoot.Name);
		writer.WriteAttributeString("release-module", Moudle.LocalizationData.ToString());
		writer.WriteAttributeString("release-side", settings.ReleaseSide.ToString().ToLower());
		writer.WriteAttributeString("type", Definition.Name);
		writer.WriteAttributeString("version", MajorVersion + "." + MinorVersion);
		writer.WriteComment($" {Name}.xml ");

		if (records.Length == 0) records = [.. Records];
		foreach (var record in records)
			record.WriteXml(writer, Definition.ElRecord);

		writer.WriteEndElement();
		writer.WriteEndDocument();
		writer.Flush();

		return ms.ToArray();
	}
	#endregion


	#region Interface
	public void Clear()
	{
		// prevent reload
		Archive = null;
		GlobalString = new StringLookup();

		_records?.Clear();
		_records = [];

		ByRef.Clear();
		ByRequired.Clear();
	}

	public void Dispose()
	{
		this.Clear();
		Definition = null;

		GC.SuppressFinalize(this);
	}
	#endregion
}