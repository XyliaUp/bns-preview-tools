using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public class ItemRandomOptionGroup : ModelElement
{
	#region Attributes
	public int Id { get; set; }

	public JobSeq Job { get; set; }


	public Ref<SkillTrainByItemList>[] SkillTrainByItemList { get; set; }
	public sbyte SkillTrainByItemListTotalCount { get; set; }
	public sbyte SkillTrainByItemListSelectMin { get; set; }
	public sbyte SkillTrainByItemListSelectMax { get; set; }
	public Ref<Text> SkillTrainByItemListTitle { get; set; }
	#endregion


	#region Methods
	public static void Test()
	{
		//var ItemRandomOptionGroup = FileCache.Data.Provider.GetTable<ItemRandomOptionGroup>().Where(x => x.Id == record.RandomOptionGroupId).First();

		foreach (var group in FileCache.Data.Provider.GetTable<ItemRandomOptionGroup>().GroupBy(x => x.Id))
		{
			foreach (var record in group)
			{
				record.Test2();
			}
		}
	}

	public void Test2()
	{
		foreach (var SkillTrainByItemList in SkillTrainByItemList.SelectNotNull(x => x.Instance))
		{
			Console.WriteLine($"# {SkillTrainByItemList}");

			SkillTrainByItemList.ChangeSet.SelectNotNull(x => x.Instance)
				.ForEach(SkillTrainByItem => Console.WriteLine(SkillTrainByItem));
		}
	}
	#endregion
}