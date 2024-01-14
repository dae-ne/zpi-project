using Dietly.Application;
using Dietly.Infrastructure;
using Dietly.Infrastructure.Identity;
using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Infrastructure.Extensions;
using Dietly.WebApi.Swagger;
using Microsoft.AspNetCore.Routing;
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

app.UseExceptionHandler(appBuilder =>
    appBuilder.Run(async context =>
        await Results.Problem(statusCode: 500).ExecuteAsync(context)));

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

app.UseApiEndpoints(config =>
{
    config.RequireAuthorization();
    config.RequireCors();
    config.ProducesProblem(400);
    config.ProducesProblem(403);
    config.ProducesProblem(404);
    config.ProducesProblem(500);
    config.ProducesValidationProblem();
});

app.MapGroup("/api/account")
    .MapIdentityApi<AppUser>()
    .WithTags("Account");

app.Run();
