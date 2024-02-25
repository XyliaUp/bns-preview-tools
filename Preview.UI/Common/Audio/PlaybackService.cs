using System.IO;
using System.Timers;

namespace Xylia.Preview.UI.Audio;
public class PlaybackService
{
	#region Fields
	private QueueManager<AudioFile> queueManager;
	private System.Timers.Timer progressTimer = new System.Timers.Timer();
	private double progressTimeoutSeconds = 0.5;
	private float volume = 0.0f;
	private LoopMode loopMode;
	private bool shuffle;
	private bool mute;
	private CSCorePlayer player;

	private SynchronizationContext context;
	private bool isLoadingTrack;

	public AudioDevice audioDevice;
	#endregion

	#region Properties
	public bool IsStopped
	{
		get
		{
			if (this.player != null)
			{
				return !this.player.CanStop;
			}
			else
			{
				return true;
			}
		}
	}

	public bool IsPlaying
	{
		get
		{
			if (this.player != null)
			{
				return this.player.CanPause;
			}
			else
			{
				return false;
			}
		}
	}

	public IList<AudioFile> Queue => this.queueManager;

	public AudioFile CurrentTrack { get => this.queueManager.Current; set => this.queueManager.Current = value; }

	public double Progress { get; set; }

	public float Volume
	{
		get => this.volume;
		set
		{
			if (value > 1)
			{
				value = 1;
			}

			if (value < 0)
			{
				value = 0;
			}

			this.volume = value;

			if (this.player != null && !this.mute) this.player.SetVolume(value);

			//SettingsClient.Set<double>("Playback", "Volume", Math.Round(value, 2));
			this.PlaybackVolumeChanged(this, null);
		}
	}

	public LoopMode LoopMode
	{
		get { return this.loopMode; }
		set
		{
			this.loopMode = value;
			this.PlaybackLoopChanged(this, new EventArgs());
		}
	}

	public bool Shuffle
	{
		get { return this.shuffle; }
	}

	public bool Mute
	{
		get => this.mute;
		set => this.SetMute(value);
	}

	public bool UseAllAvailableChannels { get; set; }

	public int Latency { get; set; }

	public bool EventMode { get; set; }

	public bool ExclusiveMode { get; set; }

	public TimeSpan GetCurrentTime
	{
		get
		{
			try
			{
				// Check if there is a Track playing
				if (this.player != null && this.player.CanStop)
				{
					// This prevents displaying a current time which is larger than the total time
					if (this.player.GetCurrentTime() <= this.player.GetTotalTime())
					{
						return this.player.GetCurrentTime();
					}
					else
					{
						return this.player.GetTotalTime();
					}
				}
				else
				{
					return new TimeSpan(0);
				}
			}
			catch (Exception ex)
			{
				//LogClient.Error("Failed to get current time. Returning 00:00. Exception: {0}", ex.Message);
				return new TimeSpan(0);
			}

		}
	}

	public TimeSpan GetTotalTime
	{
		get
		{
			return this.player.GetTotalTime();

			//try
			//{
			//	// Check if there is a Track playing
			//	if (this.player != null && this.player.CanStop && this.HasCurrentTrack && this.CurrentTrack.Duration != null)
			//	{
			//		// In some cases, the duration reported by TagLib is 1 second longer than the duration reported by IPlayer.
			//		if (this.CurrentTrack.Track.Duration > this.player.GetTotalTime().TotalMilliseconds)
			//		{
			//			// To show the same duration everywhere, we report the TagLib duration here instead of the IPlayer duration.
			//			return new TimeSpan(0, 0, 0, 0, Convert.ToInt32(this.CurrentTrack.Track.Duration));
			//		}
			//		else
			//		{
			//			// Unless the TagLib duration is incorrect. In rare cases it is 0, even if 
			//			// IPlayer reports a correct duration. In such cases, report the IPlayer duration.
			//			return this.player.GetTotalTime();
			//		}
			//	}
			//	else
			//	{
			//		return new TimeSpan(0);
			//	}
			//}
			//catch (Exception ex)
			//{
			//	//LogClient.Error("Failed to get total time. Returning 00:00. Exception: {0}", ex.Message);
			//	return new TimeSpan(0);
			//}
		}
	}
	#endregion

	#region Constructorss
	public PlaybackService()
	{
		this.context = SynchronizationContext.Current;

		this.queueManager = new();

		// Set up timers
		this.progressTimer.Interval = TimeSpan.FromSeconds(this.progressTimeoutSeconds).TotalMilliseconds;
		this.progressTimer.Elapsed += new ElapsedEventHandler(this.ProgressTimeoutHandler);

		this.Initialize();
	}

