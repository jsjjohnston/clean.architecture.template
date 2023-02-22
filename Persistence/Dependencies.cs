using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interceptors;

namespace Persistence;

public static class Dependencies
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database"); // TODO: Add Configuration

        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, optionsBuilder) =>
        {

            var intercepter = sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();

            optionsBuilder.UseSqlServer(connectionString)
                          .AddInterceptors(intercepter);
        });
        return services;
    }
}

