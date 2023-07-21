using System.Xml;

namespace Xylia.Preview.Tests.DatTool.Utils;

public static class MyComparer
{
    public static void AliasModify(string OldPath, string NewPath)
    {
        var AliasGroup = new List<string>();

        var doc = new XmlDocument();
        doc.Load(OldPath);
        var Alias1 = doc.SelectNodes("table/record").OfType<XmlElement>().Select(node => node.Attributes["alias"].Value);

        var doc2 = new XmlDocument();
        doc2.Load(NewPath);
        var Alias2 = doc2.SelectNodes("table/record").OfType<XmlElement>().Select(node => node.Attributes["alias"].Value);


        var ExceptA = Alias1.Except(Alias2).ToList();
        var ExceptB = Alias2.Except(Alias1).ToList();

        System.Diagnostics.Trace.WriteLine($"Add {ExceptB.Count}，Remove {ExceptA.Count}");

        foreach (var ea in ExceptA)
        {
            System.Diagnostics.Trace.WriteLine(ea);
        }

        System.Diagnostics.Trace.WriteLine($" ↑ Remove =============================== Add ↓");

        foreach (var eb in ExceptB)
        {
            System.Diagnostics.Trace.WriteLine(eb);
        }
    }
}