using System.Xml;

namespace Xylia.Preview.Tests.DatTool.Utils.ServerHandle;
public abstract class TableHandle
{
    protected virtual string Name => GetType().Name;


    private void FixTable()
    {
        ReadTable(Name, (record) => Fix(record), true);
    }

    protected virtual void Fix(XmlElement record)
    {

    }


    protected static void ReadTable(string table, Action<XmlElement> fix, bool save = false)
    {
        foreach (var path in BNSFileHelper.GetFiles(table + "Data"))
        {
            var data = new XmlDocument();
            data.Load(path.FullName);

            foreach (var record in data.SelectNodes("table/record").OfType<XmlElement>())
                fix?.Invoke(record);


            if (save) data.Save(path.FullName);
        }
    }
}