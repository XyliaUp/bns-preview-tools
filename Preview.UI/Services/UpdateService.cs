using System.Text;
using System.Windows;
using AutoUpdaterDotNET;
using CUE4Parse.UE4.Pak;
using HandyControl.Controls;
using HandyControl.Data;
using Newtonsoft.Json;
using Serilog;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Services;
internal class UpdateService : IService
{
	const string APP_NAME = "bns-preview-tools";
	const float APP_VERSION = 1F;

	public bool Register()
	{
#if DEVELOP
		return false;
#elif DEBUG
		Growl.Info(StringHelper.Get("Version_Tip1"));
#endif
		AutoUpdater.RemindLaterTimeSpan = 0;
		AutoUpdater.ParseUpdateInfoEvent += ParseUpdateInfoEvent;
		AutoUpdater.CheckForUpdateEvent += CheckForUpdateEvent;
		AutoUpdater.Start($"https://tools.bnszs.com/api/update?app={APP_NAME}&version={APP_VERSION}");
		return true;
	}

	private void ParseUpdateInfoEvent(ParseUpdateInfoEventArgs args)
	{
		args.UpdateInfo = JsonConvert.DeserializeObject<UpdateInfoArgs>(args.RemoteData);
	}

	private void CheckForUpdateEvent(UpdateInfoEventArgs args)
	{
		if (args is UpdateInfoArgs arg2)
		{
			IPlatformFilePak.Signature = Encoding.UTF8.GetBytes(arg2.Signature);

			if (arg2.NoticeID < 0 || UserSettings.Default.NoticeId < arg2.NoticeID)
			{
				UserSettings.Default.NoticeId = arg2.NoticeID;
				Growl.Info(new GrowlInfo()
				{
					Message = arg2.Notice,
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

			HandyControl.Controls.MessageBox.Show(StringHelper.Get("Version_Tip3"), icon: MessageBoxImage.Error);
			Environment.Exit(500);
		}
	}


	class UpdateInfoArgs : UpdateInfoEventArgs
	{
		public int NoticeID { get; set; }
		public string? Notice { get; set; }
		public string? Signature { get; set; }
	}
}