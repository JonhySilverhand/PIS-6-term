using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ASPCMVC08.Attributes;

namespace ASPCMVC08.Controllers;

[Authorize(Roles = "Employee,Master")]
[IsValidForEnter]
[AuthHelper]
public class CalcController : Controller
{
    public IActionResult Index(float x, float y, float z, string press = "+")
    {
        if (!isCorrectParams(x, y, press))
            return View("Calc");

        (ViewBag.x, ViewBag.y, ViewBag.z, ViewBag.press) = (x, y, z, press);
        return View("Calc");
    }

    [HttpPost]
    public IActionResult Sum([FromForm] float? x, [FromForm] float? y)
    {
        if (!isCorrectParams(x, y, "+"))
            return View("Calc");

        try
        {
            ViewBag.z = x + y;

            if (float.IsInfinity((float)ViewBag.x) || float.IsInfinity((float)ViewBag.y))
            {
                ViewBag.z = 0;
                ViewBag.x = 0;
                ViewBag.y = 0;
                ViewBag.Error += " Значение является бесконечностью!";
            }

            if (float.IsNaN((float)ViewBag.z))
            {
                ViewBag.Error += " Невозможная операция!";
                ViewBag.z = 0;
                ViewBag.x = 0;
                ViewBag.y = 0;

            }
        }
        catch
        {
            ViewBag.Error = "Неопознанная ошибка";
        }
        return View("Calc");
    }

    [HttpPost]
    public IActionResult Sub([FromForm] float? x, [FromForm] float? y)
    {
        if (!isCorrectParams(x, y, "-"))
            return View("Calc");

        try
        {
            ViewBag.z = x - y;


            if (float.IsInfinity((float)ViewBag.x) || float.IsInfinity((float)ViewBag.y))
            {
                ViewBag.z = 0;
                ViewBag.x = 0;
                ViewBag.y = 0;
                ViewBag.Error += " Значение является бесконечностью!";
            }

            if (float.IsNaN((float)ViewBag.z))
            {
                ViewBag.Error += " Невозможная операция!";
                ViewBag.z = 0;
                ViewBag.x = 0;
                ViewBag.y = 0;

            }
            return View("Calc");
        }
        catch
        {
            ViewBag.Error = "Непознанная ошибка";
        }

        return View("Calc");
    }

    [HttpPost]
    public IActionResult Mul([FromForm] float? x, [FromForm] float? y)
    {
        if (!isCorrectParams(x, y, "*"))
            return View("Calc");

        try
        {
            ViewBag.z = x * y;


            if (float.IsInfinity((float)ViewBag.x) || float.IsInfinity((float)ViewBag.y))
            {
                ViewBag.z = 0;
                ViewBag.x = 0;
                ViewBag.y = 0;
                ViewBag.Error += " Значение является бесконечностью!";
            }

            if (float.IsNaN((float)ViewBag.z))
            {
                ViewBag.Error += " Невозможная операция!";
                ViewBag.z = 0;
                ViewBag.x = 0;
                ViewBag.y = 0;

            }
        }
        catch
        {
            ViewBag.Error = "Неопознанная ошибка";
        }

        return View("Calc");
    }

    [HttpPost]
    public IActionResult Div([FromForm] float? x, [FromForm] float? y)
    {
        if (!isCorrectParams(x, y, "/"))
            return View("Calc");

        try
        {
            ViewBag.z = x / y;

            if (y == 0)
            {
                ViewBag.Error += " Деление на 0 запрещено!";
                ViewBag.z = 0;
                ViewBag.x = 0;
                ViewBag.y = 0;
            }

            if (float.IsInfinity((float)ViewBag.x) || float.IsInfinity((float)ViewBag.y))
            {
                ViewBag.z = 0;
                ViewBag.x = 0;
                ViewBag.y = 0;
                ViewBag.Error += " Значение является бесконечностью!";
            }

            if (float.IsNaN((float)ViewBag.z))
            {
                ViewBag.Error += " Невозможная операция!";
                ViewBag.z = 0;
                ViewBag.x = 0;
                ViewBag.y = 0;
            }
        }
        catch
        {
            ViewBag.Error = "Неопознанная ошибка!";
        }

        return View("Calc");
    }

    private bool isCorrectParams(float? x, float? y, string press = "+")
    {
        var err = "";

        ViewBag.x = x ?? 0;
        ViewBag.y = y ?? 0;
        ViewBag.z = 0;
        ViewBag.press = press;

        if (x is null)
            err += " Первый параметр некорректный!\n";

        if (y is null)
            err += " Второй параметр некорректный!\n";

        if (press is not "+" and not "-" and not "*" and not "/")
        {
            ViewBag.press = "+";
            err += " Некорректная операция!\n";
        }

        if (string.IsNullOrWhiteSpace(err)) return true;

        ViewBag.Error = err;
        return false;
    }
}
