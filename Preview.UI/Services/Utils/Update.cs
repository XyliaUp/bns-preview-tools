using System.Xml;

using AduSkin.Controls.Metro;

using AutoUpdaterDotNET;

namespace Xylia.Preview.UI.Services.Utils;
public class Update
{
    public void CheckForUpdates()
    {
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

			//AduMessageBox.Show(messageBox);
			//if (messageBox.Result != MessageBoxResult.Yes) return;

			if (AutoUpdater.DownloadUpdate(args))
				Application.Current.Shutdown();
		}
        else
        {
			AduMessageBox.Show(
                "There is a problem reaching the update server, please check your internet connection or try again later.",
                "Update Check Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}