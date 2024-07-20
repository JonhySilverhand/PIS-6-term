using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace ASPCMVC04.Controllers
{
	public class StatusController : Controller
	{
		private readonly Random random = new Random();

		public IActionResult S200()
		{
			int statusCode = random.Next(200, 300);

			return StatusCode(statusCode);
		}

		public IActionResult S300()
		{
			int statusCode = random.Next(300, 400);
			return StatusCode(statusCode);
		}

		public IActionResult S500()
		{
			try
			{
				int divisor = 0;
				int result = 10 / divisor;
				return View();
			}
			catch (Exception ex)
			{
				int statusCode = random.Next(500, 600);

				var errorResponse = new
				{
					Status = statusCode,
					Message = "Ошибка: деление на ноль.",
					ExceptionMessage = ex.Message
				};

				return new ObjectResult(errorResponse)
				{
					StatusCode = statusCode
				};
			}
		}
	}
}
