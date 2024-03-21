using System;
using System.Collections;

namespace Xylia.Preview.UI.Controls.Helpers;

/// <summary>
/// Returns an Enumerable that is empty.
/// </summary>
internal class EmptyEnumerable : IEnumerable
{
	// singleton class, private ctor
	private EmptyEnumerable()
	{
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return EmptyEnumerator.Instance;
	}

	/// <summary>
	/// Read-Only instance of an Empty Enumerable.
	/// </summary>
	public static IEnumerable Instance
	{
		get
		{
			_instance ??= new EmptyEnumerable();
			return _instance;
		}
	}

	private static IEnumerable _instance;
}

/// <summary>
/// Returns an Enumerator that enumerates over nothing.
/// </summary>
internal class EmptyEnumerator : IEnumerator
{
	// singleton class, private ctor
	private EmptyEnumerator()
	{
	}

	/// <summary>
	/// Read-Only instance of an Empty Enumerator.
	/// </summary>
	public static IEnumerator Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new EmptyEnumerator();
			}
			return _instance;
		}
	}

	/// <summary>
	/// Does nothing.
	/// </summary>
	public void Reset() { }

	/// <summary>
	/// Returns false.
	/// </summary>
	/// <returns>false</returns>
	public bool MoveNext() { return false; }

	/// <summary>
	/// Returns null.
	/// </summary>
	public object Current
	{
		get
		{

			throw new InvalidOperationException();
		}
	}


	private static IEnumerator _instance;
}