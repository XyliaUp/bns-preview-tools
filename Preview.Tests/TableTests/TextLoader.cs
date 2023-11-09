using System.Data;
using System.IO;
using System.Xml;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Tests.TableTests;
public class Loader<T> : List<T> where T : Record
{
    // else if (t.Text.Contains('<') || t.Text.Contains('>') || t.Text.Contains('&')) Xe.AppendChild(xi.CreateCData(t.Text));



    /// <summary>
    /// data handle mode
    /// </summary>
    public bool NewCreat = false;


    public void LoadDirectory(string DirectoryPath)
    {
        if (string.IsNullOrWhiteSpace(DirectoryPath) || !Directory.Exists(DirectoryPath))
            return;

        NewCreat = true;
        LoadDirectory(new DirectoryInfo(DirectoryPath));
    }

    private void LoadDirectory(DirectoryInfo DirInfo)
    {
        foreach (var FileInfo in DirInfo.GetFiles())
            Load(FileInfo);

        foreach (var SubDirInfo in DirInfo.GetDirectories())
            LoadDirectory(SubDirInfo);
    }


    public void Load(FileInfo file)
    {
        switch (file.Extension)
        {
            case ".xls":
            case ".xlsx":
                {
                    //#region target sheet
                    //List<string> DataName = new();

                    //var Sheets = file.FullName.GetSheetName(out IWorkbook Workbook);
                    //if (Sheets.Length == 1) DataName.Add(Sheets[0]);
                    //else
                    //{
                    //	//var listSelect = new ListSelect(Sheets);
                    //	//listSelect.ShowDialog();

                    //	//DataName = listSelect.Result;
                    //}

                    //if (DataName == null) throw new ArgumentNullException(nameof(DataName));
                    //else if (DataName.Count == 0) throw new ArgumentException("请先选择工作表");
                    //#endregion


                    //foreach (var SheetName in DataName)
                    //{
                    //	var dataDt = Workbook.ExcelToDataTable(SheetName);
                    //	foreach (DataRow row in dataDt.Rows)
                    //		this.AddItem(Test(row));
                    //}
                }
                break;


            case ".xml" or ".x16":
                {
                    var document = new XmlDocument();
                    document.Load(file.FullName);

                    foreach (XmlElement row in document.SelectNodes("table/record").OfType<XmlElement>())
                        this.AddItem(Test(row));
                }
                break;

            default: throw new Exception("导入类型暂不支持");
        }
    }


    protected virtual T Test(DataRow row) => throw new NotImplementedException();

    protected virtual T Test(XmlElement row) => throw new NotImplementedException();
}

public sealed class TextLoader : Loader<Text>
{
    protected override Text Test(DataRow row)
    {
        string alias = row["A"].ToString();
        string text = row["B"].ToString();
        if (string.IsNullOrWhiteSpace(alias) || alias == "alias") return null;

        return new Text() { Alias = alias, text = text };
    }

    protected override Text Test(XmlElement row)
    {
        string alias = row.Attributes["alias"]?.Value;
        string text = row.Attributes["text"]?.Value ?? row.InnerXml;

        //由于解析库不会保留空格
        text = text.Replace("&#160;", " ").Replace("&#38;", "&");
        return new Text() { Alias = alias, text = text };
    }
}