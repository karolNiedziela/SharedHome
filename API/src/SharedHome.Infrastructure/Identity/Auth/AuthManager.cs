﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SharedHome.Infrastructure.Identity.Entities;
using SharedHome.Infrastructure.Identity.Models;
using SharedHome.Infrastructure.Identity.Repositories;
using SharedHome.Infrastructure.Identity.Services;
using SharedHome.Shared.Abstractions.Time;
using SharedHome.Shared.Abstractions.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SharedHome.Infrastructure.Identity.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly AuthOptions _authOptions;
        private readonly ITimeProvider _time;

        public AuthManager(AuthOptions authOptions, ITimeProvider time)
        {
            _authOptions = authOptions;
            _time = time;
        }

        public AuthenticationSucessResult CreateToken(string userId, string email, IEnumerable<string> roles)
        {
            var now = _time.CurrentDate();

            var jwtClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString()),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, string.Join(",", roles))
            };          

            var expires = now.Add(_authOptions.Expiry);

            var key = Encoding.UTF8.GetBytes(_authOptions.Secret);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var jwt = new JwtSecurityToken(
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthenticationSucessResult
            {
                AccessToken = accessToken,
                UserId = userId,
                Roles = roles,
                Expiry = new DateTimeOffset(expires).ToUnixTimeMilliseconds(),
                Email = email,
            }; ;
        }
    }
}
