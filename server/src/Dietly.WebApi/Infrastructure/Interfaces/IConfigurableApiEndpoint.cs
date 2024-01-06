namespace Dietly.WebApi.Infrastructure.Interfaces;

internal interface IConfigurableApiEndpoint
{
    void Configure(RouteHandlerBuilder builder);
}
