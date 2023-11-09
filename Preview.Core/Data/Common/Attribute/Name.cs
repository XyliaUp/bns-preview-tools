using System.ComponentModel;

namespace Xylia.Preview.Data.Common.Attribute;
public sealed class Name : DescriptionAttribute
{
    public Name(string Description) : base(Description) { }
}