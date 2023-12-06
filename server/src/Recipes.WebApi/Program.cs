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
    .AddSwaggerGenerator();

builder.Services
    .AddUseCases()
    .AddInfrastructure(builder.Configuration);

var serviceProvider = builder.Services.BuildServiceProvider();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(serviceProvider);

app.Run();
