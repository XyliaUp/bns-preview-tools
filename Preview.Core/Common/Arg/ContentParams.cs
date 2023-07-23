using System.Text.RegularExpressions;

using HtmlAgilityPack;

using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Common.Arg;
public sealed class ContentParams : Params<object>
{
	#region Constructor
	public ContentParams()
	{
		objs.Add(new Creature()
		{
			Job = JobSeq.검사.Convert(),
		});
	}

	public ContentParams(params object[] args)
	{
		objs.AddRange(args);
	}
	#endregion

	#region Functions

	public object Handle(HtmlNode ArgNode)
	{
		object obj;

		#region source
		var p = ArgNode.Attributes["p"]?.Value?.Split(':');

		var ArgType = p[0];
		if (ArgType is null) return null;
		else if (ArgType == "id") obj = ArgNode.Attributes["id"]?.Value.CastObject();
		else if (ArgType == "seq")
		{
			var seq = ArgNode.Attributes["seq"]?.Value.Split(':');
			obj = seq[1].CastSeq(seq[0]);
		}
		else
		{
			if (!byte.TryParse(ArgType, out var index)) throw new InvalidCastException("bad param, must be byte value: " + ArgType);

			ArgumentNullException.ThrowIfNull(objs);
			if (objs.Count < index) throw new ArgumentOutOfRangeException(nameof(objs));
			obj = objs[index - 1];
		}

		if (obj is null) return null;
		#endregion

		#region child
		foreach (var type in p.Skip(1))
		{
			var args = ArgItem.GetArgs(type);
			for (int x = 0; x < args.Length; x++)
			{
				if (x == 0) args[0].ValidType(ref obj);
				else obj = args[x].GetObject(obj);
			}
		}
		#endregion


		return obj;
	}



	public string Handle(string TextAlias) => HandleText(TextAlias.GetText());

	public string HandleText(string Text)
	{
		foreach (Match m in new Regex("<arg.*?/>").Matches(Text).Cast<Match>())
		{
			string html = m.ToString();
			var doc2 = new HtmlAgilityPack.HtmlDocument();
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

public class Params<T>
{
	#region Constructor
	protected readonly List<T> objs = new();

	public Params()
	{

	}

	public Params(params T[] args)
	{
		objs.AddRange(args);
	}
	#endregion

	#region Params
	/// <summary>
	/// Gets or sets the element at the specified index.
	/// </summary>
	/// <param name="ArgIndex">The zero-based index of the element to get or set.</param>
	/// <returns></returns>
	public T this[int ArgIndex]
	{
		get => ArgIndex > objs.Count ? default : objs[ArgIndex - 1];
		set
		{
			if (ArgIndex > objs.Count)
			{
				int count = ArgIndex - objs.Count;
				for (int x = 0; x < count; x++)
					objs.Add(default);
			}

			objs[ArgIndex - 1] = value;
		}
	}


	/// <summary>
	///  Adds an object to the end of the <see cref="objs"/>.
	/// </summary>
	/// <param name="item"></param>
	public void Add(T item) => objs.Add(item);

	public void Clear() => objs.Clear();
	#endregion
}


internal sealed class ArgItem
{
	#region Constructor
	public ArgItem(string target) => Target = target;

	public readonly string Target;

	public ArgItem Prev;

	public ArgItem Next;
	#endregion


	#region Functions		
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
		else if (value is Bitmap @bitmap) return new ImageData(@bitmap, this);
		else if (value is ImageData) return value;
		else if (value is string) return value;
		else if (value.GetType().IsClass && value.TryGetParam(Target, out var param)) return param;

		Debug.WriteLine($"NOT SUPPORTED {value} ({value.GetType().Name} > {Target})");
		return null;
	}
	#endregion
}