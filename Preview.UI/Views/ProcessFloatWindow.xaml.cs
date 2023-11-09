using System.Windows.Input;
using System.Windows.Interop;

using Vanara.PInvoke;

namespace Xylia.Preview.UI.Views;
public partial class ProcessFloatWindow : Window
{
	static ProcessFloatWindow _Instance;
	public static ProcessFloatWindow Instance
	{
		get
		{
			if (_Instance is null || 
				(PresentationSource.FromVisual(_Instance)?.IsDisposed ?? true))
				_Instance = new();

			return _Instance;
		}
	}



	private ProcessFloatWindow()
	{
		InitializeComponent();
	}



	System.Timers.Timer timer;

	private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		this.DragMove();
	}

	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		const int Interval = 1500;
		var prevCpuTime = TimeSpan.Zero;

		timer = new System.Timers.Timer(Interval);
		timer.Enabled = true;
		timer.Elapsed += new((s, e) =>
		{
			var process = Process.GetCurrentProcess();

			int num = 0;
			double memory = process.PrivateMemorySize64;
			while (memory > 1024)
			{
				memory /= 1024;
				num++;
			}

			var value = (process.TotalProcessorTime - prevCpuTime).TotalMilliseconds / Interval / Environment.ProcessorCount;
			prevCpuTime = process.TotalProcessorTime;

			Application.Current.Dispatcher.Invoke(() =>
			{
				UsedCPU.Text = value.ToString("P0");
				UsedMemory.Text = memory.ToString("0.0 ") + (num switch { 0 => "Bit", 1 => "KB", 2 => "MB", 3 => "GB", _ => null });
			});
		});

		// hide in task manager
		User32.SetWindowLong(new WindowInteropHelper(this).Handle, User32.WindowLongFlags.GWL_EXSTYLE, (int)User32.WindowStylesEx.WS_EX_TOOLWINDOW);
	}

	private void Window_Closed(object sender, EventArgs e)
	{
		if (timer != null)
		{
			timer.Enabled = false;
			timer = null;
		}
	}
}