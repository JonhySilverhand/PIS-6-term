using ASPCMVC06.Constraints;
using ASPCMVC06.Controllers;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.Configure<RouteOptions>(options =>
		{
			options.ConstraintMap.Add("V3", typeof(StringRouteConstraint));
			options.ConstraintMap.Add("ID", typeof(IdConstraint));
		});
		// Add services to the container.
		builder.Services.AddControllersWithViews();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
		}
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();


		app.MapControllerRoute(
				name: "default",
				pattern: "{MResearch:ID:regex(^MResearch$)?}/{action:regex(^M01|M02$)}/{id:regex(^1$)?}",
				defaults: new { controller = "MResearch", action = "M01" }
		);

		app.MapControllerRoute(
				name: "V2",
				pattern: "{V2:regex(^V2$)}/{MResearch:regex(^MResearch$)?}/{action:regex(^M01|M02$)}",
				defaults: new { controller = "MResearch", action = "M02" }
			);

		app.MapControllerRoute(
				name: "V3",
				pattern: "{V3:V3:regex(^V3$)}/{MResearch:regex(^MResearch$)?}/{stringValue?}/{action:regex(^M01|M02|M03$)}",
				defaults: new { controller = "MResearch", action = "M03" }
			);

		app.MapControllerRoute(
			name: "MXX",
			pattern: "{*url}",
			defaults: new { controller = "MResearch", action = "MXX" }
		);

		app.Run();
	}
}