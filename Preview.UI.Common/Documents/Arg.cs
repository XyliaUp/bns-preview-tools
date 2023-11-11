using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

using HtmlAgilityPack;

using Xylia.Preview.Data.Common.Cast;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Documents.Args;

namespace Xylia.Preview.UI.Documents;
public class Arg : Element
{
	public string p;
	public string id;
	public string seq;

	internal object GetObject()
	{
		object obj;

		#region source
		var p = this.p?.Split(':');

		var type = p[0];
		if (type is null) return null;
		// Prevent text editors to load data
		else if (type == "id") obj = FileCache.IsEmpty ? null : new Ref<Record>(this.id).Instance;
		else if (type == "seq")
		{
			var seq = this.seq?.Split(':');
			obj = seq[1].CastSeq(seq[0]);
		}
		else
		{
			if (!byte.TryParse(type, out var index))
				throw new InvalidCastException("bad param, must be byte value: " + type);

			obj = Params?[index];
		}

		if (obj is null) return null;
		#endregion

		#region child
		foreach (var pl in p.Skip(1))
		{
			var args = ArgItem.GetArgs(pl);
			for (int x = 0; x < args.Length; x++)
			{
				if (x == 0) args[0].ValidType(ref obj);
				else obj = args[x].GetObject(obj);
			}
		}
		#endregion


		return obj;
	}

	protected override Size MeasureCore(Size availableSize)
	{
		this.Children.Clear();

		var result = this.GetObject();
		if (result is null) return new Size();
		//else if(result is ImageData b) FinalHeight = Math.Max(FinalHeight, DrawImage(param, ref LocX, ref LocY, b.source, BasicHeight, true /* , b.scale*/));
		else if (result is int @int) this.Children.Add(new Run() { Text = @int.ToString("N0") });
		else if (result is not null) this.Children.Add(new Run() { Text = result.ToString() });

		return base.MeasureCore(availableSize);
	}
}


public sealed class ArgItem
{
	#region Constructor
	public ArgItem(string target) => Target = target;

	public readonly string Target;

	public ArgItem Prev;

	public ArgItem Next;
	#endregion

	#region Methods		
	public override string ToString() => Target;

	public static ArgItem[] GetArgs(string text)
	{
		var args = text.Split('.').Select(o => new ArgItem(o)).ToArray();
		for (int x = 0; x < args.Length; x++)
		{
			if (x != 0)
				args[x].Prev = args[x - 1];

			if (x != args.Length - 1)
				args[x].Next = args[x + 1];
		}

		return args;
	}


	public void ValidType(ref object value)
	{
		var target = Target?.ToLower();

		if (value is null) return;
		if (value is Enum)
		{
			// may be get null value, so keep origin value
			var ori = value;
			if (target == "job" && value is JobSeq job)
				value = job.Convert();

			value ??= ori;
			return;
		}


		var type = value.GetType();
		if (target == "string")
		{
			if (type != typeof(string)) value = value.ToString();
			return;
		}
		else if (target == "integer")
		{
			if (type == typeof(int)) value = new Integer((int)value);
			else if (type == typeof(float)) value = new Integer((float)value);
			else if (type == typeof(short)) value = new Integer((short)value);
			else if (type == typeof(byte)) value = new Integer((byte)value);

			return;
		}
		else if (target.Equals("skill") && value is Skill3) return;
		else if (target.Equals("item-name") && value is Item item)
		{
			value = item.ItemName;
			return;
		}
		else if (value is Integer integer && integer.TryGetParam(target, out var temp))
		{
			value = temp;
			return;
		}

		else
		{
			target = target?.Replace("-", null);
			if (target.Equals(type.Name, StringComparison.OrdinalIgnoreCase)) return;
			else if (target.Equals(type.BaseType?.Name, StringComparison.OrdinalIgnoreCase)) return;

			throw new InvalidCastException($"Valid Failed: {Target} > {type}");
		}
	}

	public object GetObject(object value)
	{
		if (value is null) return null;
		//else if (value is SKBitmap @bitmap) return new ImageData(@bitmap, this);
		//else if (value is ImageData) return value;
		else if (value is string) return value;
		else if (value.GetType().IsClass && value.TryGetParam(Target, out var param)) return param;

		Debug.WriteLine($"not supported class: {value} ({value.GetType().Name} > {Target})");
		return null;
	}
	#endregion
}

public static class ArgExtension
{
	public static string Handle(this ContentParams collection, string Text)
	{
		foreach (Match m in new Regex("<arg.*?/>").Matches(Text).Cast<Match>())
		{
			string html = m.ToString();

			var doc2 = new HtmlDocument();
			doc2.LoadHtml(html);

			try
			{
				var arg = TextDocument.ToElement(doc2.DocumentNode.FirstChild) as Arg;
				arg.Params = collection;

				var result = arg.GetObject();
				var text = result is int @int ? @int.ToString("N0") : result?.ToString();
				Text = Text.Replace(html, text);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"handle arg failed: {html}\n\t{ex.Message}");
			}
		}

		return Text;
	}
}