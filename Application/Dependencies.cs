using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Dependencies
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly,
            includeInternalTypes: true);

        return services;
    }
}
