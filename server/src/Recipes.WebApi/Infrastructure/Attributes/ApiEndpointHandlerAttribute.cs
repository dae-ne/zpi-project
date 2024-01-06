namespace Recipes.WebApi.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Method)]
[MeansImplicitUse]
internal sealed class ApiEndpointHandlerAttribute : Attribute;
