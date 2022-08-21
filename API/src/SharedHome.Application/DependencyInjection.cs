using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Application.Bills.Services;
using SharedHome.Application.Common.Events;
using SharedHome.Application.PipelineBehaviours;
using SharedHome.Application.ShoppingLists.Services;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Shared.Abstractions.Domain;

namespace SharedHome.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UserInformationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PagedQueryBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();

            services.AddScoped<IShoppingListService, ShoppingListService>();
            services.AddScoped<IBillService, BillService>();

            return services;
        }
    }
}
