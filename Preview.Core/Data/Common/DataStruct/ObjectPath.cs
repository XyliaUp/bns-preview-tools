namespace Xylia.Preview.Data.Common.DataStruct;
public readonly struct ObjectPath(string path)
{
	public readonly string Path = path;

	public override string ToString() => this.Path;

	public static implicit operator string(ObjectPath obj) => obj.Path;
}