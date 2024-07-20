using Microsoft.AspNetCore.Mvc;

namespace ASPCMVC07.Controllers
{
	[Route("it")]
	public class TAResearchController : Controller
	{
		[HttpGet]
		[Route("{n:int}/{str}", Order = 1)]
		public IActionResult M04(int n, string str)
		{
			return Content($"GET:M04:/{n}/{str}");
		}

		[AcceptVerbs("GET", "POST")]
		[Route("{b:bool}/{letters:alpha}")]
		public IActionResult M05(bool b, string letters)
		{
			return Content($"{HttpContext.Request.Method}:M05:/{b}/{letters}");
		}
		
		[AcceptVerbs("GET", "DELETE")]
		[Route("{f:float}/{str:length(2,5)}", Order = 2)]
		public IActionResult M06(float f, string str)
		{
			return Content($"{HttpContext.Request.Method}:M06:/{f}/{str}");
		}

		[HttpPut]
		[Route("{letters:alpha:length(3,4)}/{n:int:range(100,200)}")]
		public IActionResult M07(string letters, int n)
		{
			return Content($"PUT:M07:/{letters}/{n}/");
		}

		[HttpPost]
		[Route("{mail:regex(^\\S+@\\S+\\.\\S+$)}")]
		public IActionResult M08(string mail)
		{
			return Content($"POST:M08:/{mail}");
		}
	}
}
