using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

using BnsBinTool.Core.DataStructs;

using Xylia.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Attributes;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Data.Table.XmlRecord;
using Xylia.Preview.Properties;
using Xylia.Xml;

namespace Xylia.Preview.Data
{
	public class DataTable<T> : IEnumerable<T>, ITable where T : BaseRecord, new()
	{
		#region Constructor
		public DataTableSet DataTableSet;

		public string TypeName = typeof(T).Name;
		#endregion

		#region Fields
		/// <summary>
		/// 显示调试信息
		/// </summary>
		public bool ShowDebugInfo { get; set; } = true;

		public string XmlDataPath;
		#endregion


		#region 数据处理
		protected readonly Dictionary<Ref, Lazy<T>> ByRef = new();

		protected readonly Dictionary<string, Ref> ByAlias = new(StringComparer.OrdinalIgnoreCase);

		protected void AddAlias(string Alias, Ref Ref)
		{
			if (Alias is not null && !this.ByAlias.ContainsKey(Alias))
				this.ByAlias[Alias] = Ref;
		}






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


		#region Load Functions
		public FileInfo[] TestPath;

		protected bool LoadFromGame => true && TestPath is null;




		private bool InLoading = false;

		public void TryLoad() => this.Load();

		private void Load(bool Reload = false)
		{
			#region Initialize
			//等待到数据载入完成
			//可能的异常原因：结束后不解除标志、在同一线程上多次请求Load 
			if (this.InLoading)
			{
				while (this.InLoading) Thread.Sleep(100);
				return;
			}

			//有数据且不需要重载, 则不进行处理
			if (this._data != null && !Reload) return;


			//如果是设计器模式, 则不进行处理
			if (Program.IsDesignMode) return;


			this.InLoading = true;
			this._data = null;
			#endregion

			lock (this)
			{
				var task = new Task(() =>
				{
					try
					{
						if (LoadFromGame) this.LoadData();
						else
						{
							var Files = TestPath ?? new DirectoryInfo(CommonPath.WorkingDirectory).GetFiles(TypeName + "Data*");
							if (!Files.Any()) throw new FileNotFoundException("Data Not Found!");

							LoadXml2(Files.Select(o => o.FullName.GetXmlDocument().DocumentElement));
						}

						Trace.WriteLine($"[{DateTime.Now}] load table successful: {TypeName} ({this._data.Length}s)");
					}
					catch (Exception ex)
					{
						this._data = Array.Empty<Lazy<T>>();
						Trace.WriteLine($"[{DateTime.Now}] load table failed: {TypeName} -> {ex}");
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

		private void LoadXml2(IEnumerable<XmlElement> rootNode)
		{
			uint TableIndex = 0;
			var tables = rootNode.SelectMany(o => o.ReadFile<T>(ref TableIndex)).ToList();

			this._data = new Lazy<T>[tables.Count];
			for (var x = 0; x < this._data.Length; x++)
			{
				var data = tables[x];
				var Ref = new Ref(data.Key());

				this.ByRef[Ref] = this._data[x] = new(data);
				AddAlias(data.alias, Ref);
			}
		}

		private void LoadData()
		{
			lock (DataTableSet) DataTableSet.LoadData(XmlDataPath is null);
			var helper = DataTableSet.GetHelper(TypeName, false);


			if (XmlDataPath is null)
			{
				ArgumentNullException.ThrowIfNull(helper);

				//get table
				if (helper.Definition is null || helper.Definition.Type == 0) throw new Exception(TypeName + " get specified data failed");
				var Table = DataTableSet.Tables.FirstOrDefault(table => table.Type == helper.Definition.Type);
				ArgumentNullException.ThrowIfNull(Table);

				helper.CheckVersion(Table);
				_processTable += new ProcessTableHandle((o) => DataTableSet.datafileToXml.ProcessTable(Table, helper.Definition, o));


				if (CommonPath.Test == 1 && this.DataTableSet is not LocalDataTableSet)
					ProcessTable(CommonPath.WorkingDirectory);


				var attrDef = helper.GetAttrDef();

				this._data = new Lazy<T>[Table.Records.Count];
				for (var x = 0; x < this._data.Length; x++)
				{
					var data = Table.Records[x];
					if (data is null) continue;

					int index = x;
					this.ByRef[data.RecordRef] = this._data[x] = new(() =>
					{
						var Object = new T();
						Object.TableIndex = (uint)index;
						Object.LoadData(new DbData(DataTableSet.datafileToXml, attrDef, data));

						return Object;
					});
				}




				if (helper.Aliases is not null)
				{
					foreach (var o in helper.Aliases)
						AddAlias(o.Alias, new Ref((int)o.MainID, (int)o.Variation));
				}

				//仅读取汉化时的替代处理
				else if (typeof(T) == typeof(Text))
				{
					foreach (var data in Table.Records)
						AddAlias(data.StringLookup.GetString(0), data.RecordRef);
				}
			}

			else LoadXml2(DataTableSet.XmlData.EnumerateFiles(XmlDataPath).Select(o => o.Xml.Nodes.DocumentElement));
		}
		#endregion

		#region Get Info
		public T this[string Alias] => this.GetLazyInfo(Alias)?.Value;

		public T this[int Id, int Variant = 0] => this.GetLazyInfo(new Ref(Id, Variant))?.Value;

		public T this[BaseRecord resolvedRecord] => this[resolvedRecord?.alias];


		protected Lazy<T> GetLazyInfo(string Alias)
		{
			if (string.IsNullOrWhiteSpace(Alias)) return null;
			else if (Alias.Contains(':'))
			{
				var o = Alias.Split(':');
				return GetLazyInfo(new Ref(o[0].ToInt(), o[1].ToInt()));
			}
			else if (int.TryParse(Alias, out var MainID)) return GetLazyInfo(new Ref(MainID));


			this.TryLoad();
			if (this.ByAlias.TryGetValue(Alias, out var item)) return GetLazyInfo(item);
			if (this._data.Length != 0 && ShowDebugInfo) Debug.WriteLine($"[{TypeName}] get failed ,alias: {Alias}");
			return null;
		}

		protected Lazy<T> GetLazyInfo(Ref Ref)
		{
			if (Ref.Id <= 0) return null;


			this.TryLoad();
			if (this.ByRef.TryGetValue(Ref, out var item)) return item;
			if (this._data.Length != 0) Debug.WriteLine($"[{TypeName}] get failed,id: {Ref.Id} variation: {Ref.Variant}");
			return null;
		}
		#endregion


		#region Process Functions
		public void ProcessTable(string _outputPath) => this._processTable?.Invoke(_outputPath);

		private delegate void ProcessTableHandle(string _outputPath);

		private event ProcessTableHandle _processTable;
		#endregion

		#region Interface Functions
		void ITable.Clear()
		{
			this._data = null;

			this.ByRef.Clear();
			this.ByAlias.Clear();
		}

		object ITable.this[string Alias] => this[Alias];


		public IEnumerator<T> GetEnumerator()
		{
			foreach (var info in this.Data)
				yield return info.Value;

			yield break;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		#endregion
	}

	public interface ITable : IEnumerable
	{
		object this[string Alias] { get; }

		void Clear();
	}
}