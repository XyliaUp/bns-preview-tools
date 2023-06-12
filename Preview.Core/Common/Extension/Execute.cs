using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

using Xylia.Preview.Data.Helper.Output;

namespace Xylia.Preview.Common.Extension;

public static class Execute
{
	/// <summary>
	/// 显示窗体
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static void MyShowDialog<T>() where T : Form, new()
	{
		var thread = new Thread(act => new T().ShowDialog());
		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
	}

	/// <summary>
	/// 由于在其他进程创建对象, 需要注意控件所在线程
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="Frm"></param>
	public static void MyShowDialog<T>(this T Frm) where T : Form
	{
		if (Frm.Visible) 
		{
			Frm.Activate();
			return;
		}

		var thread = new Thread(act => Frm.ShowDialog());
		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
	}


	/// <summary>
	/// 输出表格
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static void StartOutput<T>() where T : OutBase, new()
	{
		var thread = new Thread(act => new T().Output());
		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
	}



	private static Random[] m_RandomStream;

	public static Random[] RandomStream
	{
		get
		{
			if (m_RandomStream is null)
			{
				var temp = new List<Random>();
				for (int i = 0; i < 100; i++)
					temp.Add(new Random());

				m_RandomStream = temp.ToArray();
			}


			return m_RandomStream;
		}
	}
}