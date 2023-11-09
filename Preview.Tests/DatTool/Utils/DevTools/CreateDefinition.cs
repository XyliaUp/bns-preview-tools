using System.Xml;
using System.Xml.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Engine.BinData.Definitions;

namespace Xylia.Preview.Tests.DatTool.Utils.DevTools;

[TestClass]
public class CreateDefinition
{
    public Dictionary<short, string> tables;

    //[TestMethod]
    public void Test()
    {
        tables = new();
        foreach (var file in new DirectoryInfo(@"F:\Build\剑灵\Dump\Dump 数据").GetFiles())
        {
            XmlDocument doc = new();
            doc.Load(file.FullName);

            var TableDef = doc.SelectSingleNode("table/TableDef");
            var name = TableDef.SelectSingleNode("./name").InnerText;
            var type = TableDef.SelectSingleNode("./type").InnerText.ToInt16();

            tables[type] = name;
        }

        foreach (var file in new DirectoryInfo(@"F:\Build\剑灵\Dump\Dump 数据").GetFiles())
        {
            XmlDocument doc = new();
            doc.Load(file.FullName);

            var TableDef = doc.SelectSingleNode("table/TableDef");
            var name = TableDef.SelectSingleNode("./name").InnerText;
            var major_ver = TableDef.SelectSingleNode("./major_ver").InnerText;
            var minor_ver = TableDef.SelectSingleNode("./minor_ver").InnerText;
            var elCount = TableDef.SelectSingleNode("./elCount").InnerText.ToInt8();
            var maxid = TableDef.SelectSingleNode("./maxid").InnerText.ToInt32();
            var autokey = TableDef.SelectSingleNode("./autokey").InnerText.ToInt8() == 1;
            var module = TableDef.SelectSingleNode("./module").InnerText;


            var table = new XElement("table");
            table.SetAttributeValue("name", name);
            table.SetAttributeValue("version", major_ver + "." + minor_ver);
            table.SetAttributeValue("autokey", autokey);
            table.SetAttributeValue("module", module);
            if (maxid != 0) table.SetAttributeValue("maxid", maxid);


            foreach (XmlNode elementDef in TableDef.SelectNodes("./DrElBodyDef/array"))
            {
                var el_name = elementDef.SelectSingleNode("./name").InnerText;
                var el_type = elementDef.SelectSingleNode("./type").InnerText;
                var el_child = elementDef.SelectSingleNode("./child").InnerText.ToInt8();

                var bodyDef = elementDef.SelectSingleNode("./ElBodyDef");

                var parent = table;
                if (elCount > 0)
                {
                    var el = parent = new XElement("el");
                    table.Add(el);

                    el.SetAttributeValue("name", el_name);
                    if (el_child != 0) el.SetAttributeValue("child", el_child);
                }

                LoadAttr(bodyDef, parent);
                foreach (XmlNode Sub in bodyDef.SelectNodes("./sub/subrecord"))
                {
                    var _sub = new XElement(name: "sub");
                    parent.Add(_sub);

                    var sub_name = Sub.Attributes["name"].Value;
                    _sub.SetAttributeValue("name", sub_name);

                    LoadAttr(Sub, _sub);
                }
            }

            File.WriteAllText($@"F:\Build\剑灵\Dump\Defs\{name.TitleCase()}.xml",
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" + table.ToString());
        }
    }

    public void LoadAttr(XmlNode node, XElement parent)
    {
        foreach (XmlNode record in node.SelectNodes("./attr/attrrecord"))
        {
            var type = (AttributeType)record.Attributes["type"].Value.ToInt8();
            var name = record.Attributes["name"].Value;
            var required = record.Attributes["required"].Value.ToInt8() == 1;
            var key = record.Attributes["key"].Value.ToInt8() == 1;
            var offset = record.Attributes["offset"].Value;
            var refel = record.Attributes["refel"].Value;
            var size = record.Attributes["size"].Value;
            var dynamic = record.Attributes["dynamic"].Value;
            var hash = record.Attributes["hash"].Value;
            var propfield = record.Attributes["propfield"]?.Value;
            var deprecated = record.Attributes["deprecated"].Value.ToInt8() == 1;
            var repeat = record.Attributes["repeat"].Value.ToInt8();
            var hidden = record.Attributes["hidden"].Value.ToInt8() == 1;
            var min = record.Attributes["min"]?.Value;
            var max = record.Attributes["max"]?.Value;
            var defvalue = record.Attributes["defvalue"].Value;
            var reftable = record.Attributes["reftable"].Value.ToInt16();
            var seqcount = record.Attributes["seqcount"].Value;
            var seq = record.SelectSingleNode("seq")?.InnerText.Split(", ");



            #region XElement
            var xe = new XElement("record");
            parent.Add(xe);

            xe.SetAttributeValue("name", name);
            xe.SetAttributeValue("type", type.ToString()[1..]);


            bool UseDefault = true;
            if (type == AttributeType.TBool && defvalue == "n") UseDefault = false;
            else if (defvalue == "") UseDefault = false;


            if (type == AttributeType.TRef) xe.SetAttributeValue("ref", tables.GetValueOrDefault(reftable));
            else if (type == AttributeType.TSeq || type == AttributeType.TSeq16 || type == AttributeType.TProp_seq || type == AttributeType.TProp_field)
            {
                void SetSeq(string name) => xe.SetAttributeValue("seq", name);

                var seqs = record.SelectSingleNode("./seq").InnerText.Split(", ");
                if (seqs.Length == 6 && seqs[0] == "eq" && seqs[1] == "neq") SetSeq("op2");
                else if (seqs.Length == 2 && seqs[0] == "and" && seqs[1] == "or") SetSeq("check");
                else if (seqs.Length == 2 && seqs[1] == "and" && seqs[0] == "or") SetSeq("op");
                else if (seqs[0] == "creature_field_none") SetSeq("creature_field");
                else if (seqs.Length >= 7 && seqs[1] == "not-dead-restoration") SetSeq("dead-state");
                else if (seqs.Length == 4 && seqs[1] == "easy" && seqs[2] == "normal" && seqs[3] == "hard") SetSeq("difficulty-type");
                else if (seqs[0] == "job-none") SetSeq("job");
                else if (seqs.Length == 10 && seqs[0] == "base-1" && seqs[5] == "advanced-1") SetSeq("job-style");
                else if (seqs.Length == 10 && seqs[2] == "champion" && seqs[6] == "weakest") SetSeq("npc-grade2");
                else if (seqs.Length == 15 && seqs[1] == "production-type-1" && seqs[8] == "gathering-type-1") SetSeq("production-type");
                else if (seqs[0] == "race-none") SetSeq("race");
                else if (seqs[0] == "sex-none") SetSeq("sex");
                else if (seqs.Length >= 20 && seqs[1] == "attack-power-creature-min-max") SetSeq("attach-ability");
                else if (seqs.Length >= 50 && seqs[5] == "attack-hit-value") SetSeq("main-ability");
                else if (seqs.Length >= 20 && seqs[5] == "dead-swim-area") SetSeq("idle-type");
                else if (seqs.Length >= 20 && seqs[4] == "blade-master-sword") SetSeq("condition-type");
                else if (seqs.Length >= 30 && seqs[8] == "gem-1") SetSeq("equip-type");
                else if (seqs.Length >= 30 && seqs[3] == "distribution-pouch") SetSeq("pouch-appearance");
                else if (seqs.Length >= 120 && seqs[4] == "bind-phantom") SetSeq("flag");
                else if (seqs.Length >= 120 && seqs[9] == "skill-slot1") SetSeq("key-command");
                else if (seqs.Length >= 20 && seqs[4] == "jade") SetSeq("weapon-gem-type");
                else if (seqs.Length >= 20 && seqs[5] == "거미줄") SetSeq("effect-attribute");
                else if (seqs.Length >= 20 && seqs[4] == "방어행동") SetSeq("skill-attribute");
                else if (seqs.Length >= 20 && seqs[5] == "perfect-parry") SetSeq("skill-result");
                else if (seqs.Length == 5 && seqs[1] == "open" && seqs[3] == "enable") SetSeq("env-operation");
                else if (seqs.Length == 11 && seqs[1] == "open" && seqs[10] == "step-7") SetSeq("env-state");
                else if (seqs.Length == 4 && seqs[1] == "effect-owner") SetSeq("effect-target");
                else if (seqs.Length >= 10 && seqs[0] == "heal-potion" && seqs[2] == "buff-item-01") SetSeq("grocery-effect-type");
                else if (seqs.Length == 3 && seqs[1] == "not-attach" && seqs[2] == "dispel") SetSeq("link");
                else if (seqs.Length == 3 && seqs[1] == "stage-1" && seqs[2] == "stage-2") SetSeq("link-stage");
                else if (seqs.Length == 3 && seqs[1] == "link" && seqs[2] == "linked") SetSeq("link-state");

                else if (seqs[0] == "stance-none") SetSeq("stance");
                else if (seqs.Length >= 15 && seqs[1] == "bare-hand" && seqs[5] == "pistol") SetSeq("weapon-type");

                else
                {
                    UseDefault = false;
                    foreach (var s in seqs)
                    {
                        var _seq = new XElement("case");
                        xe.Add(_seq);

                        _seq.Add(new XAttribute("alias", s));
                        if (s == defvalue) _seq.Add(new XAttribute("default", "y"));
                    }
                }
            }



            if (UseDefault) xe.SetAttributeValue("default", defvalue);
            if (repeat != 1) xe.SetAttributeValue("repeat", repeat);

            if (key) xe.SetAttributeValue("key", "y");
            if (required) xe.SetAttributeValue("required", "y");
            if (hidden) xe.SetAttributeValue("hidden", "y");
            if (deprecated) xe.SetAttributeValue("deprecated", "y");


            if (type == AttributeType.TInt8)
            {
                if (min is null || min != sbyte.MinValue.ToString()) xe.SetAttributeValue("min", min);
                if (max is null || max != sbyte.MaxValue.ToString()) xe.SetAttributeValue("max", max);
            }
            else if (type == AttributeType.TInt16 || type == AttributeType.TDistance || type == AttributeType.TVelocity || type == AttributeType.TVector16)
            {
                if (min is null || min != short.MinValue.ToString()) xe.SetAttributeValue("min", min);
                if (max is null || max != short.MaxValue.ToString()) xe.SetAttributeValue("max", max);
            }
            else if (type == AttributeType.TInt32 || type == AttributeType.TMsec || type == AttributeType.TVector32)
            {
                if (min is null || min != (-int.MaxValue).ToString()) xe.SetAttributeValue("min", min);
                if (max is null || max != int.MaxValue.ToString()) xe.SetAttributeValue("max", max);
            }
            else if (type == AttributeType.TInt64)
            {
                if (min is null || min != (-long.MaxValue).ToString()) xe.SetAttributeValue("min", min);
                if (max is null || max != long.MaxValue.ToString()) xe.SetAttributeValue("max", max);
            }
            else if (type == AttributeType.TFloat32)
            {
                var fmax = record.Attributes["fmax"].Value;
                var fmin = record.Attributes["fmin"].Value;

                if (fmax != "-340282346638528859811704183484516925440.0") xe.SetAttributeValue("min", fmin);
                if (fmin != "340282346638528859811704183484516925440.0") xe.SetAttributeValue("max", fmax);
            }
            else if (type == AttributeType.TXUnknown2)
            {
                if (min != "0") xe.SetAttributeValue("min", min);
                if (max != "256") xe.SetAttributeValue("max", max);
            }
            else
            {
                if (min != "0") xe.SetAttributeValue("min", min);
                if (max != "0") xe.SetAttributeValue("max", max);
            }
            #endregion
        }
    }



    //[TestMethod]
    public void Test2()
    {
        foreach (var file in new DirectoryInfo(@"F:\Resources\文档\Programming\C#\Xylia\bns\bns-preview-tools\Preview.Core\Data\Records\TableDef").GetFiles())
        {
            XmlDocument doc = new();
            doc.Load(file.FullName);

            doc.Save(@"F:\Resources\文档\Programming\C#\Xylia\bns\bns-preview-tools\Preview.Core\Data\Records\新建文件夹\" + file.Name.Replace("Data", null));
        }
    }
}