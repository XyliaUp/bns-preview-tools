using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;

using HandyControl.Tools.Extension;

namespace Xylia.Preview.UI.Views.Selector;

[ObservableObject]
public partial class FileModeDialog : IDialogResultable<FileModeDialog.FileMode>
{
	#region Ctor
	public enum FileMode
	{
		None,

		Text,
		Xlsx,
	}

	public FileModeDialog()
	{
		DataContext = this;
		InitializeComponent();
	}
	#endregion

	#region Methods
	private void TextFile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		Result = FileMode.Text;
		CloseAction?.Invoke();
	}

	private void ExcelFile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		Result = FileMode.Xlsx;
		CloseAction?.Invoke();
	}
	#endregion


	#region Interface
	[ObservableProperty]
	private FileMode result;

	public Action CloseAction { get; set; }
	#endregion
}