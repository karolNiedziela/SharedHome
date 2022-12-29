using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharedHome.Identity.Authentication;
using SharedHome.Identity.Authentication.Services;
using SharedHome.Shared.Application.Responses;
using SharedHome.Shared.Time;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SharedHome.IntegrationTests.Controllers
{

    public abstract class ControllerTests
    {
        private readonly IAuthManager _authManager;

        protected HttpClient Client { get; }

        public ControllerTests(CustomWebApplicationFactory factory)
        {
            Client = factory.HttpClient;

            var settingsProvider = factory.Services.GetRequiredService<SettingsProvider>();

            var jwtSettings = settingsProvider.Get<JwtOptions>(JwtOptionsSetup.SectionName);
            _authManager = new AuthManager(new OptionsWrapper<JwtOptions>(jwtSettings), new UtcTimeProvider());
        }

        protected AuthenticationResponse Authorize(Guid userId, string firstName = "firstName", string lastName = "lastName", string email = "test@email.com", IEnumerable<string>? roles = null)
        {
            roles ??= new List<string>
                {
                    "User"
                };

            var jwt = _authManager.Authenticate(userId.ToString(), firstName, lastName, email, roles);         
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);            

            return jwt;
        }     
    }
}
