using CUE4Parse.FileProvider;

using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Helpers.Output.Textures;
public sealed class GoodIcon : IconOutBase
{
	public GoodIcon(string GameFolder, string OutputFolder) : base(GameFolder , OutputFolder) { }

	protected override void AnalyseSourceData(DefaultFileProvider provider, string format, CancellationToken cancellationToken)
	{
		Parallel.ForEach(set.Get<GoodsIcon>(), record =>
		{
			cancellationToken.ThrowIfCancellationRequested();

			var bitmap = record.Icon.GetIcon(set, provider);
			Save(ref bitmap, record.Source.RecordId.ToString());
		});
	}
}