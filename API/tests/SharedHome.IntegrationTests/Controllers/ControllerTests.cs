using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharedHome.Application;
using SharedHome.Infrastructure;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Options;
using SharedHome.Shared.Abstractions.Authentication;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Authentication;
using SharedHome.Shared.Time;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers
{
    [Collection("api")]
    public abstract class ControllerTests : IClassFixture<SettingsProvider>
    {
        private readonly IAuthManager _authManager;

        protected HttpClient Client { get; }

        //protected DatabaseFixture Database { get; }

        public ControllerTests(SettingsProvider settingsProvider)
        {
            var jwtSettings = settingsProvider.Get<JwtSettings>(JwtSettings.SectionName);
            _authManager = new AuthManager(new OptionsWrapper<JwtSettings>(jwtSettings), new UtcTimeProvider());

            var app = new CustomWebApplicationFactory(ConfigureServices);
            Client = app.Client;
        }

        protected AuthenticationResponse Authorize(string userId = "userId", string firstName = "firstName", string lastName = "lastName", string email = "test@email.com", IEnumerable<string>? roles = null)
        {
            if (roles is null)
            {
                roles = new List<string>
                {
                    "User"
                };
            }

            var jwt = _authManager.Authenticate(userId, firstName, lastName, email, roles);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

            return jwt;
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            var settings = new SettingsProvider().Get<MySQLSettings>(MySQLSettings.SectionName);

            services.AddDbContext<WriteSharedHomeDbContext>(options =>
            {
                options.UseMySql(settings.ConnectionString, ServerVersion.AutoDetect(settings.ConnectionString));
            });

            services.AddDbContext<ReadSharedHomeDbContext>(options =>
            {
                options.UseMySql(settings.ConnectionString, ServerVersion.AutoDetect(settings.ConnectionString));
            });

            services.AddMediatR(new[] { typeof(ApplicationAssemblyReference).Assembly, typeof(InfrastructureAssemblyReference).Assembly });
        }
    }
}
