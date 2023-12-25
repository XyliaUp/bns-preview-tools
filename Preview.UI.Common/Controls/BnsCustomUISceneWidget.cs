using System.Windows.Markup;

namespace Xylia.Preview.UI.Controls;

[ContentProperty("Items")]
public class BnsCustomUISceneWidget : IBnsCustomBaseWidget
{
	#region Constructors
	/// <summary>
	///     Default BnsCustomUISceneWidget constructor
	/// </summary>
	/// <remarks>
	///     Automatic determination of current Dispatcher. Use alternative constructor
	///     that accepts a Dispatcher for best performance.
	/// </remarks>
	public BnsCustomUISceneWidget()
	{
		Items = new(this);
	}
	#endregion

	#region Properies
	public ChildCollection Items { get; }
	#endregion

	#region Private helpers

	#endregion
}