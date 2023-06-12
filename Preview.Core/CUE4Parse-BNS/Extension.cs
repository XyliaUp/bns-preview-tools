using System.Text;

using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;

namespace CUE4Parse.BNS;

public static class Extension
{
	public static string GetPathName_Bns(this UObject obj)
	{
		var result = new StringBuilder();

		obj.GetPathName_Bns(result);
		return result.ToString();
	}

	public static void GetPathName_Bns(this UObject obj, StringBuilder resultString)
	{
		var objOuter = obj.Outer;
		if (objOuter != null)
		{
			if (objOuter.Outer is IPackage)
				objOuter = objOuter.Outer;

			objOuter.GetPathName_Bns(resultString);
			resultString.Append('.');
		}

		resultString.Append(obj.Name);
	}
}