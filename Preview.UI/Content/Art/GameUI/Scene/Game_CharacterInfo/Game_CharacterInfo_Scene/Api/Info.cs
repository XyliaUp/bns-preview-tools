using System.IO;
using System.Net.Http;

using Newtonsoft.Json;

using Xylia.Preview.Data.Models.Creature;

namespace Xylia.Preview.UI.GameUI.Scene.Game_CharacterInfo.Api;
public class Info
{
	public int ability_achievement_id;
	public sbyte ability_achievement_step;
	public Guid account_id;
	public string clazz;
	public string class_name;
	public int depository_size;
	public long exp;
	public string faction;
	public string faction_name;
	public sbyte faction_grade;
	public string faction_grade_name;
	public int faction_reputation;
	public string faction2;
	public string faction2_name;
	public string gender;
	public string gender_name;
	public int geo_zone;
	public string geo_zone_name;
	public int id;
	public short inventory_size;
	public sbyte level;
	public long mastery_exp;
	public string mastery_faction; 
	public string mastery_faction_name;
	public sbyte mastery_level;
	public int money;
	public string name;
	public string race;
	public string race_name;
	public short server_id;
	public string server_name;
	public short wardrobe_size;
	public bool membership;
	public Guild guild;
	public struct Guild
	{
		public int guild_id;
		public string guild_name;
	}
	
	public string profile_url;

	


	public static Info Get(Creature creature)
	{
		string Host = @"https://19gate.bns.qq.com";
		string Url = @"/ingame/api/character/info.json";

		var url = new UriBuilder(Host + Url) { Query = $"c={creature.Name}" }.Uri;

		var response = new HttpClient().GetAsync(url).Result;
		if (!response.IsSuccessStatusCode)
			throw new InvalidDataException();

		return JsonConvert.DeserializeObject<Info>(response.Content.ReadAsStringAsync().Result);
	}
}