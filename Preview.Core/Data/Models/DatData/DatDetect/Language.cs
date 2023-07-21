using System.ComponentModel;

namespace Xylia.Preview.Data.Models.DatData.DatDetect;
public enum Language
{
	None,

	[Description("中国大陆 (腾讯)")]
	ChineseS,

	[Description("中國台灣 (NCT)")]
	ChineseT,

	[Description("English")]
	English,

	[Description("日本語")]
	Japanese,

	[Description("한국어")]
	Korean,
}

public enum Publisher
{
	Default,  //NcKorean
	Tencent,
	Innova,
	NcJapan,
	Sea,
	NcTaiwan,
	NcWest,
	Garena,
}