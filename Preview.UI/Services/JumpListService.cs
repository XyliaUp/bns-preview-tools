using System.Windows;
using System.Windows.Shell;

namespace Xylia.Preview.UI.Services;
internal class JumpListService : IService
{
	public bool Register()
	{
		CreateAsync();
		return true;
	}

	public static async void CreateAsync()
	{
		// Create a jump-list and assign it to the current application
		var jumpList = new JumpList();
		JumpList.SetJumpList(Application.Current, jumpList);

		await Task.Run(() =>
		{
			if (jumpList != null)
			{
				jumpList.JumpItems.Clear();
				jumpList.ShowFrequentCategory = false;
				jumpList.ShowRecentCategory = false;

				#region Items
				jumpList.JumpItems.Add(new JumpTask
				{
					Title = StringHelper.Get("Command_QueryAsset"),
					ApplicationPath = Environment.ProcessPath,
					Arguments = "-command=query -type=ue4",
					IconResourcePath = null,
				});

				jumpList.JumpItems.Add(new JumpTask
				{
					Title = StringHelper.Get("Command_QueryAsset2"),
					ApplicationPath = Environment.ProcessPath,
					Arguments = "-command=query -type=ue",
					IconResourcePath = null,
				});
				#endregion
			}
		});

		jumpList.Apply();
	}
}