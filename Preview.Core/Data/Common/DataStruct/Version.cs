namespace Xylia.Preview.Data.Common.DataStruct;
public struct Version
{
	public ushort Major { get; }
	public ushort Minor { get; }
	public ushort Build { get; }
	public ushort Revision { get; }

	public Version(string data)
	{
		var strings = data.Split('.');

		this.Major = ushort.Parse(strings[0]);
		this.Minor = ushort.Parse(strings[1]);
		this.Build = ushort.Parse(strings[2]);
		this.Revision = ushort.Parse(strings[3]);
	}


	public readonly override string ToString() => $"{Major}.{Minor}.{Build}.{Revision}";
}