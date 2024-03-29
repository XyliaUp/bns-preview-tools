﻿using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

using Xylia.Preview.Data.Engine.DatData;

namespace Xylia.Preview.UI.Views.Selector;
public partial class DatSelectDialog : Window, IDatSelect
{
	#region Constructor
	private IEnumerable<FileInfo> list_xml;
	private IEnumerable<FileInfo> list_local;

	public string XML_Select;
	public string Local_Select;

	public DatSelectDialog()
	{
		InitializeComponent();
	}
	#endregion

	#region Methods
	private void Window_MouseEnter(object sender, MouseEventArgs e)
	{
		StopCountDown();
		LastActTime = DateTime.Now;
	}

	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		RefreshList();

		CountDown = new DispatcherTimer();
		CountDown.Interval = new TimeSpan(0, 0, 1);
		CountDown.Tick += Timer_Tick;
		CountDown.IsEnabled = true;

		NoResponse = new DispatcherTimer();
		NoResponse.Interval = new TimeSpan(0, 0, 1);
		NoResponse.Tick += NoResponse_Tick;
		NoResponse.IsEnabled = true;



		StartCountDown();
		LastActTime = DateTime.Now;
		this.NoResponse.IsEnabled = true;
	}



	private void RefreshList()
	{
		Load_Cmb(comboBox1, list_xml);
		Load_Cmb(comboBox2, list_local);
	}

	private void Load_Cmb(ComboBox Cmb, IEnumerable<FileInfo> FileCollection)
	{
		Cmb.Items.Clear();

		foreach (var w in FileCollection.GetFiles(true).Select(f => f.FullName))
		{
			var s = w.Replace(@"contents\Local", "...");
			Cmb.Items.Add(s);
		}

		if (Cmb.Items.Count > 0) Cmb.Text = Cmb.Items[0].ToString();

		Cmb.IsEnabled = Cmb.Items.Count != 1;
	}


	private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
	{
		// stop timer
		NoResponse.Stop();
		CountDown.Stop();

		XML_Select = comboBox1.Text.Replace("...", @"contents\Local");
		Local_Select = comboBox2.Text.Replace("...", @"contents\Local");

		this.DialogResult = true;
		this.TimeInfo.Visibility = Visibility.Hidden;
	}

	private void Btn_Cancel_Click(object sender, EventArgs e)
	{
		this.DialogResult = false;
	}
	#endregion

	#region CountDown
	private DispatcherTimer CountDown;
	private DispatcherTimer NoResponse;


	/// <summary>
	/// 最后活动时间
	/// </summary>
	DateTime LastActTime = DateTime.Now;

	/// <summary>
	/// 倒计时启动时间
	/// </summary>
	DateTime dt = DateTime.Now;

	/// <summary>
	/// 倒计时总秒数
	/// </summary>
	const int CountDownSec = 10;

	/// <summary>
	/// 无响应上限时长
	/// </summary>
	const int NoResponseSec = 15;


	private void StartCountDown()
	{
		TimeInfo.Text = null;

		dt = DateTime.Now;
		this.CountDown.IsEnabled = true;
		this.TimeInfo.Visibility = Visibility.Visible;
	}

	private void StopCountDown()
	{
		this.CountDown.IsEnabled = false;
		this.TimeInfo.Visibility = Visibility.Hidden;
	}

	private void NoResponse_Tick(object sender, EventArgs e)
	{
		int CurNoResponseSec = (int)DateTime.Now.Subtract(LastActTime).TotalSeconds;
		if (CurNoResponseSec >= NoResponseSec)
		{
			StartCountDown();
			LastActTime = DateTime.Now;
		}
	}

	private void Timer_Tick(object sender, EventArgs e)
	{
		int RemainSec = CountDownSec - (int)DateTime.Now.Subtract(dt).TotalSeconds;
		TimeInfo.Text = StringHelper.Get("DatSelector_CountDown", RemainSec);

		if (RemainSec <= 0) Btn_Confirm_Click(null, null);
	}
	#endregion


	#region IDatSelect
	DefaultProvider IDatSelect.Show(IEnumerable<FileInfo> Xml, IEnumerable<FileInfo> Local)
	{
		return Application.Current.Dispatcher.Invoke(() =>
		{
			var dialog = new DatSelectDialog()
			{
				list_xml = Xml,
				list_local = Local,
			};
			if (dialog.ShowDialog() != true) throw new OperationCanceledException();

			return new DefaultProvider()
			{
				XmlData = dialog.XML_Select,
				LocalData = dialog.Local_Select,
			};
		});
	}
	#endregion
}