	public event EventHandler PlaybackSuccess = delegate { };
	public event EventHandler PlaybackPaused = delegate { };
	public event EventHandler PlaybackFailed = delegate { };
	public event EventHandler PlaybackProgressChanged = delegate { };
	public event EventHandler PlaybackResumed = delegate { };
	public event EventHandler PlaybackStopped = delegate { };
	public event EventHandler PlaybackVolumeChanged = delegate { };
	public event EventHandler PlaybackMuteChanged = delegate { };
	public event EventHandler PlaybackLoopChanged = delegate { };
	public event EventHandler PlaybackShuffleChanged = delegate { };
	public event Action<bool> LoadingTrack = delegate { };
	public event EventHandler QueueChanged = delegate { };
	#endregion

	#region Methods
	public async Task PlayOrPauseAsync()
	{
		if (!this.IsStopped)
		{
			if (this.IsPlaying)
			{
				await this.PauseAsync();
			}
			else
			{
				await this.ResumeAsync();
			}
		}
		else
		{
			if (this.Queue != null && this.Queue.Count > 0)
			{
				// There are already tracks enqueued. Start playing immediately.
				await this.PlayFirstAsync();
			}
			else
			{
				// Enqueue all tracks before playing
				//await this.EnqueueAsync(false, false);
			}
		}
	}

	public void SetMute(bool mute)
	{
		this.mute = mute;

		this.player?.SetVolume(mute ? 0.0f : this.Volume);

		//SettingsClient.Set<bool>("Playback", "Mute", this.mute);
		this.PlaybackMuteChanged(this, new EventArgs());
	}

	public void SetShuffle(bool isShuffled)
	{
		this.shuffle = isShuffled;
		this.queueManager.Shuffle = isShuffled;

		this.PlaybackShuffleChanged(this, new EventArgs());
		this.QueueChanged(this, new EventArgs());
	}

	public void SkipProgress(double progress)
	{
		if (this.player != null && this.player.CanStop)
		{
			this.Progress = progress;
			int newSeconds = Convert.ToInt32(progress * this.player.GetTotalTime().TotalSeconds);
			this.player.Skip(newSeconds);
		}
		else
		{
			this.Progress = 0.0;
		}

		this.PlaybackProgressChanged(this, new EventArgs());
	}

	public void SkipSeconds(int seconds)
	{
		if (this.player != null && this.player.CanStop)
		{
			double totalSeconds = this.GetCurrentTime.TotalSeconds;

			if (seconds < 0 && totalSeconds <= Math.Abs(seconds))
			{
				this.player.Skip(0);
			}
			else
			{
				this.player.Skip(Convert.ToInt32(this.GetCurrentTime.TotalSeconds + seconds));
			}

			this.PlaybackProgressChanged(this, new EventArgs());
		}
	}

	public void Stop()
	{
		if (this.player != null && this.player.CanStop)
		{
			this.player.Stop();
		}

		this.progressTimer.Stop();
		this.Progress = 0.0;
		this.PlaybackStopped(this, new EventArgs());
	}

	public async Task PlayNextAsync()
	{
		// We don't want interruptions when trying to play the next Track.
		// If the next Track cannot be played, keep skipping to the 
		// following Track until a working Track is found.
		bool playSuccess = false;
		int numberSkips = 0;

		while (!playSuccess)
		{
			// We skip maximum 3 times. This prevents an infinite 
			// loop if shuffledTracks only contains broken Tracks.
			if (numberSkips < 3)
			{
				numberSkips += 1;
				playSuccess = await this.TryPlayNextAsync();
			}
			else
			{
				this.Stop();
				playSuccess = true; // Otherwise we never get out of this While loop
			}
		}
	}

	public async Task PlayPreviousAsync()
	{
		// We don't want interruptions when trying to play the previous Track. 
		// If the previous Track cannot be played, keep skipping to the
		// preceding Track until a working Track is found.
		bool playSuccess = false;
		int numberSkips = 0;

		while (!playSuccess)
		{
			// We skip maximum 3 times. This prevents an infinite 
			// loop if shuffledTracks only contains broken Tracks.
			if (numberSkips < 3)
			{
				numberSkips += 1;
				playSuccess = await this.TryPlayPreviousAsync(true);
			}
			else
			{
				this.Stop();
				playSuccess = true; // Otherwise we never get out of this While loop
			}
		}
	}

	public async Task PlaySelectedAsync(AudioFile track)
	{
		await this.TryPlayAsync(track);
	}

	private async void Initialize()
	{
		// Settings
		this.SetPlaybackSettings();

		// Player (default for now, can be changed later when playing a file)
		this.player = new CSCorePlayer();

		// Audio device
		await this.SetAudioDeviceAsync();
	}


