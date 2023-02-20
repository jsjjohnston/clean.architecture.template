using Infrastructure.BackgroundJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Infrastructure
{
    public static class Dependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddQuartz(configure =>
            {

                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

                configure.AddJob<ProcessOutboxMessagesJob>(jobKey)
                        .AddTrigger(trigger => trigger
                                    .ForJob(jobKey)
                                    .WithSimpleSchedule(schedule => schedule
                                                        .WithIntervalInSeconds(10) // TODO: Add Configuration for ProcessOutboxMessagesJob WithIntervalInSeconds
                                                        .RepeatForever()));

                configure.UseMicrosoftDependencyInjectionJobFactory();

            });

            services.AddQuartzHostedService();
            
            return services;
        }
    }
}
