namespace Xylia.Match.Util.Paks.Textures;
public sealed class GoodIcon : IconOutBase
{
	public GoodIcon(string GameFolder) : base(GameFolder) { }

	protected override void AnalyseSourceData()
	{
		Parallel.ForEach(set.GoodsIcon, record =>
		{
			this.QuoteInfos.Add(new QuoteInfo()
			{
				MainId = record.Ref.Id,
				Icon = record.Icon,
			});
		});
	}
}