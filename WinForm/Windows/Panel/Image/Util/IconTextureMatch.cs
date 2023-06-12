using System;
using System.Threading;

using HZH_Controls.Forms;

using Xylia.Extension;

namespace Xylia.Match.Util.Paks
{
	public sealed class IconTextureMatch
	{
		#region Fields
		/// <summary>
		/// 起始时活动
		/// </summary>
		public EventHandler Start;

		/// <summary>
		/// 结束时活动
		/// </summary>
		public EventHandler Finish;


		public bool CheckFormat = true;

		/// <summary>
		/// 格式选择器
		/// </summary>
		public string FormatSelect;
		#endregion

		#region Functions
		public void StartMatch(Textures.IconOutBase IconOutBase, ref Thread RunThread, Action<string> action)
		{
			#region Initialize
			//清理tip
			FrmTips.ClearTips();

			if (CheckFormat && (string.IsNullOrWhiteSpace(FormatSelect) || !FormatSelect.Contains('[')))
			{
				Tip.Message("输出格式必须至少包含一个特殊规则");
				return;
			}
			#endregion

			#region 执行
			RunThread = new Thread(o =>
			{
				//触发开始Event
				Start?.Invoke(null, new());

				try
				{
					DateTime d1 = DateTime.Now;
					action($"正在Initialize数据");

					using (IconOutBase)
					{
						IconOutBase.LoadData(action);
						IconOutBase.SaveAsPicture(FormatSelect);
					}

					TimeSpan Ts = DateTime.Now - d1;
					action($"任务已经全部结束！ 共计 { Ts.Hours }小时 { Ts.Minutes }分 { Ts.Seconds }秒。");
				}
				catch (ThreadInterruptedException)
				{
					action("任务已强制结束。");
					return;
				}
				catch (Exception ee)
				{
					action("由于发生了错误, 进程已提前结束。");

					Xylia.Tip.Stop(ee.ToString());
					Console.WriteLine(ee);
				}
				finally
				{
					//结束Event
					Finish?.Invoke(null, null);
					lib.ClearMemory();
				}
			});
			RunThread.Start();
			#endregion
		}
		#endregion
	}
}