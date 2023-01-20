using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SharedHome.Application;
using SharedHome.Infrastructure.BackgroundJobs;
using SharedHome.Infrastructure.EF;
using SharedHome.Infrastructure.ImagesCloudinary;
using SharedHome.Infrastructure.Mapping;

namespace SharedHome.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMediatR(new[] { typeof(ApplicationAssemblyReference).Assembly, typeof(InfrastructureAssemblyReference).Assembly });

            services.AddMappings();

            services.AddMySharedHomeSQL();

            services.AddCloudinary();

            services.AddQuartz(configure =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

                configure.AddJob<ProcessOutboxMessagesJob>(jobKey)
                    .AddTrigger(trigger =>
                        trigger.ForJob(jobKey)
                            .WithSimpleSchedule(schedule =>
                                schedule.WithIntervalInSeconds(10)
                                    .RepeatForever()));

                configure.UseMicrosoftDependencyInjectionJobFactory();
            });

            services.AddQuartzHostedService();


            return services;
        }
    }
}
