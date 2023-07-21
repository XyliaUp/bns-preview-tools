namespace Xylia.Match.Util.Paks.Textures;
public class QuoteInfo
{
	#region Field
	public int MainId;

	public string Name;

	public string Alias;


	public string Icon;
	#endregion


	public virtual Bitmap ProcessImage(Bitmap bitmap) => bitmap;
}
