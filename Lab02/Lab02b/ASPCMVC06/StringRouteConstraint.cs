namespace ASPCMVC06.Constraints
{
	public class StringRouteConstraint : IRouteConstraint
	{
		public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
		{
			values.TryGetValue("MResearch", out var mResearch);
			values.TryGetValue("stringValue", out var stringVar);

			return (mResearch is not null && stringVar is not null) || (mResearch is null && stringVar is null);
		}
	}
}
