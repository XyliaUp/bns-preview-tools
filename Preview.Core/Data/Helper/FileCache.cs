using CUE4Parse.BNS;

using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Helper;
public static class FileCache
{
	public static TableSet Data = new();


	private static GameFileProvider _provider;
	public static GameFileProvider Provider => _provider ??= new GameFileProvider();


	public static void Clear()
	{
		Data?.Dispose();
		Data = new();

		(Provider as IDisposable)?.Dispose();
		_provider = new();
	}
}