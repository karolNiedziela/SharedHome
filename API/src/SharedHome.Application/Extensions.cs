using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedHome.Application.PipelineBehaviours;
using SharedHome.Application.ShoppingLists.Commands.Services;
using SharedHome.Domain.ShoppingLists.Services;

namespace SharedHome.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UserInformationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PagedQueryBehavior<,>));

            services.AddScoped<IShoppingListService, ShoppingListService>();

            return services;
        }
    }
}
