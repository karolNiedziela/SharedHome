﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Shared.Abstractions.Authentication;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Authentication;
using SharedHome.Shared.Time;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers
{

    public abstract class ControllerTests
    {
        private readonly IAuthManager _authManager;

        //protected readonly WriteSharedHomeDbContext _context = default!;

        protected HttpClient Client { get; }

        public ControllerTests(CustomWebApplicationFactory factory)
        {
            Client = factory.HttpClient;

            //_context = factory.Services.GetRequiredService<WriteSharedHomeDbContext>();

            //Utilities.InitializeDbForTests(_context).GetAwaiter().GetResult();

            var settingsProvider = factory.Services.GetRequiredService<SettingsProvider>();

            var jwtSettings = settingsProvider.Get<JwtSettings>(JwtSettings.SectionName);
            _authManager = new AuthManager(new OptionsWrapper<JwtSettings>(jwtSettings), new UtcTimeProvider());
        }

        protected AuthenticationResponse Authorize(Guid userId, string firstName = "firstName", string lastName = "lastName", string email = "test@email.com", IEnumerable<string>? roles = null)
        {
            if (roles is null)
            {
                roles = new List<string>
                {
                    "User"
                };
            }

            var jwt = _authManager.Authenticate(userId.ToString(), firstName, lastName, email, roles);         
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);            

            return jwt;
        }     
    }
}
