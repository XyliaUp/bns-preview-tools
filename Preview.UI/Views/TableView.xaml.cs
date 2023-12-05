using System.ComponentModel;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;

using CommunityToolkit.Mvvm.Input;

using HandyControl.Data;

using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Interactivity;

namespace Xylia.Preview.UI.Views;
public partial class TableView
{
	#region Ctors
	private ContextMenu ItemMenu;

	public TableView()
	{
		InitializeComponent();
		ItemMenu = (ContextMenu)this.TryFindResource("ItemMenu");
	}
	#endregion

	#region Properties
	private ICollectionView _source;

	public Table Table
	{
		set
		{
			this.Title = string.Format("{0} {1}", StringHelper.Get("TableView_Name"), value.Name);
			this.ObjectList.ItemsSource = _source = CollectionViewSource.GetDefaultView(value.Records);

			CreateItemMenu(value.Name);
		}
	}
	#endregion

	#region Methods
	private void SearchStarted(object sender, FunctionEventArgs<string> e)
	{
		_source.Filter = (o) => Filter(o, e.Info);
		_source.Refresh();
	}

	/// <summary>
	/// filter item
	/// </summary>	
	public static bool Filter(object item, string rule)
	{
		if (item is Record record)
		{
			return record.ToString().Contains(rule, StringComparison.OrdinalIgnoreCase);
		}

		return false;
	}
	#endregion



	#region ItemMenu
	private void CreateItemMenu(string name)
	{
		ItemMenu.Items.Clear();

		var assembly = Assembly.GetExecutingAssembly();
		var baseType = typeof(RecordCommand);
		foreach (var definedType in assembly.DefinedTypes)
		{
			if (definedType.IsAbstract || definedType.IsInterface || !baseType.IsAssignableFrom(definedType)) continue;

			// create
			var instance = Activator.CreateInstance(definedType);
			if (instance is RecordCommand command && command.CanExecute(name))
			{
				ItemMenu.Items.Add(new MenuItem()
				{
					Header = StringHelper.Get(command.Name),
					Command = new RelayCommand(() => command.Execute(ObjectList.SelectedItem)),
				});
			}
		}
	}
	#endregion
}