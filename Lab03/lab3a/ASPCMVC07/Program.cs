
internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddMvc();
		builder.Services.AddControllersWithViews();

		var app = builder.Build();

		app.UseStaticFiles();
		app.MapControllers();


		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Calc}/{action=Index}/{id?}");

		app.Run();
	}
}