using Finnova.Domain.Exceptions;

namespace Finnova.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }
        public int UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime CreateAt { get; private set ; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        
        private RefreshToken(int userId, string token)
        {
            UserId = userId;
            Token = token;

            CreateAt = DateTime.UtcNow;
            ExpiresAt = DateTime.UtcNow.AddDays(7);
        }

        public static RefreshToken Create(int userId, string token)
        {
            return new RefreshToken(userId, token);
        }

        public bool IsActive => RevokedAt == null && DateTime.UtcNow < ExpiresAt;

        public void Revoke()
        {
            if (!IsActive)
            {
                throw new DomainException("token already expired or revoked");
            }

            RevokedAt = DateTime.UtcNow;
        }
    }
}