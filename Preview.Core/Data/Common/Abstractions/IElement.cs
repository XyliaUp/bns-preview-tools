using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Common.Abstractions;
internal interface IElement
{
	ElementType ElementType { get; }

	Ref PrimaryKey { get; }

	/// <summary>
	/// Gets an <see cref="AttributeCollection"/> containing the list of attributes for this element.
	/// </summary>
	AttributeCollection Attributes { get; }
}

public enum ElementType
{
	None = -1,

	Zero,

	Element,

	Text,
}