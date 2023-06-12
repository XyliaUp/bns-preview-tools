using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Xylia.Preview.GameUI.Controls;


namespace Xylia.Preview.GameUI.Scene.Skill
{
	[DesignTimeVisible(false)]
	public sealed class SkillTooltipPanel : Panel
	{
		public List<ContentPanel> Tooltips = new();

		public override void Refresh()
		{
			this.Controls.Clear();

			int ContentY = 0;
			foreach (var o in this.Tooltips)
			{
				if (!this.Controls.Contains(o))
					this.Controls.Add(o);

				o.Location = new Point(0, ContentY);
				o.Refresh();

				ContentY = o.Bottom + 5;
			}

			this.Height = ContentY;
		}
	}
}