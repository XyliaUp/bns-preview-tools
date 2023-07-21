using System.ComponentModel;

using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Helper.Output;
using Xylia.Preview.GameUI.Scene.Game_QuestJournal;
using Xylia.Preview.Helper.Output;

namespace Xylia.Match.Windows.Panel;

[DesignTimeVisible(false)]
public partial class QuestPage : UserControl
{
	#region Constructor
	public QuestPage()
	{
		InitializeComponent();
		CheckForIllegalCrossThreadCalls = false;

		Num.Num = Math.Max(1, QuestSelector.LastRule);
	}
	#endregion


	#region Functions
	public void MatchQuest_KeyDown(Keys keys)
	{
		switch (keys)
		{
			case Keys.Up: Num.Num++; break;
			case Keys.Down: Num.Num--; break;

			case Keys.Enter: Button1_Click(null, null); break;
		}
	}

	private void Button1_Click(object sender, EventArgs e)
	{
		if (Num.Num <= 0) return;

		var thread = new Thread(act =>
		{
			var quest = FileCache.Data.Quest[(int)Num.Num];
			if (quest is null) return;

			new Game_QuestJournalScene(quest).ShowDialog();
		});

		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
	}

	private void Num_NumChanged(object sender, EventArgs e) => QuestSelector.LastRule = (int)this.Num.Num;


	private void Btn_QuestList_Click(object sender, EventArgs e) => ItemPage.ShowDialog<QuestSelector>();

	private void Btn_QusetEpic_Click(object sender, EventArgs e) => ItemPage.ShowDialog<Completed>();

	private void Output_QuestList_Click(object sender, EventArgs e) => OutBase.StartOutput<QuestOut>();

	private void Output_EpicList_Click(object sender, EventArgs e) => OutBase.StartOutput<QuestEpic>();
	#endregion
}