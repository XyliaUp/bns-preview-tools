using System.Xml;

namespace Xylia.Preview.Tests.DatTool.Utils.ServerHandle;
public class Npc : TableHandle
{
    protected override void Fix(XmlElement record)
    {
        string alias = record.Attributes["alias"].Value;
        var comparer = StringComparison.OrdinalIgnoreCase;

        if (record.Attributes["brain"] is null)
        {
            string BrainInfo = null;

            if (record.Attributes["boss-npc"] != null) BrainInfo = "Boss";
            else if (alias.StartsWith("CH_", comparer) || alias.StartsWith("CE_", comparer)) BrainInfo = "Citizen";
            else if (alias.StartsWith("MH_", comparer) || alias.StartsWith("ME_", comparer)) BrainInfo = "Monster";
            else return;

            record.SetAttribute("brain", BrainInfo);
            record.SetAttribute("brain-parameters", alias + "_bp");
        }

        if (record.Attributes["formal-radius"] is null)
        {
            if (record.Attributes["radius"] is not null)
                record.SetAttribute("formal-radius", record.Attributes["radius"].Value);
        }
    }
}