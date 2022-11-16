using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedHome.Shared.Time;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SharedHome.Identity.Authentication.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly JwtOptions _jwtOptions;
        private readonly ITimeProvider _time;

        public AuthManager(IOptions<JwtOptions> jwtOptions, ITimeProvider time)
        {
            _jwtOptions = jwtOptions.Value;
            _time = time;
        }

        public AuthenticationResponse Authenticate(string userId, string firstName, string lastName, string email, IEnumerable<string> roles)
        {
            var now = _time.CurrentDate();

            var jwtClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.GivenName, firstName),
                new(ClaimTypes.Surname, lastName),
                new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString()),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, string.Join(",", roles))
            };

            var expires = now.Add(_jwtOptions.Expiry);

            var key = Encoding.UTF8.GetBytes(_jwtOptions.Secret);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials,
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience
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
                Roles = roles
            }; ;
        }
    }
}
