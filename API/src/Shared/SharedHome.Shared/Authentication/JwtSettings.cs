using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Authentication
{
    public class JwtSettings
    {
        public const string SectionName = "Jwt";

        public string Secret { get; init; } = default!;

        public TimeSpan Expiry { get; init; } = default!;

        public string Issuer { get; init; } = default!;

        public string Audience { get; init; } = default!;
    }
}
