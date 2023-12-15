using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recipes.WebApi.Swagger;
using Recipes.Infrastructure;
using Recipes.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CurrentUser>();

builder.Services
    .AddHttpContextAccessor()
    .AddEndpointsApiExplorer()
    .AddSwaggerGenerator()
    .AddCorsConfig()
#if DEBUG
    .AddFullHttpLogging();
#else
    .AddBasicHttpLogging();
#endif

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var serviceProvider = builder.Services.BuildServiceProvider();

var app = builder.Build();

if (app.Environment.IsDevelopment() ||
    app.Environment.IsEnvironment("Docker"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(serviceProvider);

app.Run();
