using System.Xml;

namespace Xylia.Preview.Data.Models.BinData.Table.Record.Attributes;
public sealed class XElementData : IAttributeCollection
{
    #region Constructor
    public XmlElement data;

	public XElementData(XmlElement data) => this.data = data;
	#endregion

	#region Attribute
	public string this[string param, int index, bool convert] => data.Attributes[index == 0 ? param : $"{param}-{index}"]?.Value;

    public void SetAttribute(string Name, string Value) => data.SetAttribute(Name, Value);

    public bool ContainsKey(string Name, out string Value)
    {
        var result = data.Attributes[Name];

        Value = result?.Value;
        return result is not null;
    }

    public override string ToString() => data.OuterXml.ToString();

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        foreach (XmlAttribute attribute in data.Attributes)
            yield return new(attribute.Name, attribute.Value);

        yield break;
    }
	#endregion
}