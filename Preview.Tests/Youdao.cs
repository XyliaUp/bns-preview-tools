using System.Text.RegularExpressions;
using System.Web;

using Newtonsoft.Json;

namespace Xylia.Preview.Tests;
public class Youdao
{
	public static Uri Combine(string key, string p) => new UriBuilder("http://note.youdao.com/yws/api/personal/file/" + p) { Query = $"method=download&inline=true&shareKey={key}" }.Uri;

	public static Uri Resolve(string shareUrl, out FileInfo info)
	{
		var shareUri = new Uri(shareUrl);
		string key = HttpUtility.ParseQueryString(shareUri.Query)["id"];


		var response = new HttpClient().GetAsync($"https://note.youdao.com/yws/public/note/{key}").Result;
		if (!response.IsSuccessStatusCode)
			throw new InvalidDataException();

		info = JsonConvert.DeserializeObject<FileInfo>(response.Content.ReadAsStringAsync().Result);
		info.key = key;

		return Combine(key, info.p);
	}

	public struct FileInfo
	{
		public string key;

		public string p;
		public int ct;
		public object su;
		public int pr;
		public object au;

		/// <summary>
		/// 浏览次数
		/// </summary>
		public int pv;
		public int mt;
		public int size;
		public int domain;
		public string tl;
		public bool isFinanceNote;
	}


	public static void Create(string source)
	{
		Regex re = new Regex(@"(?<url>http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?)");
		MatchCollection mc = re.Matches(source);

		if (mc.Count != 1) throw new Exception("无效的链接信息");


		var shareUrl = mc[0].Result("${url}");
		var downUrl = Resolve(shareUrl, out var info);

		Console.WriteLine(downUrl);

		Console.WriteLine(" ========== Key ========== ");
		Console.WriteLine(info.key);
		Console.WriteLine(info.p);
	}
}