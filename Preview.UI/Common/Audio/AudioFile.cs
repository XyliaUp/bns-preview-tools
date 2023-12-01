using System.IO;

using CommunityToolkit.Mvvm.ComponentModel;

using CSCore;

namespace Xylia.Preview.UI.Audio;

public partial class AudioFile : ObservableObject
{
	[ObservableProperty]
	string path;

	[ObservableProperty]
	string name;

	[ObservableProperty]
	private long length;

	[ObservableProperty]
	private AudioEncoding encoding = AudioEncoding.Unknown;

	[ObservableProperty]
	private int bytesPerSecond;


	public byte[] Data { get; set; }
	public string Extension { get; }


	public AudioFile(byte[] data, string extension)
	{
		Extension = extension;
		Length = data.Length;
		Data = data;
	}

	public AudioFile(FileInfo fileInfo)
	{
		Path = fileInfo.FullName.Replace('\\', '/');
		Name = fileInfo.Name;
		Length = fileInfo.Length;
		Extension = fileInfo.Extension[1..];
		Data = File.ReadAllBytes(fileInfo.FullName);


		//// Check that the file exists
		//if (!File.Exists(file.Path))
		//{
		//	throw new FileNotFoundException(string.Format("File '{0}' was not found", file.Path));
		//}
	}

	public AudioFile(AudioFile audioFile, IAudioSource wave)
	{
		Path = audioFile.Path;
		Name = audioFile.Name;
		Length = audioFile.Length;
		Encoding = wave.WaveFormat.WaveFormatTag;
		BytesPerSecond = wave.WaveFormat.BytesPerSecond;
		Extension = audioFile.Extension;
		Data = audioFile.Data;
	}
}