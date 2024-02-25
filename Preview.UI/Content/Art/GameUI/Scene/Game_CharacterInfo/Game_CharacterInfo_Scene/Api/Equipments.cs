using System.IO;
using System.Net.Http;

using Newtonsoft.Json;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Models.Creature;

namespace Xylia.Preview.UI.GameUI.Scene.Game_CharacterInfo.Api;
public class Equipments
{
	public Equipment hand;
	public Equipment hand_appearance;
	public Equipment body;
	public Equipment eye;
	public Equipment head;
	public Equipment ear_left;
	public Equipment finger_left;
	public Equipment bracelet;
	public Equipment neck;
	public Equipment soul;
	public Equipment soul_2;
	public Equipment gloves;
	public Equipment belt;
	public Equipment pet;
	public Equipment pet_1_appearance;
	public Equipment swift_badge;       //rune-1
	public Equipment soul_badge;        //rune-2

	public Equipment soulshield_1;
	public Equipment soulshield_2;
	public Equipment soulshield_3;
	public Equipment soulshield_4;
	public Equipment soulshield_5;
	public Equipment soulshield_6;
	public Equipment soulshield_7;
	public Equipment soulshield_8;
	public Equipment alternate_soulshield_1;
	public Equipment alternate_soulshield_2;
	public Equipment alternate_soulshield_3;
	public Equipment alternate_soulshield_4;
	public Equipment alternate_soulshield_5;
	public Equipment alternate_soulshield_6;
	public Equipment alternate_soulshield_7;
	public Equipment alternate_soulshield_8;


	public static Equipments Get(Creature creature)
	{
		string Host = @"https://%sgate.bns.qq.com";
		string Url = @"/ingame/api/character/equipments.json";

		var url = new UriBuilder(Host.Replace("%s", creature.WorldId.ToString()[..2]) + Url) { Query = $"c={creature.Name}" }.Uri;

		var response = new HttpClient().GetAsync(url).Result;
		if (!response.IsSuccessStatusCode) throw new InvalidDataException();

		return JsonConvert.DeserializeObject<Equipments>(response.Content.ReadAsStringAsync().Result);
	}
}

public struct Equipment
{
	public Equip equip;
	public Detail detail;
	public string guild_uniform;
	public string tooltip_string;

	public struct Equip
	{
		public long id;
		public AssetType asset_type;
		public int pos;
		public string equipped_part;
		public bool sequestration;
		public bool Sealed;
		public bool wearable;

		public Item item;
		public object appearance;

		public struct Item
		{
			public int id;
			public sbyte level;
			public string name;

			public string icon;
			public string icon_extra;
			public string icon_transparent;

			public sbyte grade;
			public string grade_name;

			public string set_item_code;
			public bool resealable;
			public string type;
		}
	}

	public struct Detail
	{
		//public AssetType asset_type;
		//public int pos;
		//public string equipped_part;
		//public bool sequestration;
		//public bool Sealed;
	}

	public readonly Ref Item => new(this.equip.item.id, this.equip.item.level);
}