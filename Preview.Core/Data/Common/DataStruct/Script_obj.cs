using System.Runtime.InteropServices;
using System.Text;

namespace Xylia.Preview.Data.Common.DataStruct;

[StructLayout(LayoutKind.Sequential, Size = 20)]
public struct Script_obj
{
	string Full;

	public Script_obj(string text)
	{
		Full = text;

		if(text != null)
		{
			var x = Encoding.UTF8.GetBytes(Full);
		}
	}

	public override string ToString() => this.Full;
}