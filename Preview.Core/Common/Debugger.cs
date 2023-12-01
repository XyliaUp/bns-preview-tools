using System.Net;
using System.Text;

namespace Xylia.Preview.Common;
public class Debugger
{
	private HttpListener _listener;
	private readonly int _port = new Random().Next(8000, 9000);
	
	public string Host => $"http://localhost:{_port}/";

	public Task Start() => Task.Run(() =>
	{
		_listener = new HttpListener();
		_listener.Prefixes.Add($"http://localhost:{_port}/");
		_listener.Start();
		_listener.BeginGetContext(GetContextCallBack, _listener);
	});

	private void GetContextCallBack(IAsyncResult ar)
	{
		try
		{
			HttpListener _listener = ar.AsyncState as HttpListener;
			HttpListenerContext context = _listener.EndGetContext(ar);
			_listener.BeginGetContext(new AsyncCallback(GetContextCallBack), _listener);

			// Request
			var body = GetMessage(context.Request.RawUrl[1..], out int status);
			var message = Encoding.UTF8.GetBytes(body);

			// Response
			context.Response.StatusCode = status;
			context.Response.ContentType = "text/html";
			context.Response.ContentLength64 = message.Length;
			context.Response.OutputStream.Write(message, 0, message.Length);
		}
		catch
		{

		}
	}

	protected virtual string GetMessage(string url, out int status)
	{
		status = 404;
		return "Url not found";
	}
}