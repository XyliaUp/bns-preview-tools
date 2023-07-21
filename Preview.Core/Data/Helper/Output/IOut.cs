namespace Xylia.Preview.Data.Helper.Output;
public interface IOut
{
    OutSetTable OutTable();
}

public class FileConfigOut : IOut
{
	public OutSetTable OutTable() => null;
}