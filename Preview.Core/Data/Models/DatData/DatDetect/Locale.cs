using System.Configuration;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xylia.Configure;
using Xylia.Extension;

namespace Xylia.Preview.Data.Models.DatData.DatDetect;
public sealed class Locale
{
	private string _publisher;

	public string _language;

	public string Universe;


	public Language Language => _language.ToEnum<Language>();
	public Publisher Publisher => _publisher.ToEnum<Publisher>();


	public Locale(DirectoryInfo directory)
	{
		Load(directory);

		if (Publisher == Publisher.Tencent)
		{
			int game = 0;
			var rail_game = directory.GetDirectories("rail_files", SearchOption.AllDirectories).FirstOrDefault()?.GetFiles("rail_game_identify.json").FirstOrDefault();
			if (rail_game != null) game = JToken.ReadFrom(new JsonTextReader(File.OpenText(rail_game.FullName)))["game_id"]?.Value<int>() ?? 0;
			
			if (game != 48 && game != 10048 && game != 10148 && game != 10248)
				throw new ConfigurationErrorsException($"invalid game ({game})");
		}
	}

	private void Load(DirectoryInfo directory)
	{
		#region mode
		var local = directory.GetDirectories("Win64", SearchOption.AllDirectories).FirstOrDefault()?
			.GetFiles("local.ini").FirstOrDefault();
		if (local is not null)
		{
			_publisher = Ini.ReadValue("Locale", "Publisher", local.FullName);
			_language = Ini.ReadValue("Locale", "Language", local.FullName);
			Universe = Ini.ReadValue("Locale", "Universe", local.FullName);

			return;
		}
		#endregion

		#region mode2
		var temp = directory.GetDirectories("Content", SearchOption.AllDirectories).FirstOrDefault()?
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