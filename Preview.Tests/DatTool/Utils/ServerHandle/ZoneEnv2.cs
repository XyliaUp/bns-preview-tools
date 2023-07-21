using System.Xml;

using Xylia.Extension;

namespace Xylia.Preview.Tests.DatTool.Utils.ServerHandle;
public class ZoneEnv2 : TableHandle
{
	protected override void Fix(XmlElement record)
	{
		if (record.Attributes["script"] != null) return;

		var type = record.Attributes["type"].Value.ToLower();
		if (type.Equals("portal")) return;
		if (type.Equals("oceanic-region")) return;
		if (type.Equals("fall-death")) return;
		if (type.Equals("attraction-popup")) return;
		if (type.Equals("enter-arena-dungeonlobby")) return;


		record.SetAttribute("script", record.Attributes["alias"].Value + "_ai");
	}
}