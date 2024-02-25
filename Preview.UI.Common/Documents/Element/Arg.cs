using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using CUE4Parse.BNS.Assets.Exports;
using HtmlAgilityPack;
using SkiaSharp.Views.WPF;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.UI.Documents;
public class Arg : BaseElement
{
	#region Fields
	public string? p;
	public string? id;
	public string? seq;
	#endregion

	#region Methods
	protected internal override void Load(HtmlNode node)
	{
		this.p = node.Attributes[nameof(p)]?.Value;
		this.id = node.Attributes[nameof(id)]?.Value;
		this.seq = node.Attributes[nameof(seq)]?.Value;
	}

	protected override Size MeasureCore(Size availableSize)
	{
		this.Children = [];

		var result = this.GetObject();
		if (result is null) return new Size();
		else if (result is ImageProperty bitmap) Children.Add(new Image() { Source = bitmap.Image?.ToWriteableBitmap() });
		else if (result is int @int) Children.Add(new Run() { Text = @int.ToString("N0") });
		else if (result is not null) Children.Add(new Paragraph(result.ToString()));

		return base.MeasureCore(availableSize);
	}

	internal object? GetObject()
	{
		try
		{
			object obj;

			#region source
			var p = this.p?.Split(':');
			var type = p?[0];
			if (type is null) return null;
			else if (type == "id") obj = new Ref<ModelElement>(this.id).Instance;
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

					if (obj is ImageProperty) break;
				}
			}
			#endregion

			return obj;
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"handle arg failed: {p}\n\t{ex.Message}");
			return null;
		}
	}
	#endregion
}

internal sealed class ArgItem(string target)
{
	#region Props
	public string Target => target;

	public ArgItem Prev { get; private set; }

	public ArgItem Next { get; private set; }
	#endregion

	#region Methods		
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
		if (target is null) return;

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
			value = new Integer(Convert.ToDouble(value));
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
			target = target.Replace("-", null).Replace("_", null);
			if (target.Equals(type.Name, StringComparison.OrdinalIgnoreCase)) return;
			else if (target.Equals(type.BaseType?.Name, StringComparison.OrdinalIgnoreCase)) return;

			throw new InvalidCastException($"valid failed: {Target} > {type}");
		}
	}

	public object GetObject(object value)
	{
		if (value is null) return null;
		else if (value is string) return value;
		else if (value is ImageProperty bitmap)
		{
			if (Next.Target == "scale")
			{
				var scale = short.Parse(Next.Next.Target);
				//bitmap.Scale = scale * 0.01F;
			}

			return bitmap;
		}
		else if (value.GetType().IsClass && value.TryGetParam(Target, out var param)) return param;

		Debug.WriteLine($"not supported class: {value} ({value.GetType().Name} > {Target})");
		return null;
	}
	#endregion
}

public static class ArgExtension
{
	public static bool TryGetParam<T>(this T instance, string name, out object value)
	{
		if (name == instance.GetType().Name)
		{
			value = instance;
			return true;
		}

		// record
		if (instance is ModelElement record && record.Attributes.TryGetValue(name, out value))
		{
			if (value is Record @ref && @ref.Owner.Name == "text")
				value = @ref.Attributes["text"];

			return true;
		}

		// instance
		var Member = instance.GetProperty(name);
		if (Member != null)
		{
			var obj = Member.GetValue(instance);
			value = obj is Ref<Text> text ? text.GetText() : obj;
			return true;
		}

		value = null;
		return false;
	}

	public static string Handle(this DataParams collection, string text)
	{
		foreach (Match m in new Regex("<arg.*?/>").Matches(text).Cast<Match>())
		{
			var doc = new HtmlDocument();
			string html = m.Value;
			doc.LoadHtml(html);

			// element
			var arg = new Arg();
			arg.Load(doc.DocumentNode.FirstChild);
			arg.Params = collection;

			// result
			var result = arg.GetObject();
			text = text.Replace(html, result?.ToString());
		}

		return text;
	}
}