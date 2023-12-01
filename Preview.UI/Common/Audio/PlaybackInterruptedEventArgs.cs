namespace Xylia.Preview.UI.Audio;

public delegate void PlaybackInterruptedEventHandler(object sender, PlaybackInterruptedEventArgs e);

public class PlaybackInterruptedEventArgs : EventArgs
{
	public string Message { get; set; }
}