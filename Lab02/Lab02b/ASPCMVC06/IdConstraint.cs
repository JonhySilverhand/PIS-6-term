namespace ASPCMVC06.Constraints
{
	public class IdConstraint : IRouteConstraint
	{
		public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
		{
			values.TryGetValue("action", out var action);
			values.TryGetValue("id", out var id);

			return id is not null ? action is not null && action.ToString() == "M01" : true;

		}
	}
}
