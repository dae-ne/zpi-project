using System.Net.Http;
using Dietly.Application.Common.Results;
using Dietly.Application.Common.Results.ErrorsDefinition;

namespace Dietly.WebApi.Infrastructure.ApiEndpoints;

[ApiEndpoint]
public abstract class ApiEndpointBase
{
    public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;

    public string Route { get; set; } = string.Empty;

    public abstract void Configure(IApiEndpointBuilder builder);

    public IResult HandleError(ErrorBase error)
    {
        return error.GetType() switch
        {
            { } t when t == typeof(InvalidError) => Results.Problem(statusCode: 400, detail: error.Message),
            { } t when t == typeof(ForbiddenError) => Results.Problem(statusCode: 403, detail: error.Message),
            { } t when t == typeof(NotFoundError) => Results.Problem(statusCode: 404, detail: error.Message),
            _ => Results.Problem(statusCode: 500)
        };
    }
}
