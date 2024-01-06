namespace Dietly.WebApi.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Class)]
[MeansImplicitUse]
internal abstract class ApiEndpointAttribute(string route) : Attribute
{
    public string Route { get; } = route;
}

internal sealed class ApiEndpointGetAttribute(string route) : ApiEndpointAttribute(route);

internal sealed class ApiEndpointPostAttribute(string route) : ApiEndpointAttribute(route);

internal sealed class ApiEndpointPutAttribute(string route) : ApiEndpointAttribute(route);

internal sealed class ApiEndpointDeleteAttribute(string route) : ApiEndpointAttribute(route);
