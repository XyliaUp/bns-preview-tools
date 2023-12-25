namespace Xylia.Preview.Data.Engine.DatData;
public static class PATH
{
	public static string Xml(bool Is64Bit) => Is64Bit ? "xml64.dat" : "xml.dat";
	public static string Local(bool Is64Bit) => Is64Bit ? "local64.dat" : "local.dat";
	public static string Config(bool Is64Bit) => Is64Bit ? "config64.dat" : "config.dat";

	public static string Datafile(bool Is64Bit) => Is64Bit ? "datafile64.bin" : "datafile.bin";
	public static string Localfile(bool Is64Bit) => Is64Bit ? "localfile64.bin" : "localfile.bin";
}