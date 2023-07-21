using System.ComponentModel;

using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;

namespace Xylia.Preview.Data.Package.SoundWave;

/// <summary>
/// 封装音频播放器
/// </summary>
public class MusicPlayer : Component
{
	private ISoundOut _soundOut;
	private IWaveSource _waveSource;

	public event EventHandler<PlaybackStoppedEventArgs> PlaybackStopped;
	/// <summary>
	/// 获取播放器的播放状态
	/// </summary>
	public PlaybackState PlaybackState
	{
		get
		{
			if (_soundOut != null)
				return _soundOut.PlaybackState;
			return PlaybackState.Stopped;
		}
	}
	/// <summary>
	/// 目前播放音频的位置
	/// </summary>
	public TimeSpan Position
	{
		get
		{
			if (_waveSource != null)
				return _waveSource.GetPosition();
			return TimeSpan.Zero;
		}
		set
		{
			if (_waveSource != null)
				_waveSource.SetPosition(value);
		}
	}
	/// <summary>
	/// 获取播放音频的长度
	/// </summary>
	public TimeSpan Length
	{
		get
		{
			if (_waveSource != null)
				return _waveSource.GetLength();
			return TimeSpan.Zero;
		}
	}
	/// <summary>
	/// 播放器的音量
	/// </summary>
	public int Volume
	{
		get
		{
			if (_soundOut != null)
				return Math.Min(100, Math.Max((int)(_soundOut.Volume * 100), 0));
			return 100;
		}
		set
		{
			if (_soundOut != null)
			{
				_soundOut.Volume = Math.Min(1.0f, Math.Max(value / 100f, 0f));
			}
		}
	}
	/// <summary>
	/// Load 音频文件地址
	/// </summary>
	/// <param name="filename"></param>
	public void Open(string filename)
	{
		CleanupPlayback();

		_waveSource =
			CodecFactory.Instance.GetCodec(filename)
				.ToSampleSource()
				.ToWaveSource();
		_soundOut = new WaveOut() { Latency = 100 };
		_soundOut.Initialize(_waveSource);
		if (PlaybackStopped != null) _soundOut.Stopped += PlaybackStopped;
	}
	/// <summary>
	/// 播放
	/// </summary>
	public void Play()
	{
		if (_soundOut != null)
			_soundOut.Play();
	}
	/// <summary>
	/// 暂停
	/// </summary>
	public void Pause()
	{
		if (_soundOut != null)
			_soundOut.Pause();
	}
	/// <summary>
	/// 停止
	/// </summary>
	public void Stop()
	{
		if (_soundOut != null)
			_soundOut.Stop();
	}
	/// <summary>
	/// 释放设备和音频资源
	/// </summary>
	public void CleanupPlayback()
	{
		if (_soundOut != null)
		{
			_soundOut.Dispose();
			_soundOut = null;
		}
		if (_waveSource != null)
		{
			_waveSource.Dispose();
			_waveSource = null;
		}
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
		CleanupPlayback();
	}
}
