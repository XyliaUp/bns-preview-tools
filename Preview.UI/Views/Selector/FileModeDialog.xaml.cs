using System.Windows;
using System.Windows.Input;

namespace Xylia.Preview.UI.Views.Selector;
public partial class FileModeDialog : Window
{
	public FileModeDialog() => InitializeComponent();


	public FileMode Result;

	public enum FileMode
	{
		None,

		Text,
		Xlsx,
	}

	private void TextFile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		Result = FileMode.Text;
		this.DialogResult = true;
	}

	private void ExcelFile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		Result = FileMode.Xlsx;
		this.DialogResult = true;
	}
}