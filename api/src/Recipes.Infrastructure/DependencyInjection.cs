using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Recipes.Application.Common.Interfaces;
using Recipes.Infrastructure.Blob;
using Recipes.Infrastructure.Data;
using Recipes.Infrastructure.Data.Interceptors;
using Recipes.Infrastructure.Email;
using Recipes.Infrastructure.Identity;

namespace Recipes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEmailSender<AppUser>, IdentityEmailService>(); // TODO: singleton?
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IImageStorage, ImageFoodBlobStorage>();
        services.AddScoped<IAvatarStorage, AvatarBlobStorage>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        
        services.Configure<ImageStorageOptions>(configuration.GetSection(ImageStorageOptions.Position));
        services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.Position));
        
        services.AddAzureClients(builder =>
        {
            builder.AddBlobServiceClient(configuration.GetConnectionString("ImageStorage"));
        });
        
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>()!);
            options.UseNpgsql(configuration.GetConnectionString("DefaultDB"))
#if DEBUG
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name &&
                        level >= LogLevel.Information)))
                .EnableSensitiveDataLogging()
#endif
                .UseSnakeCaseNamingConvention();
        });
        
        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        services.AddIdentityCore<AppUser>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();

        return services;
    }
}
