using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using AduSkin.Utility.Element;

namespace Xylia.Preview.UI;

/// <summary>
/// base class of game scene
/// </summary>
[TemplatePart(Name = "PART_QuestionButton", Type = typeof(Button))]
[TemplatePart(Name = "PART_MinimizedButton", Type = typeof(Button))]
[TemplatePart(Name = "PART_MaximizedButton", Type = typeof(Button))]
[TemplatePart(Name = "PART_NormalButton", Type = typeof(Button))]
[TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
public abstract class GameScene : Window, INotifyPropertyChanged
{
	#region Constructor	  
	/// <summary>
	/// 系统控件命名
	/// </summary>
	private const string MinimizedButton = "PART_MinimizedButton";
	private const string MaximizedButton = "PART_MaximizedButton";
	private const string NormalButton = "PART_NormalButton";
	private const string CloseButton = "PART_CloseButton";

	/// <summary>
	/// 系统按钮
	/// </summary>
	private Button _MinimizedButton;
	private Button _MaximizedButton;
	private Button _NormalButton;
	private Button _CloseButton;

	static GameScene()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(GameScene), new FrameworkPropertyMetadata(typeof(GameScene)));
	}

	public GameScene()
	{
		var sizeToContent = SizeToContent.Manual;
		Loaded += (s, e) =>
		{
			sizeToContent = SizeToContent;
			OnLoading(e);
		};
		ContentRendered += (ss, ee) =>
		{
			SizeToContent = SizeToContent.Manual;
			Width = ActualWidth;
			Height = ActualHeight;
			SizeToContent = sizeToContent;
		};

		KeyUp += delegate (object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				Close();
			}
		};
		StateChanged += delegate
		{
			if (ResizeMode == ResizeMode.CanMinimize || ResizeMode == ResizeMode.NoResize)
			{
				if (WindowState == WindowState.Maximized)
				{
					WindowState = WindowState.Normal;
				}
			}
		};
	}
	#endregion


	#region Property
	public static readonly new DependencyProperty BorderBrushProperty = ElementBase.Property<GameScene, Brush>("BorderBrushProperty");
	public static readonly DependencyProperty TitleForegroundProperty = ElementBase.Property<GameScene, Brush>("TitleForegroundProperty");
	public static readonly DependencyProperty SysButtonColorProperty = ElementBase.Property<GameScene, Brush>("SysButtonColorProperty");
	public static readonly DependencyProperty MetaDataProperty = ElementBase.Property<GameScene, string>(nameof(MetaData));

	public new Brush BorderBrush { get { return (Brush)GetValue(BorderBrushProperty); } set { SetValue(BorderBrushProperty, value); } }
	public Brush TitleForeground { get { return (Brush)GetValue(TitleForegroundProperty); } set { SetValue(TitleForegroundProperty, value); } }
	public Brush SysButtonColor { get { return (Brush)GetValue(SysButtonColorProperty); } set { SetValue(SysButtonColorProperty, value); } }
	public string MetaData { get { return (string)GetValue(MetaDataProperty); } set { SetValue(MetaDataProperty, value); } }
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

	protected override void OnInitialized(EventArgs e)
	{
		base.OnInitialized(e);

		WindowStartupLocation = WindowStartupLocation.CenterScreen;
		AllowsTransparency = false;
		if (WindowStyle == WindowStyle.None)
		{
			WindowStyle = WindowStyle.SingleBorderWindow;
		}
	}

	/// <summary>
	/// load data
	/// </summary>
	/// <param name="e"></param>
	protected virtual void OnLoading(EventArgs e)
	{

	}
	#endregion
}