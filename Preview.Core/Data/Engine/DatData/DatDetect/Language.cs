using System.ComponentModel;
using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Engine.DatData;
public enum ELanguage
{
	None,

	[Name("ko-KR")]
	[Description("한국어 (Korean)")]
	Korean,

	[Name("en-US")]
	[Description("English")]
	English,

	[Name("zh-CN")]
	[Description("中文简体 (Chinese)")]
	ChineseS,

	//[Name("zh-TW")]
	[Description("中文繁體 (Traditional Chinese)")]
	ChineseT,

	[Name("ja-JP")]
	[Description("日本語 (Japanese)")]
	Japanese,
}

public enum Publisher 
{
	None = -1,
	NcKorean,
	Tencent,
	Innova,
	NcJapan,
	Sea,
	NcTaiwan,
	NcWest,
	Garena,
}