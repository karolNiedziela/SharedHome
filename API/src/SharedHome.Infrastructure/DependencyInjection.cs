﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Application;
using SharedHome.Infrastructure.EF;
using SharedHome.Infrastructure.ImagesCloudinary;
using SharedHome.Infrastructure.Mapping;
using SharedHome.Notifications;

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

            return services;
        }
    }
}
