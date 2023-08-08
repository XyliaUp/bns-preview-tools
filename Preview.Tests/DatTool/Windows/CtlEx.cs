using System.Text;

namespace Xylia.Preview.Tests.DatTool.Windows;
public static class CtlEx
{
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
}