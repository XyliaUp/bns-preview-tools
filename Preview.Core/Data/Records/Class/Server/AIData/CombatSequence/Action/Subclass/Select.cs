using System.Collections.Generic;
using System.Xml;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	public sealed class Select : IAction
	{
		/// <summary>
		/// 进入概率
		/// </summary>
		[Signal("enter-prob")]
		public byte EnterProb = 100;

		public List<IAction> Actions;



		public override void LoadData(XmlElement xe)
		{
			base.LoadData(xe);

			this.Actions = new();
			var Actions = xe.SelectNodes("./action");
			for (int i = 0; i < Actions.Count; i++)
			{
				var ActionNode = (XmlElement)Actions[i];
				this.Actions.Add(ActionNode.ActionFactory());
			}
		}
	}
}