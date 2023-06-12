using System.Threading.Tasks;

using Xylia.Preview.Data.Record;

namespace Xylia.Match.Util.Paks.Textures
{
	public sealed class GoodIcon : IconOutBase
	{
		public GoodIcon(string GameFolder) : base(GameFolder) { }

		protected override void AnalyseSourceData()
		{
			Parallel.ForEach(set.GoodsIcon, record =>
			{
				int MainId = record.Key();
				record.Icon.GetInfo(out string TextureAlias, out short IconIndex);

				this.QuoteInfos.Add(new QuoteInfo()
				{
					MainId = MainId,
					TextureAlias = TextureAlias,
					IconIndex = IconIndex,
				});
			});
		}
	}
}