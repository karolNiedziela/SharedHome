using SharedHome.Infrastructure.Identity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Infrastructure.Identity.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string TokenHash { get; set; } = default!;

        public string TokenSalt { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
        
        public DateTime ExpiryDate { get; set; }

        public string UserId { get; set; } = default!;

        public RefreshToken(string tokenHash, string tokenSalt, DateTime createdAt, DateTime expiryDate, string userId)
        {
            if (string.IsNullOrEmpty(tokenHash) && string.IsNullOrEmpty(tokenSalt) || string.IsNullOrEmpty(userId))
            {
                throw new InvalidRefreshTokenException();
            }

            TokenHash = tokenHash;
            TokenSalt = tokenSalt;
            CreatedAt = createdAt;
            ExpiryDate = expiryDate;
            UserId = userId;
        }
    }
}
