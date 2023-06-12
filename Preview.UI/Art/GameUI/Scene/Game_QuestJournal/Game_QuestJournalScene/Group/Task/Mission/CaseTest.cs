﻿using System.Collections.Generic;
using System.Windows.Forms;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Data.Record.QuestData;
using Xylia.Preview.Data.Record.QuestData.Case;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	/// <summary>
	/// 实例测试
	/// </summary>
	public class CaseTest
	{
		#region Constructor
		readonly BaseRecord data;

		public CaseTest(BaseRecord Case)
		{
			this.data = Case;
			this.Controls = new();
		}
		#endregion



		public readonly List<Control> Controls;

		public override string ToString()
		{
			if (data is CaseBase @case)
			{
				string CompletionInfo = null;
				if (@case.CompletionCount == 0 && @case.CompletionCountOp == Op.eq) CompletionInfo = "首次 ";
				else if (@case.CompletionCount == 1 && @case.CompletionCountOp == Op.lt) CompletionInfo = "首次 ";
				else if (@case.CompletionCount > 1) CompletionInfo = @case.CompletionCount + "  " + @case.CompletionCountOp + " ";


				string MainInfo = null;
				if (data is TalkToSelf talktoself) MainInfo = $"书信对话";
				else if (data is NpcTalkBase talk) MainInfo = $"和{ talk.Object.GetName() }对话";

				return CompletionInfo + MainInfo;
			}

			return null;
		}
	}
}