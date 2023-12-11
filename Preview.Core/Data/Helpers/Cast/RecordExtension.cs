using System.Reflection;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Helpers;
public static partial class RecordExtension
{
	
	public static T Get<T>(this T[] array, int num) => array.Length < num ? default : array[num - 1];
}