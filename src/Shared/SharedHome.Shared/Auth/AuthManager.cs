using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedHome.Shared.Abstractions.Auth;
using SharedHome.Shared.Abstractions.Time;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly AuthOptions _authOptions;
        private readonly ITime _time;

        public AuthManager(AuthOptions authOptions, ITime time)
        {
            _authOptions = authOptions;
            _time = time;
        }

        public AuthenticationSucessResult CreateToken(string userId, string? role = null, string? email = null)
        {
            var now = _time.CurrentDate();

            var jwtClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString())
            };

            if (!string.IsNullOrWhiteSpace(role))
            {
                jwtClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var expires = now.Add(_authOptions.Expiry);

            var key = Encoding.UTF8.GetBytes(_authOptions.Secret);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var jwt = new JwtSecurityToken(
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthenticationSucessResult
            {
                AccessToken = token,
                UserId = userId,
                Role = role ?? string.Empty,
                Expiry = new DateTimeOffset(expires).ToUnixTimeMilliseconds(),
                Email = email ?? string.Empty,
            };
        }
    }
}
