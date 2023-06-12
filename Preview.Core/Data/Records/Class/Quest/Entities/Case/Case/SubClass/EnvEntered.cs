using System.Collections.Generic;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class EnvEntered : CaseBase
	{
		public string Object2;

		[Signal("env-response")]
		public EnvResponse EnvResponse;


		public override List<string> AttractionObject => new() { Object2 };
	}
}