	private async Task PauseAsync()
	{
		if (this.player != null)
		{
			await Task.Run(() => this.player.Pause());
			this.progressTimer.Stop();
			this.PlaybackPaused(this, null);
		}
	}

	private async Task ResumeAsync()
	{
		if (this.player != null)
		{
			bool isResumed = false;
			await Task.Run(() => isResumed = this.player.Resume());

			if (isResumed)
			{
				this.progressTimer.Start();
				this.PlaybackResumed(this, new EventArgs());
			}
			else
			{
				this.PlaybackStopped(this, new EventArgs());
			}
		}
	}

	private async Task PlayFirstAsync()
	{
		if (this.Queue.Count > 0)
		{
			AudioFile firstTrack = this.queueManager.First();

			await this.TryPlayAsync(firstTrack);
		}
	}

	private void StopPlayback()
	{
		if (this.player != null)
		{
			// Remove the previous Stopped handler (not sure this is needed)
			this.player.PlaybackInterrupted -= this.PlaybackInterruptedHandler;
			this.player.PlaybackFinished -= this.PlaybackFinishedHandler;

			this.player.Stop();
			this.player.Dispose();
			this.player = null;
		}
	}

	private async Task StartPlaybackAsync(AudioFile file, bool silent = false)
	{
		// Settings
		this.SetPlaybackSettings();

		// Play the Track from its runtime path (current or temporary)
		this.player = new CSCorePlayer();

		//this.player.SetPlaybackSettings(this.Latency, this.EventMode, this.ExclusiveMode, this.activePreset.Bands, this.UseAllAvailableChannels);
		this.player.SetVolume(silent | this.Mute ? 0.0f : this.Volume);

		// We need to set PlayingTrack before trying to play the Track.
		// So if we go into the Catch when trying to play the Track,
		// at least, the next time TryPlayNext is called, it will know that 
		// we already tried to play this track and it can find the next Track.
		this.queueManager.Current = file;

		// Play the Track
		await Task.Run(() => this.player.Play(file.Data, file.Extension, this.audioDevice));

		// Start reporting progress
		this.progressTimer.Start();

		// Hook up the Stopped event
		this.player.PlaybackInterrupted += this.PlaybackInterruptedHandler;
		this.player.PlaybackFinished += this.PlaybackFinishedHandler;
	}

	private async Task<bool> TryPlayAsync(AudioFile file, bool isSilent = false)
	{
		if (file == null) return false;

		if (this.isLoadingTrack)
		{
			// Only load 1 track at a time (just in case)
			return true;
		}

		this.OnLoadingTrack(true);

		bool isPlaybackSuccess = true;
		EventArgs playbackFailedEventArgs = null;

		try
		{
			// If a Track was playing, make sure it is now stopped.
			this.StopPlayback();

			// Start playing
			await this.StartPlaybackAsync(file, isSilent);

			// Playing was successful
			this.PlaybackSuccess(this, null);

			// Set this to false again after raising the event. It is important to have a correct slide 
			// direction for cover art when the next Track is a file from double click in Windows.
			//this.isPlayingPreviousTrack = false;
			//LogClient.Info("Playing the file {0}. EventMode={1}, ExclusiveMode={2}, LoopMode={3}, Shuffle={4}", track.Path, this.EventMode, this.ExclusiveMode, this.LoopMode, this.shuffle);
		}
		catch (FileNotFoundException fnfex)
		{
			//playbackFailedEventArgs = new PlaybackFailedEventArgs { FailureReason = PlaybackFailureReason.FileNotFound, Message = fnfex.Message, StackTrace = fnfex.StackTrace };
			isPlaybackSuccess = false;
		}
		catch (Exception ex)
		{
			//playbackFailedEventArgs = new PlaybackFailedEventArgs { FailureReason = PlaybackFailureReason.Unknown, Message = ex.Message, StackTrace = ex.StackTrace };
			isPlaybackSuccess = false;
		}

		if (!isPlaybackSuccess)
		{
			try
			{
				this.player?.Stop();
			}
			catch (Exception)
			{
				//LogClient.Error("Could not stop the Player");
			}

			//LogClient.Error("Could not play the file {0}. EventMode={1}, ExclusiveMode={2}, LoopMode={3}, Shuffle={4}. Exception: {5}. StackTrace: {6}", track.Path, this.EventMode, this.ExclusiveMode, this.LoopMode, this.shuffle, playbackFailedEventArgs.Message, playbackFailedEventArgs.StackTrace);
			this.PlaybackFailed(this, playbackFailedEventArgs);
		}

		this.OnLoadingTrack(false);

		return isPlaybackSuccess;
	}

