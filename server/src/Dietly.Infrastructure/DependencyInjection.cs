using Dietly.Infrastructure.Blob;
using Dietly.Infrastructure.Data;
using Dietly.Infrastructure.Data.Interceptors;
using Dietly.Infrastructure.Email;
using Dietly.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dietly.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        bool logSensitiveData = false)
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

            var dbBuilder = options
                .UseNpgsql(configuration.GetConnectionString("DefaultDB"))
                .UseSnakeCaseNamingConvention();

            if (!logSensitiveData)
            {
                return;
            }

            dbBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder
                .AddConsole()
                .AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name &&
                    level >= LogLevel.Information)));

            dbBuilder.EnableSensitiveDataLogging();
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
