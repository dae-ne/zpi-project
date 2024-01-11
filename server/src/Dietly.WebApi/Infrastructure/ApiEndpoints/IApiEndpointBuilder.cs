namespace Dietly.WebApi.Infrastructure.ApiEndpoints;

public interface IApiEndpointBuilder
{
    RouteHandlerBuilder Get(string route = "");

    RouteHandlerBuilder Put(string route = "");

    RouteHandlerBuilder Post(string route = "");

    RouteHandlerBuilder Delete(string route = "");

    RouteHandlerBuilder Patch(string route = "");
}
