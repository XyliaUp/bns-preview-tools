using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xylia.Attribute;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Struct.Condition.Subclass
{
	public class NonOut : Base.Condition
	{
		#region Functions
		protected override bool IsMeet(IHash Hash, bool ExistTarget) => false;
		#endregion
	}
}
