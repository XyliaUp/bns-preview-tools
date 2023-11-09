using CommunityToolkit.Mvvm.ComponentModel;

using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_QuestJournal;
public partial class Game_QuestJournalSceneViewModel : ObservableObject
{
	[ObservableProperty]
	Quest selectedQuest;
}