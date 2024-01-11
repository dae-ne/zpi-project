using System.Net.Http;

namespace Dietly.WebApi.Infrastructure.ApiEndpoints;

[ApiEndpoint]
public abstract class ApiEndpointBase
{
    public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;

    public string Route { get; set; } = string.Empty;

    public abstract void Configure(IApiEndpointBuilder builder);
}
