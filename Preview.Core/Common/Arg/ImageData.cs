namespace Xylia.Preview.Common.Arg;
public sealed class ImageData
{
	public Bitmap source;

	public float scale = 1f;


	internal ImageData(Bitmap bitmap, ArgItem arg)
	{
		this.source = bitmap;

		if (arg.Target == "scale" && int.TryParse(arg.Next.Target, out var scale))
			this.scale = 0.01F * scale;
	}
}