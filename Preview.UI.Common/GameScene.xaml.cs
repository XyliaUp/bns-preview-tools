using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Xylia.Preview.UI;

/// <summary>
/// base class of game scene
/// </summary>
[TemplatePart(Name = MinimizedButton, Type = typeof(Button))]
[TemplatePart(Name = MaximizedButton, Type = typeof(Button))]
[TemplatePart(Name = NormalButton, Type = typeof(Button))]
[TemplatePart(Name = CloseButton, Type = typeof(Button))]
[TemplatePart(Name = QuestionButton, Type = typeof(Button))]
public abstract class GameScene : Window, INotifyPropertyChanged
{
	#region Fields
	private const string MinimizedButton = "PART_MinimizedButton";
	private const string MaximizedButton = "PART_MaximizedButton";
	private const string NormalButton = "PART_NormalButton";
	private const string CloseButton = "PART_CloseButton";
	private const string QuestionButton = "PART_QuestionButton";

	private Button _MinimizedButton;
	private Button _MaximizedButton;
	private Button _NormalButton;
	private Button _CloseButton;
	#endregion

	#region Constructor	  
	static GameScene()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(GameScene), new FrameworkPropertyMetadata(typeof(GameScene)));
	}

	public GameScene()
	{
		Loaded += (s, e) => OnLoaded(e);
	}
	#endregion

	#region Property
	public static readonly DependencyProperty TitleForegroundProperty = DependencyProperty.Register(nameof(TitleForeground), typeof(Brush), typeof(GameScene));
	public static readonly DependencyProperty SysButtonColorProperty = DependencyProperty.Register(nameof(SysButtonColor), typeof(Brush), typeof(GameScene));

	public Brush TitleForeground { get { return (Brush)GetValue(TitleForegroundProperty); } set { SetValue(TitleForegroundProperty, value); } }
	public Brush SysButtonColor { get { return (Brush)GetValue(SysButtonColorProperty); } set { SetValue(SysButtonColorProperty, value); } }
	#endregion
	

	#region	PropertyChange

	public event PropertyChangedEventHandler PropertyChanged;

	protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(storage, value))
			return false;

		storage = value;
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		return true;
	}
	#endregion

	#region Methods
	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();

		_MinimizedButton = GetTemplateChild(MinimizedButton) as Button;
		_MaximizedButton = GetTemplateChild(MaximizedButton) as Button;
		_NormalButton = GetTemplateChild(NormalButton) as Button;
		_CloseButton = GetTemplateChild(CloseButton) as Button;

		if (_MinimizedButton != null)
			_MinimizedButton.Click += delegate { this.WindowState = WindowState.Minimized; };
		if (_MaximizedButton != null)
			_MaximizedButton.Click += delegate { this.WindowState = WindowState.Maximized; };
		if (_NormalButton != null)
			_NormalButton.Click += delegate { this.WindowState = WindowState.Normal; };
		if (_CloseButton != null)
			_CloseButton.Click += delegate { this.Close(); };
	}

	//protected override void OnInitialized(EventArgs e)
	//{
	//	base.OnInitialized(e);

	//	WindowStartupLocation = WindowStartupLocation.CenterScreen;
	//	AllowsTransparency = false;
	//	if (WindowStyle == WindowStyle.None)
	//	{
	//		WindowStyle = WindowStyle.SingleBorderWindow;
	//	}
	//}

	protected override void OnStateChanged(EventArgs e)
	{
		base.OnStateChanged(e);
		//if (ResizeMode == ResizeMode.CanMinimize || ResizeMode == ResizeMode.NoResize)
		//{
		//	if (WindowState == WindowState.Maximized)
		//	{
		//		WindowState = WindowState.Normal;
		//	}
		//}
	}

	protected override void OnKeyUp(KeyEventArgs e)
	{
		base.OnKeyUp(e);
		if (e.Key == Key.Escape)
		{
			Close();
		}
	}

	protected virtual void OnLoaded(EventArgs e)
	{

	}

	protected override void OnContentRendered(EventArgs e)
	{
		base.OnContentRendered(e);
		if (SizeToContent == SizeToContent.WidthAndHeight)
			InvalidateMeasure();
	}
	#endregion
}