	private void OnLoadingTrack(bool isLoadingTrack)
	{
		this.isLoadingTrack = isLoadingTrack;
		this.LoadingTrack(isLoadingTrack);
	}

	private async Task<bool> TryPlayPreviousAsync(bool ignoreLoopOne)
	{
		//this.isPlayingPreviousTrack = true;

		if (this.GetCurrentTime.Seconds > 3)
		{
			// If we're more than 3 seconds into the Track, try to
			// jump to the beginning of the current Track.
			this.player.Skip(0);
			return true;
		}

		// When "loop one" is enabled and ignoreLoopOne is true, act like "loop all".
		LoopMode loopMode = this.LoopMode == LoopMode.One && ignoreLoopOne ? LoopMode.All : this.LoopMode;

		AudioFile previousTrack = this.queueManager.Previous(loopMode);

		if (previousTrack == null)
		{
			this.Stop();
			return true;
		}

		return await this.TryPlayAsync(previousTrack);
	}

	private async Task<bool> TryPlayNextAsync()
	{
		//bool returnToStart = SettingsClient.Get<bool>("Playback", "LoopWhenShuffle") & this.shuffle;
		bool returnToStart = false;

		AudioFile nextTrack = this.queueManager.Next(loopMode, returnToStart);

		if (nextTrack == null)
		{
			this.Stop();
			return true;
		}

		return await this.TryPlayAsync(nextTrack);
	}

	private void ProgressTimeoutHandler(object sender, ElapsedEventArgs e)
	{
		this.HandleProgress();
	}

	private void PlaybackInterruptedHandler(object sender, PlaybackInterruptedEventArgs e)
	{
		// Playback was interrupted for some reason. Make sure we are in a correct state.
		// Use our context to trigger the work, because this event is fired on the Player's Playback thread.
		this.context.Post(new SendOrPostCallback((state) =>
		{
			this.Stop();
		}), null);
	}

	private void PlaybackFinishedHandler(object sender, EventArgs e)
	{
		// Try to play the next Track from the list automatically
		// Use our context to trigger the work, because this event is fired on the Player's Playback thread.
		this.context.Post(new SendOrPostCallback(async (state) =>
		{
			await this.TryPlayNextAsync();
		}), null);
	}

	private void HandleProgress()
	{
		if (this.player != null && this.player.CanStop)
		{
			TimeSpan totalTime = this.player.GetTotalTime();
			TimeSpan currentTime = this.player.GetCurrentTime();

			this.Progress = currentTime.TotalMilliseconds / totalTime.TotalMilliseconds;
		}
		else
		{
			this.Progress = 0.0;
		}

		PlaybackProgressChanged(this, new EventArgs());
	}

	private void SetPlaybackSettings()
	{
		//this.UseAllAvailableChannels = SettingsClient.Get<bool>("Playback", "WasapiUseAllAvailableChannels");
		//this.LoopMode = (LoopMode)SettingsClient.Get<int>("Playback", "LoopMode");
		//this.Latency = SettingsClient.Get<int>("Playback", "AudioLatency");
		//this.Volume = SettingsClient.Get<float>("Playback", "Volume");
		//this.mute = SettingsClient.Get<bool>("Playback", "Mute");
		//this.shuffle = SettingsClient.Get<bool>("Playback", "Shuffle");
		//this.EventMode = false;
		////this.EventMode = SettingsClient.Get<bool>("Playback", "WasapiEventMode");
		////this.ExclusiveMode = false;
		//this.ExclusiveMode = SettingsClient.Get<bool>("Playback", "WasapiExclusiveMode");
	}



	public IList<AudioDevice> GetAllAudioDevicesAsync()
	{
		var audioDevices = new List<AudioDevice>();

		if (this.player != null)
		{
			audioDevices.AddRange(this.player.GetAllAudioDevices());
		}

		return audioDevices;
	}

	public void SwitchAudioDevice(AudioDevice device)
	{
		this.audioDevice = device;
		this.player?.SwitchAudioDevice(this.audioDevice);
	}

	private async Task SetAudioDeviceAsync()
	{
		if (this.player is null) return;

		await Task.Run(() =>
		{
			var audioDevices = this.GetAllAudioDevicesAsync();
			this.audioDevice = null;
			//audioDevices.Where(x => x.DeviceId.Equals(UserSettings.Default.AudioDeviceId)).FirstOrDefault();

			//if (savedDevice == null)
			//{
			//	LogClient.Warning($"Audio device with deviceId={savedAudioDeviceId} could not be found. Using default device instead.");
			//	savedDevice = this.CreateDefaultAudioDevice();
			//}

			this.audioDevice ??= audioDevices.FirstOrDefault();
		});
	}
	#endregion
}