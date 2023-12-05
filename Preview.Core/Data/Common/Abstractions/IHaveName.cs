namespace Xylia.Preview.Data.Common.Abstractions;
public interface IHaveName
{
	string Text { get; }
}

public interface IAttraction : IHaveName
{
	string Describe { get; }
}