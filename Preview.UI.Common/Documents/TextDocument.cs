using System;
using System.Windows;

using HtmlAgilityPack;

namespace Xylia.Preview.UI.Documents;
public class TextDocument
{
	#region Constructors
	/// <summary>
	/// Creates a new TextContainer instance.
	/// </summary>
	/// <param name="parent">
	/// A DependencyObject used to supply inherited property values for
	/// TextElements contained within this TextContainer.
	///
	/// parent may be null.
	///
	/// If the object is FrameworkElement or FrameworkContentElement, it will be
	/// the parent of all top-level TextElements.
	/// </param>
	/// <param name="plainTextOnly">
	/// If true, only plain text may be inserted into this
	/// TextContainer and perf optimizations are enabled.
	/// </param>
	internal TextDocument(DependencyObject parent, bool plainTextOnly)
	{
		_parent = parent;
		//SetFlags(plainTextOnly, Flags.PlainTextOnly);
	}
	#endregion


	#region Internal Methods
	internal void BeginChange()
	{
		BeginChange(true /* undo */);
	}

	internal void BeginChangeNoUndo()
	{
		BeginChange(false /* undo */);
	}

	private void BeginChange(bool undo)
	{
		
	}

	internal void EndChange()
	{
		EndChange(false /* skipEvents */);
	}

	internal void EndChange(bool skipEvents)
	{
		if (this.ChangedHandler != null && !skipEvents)
		{
			ChangedHandler(this, null);
		}
	}
	#endregion

	#region Internal Properties

	#endregion


	#region Internal Events
	internal event EventHandler Changed
	{
		add { ChangedHandler += value; }
		remove { ChangedHandler -= value; }
	}
	#endregion

	#region Private Fields
	private readonly DependencyObject _parent;

	private EventHandler ChangedHandler;
	#endregion


	public static Element ToElement(HtmlNode node)
	{
		Element element;
		switch (node.Name)
		{
			case "#text": element = new Run(); break;
			case "arg": element = new Arg(); break;
			case "br": element = new BR(); break;
			case "ga": element = new GA(); break;
			case "font": element = new Font(); break;
			case "image": element = new Image(); break;
			case "link": element = new Link(); break;
			case "p": element = new Paragraph(); break;

			default:
				element = new Run();
				Serilog.Log.Warning("unknown tag: " + node.Name);
				break;
		}

		element.InternalLoad(node);
		return element;
	}
}