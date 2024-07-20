using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPCMVC08.Attributes;

public class CheckerAuthAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            base.OnActionExecuting(context);
            return;
        }

        var query = context.HttpContext.Request.Query;
        query.TryGetValue("act", out var action);
        query.TryGetValue("cntrl", out var controller);

        context.Result = new RedirectToActionResult(action.FirstOrDefault() ?? "Index", controller.FirstOrDefault() ?? "Home", null);
        return;
    }
}
