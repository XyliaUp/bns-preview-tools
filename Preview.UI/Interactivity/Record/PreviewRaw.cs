using System.Text;
using System.Windows;

using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Views.Editor;

namespace Xylia.Preview.UI.Interactivity;
public class PreviewRaw : RecordCommand
{
	public override bool CanExecute(string name) => true;

	public override void Execute(Record record) => Execute(record, false);

	public void Execute(Record record, bool mode)
	{
		Application.Current.Dispatcher.Invoke(() =>
		{
			// TODO: valid children
			// Warning: is not original text
			if (record.HasChildren || mode)
			{
				var editor = new TextEditor();

				var settings = new TableWriterSettings() { Encoding = Encoding.Unicode };
				editor.Text = settings.Encoding.GetString(record.Owner.WriteXml(settings, record));
				editor.Show();
			}
			else
			{
				var editor = new PropertyEditor();
				editor.Source = record;
				editor.Show();
			}
		});
	}
}