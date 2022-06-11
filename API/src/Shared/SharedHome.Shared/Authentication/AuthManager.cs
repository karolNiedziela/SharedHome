using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedHome.Shared.Abstractions.Authentication;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Abstractions.Time;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SharedHome.Shared.Authentication
{
    public class AuthManager : IAuthManager
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ITimeProvider _time;

        public AuthManager(IOptions<JwtSettings> jwtOptions, ITimeProvider time)
        {
            _jwtSettings = jwtOptions.Value;
            _time = time;
        }

        public AuthenticationResponse Authenticate(string userId, string firstName, string lastName, string email, IEnumerable<string> roles)
        {
            var now = _time.CurrentDate();

            var jwtClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.GivenName, firstName),
                new(JwtRegisteredClaimNames.FamilyName, lastName),
                new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString()),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, string.Join(",", roles))
            };

            var expires = now.Add(_jwtSettings.Expiry);

            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthenticationResponse
            {
                AccessToken = accessToken,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UserId = userId,
                Expiry = new DateTimeOffset(expires).ToUnixTimeMilliseconds(),
            }; ;
        }
    }
}
