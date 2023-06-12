using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

using HtmlAgilityPack;

using Xylia.Extension;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.GameUI.Controls;

public sealed class ContentParams
{
	#region Constructor
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	readonly List<object> Params = new();

	public ContentParams()
	{
		Params.Add(new Creature() { Job = FileCache.Data.Job.FirstOrDefault(o => o.job == JobSeq.검사) });
	}

	public ContentParams(params object[] args)
	{
		Params.AddRange(args);
	}
	#endregion

	#region Params
	/// <summary>
	/// Gets or sets the element at the specified index.
	/// </summary>
	/// <param name="ArgIndex">The zero-based index of the element to get or set.</param>
	/// <returns></returns>
	public object this[int ArgIndex]
	{
		get => this.Params[ArgIndex - 1];
		set
		{
			if (ArgIndex > Params.Count)
			{
				int count = ArgIndex - Params.Count;
				for (int x = 0; x < count; x++)
					Params.Add(null);
			}

			this.Params[ArgIndex - 1] = value;
		}
	}

	/// <summary>
	///  Adds an object to the end of the <see cref="Params"/>.
	/// </summary>
	/// <param name="item"></param>
	public void Add(object item) => this.Params.Add(item);

	public void Clear() => this.Params.Clear();
	#endregion

	#region Functions
	public static void ValidType(string target, ref object value)
	{
		if (value is null) return;
		if (value is Integer || value is Enum) return;

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
		else if (target.Replace("-", null).MyEquals(type.Name)) return;

		throw new InvalidCastException($"Valid Failed: {target} > {type}");
	}



	public object Handle(HtmlNode ArgNode)
	{
		#region get source object 
		var p = ArgNode.Attributes["p"]?.Value?.Split(':');

		object ExecObj;

		var ArgType = p[0];
		if (ArgType is null) return null;
		else if (ArgType == "id") ExecObj = ArgNode.Attributes["id"]?.Value.CastObject();
		else if (ArgType == "seq")
		{
			var seq = ArgNode.Attributes["seq"]?.Value.Split(':');
			ExecObj = seq[1].CastSeq(seq[0]);
		}
		else
		{
			if (!byte.TryParse(ArgType, out var index)) throw new InvalidCastException("非法参数, 应为数值类型: " + ArgType);

			ArgumentNullException.ThrowIfNull(Params);
			if (Params.Count < index) throw new ArgumentOutOfRangeException(nameof(Params));
			ExecObj = Params[index - 1];
		}


		if (ExecObj is null)
			return null;
		#endregion


		#region get child object
		foreach (var type in p.Skip(1))
		{
			ValidType(type.Split('.')[0], ref ExecObj);

			//args
			if (type.Contains('.'))
			{
				var args = ArgItem.GetArgs(type);
				for (int x = 1; x < args.Length; x++)
				{
					var tmp = args[x].GetObject(ExecObj);
					if (tmp is null) break;

					ExecObj = tmp;
				}
			}

			//convert
			else ExecObj = ExecObj.GetInfo(type, true)?.GetValue(ExecObj) ?? ExecObj;
		}
		#endregion


		return ExecObj;
	}

	public string Handle(string Text)
	{
		foreach (Match m in new Regex("<arg.*?/>").Matches(Text))
		{
			string html = m.ToString();
			var doc2 = new HtmlDocument();
			doc2.LoadHtml(html);

			try
			{
				var result = Handle(doc2.DocumentNode.FirstChild);
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
	#endregion
}

public sealed class ArgItem
{
	#region Constructor
	public ArgItem(string target) => this.Target = target;

	private readonly string Target;

	public ArgItem Prev;

	public ArgItem Next;
	#endregion


	#region Functions
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


	public object GetObject(object value)
	{
		if (value is null) return null;
		else if (value is string @string) return @string;
		else if (value is Bitmap @bitmap)
		{
			if (this.Target == "scale") return value;
			if (this.Prev.Target == "scale")
			{
				float Scale = 0.01F * int.Parse(this.Target);
				return new Bitmap(bitmap,
					(int)(Scale * bitmap.Width),
					(int)(Scale * bitmap.Height));
			}
		}
		else if (value is IArgParam @ArgParam)
		{
			var result = ArgParam.ParamTarget(this.Target);
			if (result != null) return result;
		}
		else
		{
			var result = value.GetInfo(this.Target, true);
			if (result != null) return result.GetValue(value);
		}

		Debug.WriteLine($"NOT SUPPORTED {value} ({value.GetType().Name} > {this.Target})");
		return null;
	}

	public override string ToString() => this.Target;
	#endregion
}