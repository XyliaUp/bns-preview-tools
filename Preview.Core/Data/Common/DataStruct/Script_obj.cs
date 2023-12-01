using System.Runtime.InteropServices;

namespace Xylia.Preview.Data.Common.DataStruct;

[StructLayout(LayoutKind.Sequential)]
public struct Script_obj
{
	public string Full { get; set; }

	public Script_obj(string Text) => Full = Text;
}


[StructLayout(LayoutKind.Sequential)]
public struct Script_obj_Test
{
	public int p1;
	public int p2;
	public int p3;
	public int p4;
	public int p5;
}