using System.Xml;

using Xylia.Extension;
using Xylia.Preview.Tests.DatTool.Utils.ServerHandle;

namespace Xylia.Preview.Tests.DatTool.Utils.ServerHandle;
public class Reward : TableHandle
{
	protected override void Fix(XmlElement record)
	{
		if (record.Attributes["rare-item-1"] != null && record.Attributes["rare-item-1-max"] is null)
		{
			record.SetAttribute("rare-item-1-max", "1");
			record.SetAttribute("rare-item-1-min", "1");

			for (int i = 1; i <= 40; i++)
			{
				if (record.Attributes["rare-item-" + i] != null)
				{
					if (record.Attributes["rare-item-" + i].Value.MyContains("Grocery_Festival_")) record.SetAttribute("rare-item-prob-weight-" + i, "1");
					else record.SetAttribute("rare-item-prob-weight-" + i, "50");
				}
			}
		}
		if (record.Attributes["group-3-item-1"] != null && record.Attributes["group-3-probability"] is null)
		{
			record.SetAttribute("group-3-probability", "1000");

			for (int i = 1; i <= 35; i++)
			{
				if (record.Attributes["group-3-item-" + i] != null)
				{
					record.SetAttribute("group-3-item-prob-weight-" + i, "3");
				}
			}
		}

		if (record.Attributes["group-1-2-probability"] is null)
		{
			bool UseGroup1 = false;
			bool UseGroup2 = false;

			if (record.Attributes["group-2-item-1"] != null)
			{
				record.SetAttribute("group-2-assured-count", "1");
				UseGroup2 = true;
			}

			if (record.Attributes["group-1-item-1"] != null)
			{
				record.SetAttribute("group-1-assured-count", "1");
				UseGroup1 = true;
			}

			if (UseGroup1 || UseGroup2)
			{
				record.SetAttribute("group-1-2-probability", "100");

				if (UseGroup1) record.SetAttribute("group-1-prob-weight", UseGroup1 && UseGroup2 ? "50" : "100");
				if (UseGroup2) record.SetAttribute("group-2-prob-weight", UseGroup1 && UseGroup2 ? "50" : "100");
			}
		}
	}
}