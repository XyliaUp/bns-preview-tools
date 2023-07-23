namespace Xylia.Match.Util.Paks;
public sealed class IconTextureMatch
{
	#region Fields
	public EventHandler StartEvent;

	public EventHandler FinishEvent;


	private Thread RunThread;
	#endregion


	#region Functions
	public void Start(Textures.IconOutBase IconOutBase, bool CheckFormat = true, string FormatSelect = null, Action<string> action = null)
	{
		#region Initialize
		if (CheckFormat && (string.IsNullOrWhiteSpace(FormatSelect) || !FormatSelect.Contains('[')))
		{
			Tip.Message("输出格式必须至少包含一个特殊规则");
			return;
		}
		#endregion

		RunThread = new Thread(() =>
		{
			StartEvent?.Invoke(null, new());

			try
			{
				DateTime d1 = DateTime.Now;
				using (IconOutBase)
				{
					IconOutBase.LoadData(action);
					IconOutBase.SaveAsPicture(FormatSelect);
				}

				TimeSpan Ts = DateTime.Now - d1;
				action($"任务已经全部结束！ 共计 {Ts.Hours}小时 {Ts.Minutes}分 {Ts.Seconds}秒。");
			}
			catch (ThreadInterruptedException)
			{
				action("任务已强制结束。");
			}
			catch (Exception ee)
			{
				action("由于发生了错误, 进程已提前结束。");

				Tip.Stop(ee.ToString());
				Console.WriteLine(ee);
			}
			finally
			{
				FinishEvent?.Invoke(null, null);
				RunThread = null;

				ProcessEx.ClearMemory();
			}
		});
		RunThread.Start();
	}

	public void Cancel()
	{
		if (RunThread is null) return;

		var result = MessageBox.Show("是否确认强制结束？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
		if (result != DialogResult.OK) return;

		RunThread.Interrupt();
	}
	#endregion
}