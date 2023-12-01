using System.Diagnostics;
using System.Windows;

using CUE4Parse.BNS.Assets.Exports;

using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Views;

namespace Xylia.Preview.UI.Interactivity;
public class PreviewShowObject : RecordCommand
{
	public override bool CanExecute(string name) => name switch
	{
		"social" => true,
		_ => false,
	};

	public override void Execute(Record record)
	{
		if (record.Owner.Name == "social")
		{
			var source = FileCache.Provider.LoadObject<UShowObject>(record.Attributes["show"]);
			if (source is null)
			{
				Debug.WriteLine("no data");
				return;
			}

			Application.Current.Dispatcher.Invoke(() =>
			{
				var player = new ShowObjectPlayer();
				player.Source = source;
				player.Show();
			});
		}
	}
}