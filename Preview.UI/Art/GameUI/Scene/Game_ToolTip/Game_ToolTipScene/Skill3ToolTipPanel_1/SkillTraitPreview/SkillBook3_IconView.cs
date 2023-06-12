﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.GameUI.Scene.Skill
{
	public partial class SkillBook3_IconView : Form
	{
		public SkillBook3_IconView(List<Data.Record.Skill> Skills)
		{
			InitializeComponent();
			this.LoadData(Skills);
		}

		public void LoadData(List<Data.Record.Skill> data)
		{
			if (data is null) return;


			int LoX = 0;
			int LoY = 0;

			var skills = data.GroupBy(a => a.ShortCutKey).ToDictionary(a => a.Key).OrderBy(o => o.Key);
			foreach (var pair in skills)
			{
				#region	Title
				ContentPanel Title = new()
				{
					Text = KeyCommand.Cast(pair.Key)?.GetImage() ?? "无",

					AutoSize = true,
					ForeColor = Color.White,
					Location = new Point(LoX, LoY),
				};

				LoY = Title.Bottom;
				this.Controls.Add(Title);
				#endregion

				#region Skill
				foreach (var skill in pair.Value)
				{
					ItemIconCell ItemIconCell = new()
					{
						Scale = 64,
						ObjectRef = skill,

						Image = skill.Icon(),
						Location = new Point(LoX, LoY),
					};

					LoY = ItemIconCell.Bottom + 10;
					this.Controls.Add(ItemIconCell);
				}
				#endregion

				LoX += 70;
				LoY = 0;
			}
		}
	}
}