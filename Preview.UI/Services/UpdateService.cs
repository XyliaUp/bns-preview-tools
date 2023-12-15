using System.Windows;
using AutoUpdaterDotNET;
using HandyControl.Controls;
using HandyControl.Data;
using Newtonsoft.Json;
using Serilog;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Services;
internal class UpdateService
{
	public void CheckForUpdates()
	{
#if DEBUG
		Growl.Info(StringHelper.Get("Version_Tip1"));
#endif
		AutoUpdater.RemindLaterTimeSpan = 0;
		AutoUpdater.ParseUpdateInfoEvent += ParseUpdateInfoEvent;
		AutoUpdater.CheckForUpdateEvent += CheckForUpdateEvent;
		AutoUpdater.Start("https://tools.bnszs.com/api/update?app=bns-preview-tools");
	}

	private void ParseUpdateInfoEvent(ParseUpdateInfoEventArgs args)
	{
		args.UpdateInfo = JsonConvert.DeserializeObject<UpdateInfoArgs>(args.RemoteData);
	}

	private void CheckForUpdateEvent(UpdateInfoEventArgs args)
	{
		if (args is UpdateInfoArgs arg)
		{
			if (arg.NoticeID < 0 || UserSettings.Default.NoticeId < arg.NoticeID)
			{
				UserSettings.Default.NoticeId = arg.NoticeID;
				Growl.Info(new GrowlInfo()
				{
					Message = arg.Notice,
					StaysOpen = true,
				});
			}
		}

		if (args.CurrentVersion != null)
		{
			var currentVersion = new Version(args.CurrentVersion);
			if (currentVersion < args.InstalledVersion) return;

			Growl.Ask(StringHelper.Get("Version_Tip2",
				StringHelper.Get("ProductName"),
				args.CurrentVersion,
				args.InstalledVersion), isConfirmed =>
			{
				if (isConfirmed && AutoUpdater.DownloadUpdate(args))
					Application.Current.Shutdown();

				return true;
			});
		}
		else
		{
			Log.Error(args.Error.Message);
			Growl.Error(StringHelper.Get("Version_Tip3"));
		}
	}


	class UpdateInfoArgs : UpdateInfoEventArgs
	{
		public int NoticeID { get; set; }
		public string Notice { get; set; }
	}
}