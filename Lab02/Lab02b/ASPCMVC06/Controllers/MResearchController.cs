using Microsoft.AspNetCore.Mvc;

namespace ASPCMVC06.Controllers
{
	public class MResearchController : Controller
	{
		public IActionResult M01()
		{
			return Content($"GET:M01");
		}

		public IActionResult M02()
		{
			return Content($"GET:M02");
		}

		public IActionResult M03()
		{
			return Content($"GET:M03");
		}

		public IActionResult MXX()
		{
			return Content($"GET:MXX");
		}
	}
}
