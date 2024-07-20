using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPCMVC08.Attributes;

public class ManagerCtrlAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var query = context.HttpContext.Request.Query;
        query.TryGetValue("act", out var action);
        query.TryGetValue("cntrl", out var controller);

        if (action.FirstOrDefault() is null || controller.FirstOrDefault() is null)
        {
            base.OnActionExecuting(context);
            return;
        }

        context.HttpContext.Response.Redirect($"/{controller}/{action}");
    }
}
