namespace Xylia.Preview.Data.Engine.DatData;
public interface IDatSelect
{
	/// <summary>
	/// default dat select dialog 
	/// </summary>
	public static IDatSelect Default;

	DefaultProvider Show(IEnumerable<FileInfo> Xml, IEnumerable<FileInfo> Local);
}