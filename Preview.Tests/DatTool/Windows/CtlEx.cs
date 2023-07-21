using System.Diagnostics;
using System.Text;

using Xylia.Configure;
using Xylia.Extension;

namespace Xylia.Preview.Tests.DatTool.Windows;
public static class CtlEx
{
    public static void ReadConfig(this Control container, string Section = null)
    {
        foreach (Control c in container.Controls)
        {
            var val = Ini.ReadValue("Config", (Section ?? container.FindForm().Name) + "_" + c.Name);

            if (c is TextBox) c.GetPath(Section);
            else if (c is CheckBox checkBox && !string.IsNullOrWhiteSpace(val)) checkBox.Checked = val.ToBool();

            //递归子控件
            c.ReadConfig(Section);
        }
    }

    public static void SavePath(this Control Ctl, uint RepeatTime, bool MustExist = true) => Ctl.SavePath(null, null, RepeatTime, MustExist);

    public static void SavePath(this Control Ctl, string Key = null, string Section = null, uint RepeatTime = 0, bool MustExist = true)
    {
        string Path = Ctl.Text;

        if (Path.Contains('|')) return;


        if (!MustExist || Directory.Exists(Path) || File.Exists(Path) || string.IsNullOrWhiteSpace(Path))
        {
            string ConfigValue = Key ?? Ctl.Name.Replace("Txt_", null);

            if (RepeatTime != 0) ConfigValue += "_" + RepeatTime;

            Ini.WriteValue("Path", ConfigValue, string.IsNullOrWhiteSpace(Path) ? "null" : Path);
        }
    }


    public static void GetPath(this Control Ctl, string Section = null, string Key = null, uint RepeatTime = 0)
    {
        string ConfigKey = Key ?? Ctl.Name.Replace("Txt_", null);
        if (RepeatTime != 0) ConfigKey += "_" + RepeatTime;

        string Value = Ini.ReadValue(Section ?? "Path", ConfigKey);

        if (Value == "null") Ctl.Text = null;
        else if (!string.IsNullOrWhiteSpace(Value)) Ctl.Text = Value;
    }



    public static IEnumerable<string> OpenPath(this Control LinkCtl, string Filter = null, bool Multiselect = false)
    {
        var openFile = new OpenFileDialog();

        string FilePath = LinkCtl.Text;
        if (!string.IsNullOrWhiteSpace(FilePath) && !FilePath.Contains('|'))
        {
            openFile.InitialDirectory = Directory.Exists(FilePath) ? FilePath : Path.GetDirectoryName(FilePath);
        }


        openFile.Filter = (Filter == null ? null : Filter + "|") + "所有文件|*";
        openFile.Multiselect = Multiselect;

        if (openFile.ShowDialog() == DialogResult.OK)
        {
            if (openFile.FileNames.Length == 1) LinkCtl.Text = openFile.FileName;
            else
            {
                StringBuilder sb = new();

                foreach (var f in openFile.FileNames)
                    sb.Append(f + "|");

                LinkCtl.Text = sb.ToString();
            }

            return openFile.FileNames;
        }

        return null;
    }

    public static void OpenDirPath(this Control LinkCtl)
    {
        FolderBrowserDialog Folder = new();

        // *************************************************************** //
        string CPath = LinkCtl.Text;
        if (!string.IsNullOrWhiteSpace(CPath))
        {
            while (!Directory.Exists(CPath)) CPath = Path.GetDirectoryName(CPath);

            Folder.SelectedPath = CPath;
        }
        // *************************************************************** //

        if (Folder.ShowDialog() == DialogResult.OK)
        {
            LinkCtl.Text = Folder.SelectedPath;
        }
    }


    private static DateTime m_LastTime = DateTime.MinValue;
    public static void DoubleClickPath(this Control LinkCtl)
    {
        #region 必须间隔2秒以上才能再次打开
        if (DateTime.Now.Subtract(m_LastTime).TotalSeconds <= 2) return;

        m_LastTime = DateTime.Now;
        #endregion



        var selected = LinkCtl.Text?.Trim();
        if (selected.Contains('|'))
        {
            var FilePathes = selected.Split('|');
            selected = FilePathes[0];
        }

        if (Directory.Exists(selected))
        {
            Process.Start("Explorer.exe", selected);
            return;
        }

        ProcessStartInfo psi = new("Explorer.exe");
        psi.Arguments = "/e,/select," + selected;
        Process.Start(psi);
    }
}