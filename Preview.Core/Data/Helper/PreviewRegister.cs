namespace Xylia.Preview.Data.Helper;
public class PreviewRegister
{
	#region Preview
	public delegate void PreviewHandler(BaseRecord record, IWin32Window window);

	public static event PreviewHandler PreviewEvent;

	/// <summary>
	/// Create a window to show data
	/// </summary>
	/// <param name="record"></param>
	/// <param name="window"></param>
	internal static void Preview(BaseRecord record, IWin32Window window = null) => PreviewEvent?.Invoke(record, window);
	#endregion

	#region ShowTip
	public static event EventHandler ShowTipEvent;

	internal static void ShowTip(string Message) => ShowTipEvent?.Invoke(Message, new());
	#endregion
}