using System.Windows.Input;
using System.Windows.Markup;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Common.Interactivity;

/// <summary>
/// command for <see cref="Record"/>
/// will be automatically registered to TableView
/// </summary>
public abstract class RecordCommand : MarkupExtension, ICommand
{
	public override object ProvideValue(IServiceProvider serviceProvider) => this;

	public virtual string Name => GetType().Name;

    public event EventHandler? CanExecuteChanged;


    public virtual bool CanExecute(object? parameter)
	{
		if (parameter is Record record) { }
		else if (parameter is ModelElement model) record = model.Source;
		else return false;

		return CanExecute(record.Owner.Name);
	}

	/// <summary>
	/// Defines the method that determines whether the command can execute in its current state.
	/// </summary>
	/// <param name="name">owner table name</param>
	/// <returns></returns>
	public abstract bool CanExecute(string name);

    public void Execute(object? parameter) => Task.Run(() =>
    {
        if (parameter is Record record) Execute(record);
		if (parameter is ModelElement model) Execute(model.Source);
	});

	/// <summary>
	/// Defines the method to be called when the command is invoked.
	/// </summary>
	/// <param name="record"></param>
	public abstract void Execute(Record record);
}