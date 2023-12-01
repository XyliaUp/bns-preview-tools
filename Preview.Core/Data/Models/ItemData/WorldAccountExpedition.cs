using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models;
public sealed class WorldAccountExpedition : Record
{
	#region Fields
	public string Alias;


	public sbyte Step;

	public bool CanNotUsed;

	public sbyte Category;

	
	public bool Unknown;

	[Repeat(5)]
	public AttachAbility[] Ability;

	[Repeat(5)] 
	public int[] AbilityValue;


	public Ref<Text> Name;

	public Ref<Text> Description;

	public Ref<Text> Story;


	[Repeat(5)]
	public Ref<Text>[] Tooltip;
	#endregion
}