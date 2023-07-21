using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Common.Extension;
public static partial class NumExtension
{
	public static string GetCount(int count) => " " + new ContentParams(count).HandleText("<arg p=\"1:integer\"/>个");
}