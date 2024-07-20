using ASPCMVC08.Attributes;
using ASPCMVC08.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPCMVC08.Controllers;

[IsValidForEnter]
[AuthHelper]
public class HomeController : Controller
{
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;

    public HomeController(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            if (!(User.Identity?.IsAuthenticated ?? false))
                return View();

            var user = await userManager.GetUserAsync(User);
            ViewBag.IsAuthorized = true;
            ViewBag.Username = user.UserName;
            ViewBag.Roles = await userManager.GetRolesAsync(user);
        }
        catch (Exception e)
        {
            await Console.Out.WriteLineAsync(e.Message);
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
