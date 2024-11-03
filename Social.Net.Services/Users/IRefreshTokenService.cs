using Social.Net.Core.Domains.Users;

namespace Social.Net.Services.Users;

public interface IRefreshTokenService
{
    Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token);
    
    Task<IList<RefreshToken>> GetRefreshTokensByProfileIdAsync(int profileId);
    
    Task InsertRefreshTokenAsync(RefreshToken refreshToken, bool deferCacheClear = false, bool deferInsert = false);
    
    Task UpdateRefreshTokenAsync(RefreshToken refreshToken, bool deferCacheClear = false, bool deferUpdate = false);
    
    Task DeleteRefreshTokenAsync(RefreshToken refreshToken, bool deferCacheClear = false, bool deferDelete = false);
}