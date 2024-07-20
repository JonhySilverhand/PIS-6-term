using ASPCMVC08.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPCMVC08.Attributes;

public class IsValidForEnterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var response = context.HttpContext.Response;

        try
        {
            var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();

            var iuser = context.HttpContext.User.Identity;

            if (iuser is null || !iuser.IsAuthenticated)
            {
                base.OnActionExecuting(context);
                return;
            }

            var user = userManager.GetUserAsync(context.HttpContext.User).Result;
            if (user is not null)
            {
                base.OnActionExecuting(context);
                return;
            }

            signInManager.SignOutAsync().Wait();

            response.Redirect("/Home/Index");
        }
        catch (Exception e)
        {
            response.Redirect("/Admin/Error?message=" + e.Message);
        }
    }
}