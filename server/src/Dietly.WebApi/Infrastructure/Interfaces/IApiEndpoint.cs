namespace Dietly.WebApi.Infrastructure.Interfaces;

internal interface IApiEndpoint
{
    void Configure(RouteHandlerBuilder builder);
}
