using System.Windows;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.GameUI.Scene.Game_Tooltip;

namespace Xylia.Preview.UI.Interactivity;
/// <summary>
/// Provide a command to open record tooltip 
/// </summary>
public class PreviewTooltip : RecordCommand
{
	public override bool CanExecute(object? parameter)
    {
#if DEVELOP
		return base.CanExecute(parameter);
#else
		return false;
#endif
	}


	public override bool CanExecute(string name) => name switch
    {
        //"item" => true,
        _ => false,
    };

    public override void Execute(Record record)
    {
        if (record.Owner.Name == "item")
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var scene = new Game_TooltipScene();
				scene.ItemTooltipPanel_C.DataContext = record;
                //scene.ItemTooltipPanel_C.Show();
			});
        }
    }
}