using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class ZoneConvoy : Record
{
	// 护送只能在 cave2 定义区域生成
	// cannot start convoy before progress mission
	// 无法在执行任务前开始护卫
	// acquisition 的子对象护卫不生效


	public string Alias;
}