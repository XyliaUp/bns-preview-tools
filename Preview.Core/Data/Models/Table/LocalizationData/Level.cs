namespace Xylia.Preview.Data.Models;
public sealed class Level : ModelElement
{
	#region Attributes
	public sbyte level { get; set; }

	public int Exp { get; set; }

	public int[] TencentVitalityMax { get; set; }
	#endregion
}