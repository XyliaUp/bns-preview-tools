namespace Xylia.Preview.UI.Views.Pages;
internal class ControlPage
{
	private readonly string name;
	private readonly Type type;
	private object _content;

	public ControlPage(Type type, string name = null)
	{
		this.name = name ?? ("Page_" + type.Name);
		this.type = type;
	}


	public string Text => Application.Current.TryFindResource(name)?.ToString();

	public object Content
	{
		set => _content = value;
		get => _content ??= Activator.CreateInstance(type);
	}
}

internal class ControlPage<T> : ControlPage
{
	public ControlPage(string name = null) : base(typeof(T), name)
	{

	}
}