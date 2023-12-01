using System.Diagnostics;
using System.Windows;
using System.Windows.Shell;

namespace Xylia.Preview.UI.Services;
internal class JumpListService
{
	private JumpList jumpList => JumpList.GetJumpList(Application.Current);

	private string Current => Process.GetCurrentProcess().MainModule.FileName;


	public async void CreateAsync()
	{
		await Task.Run(() =>
		{
			if (this.jumpList != null)
			{
				this.jumpList.JumpItems.Clear();
				this.jumpList.ShowFrequentCategory = false;
				this.jumpList.ShowRecentCategory = false;

				#region Items
				this.jumpList.JumpItems.Add(new JumpTask
				{
					Title = StringHelper.Get("Command_QueryAsset"),
					ApplicationPath = Current,
					Arguments = "-command=query -type=ue4",
					IconResourcePath = null,
				});

				this.jumpList.JumpItems.Add(new JumpTask
				{
					Title = StringHelper.Get("Command_QueryAsset2"),
					ApplicationPath = Current,
					Arguments = "-command=query -type=ue",
					IconResourcePath = null,
				});
				#endregion
			}
		});

		this.jumpList?.Apply();
	}
}