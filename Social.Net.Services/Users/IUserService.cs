using Social.Net.Core.Domains.Users;

namespace Social.Net.Services.Users;

public interface IUserService
{
    Task<Password?> GetPasswordByProfileIdAsync(int profileId);

    Task InsertPasswordAsync(Password password, bool deferCacheClear = false, bool deferInsert = false);

    Task UpdatePasswordAsync(Password password, bool deferCacheClear = false, bool deferUpdate = false);

    Task DeletePasswordAsync(Password password, bool deferCacheClear = false, bool deferDelete = false);

    Task<Profile?> GetProfileByIdAsync(int profileId);
    
    Task<Profile?> GetProfileByEmailAsync(string email);
    
    Task<Profile?> GetProfileByUsernameAsync(string username);

    Task InsertProfileAsync(Profile profile, bool deferCacheClear = false, bool deferInsert = false);

    Task UpdateProfileAsync(Profile profile, bool deferCacheClear = false, bool deferUpdate = false);

    Task DeleteProfileAsync(Profile profile, bool deferCacheClear = false, bool deferDelete = false);
}