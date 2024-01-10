using Dietly.Application;
using Dietly.Infrastructure;
using Dietly.Infrastructure.Identity;
using Dietly.WebApi.Infrastructure.Extensions;
using Dietly.WebApi.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CurrentUser>();

builder.Services
    .AddHttpContextAccessor()
    .AddEndpointsApiExplorer()
    .AddSwaggerGenerator()
    .AddCorsConfig()
#if DEBUG
    .AddHttpLogging(fullLogging: true);
#else
    .AddHttpLogging();
#endif

builder.Services
    .AddApplication()
#if DEBUG
    .AddInfrastructure(builder.Configuration, logSensitiveData: true);
#else
    .AddInfrastructure(builder.Configuration);
#endif

var app = builder.Build();

// if (app.Environment.IsDevelopment() ||
//     app.Environment.IsEnvironment("Docker"))
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseIdentityApi<AppUser>("/api/account", config =>
{
    config.WithTags("Account");
});

app.UseApiEndpoints(config =>
{
    config.RequireAuthorization();
    config.RequireCors();
    config.Produces<ProblemDetails>(400);
    config.Produces<ProblemDetails>(403);
    config.Produces<ProblemDetails>(404);
    config.Produces<ProblemDetails>(500);
});

app.Run();
