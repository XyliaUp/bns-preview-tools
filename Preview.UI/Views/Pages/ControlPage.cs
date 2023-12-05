using System.Windows;

namespace Xylia.Preview.UI.Views.Pages;
internal interface IControlPage
{
	object Content { set; get; }
}

internal class ControlPage<T>(string name = null) : IControlPage
{
	private readonly string name = name ?? typeof(T).Name;
	private object _content;


	//public string Icon;

	public string Text => Application.Current.TryFindResource(name)?.ToString();

	public object Content
	{
		set => _content = value;
		get => _content ??= Activator.CreateInstance(typeof(T));
	}
}