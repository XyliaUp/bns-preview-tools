using System.Windows;
using System.Xml;

using AutoUpdaterDotNET;

using HandyControl.Controls;

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
		AutoUpdater.Start(Define.Update.ToString());
	}

	private void ParseUpdateInfoEvent(ParseUpdateInfoEventArgs args)
	{
		XmlDocument doc = new();
		doc.LoadXml(args.RemoteData);

		var Update = doc.SelectSingleNode($"config/update[@app='Xylia.Match']");
		if (Update is null) return;

		Mandatory mandatory = null;
		var mandatoryNode = Update.SelectSingleNode("./mandatory");
		if (mandatoryNode != null)
		{
			mandatory = new Mandatory()
			{
				Value = Convert.ToBoolean(mandatoryNode.InnerText),
				UpdateMode = (Mode)Convert.ToByte(mandatoryNode.Attributes["mode"]?.Value),
				MinimumVersion = mandatoryNode.Attributes["minVersion"]?.Value
			};
		}


		args.UpdateInfo = new UpdateInfoEventArgs
		{
			CurrentVersion = Update.SelectSingleNode("./version")?.InnerText,
			ChangelogURL = Update.SelectSingleNode("./changelog")?.InnerText,
			DownloadURL = Update.SelectSingleNode("./url")?.InnerText,
			Mandatory = mandatory
		};
	}

	private void CheckForUpdateEvent(UpdateInfoEventArgs args)
	{
		if (args is { CurrentVersion: { } })
		{
			var currentVersion = new Version(args.CurrentVersion);
			if (currentVersion < args.InstalledVersion) return;

			Growl.Ask(string.Format(StringHelper.Get("Version_Tip2"),
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
			Growl.Error(StringHelper.Get("Version_Tip3"));
		}
	}
}

internal static class Define
{
	public static Uri Update => ShareURI("abe3c5b63e52f552304fd547d69f22e8", "WEB924bfe515d51b6a37f130b609d3db285");

	public static Uri PublicConfig => ShareURI("DDE6ACC65FF647C99B9D846FDAFBFB4B", "741857554343170134b88971a717c941");


	public static Uri ShareURI(string key, string p) => new UriBuilder("http://note.youdao.com/yws/api/personal/file/" + p) { Query = $"method=download&inline=true&shareKey={key}" }.Uri;
}