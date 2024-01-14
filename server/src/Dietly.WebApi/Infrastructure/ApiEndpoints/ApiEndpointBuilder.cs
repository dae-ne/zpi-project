using System.Net.Http;

namespace Dietly.WebApi.Infrastructure.ApiEndpoints;

public class ApiEndpointBuilder(RouteHandlerBuilder builder, ApiEndpointBase endpoint) : IApiEndpointBuilder
{
    public RouteHandlerBuilder Get(string route = "") => Configure(HttpMethod.Get, route);

    public RouteHandlerBuilder Put(string route = "") => Configure(HttpMethod.Put, route);

    public RouteHandlerBuilder Post(string route = "") => Configure(HttpMethod.Post, route);

    public RouteHandlerBuilder Delete(string route = "") => Configure(HttpMethod.Delete, route);

    public RouteHandlerBuilder Patch(string route = "") => Configure(HttpMethod.Patch, route);

    private RouteHandlerBuilder Configure(HttpMethod method, string route)
    {
        endpoint.HttpMethod = method;
        endpoint.Route = route;
        return builder;
    }
}
