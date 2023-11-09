using System.Xml;

namespace Xylia.Preview.Tests.DatTool.Utils.ServerHandle;

public class Effect : TableHandle
{
    protected override void Fix(XmlElement record)
    {
        if (record.Attributes["power-percent-max"] is not null)
            return;

        var type = record.Attributes["type"].Value;
        if (type == "melee-physical-attack" ||
            type == "melee-physical-attack-hate" ||
            type == "melee-physical-attack-drain" ||
            type == "force-attack-hp-drain" ||
            type == "force-attack-sp-drain" ||
            type == "melee-physical-attack-sp-drain" ||
            type == "melee-physical-attack-hp-sp-drain" ||
            type == "force-attack-hp-sp-drain" ||
            type == "range-physical-attack" ||
            type == "force-attack" ||
            type == "force-attack-hate"
            )
        {
            record.SetAttribute("power-percent-max", "100");
            record.SetAttribute("power-percent-min", "100");
        }
    }
}