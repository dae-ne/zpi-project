using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dietly.WebApi.Swagger;
using Dietly.Infrastructure;
using Dietly.Application;
using Dietly.WebApi.Infrastructure.Extensions;

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
app.UseEndpoints();

app.Run();
