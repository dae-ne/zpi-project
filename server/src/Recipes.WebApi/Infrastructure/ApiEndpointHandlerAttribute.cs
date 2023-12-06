namespace Recipes.WebApi.Infrastructure;

[AttributeUsage(AttributeTargets.Method)]
[MeansImplicitUse]
internal sealed class ApiEndpointHandlerAttribute : Attribute;
