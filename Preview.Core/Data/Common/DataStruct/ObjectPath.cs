namespace Xylia.Preview.Data.Common.DataStruct;
public struct ObjectPath
{
	public readonly string Path;

	public ObjectPath(string path)
	{
		Path = path;
	}


	public readonly override string ToString() => this.Path;

	public static implicit operator ObjectPath(string path) => new(path);
	public static implicit operator string(ObjectPath obj) => obj.Path;
}