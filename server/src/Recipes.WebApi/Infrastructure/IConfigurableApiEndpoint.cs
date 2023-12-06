namespace Recipes.WebApi.Infrastructure;

internal interface IConfigurableApiEndpoint
{
    void Configure(RouteHandlerBuilder builder);
}
