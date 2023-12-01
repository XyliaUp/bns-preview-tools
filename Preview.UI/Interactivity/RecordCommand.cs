using System.Windows.Input;
using System.Windows.Markup;

using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Interactivity;

/// <summary>
/// command for <see cref="Record"/>
/// will be automatically registered to TableView
/// </summary>
public abstract class RecordCommand : MarkupExtension, ICommand
{
	public virtual string Name => this.GetType().Name;

	public event EventHandler? CanExecuteChanged;

	public override object ProvideValue(IServiceProvider serviceProvider) => this;

	public virtual bool CanExecute(object? parameter) => true;

	/// <summary>
	/// Defines the method that determines whether the command can execute in its current state.
	/// </summary>
	/// <param name="name">owner table name</param>
	/// <returns></returns>
	public abstract bool CanExecute(string name);

	public virtual void Execute(object? parameter) => Task.Run(() =>
	{
		if (parameter is Record record)
			Execute(record);
	});

	/// <summary>
	/// Defines the method to be called when the command is invoked.
	/// </summary>
	/// <param name="record"></param>
	public virtual void Execute(Record record)
	{

	}
}