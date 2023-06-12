using System.Text.RegularExpressions;

using Xylia.Preview.Common.Attribute;
using Xylia.Extension;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class Text : BaseRecord
	{
		public string text;
	}

	/// <summary>
	/// 提供获取文本扩展Functions
	/// </summary>
	public static class TextExtension
	{
		/// <summary>
		/// 获取对应汉化文本
		/// </summary>
		/// <param name="Alias"></param>
		/// <param name="ReturnNull"></param>
		/// <returns></returns>
		public static string GetText(this string Alias, bool ReturnNull = false)
		{
			var Record = FileCache.Data.TextData[Alias];
			if (Record is null) return ReturnNull ? null : Alias;
			return Record.GetText();
		}

		/// <summary>
		/// 获取对应汉化文本
		/// </summary>
		/// <param name="Text"></param>
		/// <returns></returns>
		public static string GetText(this Text Text)
		{
			if(Text is null) return null;

			//需要通过已Load 的别名重新Load Data
			if (Text.Attributes is null)
				return FileCache.Data.TextData[Text.alias].GetText();

			return Text.Attributes["text"];
		}


		/// <summary>
		/// 剪切文本
		/// </summary>
		/// <param name="Text"></param>
		/// <returns></returns>
		public static string CutText(this string Text)
		{
			if (Text is null) return null;

			var CopyTxt = Text.Decode();
			CopyTxt = new Regex(@"<\s*br\s*/\s*>").Replace(CopyTxt, "\n");
			return new Regex(@"<.*?>").Replace(CopyTxt, "");
		}
	}
}