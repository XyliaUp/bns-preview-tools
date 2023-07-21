using System.Data;
using System.Diagnostics;

using Xylia.Extension;

namespace Xylia.Preview.Tests.DatTool.Utils;

public static class BNSFileHelper
{
	public static string Bak_Folder = @"E:\Game\bak_auto";

	public static void Backup(string FilePath)
    {
        FileInfo fi = new(FilePath);
        string SHA1 = fi.GetFileSHA1();

        string FileName = Path.GetFileNameWithoutExtension(FilePath);
        string FolderPath = Bak_Folder + $@"\{FileName}";
        string ExplainTxt = Bak_Folder + @"\目录说明.txt";

        if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);
        else
        {
            //要求计算文件sha值，对于目录下sha值一致的文件不进行重复备份
            var SHA1s = new DirectoryInfo(FolderPath).GetFiles().Select(f => f.GetFileSHA1()).ToList();
            if (SHA1s.Contains(SHA1)) return;
        }


        if (!File.Exists(ExplainTxt)) File.WriteAllText(ExplainTxt,
            "这是自动生成的备份目录。\n\n" +
            "目录中的一级文件夹名代表当前备份时间\n" +
            "二级文件夹名代表.dat文件的最后修改时间");


        string Combine_Folder = FolderPath + @"\" + DateTime.Now.ToString("MM月dd号 HH时mm分") + @"\" + fi.LastWriteTime.ToString("MM月dd号 HH时mm分");
        if (!Directory.Exists(Combine_Folder)) Directory.CreateDirectory(Combine_Folder);

        File.Copy(fi.FullName, Combine_Folder + @"\" + FileName + fi.Extension, true);
    }



    #region SeriFile
    public static string SeriSourceFolder = @"F:\Bns";

    public static string PublicOutFolder = @"F:\Bns\Rebuild";


    private static string Seri_GetFileName(string TableName) => $"{TableName.RemoveSuffixString("Data")}Data*.xml";

    public static FileInfo[] GetFiles(string TableName, string MainFoloer = null) => Seri_DataList(TableName, MainFoloer ?? SeriSourceFolder);

    public static FileInfo[] Seri_DataList(string TableName, params string[] DirInfos) => Seri_DataList(TableName, DirInfos.Select(dir => new DirectoryInfo(dir)).ToArray());

    public static FileInfo[] Seri_DataList(string TableName, params DirectoryInfo[] DirInfos)
    {
        var result = DirInfos.SelectMany(dir =>
        {
            //只有物品类型是特殊的
            if (TableName.Equals("item"))
            {
                List<FileInfo> tmp = new();

                tmp.AddRange(dir.GetFiles(Seri_GetFileName("Item"), SearchOption.AllDirectories));
                tmp.AddRange(dir.GetFiles(Seri_GetFileName("Accessory"), SearchOption.AllDirectories));
                tmp.AddRange(dir.GetFiles(Seri_GetFileName("Gem"), SearchOption.AllDirectories));
                tmp.AddRange(dir.GetFiles(Seri_GetFileName("Grocery"), SearchOption.AllDirectories));
                tmp.AddRange(dir.GetFiles(Seri_GetFileName("Costume"), SearchOption.AllDirectories));
                tmp.AddRange(dir.GetFiles(Seri_GetFileName("Weapon"), SearchOption.AllDirectories));

                return tmp.ToArray();
            }

            return dir.GetFiles(Seri_GetFileName(TableName), SearchOption.AllDirectories);
        })
           .Where(data => !data.DirectoryName.MyEndsWith("\\server"))     //排除掉生成的服务端数据
           .ToArray();

        //调试用
        Trace.WriteLine(TableName + " => " + result.Aggregate(string.Empty, (sum, now) => sum + now.FullName.Replace(now.DirectoryName + "\\", null) + ", "));
        return result;
    }
    #endregion
}