using System.Windows.Threading;

using HZH_Controls.Forms;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helper;

namespace Xylia.Preview.Helper;
public static class Register
{
	public static Dispatcher Dispatcher;

	public static void Main()
	{
		Dispatcher = Dispatcher.CurrentDispatcher;

		PreviewRegister.PreviewEvent += new((obj, w) => obj.PreviewShow(w));
		PreviewRegister.ShowTipEvent += new((s, e) => FrmTips.ShowTipsSuccess((string)s));
	}
}