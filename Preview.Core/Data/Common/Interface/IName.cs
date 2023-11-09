namespace Xylia.Preview.Data.Common.Interface;
public interface IName
{
	/// <summary>
	/// get object text
	/// </summary>
	string Text { get; }
}

public interface IAttraction : IName
{
	string GetDescribe();
}