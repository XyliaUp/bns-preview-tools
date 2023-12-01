using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Xylia.Preview.UI.Audio;

namespace Xylia.Preview.UI.ViewModels;
public partial class AudioPlayerViewModel : ObservableObject, IDisposable
{
	#region Fields
	private PlaybackService playbackService;

	[ObservableProperty]
	private bool showPause;

	[ObservableProperty]
	private bool isLoadingTrack;


	public bool ShowLoopNone => this.playbackService.LoopMode == LoopMode.None;
	public bool ShowLoopOne => this.playbackService.LoopMode == LoopMode.One;
	public bool ShowLoopAll => this.playbackService.LoopMode == LoopMode.All;
	public bool Shuffle => this.playbackService.Shuffle;

	public TimeSpan CurrentTime => this.playbackService.GetCurrentTime;
	public TimeSpan TotalTime => this.playbackService.GetTotalTime;
	public double Progress { get => playbackService.Progress; set => playbackService.SkipProgress(value); }
	public bool CanReportProgress => !this.playbackService.IsStopped;

	public float Volume { get => this.playbackService.Volume; set => this.playbackService.Volume = value; }
	public bool Mute { get => this.playbackService.Mute || Volume == 0; set => this.playbackService.Mute = value; }



	public ICollectionView AudioFilesView { get; set; }

	public ICollectionView AudioDevicesView { get; set; }

	public AudioFile Selected
	{
		get => this.playbackService.CurrentTrack;
		set => this.playbackService.CurrentTrack = value;
	}

	public AudioDevice SelectedAudioDevice
	{
		get => this.playbackService.audioDevice;
		set
		{
			// Due to two-way binding, this can be null when the list is being filled.
			if (value != null)
			{
				this.playbackService.SwitchAudioDeviceAsync(value);
			}

			OnPropertyChanged();
		}
	}

	public RelayCommand PlayPauseCommand { get; set; }
	public RelayCommand PlayNewCommand { get; set; }
	public RelayCommand PreviousCommand { get; set; }
	public RelayCommand NextCommand { get; set; }
	public RelayCommand LoopCommand { get; set; }
	public RelayCommand ShuffleCommand { get; set; }

	public RelayCommand MuteCommand { get; set; }
	#endregion




	#region Ctors
	public AudioPlayerViewModel()
	{
		this.playbackService = new();

		// Commands
		this.PlayPauseCommand = new(async () => await this.playbackService.PlayOrPauseAsync());
		this.PlayNewCommand = new(async () => await this.playbackService.PlaySelectedAsync(Selected));
		this.PreviousCommand = new(async () => await this.playbackService.PlayPreviousAsync());
		this.NextCommand = new(async () => await this.playbackService.PlayNextAsync());
		this.LoopCommand = new(() => this.SetPlayBackServiceLoop());
		//this.ShuffleCommand = new(() => this.SetPlayBackServiceShuffle(!this.shuffle));

		// Event handlers
		this.playbackService.PlaybackFailed += (_, __) => this.ShowPause = false;
		this.playbackService.PlaybackPaused += (_, __) => this.ShowPause = false;
		this.playbackService.PlaybackResumed += (_, __) => this.ShowPause = true;
		this.playbackService.PlaybackStopped += (_, __) => this.ShowPause = false;
		this.playbackService.PlaybackSuccess += (_, __) => { this.ShowPause = true; OnPropertyChanged(nameof(Selected)); };
		this.playbackService.PlaybackProgressChanged += (_, __) => this.UpdateTime();
		this.playbackService.PlaybackLoopChanged += (_, __) => this.UpdateLoop();
		this.playbackService.PlaybackShuffleChanged += (_, __) => OnPropertyChanged(nameof(Shuffle));

		this.playbackService.LoadingTrack += (isLoadingTrack) => this.IsLoadingTrack = isLoadingTrack;

		// Volume
		this.playbackService.PlaybackVolumeChanged += (_, __) => UpdateVolume();
		this.playbackService.PlaybackMuteChanged += (_, __) => OnPropertyChanged(nameof(Mute));


		// Initial status
		this.AudioFilesView = CollectionViewSource.GetDefaultView(this.playbackService.Queue);
		this.AudioDevicesView = CollectionViewSource.GetDefaultView(this.playbackService.GetAllAudioDevicesAsync());

		playbackService.Volume = 1;
	}
	#endregion

	#region Methods
	private void UpdateTime()
	{
		OnPropertyChanged(nameof(CurrentTime));
		OnPropertyChanged(nameof(TotalTime));
		OnPropertyChanged(nameof(Progress));
		OnPropertyChanged(nameof(CanReportProgress));
	}

	private void UpdateLoop()
	{
		OnPropertyChanged(nameof(this.ShowLoopNone));
		OnPropertyChanged(nameof(this.ShowLoopOne));
		OnPropertyChanged(nameof(this.ShowLoopAll));
	}

	private void UpdateVolume()
	{
		OnPropertyChanged(nameof(Volume));
		OnPropertyChanged(nameof(Mute));
	}

	private void SetPlayBackServiceLoop()
	{
		this.playbackService.LoopMode = this.playbackService.LoopMode switch
		{
			LoopMode.None => LoopMode.All,
			LoopMode.All => LoopMode.One,
			LoopMode.One => LoopMode.None,
			_ => LoopMode.None,
		};
	}

	public void AddToPlaylist(AudioFile file)
	{
		this.playbackService.Queue.Add(file);
	}




	[RelayCommand]
	public void Remove()
	{
		this.playbackService.Queue.Remove(Selected);
	}

	[RelayCommand]
	public void SavePlaylist()
	{
		Application.Current.Dispatcher.Invoke(() =>
		{
			foreach (var a in this.playbackService.Queue)
				Save(a, true);
		});
	}

	public ICommand SaveCommand => new RelayCommand<AudioFile>((x) => Save(x));

	public void Save(AudioFile file, bool auto = false)
	{
		var fileToSave = file ?? Selected;
		//if (_audioFiles.Count < 1 || fileToSave?.Data == null) return;
		//var path = fileToSave.Path;

		//if (!auto)
		//{
		//	var saveFileDialog = new SaveFileDialog
		//	{
		//		Title = "Save Audio",
		//		FileName = fileToSave.Name,
		//		//InitialDirectory = UserSettings.Default.AudioDirectory
		//	};
		//	if (!saveFileDialog.ShowDialog().GetValueOrDefault()) return;
		//	path = saveFileDialog.FileName;
		//}
		//else
		//{
		//	Directory.CreateDirectory(path.SubstringBeforeLast('/'));
		//}

		//using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
		//using (var writer = new BinaryWriter(stream))
		//{
		//	writer.Write(fileToSave.Data);
		//	writer.Flush();
		//}

		//if (File.Exists(path))
		//{
		//	Log.Information("{FileName} successfully saved", fileToSave.FileName);
		//	FLogger.Append(ELog.Information, () =>
		//	{
		//		FLogger.Text("Successfully saved ", Constants.WHITE);
		//		FLogger.Link(fileToSave.FileName, path, true);
		//	});
		//}
		//else
		//{
		//	Log.Error("{FileName} could not be saved", fileToSave.FileName);
		//	FLogger.Append(ELog.Error, () => FLogger.Text($"Could not save '{fileToSave.FileName}'", Constants.WHITE, true));
		//}
	}




	public void Dispose()
	{
		this.playbackService.Stop();
		this.playbackService = null;
	}
	#endregion
}