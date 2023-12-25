namespace Xylia.Preview.Data.Engine.BinData.Serialization;
/// <summary>
/// Respents a set of features to support on publish package data
/// </summary>
public sealed class PublishSettings
{
	public bool Is64bit { get; set; }

	public bool RebuildAliasMap { get; set; }

	public Mode Mode { get; set; }
}

public enum Mode
{
	/// <summary>
	/// Raw datafile
	/// </summary>
	Datafile,

	/// <summary>
	/// Create new data package
	/// </summary>
	Package,

	/// <summary>
	/// Create new data package by component
	/// </summary>
	PackageThird,
}