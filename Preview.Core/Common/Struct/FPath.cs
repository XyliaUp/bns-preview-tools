namespace Xylia.Preview.Common.Struct;
public struct FPath
{
	public string Path;

	public FPath(string path)
	{
		Path = path;
	}


	public override string ToString() => this.Path;

	public static implicit operator FPath(string path) => new(path);
}