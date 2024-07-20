using System.Net;
using System.Net.WebSockets;
using System.Text;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		var app = builder.Build();

		app.UseStaticFiles();
		app.UseWebSockets();

		app.Map("/websocket", async context =>
		{
			if (context.WebSockets.IsWebSocketRequest)
			{
				using var ws = await context.WebSockets.AcceptWebSocketAsync();
				while (true)
				{
					var message = "Current time: " + DateTime.Now.ToString("HH:mm:ss");
					var bytes = Encoding.UTF8.GetBytes(message);
					var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
					if (ws.State == WebSocketState.Open)
					{
						await ws.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
					}
					else if (ws.State is WebSocketState.Closed or WebSocketState.Aborted)
					{
						break;
					}

					Thread.Sleep(2000);
				}
			}
			else
			{
				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			}
		});

		app.Run();
	}
}