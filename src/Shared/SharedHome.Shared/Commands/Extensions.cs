using Microsoft.Extensions.DependencyInjection;
using SharedHome.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Scrutor;

namespace SharedHome.Shared.Commands
{
    public static class Extensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.Scan(s => s.FromAssemblies(assembly)
               .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))     
               .AsImplementedInterfaces()
               .WithScopedLifetime());

            return services;
        }
    }
}
