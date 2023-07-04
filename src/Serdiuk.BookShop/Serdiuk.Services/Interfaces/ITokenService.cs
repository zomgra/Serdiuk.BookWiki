using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Serdiuk.BookShop.Domain.IdentityModels;

namespace Serdiuk.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(IdentityUser user, IConfiguration config);
        string GenerateRefreshToken();
        Task<RefreshToken> GetRefreshTokenByTokenAsync(string token);
        Task AddNewRefreshToken(string token, string userId);
        void SetRevokedRefreshToken(RefreshToken refreshToken);
    }
}
