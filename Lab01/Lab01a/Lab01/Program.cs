internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		var app = builder.Build();

		app.MapGet("/{query}.SVY", context =>
		{
			string parmA = context.Request.Query["ParmA"];
			string parmB = context.Request.Query["ParmB"];

			string responseTextStr = $"GET-Http-SVY:ParmA = {parmA}, ParmB = {parmB}";

			context.Response.ContentType = "text/plain";
			return context.Response.WriteAsync(responseTextStr);
		});

		app.MapPost("/{query}.SVY", context =>
		{
			string parmA = context.Request.Query["ParmA"];
			string parmB = context.Request.Query["ParmB"];

			string responseTextStr = $"POST-Http-SVY:ParmA = {parmA},ParmB = {parmB}";

			context.Response.ContentType = "text/plain";
			return context.Response.WriteAsync(responseTextStr);
		});

		app.MapPut("/{query}.SVY", context =>
		{
			string parmA = context.Request.Query["ParmA"];
			string parmB = context.Request.Query["ParmB"];

			string responseTextStr = $"PUT-Http-SVY:ParmA = {parmA},ParmB = {parmB}";

			context.Response.ContentType = "text/plain";
			return context.Response.WriteAsync(responseTextStr);
		});

		app.MapPost("/sum", async context =>
		{
			try
			{
				double x = Convert.ToDouble(context.Request.Query["X"]);
				double y = Convert.ToDouble(context.Request.Query["Y"]);

				double sum = x + y;

				context.Response.ContentType = "text/plain";
				await context.Response.WriteAsync(sum.ToString());
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = 500;
				await context.Response.WriteAsync($"An error occurred: {ex.Message}");
			}
		});

		app.Map("/task5", context =>
		{
			switch (context.Request.Method)
			{
				case "GET":
					
					context.Response.ContentType = "text/html";
					return context.Response.SendFileAsync("index.html");

				case "POST":
					try
					{
						int x = Convert.ToInt32(context.Request.Query["x"]);
						int y = Convert.ToInt32(context.Request.Query["y"]);

						double product = x * y;

						context.Response.ContentType = "text/plain";
						return context.Response.WriteAsync(product.ToString());
					}
					catch (Exception ex)
					{
						context.Response.StatusCode = 500;
						return context.Response.WriteAsync($"An error occurred: {ex.Message}");
					}

				default:
					context.Response.StatusCode = 400;
					return context.Response.WriteAsync("Bad Request");
			}
		});

		app.Map("/task6", context =>
		{
			switch (context.Request.Method)
			{
				case "GET":
					context.Response.ContentType = "text/html";
					return context.Response.SendFileAsync("task6.html");

				case "POST":
					try
					{
						int x = Convert.ToInt32(context.Request.Form["x"]);
						int y = Convert.ToInt32(context.Request.Form["y"]);

						int result = x * y;

						context.Response.ContentType = "text/plain";
						return context.Response.WriteAsync(result.ToString());
					}
					catch (Exception ex)
					{
						context.Response.StatusCode = 500;
						return context.Response.WriteAsync($"An error occurred: {ex.Message}");
					}

				default:
					context.Response.StatusCode = 400;
					return context.Response.WriteAsync("Bad Request");
			}
		});

		app.Run();
	}
}