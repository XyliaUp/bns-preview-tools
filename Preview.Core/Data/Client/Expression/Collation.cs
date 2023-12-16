using System.Globalization;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;

/// <summary>
/// Implement how database will compare to order by/find strings according defined culture/compare options
/// If not set, default is CurrentCulture with IgnoreCase
/// </summary>
public class Collation : IComparer<AttributeValue>, IComparer<string>, IEqualityComparer<AttributeValue>
{
	private readonly CompareInfo _compareInfo;

	public Collation(string collation)
	{
		var parts = collation.Split('/');
		var culture = parts[0];
		var sortOptions = parts.Length > 1 ?
			(CompareOptions)Enum.Parse(typeof(CompareOptions), parts[1]) :
			CompareOptions.None;

		this.SortOptions = sortOptions;
		this.Culture = new CultureInfo(culture);

		_compareInfo = this.Culture.CompareInfo;
	}

	public Collation(CompareOptions sortOptions)
	{
		this.SortOptions = sortOptions;
		this.Culture = CultureInfo.CurrentCulture;

		_compareInfo = this.Culture.CompareInfo;
	}


	public static Collation Binary = new Collation(CompareOptions.Ordinal);

	/// <summary>
	/// Get LCID code from culture
	/// </summary>
	public int LCID { get; }

	/// <summary>
	/// Get database language culture
	/// </summary>
	public CultureInfo Culture { get; }

	/// <summary>
	/// Get options to how string should be compared in sort
	/// </summary>
	public CompareOptions SortOptions { get; }

	/// <summary>
	/// Compare 2 string values using current culture/compare options
	/// </summary>
	public int Compare(string left, string right)
	{
		var result = _compareInfo.Compare(left, right, this.SortOptions);

		return result < 0 ? -1 : result > 0 ? +1 : 0;
	}

	public int Compare(AttributeValue left, AttributeValue rigth)
	{
		return left.CompareTo(rigth);
	}

	public bool Equals(AttributeValue x, AttributeValue y)
	{
		return this.Compare(x, y) == 0;
	}

	public int GetHashCode(AttributeValue obj)
	{
		return obj.GetHashCode();
	}

	public override string ToString()
	{
		return this.Culture.Name + "/" + this.SortOptions.ToString();
	}
}