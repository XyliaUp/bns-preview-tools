namespace CUE4Parse.Compression;
public enum CompressedStatus
{
	/// <summary>
	/// 不进行压缩
	/// </summary>
	None,

	/// <summary>
	/// 指示是压缩文件，但是数据还未加密
	/// </summary>
	IsCompressed,

	/// <summary>
	/// 已经完成压缩
	/// </summary>
	AlreadyCompressed,
}