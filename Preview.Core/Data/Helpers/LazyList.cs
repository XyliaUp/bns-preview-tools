using System.Collections;

namespace Xylia.Preview.Data.Helpers;
/// <summary>
/// Provides support for lazy list initialization.
/// </summary>
/// <typeparam name="T">The type of object that is being lazily initialized.</typeparam>
/// <param name="factory">The delegate that is invoked to produce the lazily initialized value.</param>
public class LazyList<T>(Func<object> factory) : IEnumerable<T>
{    
	private List<T> _value;

	private List<T> CreateValue()
	{
		_value = factory() as List<T>;
		factory = null;

		return Value;
	}

	public List<T> Value => _value ?? CreateValue();


	public IEnumerator<T> GetEnumerator() => Value.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator()	=> GetEnumerator();
}