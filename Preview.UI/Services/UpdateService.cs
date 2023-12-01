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
        Growl.Info("This is a preview version, some features may not work.");
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


			//var messageBox = new MessageBoxModel
			//{
			//	Text = $"The latest version of FModel {UserSettings.Default.UpdateMode} is {args.CurrentVersion}. You are using version {args.InstalledVersion}. Do you want to {(downgrade ? "downgrade" : "update")} the application now?",
			//	Caption = $"{(downgrade ? "Downgrade" : "Update")} Available",
			//	Icon = MessageBoxImage.Question,
			//	Buttons = MessageBoxButtons.YesNo(),
			//	IsSoundEnabled = false
			//};

			//HandyControl.Controls.MessageBox.Show(messageBox);
			//if (messageBox.Result != MessageBoxResult.Yes) return;

			if (AutoUpdater.DownloadUpdate(args))
				Application.Current.Shutdown();
		}
        else
        {
			HandyControl.Controls.MessageBox.Show(
                "There is a problem reaching the update server, please check your internet connection or try again later.",
                "Update Check Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

internal static class Define
{
	public static Uri Update => ShareURI("abe3c5b63e52f552304fd547d69f22e8", "WEB924bfe515d51b6a37f130b609d3db285");

	public static Uri PublicConfig => ShareURI("DDE6ACC65FF647C99B9D846FDAFBFB4B", "741857554343170134b88971a717c941");


	public static Uri ShareURI(string key, string p) => new UriBuilder("http://note.youdao.com/yws/api/personal/file/" + p) { Query = $"method=download&inline=true&shareKey={key}" }.Uri;
}