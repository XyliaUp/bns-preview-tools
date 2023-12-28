using System.Diagnostics;
using System.IO;
using System.Text;

namespace Xylia.Preview.UI.Helpers;
/// <summary>
/// redirect console to debug
/// </summary>
public class ConsoleRedirect : TextWriter
{
	public override Encoding Encoding => Encoding.UTF8;

	public override void WriteLine(string value)
	{
		// base.WriteLine(value);
		Debug.WriteLine(value);
	}
}