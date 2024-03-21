namespace Xylia.Preview.Data.Models;
public sealed class ZoneEnv2 : ModelElement
{
	protected internal override void LoadHiddenField()
	{
		if (this.Attributes["script"] != null) return;

		var type = this.Attributes.Get<string>("type");
		if (type == "portal" ||
			type == "oceanic-region" ||
			type == "fall-death" ||
			type == "attraction-popup" ||
			type == "enter-arena-dungeonlobby") return;

		this.Attributes["script"] = this.Attributes["alias"] + "_ai";
	}
}