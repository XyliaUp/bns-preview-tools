namespace Xylia.Preview.UI.Views.Pages;
internal interface IControlPage
{
	object Content { set; get; }

	string Name { get; }
}

internal class ControlPage<T>(string name = null) : IControlPage
{
	private object _content;


	//public string Icon;

	public string Name => name ?? typeof(T).Name;

	public object Content
	{
		set => _content = value;
		get => _content ??= Activator.CreateInstance(typeof(T));
	}
}