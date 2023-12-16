using IniParser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xylia.Preview.Common.Extension;

namespace Xylia.Preview.Data.Engine.DatData;
public sealed class Locale
{
	private string _publisher;
	public string _language;
	public string Universe;
	public string ProductVersion;

	public ELanguage Language => _language.ToEnum<ELanguage>();
	public Publisher Publisher => _publisher.ToEnum<Publisher>();


	public Locale(DirectoryInfo directory)
	{
		Load(directory);


		if (Publisher == Publisher.Tencent)
		{
			int game = 0;

			FileInfo rail_game = null;
			do
			{
				rail_game = directory.GetDirectories("rail_files", SearchOption.AllDirectories).FirstOrDefault()?.GetFiles("rail_game_identify.json").FirstOrDefault();
				directory = directory.Parent;
			}
			while (directory != null && rail_game is null);

			if (rail_game != null) game = JToken.ReadFrom(new JsonTextReader(File.OpenText(rail_game.FullName)))["game_id"]?.Value<int>() ?? 0;
#if !DEBUG
			if (game != 48 && game != 10048 && game != 10148 && game != 10248)
				throw Xylia.Preview.Data.Common.Exceptions.BnsDataException.InvalidGame("invalid game");
#endif
		}
	}

	private void Load(DirectoryInfo directory)
	{
		#region mode
		var Win64 = directory.GetDirectories("Win64", SearchOption.AllDirectories).FirstOrDefault();
		if (Win64 is not null)
		{
			var version = Win64?.GetFiles("version.ini").FirstOrDefault();
			if (version is not null)
			{
				var config = new FileIniDataParser().ReadFile(version.FullName);
				ProductVersion = config["Version"]["ProductVersion"];
			}

			var local = Win64?.GetFiles("local.ini").FirstOrDefault();
			if (local is not null)
			{
				var config = new FileIniDataParser().ReadFile(local.FullName);

				_publisher = config["Locale"]["Publisher"];
				_language = config["Locale"]["Language"];
				Universe = config["Locale"]["Universe"];
				return;
			}
		}
		#endregion

		#region mode2
		var temp = (directory.GetDirectories("Content", SearchOption.AllDirectories).FirstOrDefault() ?? directory)
			.GetDirectories("local").FirstOrDefault()?
			.GetDirectories().FirstOrDefault();
		if (temp is not null)
		{
			_publisher = temp.Name;
			_language = temp.GetDirectories().Where(o => o.Name != "data").FirstOrDefault()?.Name;

			return;
		}
		#endregion
	}
}