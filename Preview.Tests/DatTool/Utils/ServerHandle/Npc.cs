using System.Xml;

using Xylia.Extension;
using Xylia.Preview.Tests.DatTool.Utils.ServerHandle;

namespace Xylia.Preview.Tests.DatTool.Utils.ServerHandle;

public class Npc : TableHandle
{
	protected override void Fix(XmlElement record)
	{
		string Alias = record.Attributes["alias"].Value;

		if (record.Attributes["brain"] is null)
		{
			string BrainInfo = null;

			if (record.Attributes["boss-npc"] != null) BrainInfo = "Boss";
			else if (Alias.MyStartsWith("CH_") || Alias.MyStartsWith("CE_")) BrainInfo = "Citizen";
			else if (Alias.MyStartsWith("MH_") || Alias.MyStartsWith("ME_")) BrainInfo = "Monster";
			else return;

			record.SetAttribute("brain", BrainInfo);
			record.SetAttribute("brain-parameters", Alias + "_bp");
		}

		if (record.Attributes["formal-radius"] is null)
		{
			if (record.Attributes["radius"] is not null)
				record.SetAttribute("formal-radius", record.Attributes["radius"].Value);
		}
	}
}