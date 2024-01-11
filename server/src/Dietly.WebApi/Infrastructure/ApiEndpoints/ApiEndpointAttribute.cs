namespace Dietly.WebApi.Infrastructure.ApiEndpoints;

[AttributeUsage(AttributeTargets.Class)]
[MeansImplicitUse(ImplicitUseTargetFlags.WithMembers | ImplicitUseTargetFlags.WithInheritors)]
internal sealed class ApiEndpointAttribute : Attribute;
