using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interceptors;

namespace Persistence;

public static class Dependencies
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {

        // TODO: https://github.com/jasontaylordev/CleanArchitecture/blob/main/src/Infrastructure/ConfigureServices.cs
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var intercepter = sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            options.UseInMemoryDatabase("CleanArchitectureDb")
                   .AddInterceptors(intercepter);
        });

        return services;
    }
}

