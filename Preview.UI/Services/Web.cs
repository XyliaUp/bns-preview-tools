namespace Xylia.Preview.UI.Services;
public static class Web
{
	public static Uri ShareURI(string key, string p) => new UriBuilder("http://note.youdao.com/yws/api/personal/file/" + p) { Query = $"method=download&inline=true&shareKey={key}" }.Uri;

}
