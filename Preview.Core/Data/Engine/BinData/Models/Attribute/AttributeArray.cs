using System.Collections;
using Newtonsoft.Json;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.Data.Models;
public class AttributeArray : AttributeValue, IList<AttributeValue>
{
    public AttributeArray() : base(AttributeType.TNone, new List<AttributeValue>())
    {
    }

    public AttributeArray(List<AttributeValue> array) : this()
    {
        ArgumentNullException.ThrowIfNull(array);

        this.AddRange(array);
    }

    public AttributeArray(params AttributeValue[] array) : this()
    {
        ArgumentNullException.ThrowIfNull(array);

        this.AddRange(array);
    }

    public AttributeArray(IEnumerable<AttributeValue> items) : this()
    {
        ArgumentNullException.ThrowIfNull(items);

        this.AddRange(items);
    }

    public new IList<AttributeValue> RawValue => (IList<AttributeValue>)base.RawValue;

    public override AttributeValue this[int index]
    {
        get
        {
            return this.RawValue[index];
        }
        set
        {
            this.RawValue[index] = value ?? AttributeValue.Null;
        }
    }

    public int Count => this.RawValue.Count;

    public bool IsReadOnly => false;

    public void Add(AttributeValue item) => this.RawValue.Add(item ?? AttributeValue.Null);

    public void AddRange<TCollection>(TCollection collection)
        where TCollection : ICollection<AttributeValue>
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        var list = (List<AttributeValue>)base.RawValue;

        var listEmptySpace = list.Capacity - list.Count;
        if (listEmptySpace < collection.Count)
        {
            list.Capacity += collection.Count;
        }

        foreach (var AttributeValue in collection)
        {
            list.Add(AttributeValue ?? Null);
        }

    }

    public void AddRange(IEnumerable<AttributeValue> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        foreach (var item in items)
        {
            this.Add(item ?? AttributeValue.Null);
        }
    }

    public void Clear() => this.RawValue.Clear();

    public bool Contains(AttributeValue item) => this.RawValue.Contains(item ?? AttributeValue.Null);

    public void CopyTo(AttributeValue[] array, int arrayIndex) => this.RawValue.CopyTo(array, arrayIndex);

    public IEnumerator<AttributeValue> GetEnumerator() => this.RawValue.GetEnumerator();

    public int IndexOf(AttributeValue item) => this.RawValue.IndexOf(item ?? AttributeValue.Null);

    public void Insert(int index, AttributeValue item) => this.RawValue.Insert(index, item ?? AttributeValue.Null);

    public bool Remove(AttributeValue item) => this.RawValue.Remove(item);

    public void RemoveAt(int index) => this.RawValue.RemoveAt(index);

    IEnumerator IEnumerable.GetEnumerator()
    {
        foreach (var value in this.RawValue)
        {
            yield return value;
        }
    }

    public override int CompareTo(AttributeValue other)
    {
        // if types are different, returns sort type order
        if (!other.IsArray) return this.Type.CompareTo(other.Type);

        var otherArray = other.AsArray;

        var result = 0;
        var i = 0;
        var stop = Math.Min(this.Count, otherArray.Count);

        // compare each element
        for (; 0 == result && i < stop; i++)
            result = this[i].CompareTo(otherArray[i]);

        if (result != 0) return result;
        if (i == this.Count) return i == otherArray.Count ? 0 : -1;
        return 1;
    }

    public override string ToString() => JsonConvert.SerializeObject(this);
}