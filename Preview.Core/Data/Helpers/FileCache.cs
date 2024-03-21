using CUE4Parse.BNS;
using Xylia.Preview.Data.Client;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Properties;

namespace Xylia.Preview.Data.Helpers;
public static class FileCache
{
	private static readonly object Lock = new();

	private static BnsDatabase _data;
	public static BnsDatabase Data
	{
		set => _data = value;
		get { lock (Lock) return _data ??= new(DefaultProvider.Load(Settings.Default.GameFolder)); }
	}

	private static GameFileProvider _provider;
	public static GameFileProvider Provider
	{
		get { lock (Lock) { return _provider ??= new(Settings.Default.GameFolder); } }
	}

	public static void Clear()
	{
		_data?.Dispose();
		_data = null;

		_provider?.Dispose();
		_provider = null;
	}
}