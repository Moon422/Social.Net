using Microsoft.EntityFrameworkCore;
using Social.Net.Core.Domains.Directory;
using Social.Net.Core.Domains.FriendManagement;
using Social.Net.Core.Domains.Users;

namespace Social.Net.Data;

public class SocialDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Password> Passwords { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<StateProvince> StateProvinces { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }
    public DbSet<Friendship> Friendships { get; set; }
    
    
}