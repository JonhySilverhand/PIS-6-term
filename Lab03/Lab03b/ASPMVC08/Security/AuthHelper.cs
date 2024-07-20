using ASPCMVC08.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPCMVC08.Attributes;
 
public class AuthHelperAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;
        var request =  httpContext.Request;
        var response = httpContext.Response;

        request.Query.TryGetValue("cntrl", out var _controller);
        request.Query.TryGetValue("act", out var _action);

        try
        {
            var signInManager = httpContext.RequestServices.GetRequiredService<SignInManager<User>>();
            var userManager = httpContext.RequestServices.GetRequiredService<UserManager<User>>();

            if (signInManager is null)
                throw new("Error in signInManager");

            if (userManager is null)
                throw new("Error in userManager");

            if (!signInManager.IsSignedIn(httpContext.User))
            {
                await next();
                return;
            }

            var user = await userManager.GetUserAsync(httpContext.User);

            if (user is null)
                throw new("User does not exists");

            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(user, isPersistent: true);
            await next();
        } 
        catch (Exception e)
        {
            response.Redirect($"/{_controller.FirstOrDefault() ?? "Admin"}/{_action.FirstOrDefault() ?? "Error"}?message={e.Message}");
        } 
    }
}