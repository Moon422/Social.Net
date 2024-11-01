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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasOne(p => p.PresentAddress)
                .WithMany()
                .HasForeignKey(p => p.PresentAddressId)
                .IsRequired();

            entity.HasOne(p => p.PermanentAddress)
                .WithMany()
                .HasForeignKey(p => p.PermanentAddressId);
        });
        
        modelBuilder.Entity<Password>(entity =>
        {
            entity.Property(e => e.Hash)
                .HasMaxLength(60)
                .IsFixedLength()
                .IsRequired();

            entity.HasOne(p => p.Profile)
                .WithOne(p => p.Password)
                .HasForeignKey<Password>(p => p.ProfileId)
                .IsRequired();
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasOne(a => a.StateProvince)
                .WithMany()
                .HasForeignKey(a => a.StateProvinceId)
                .IsRequired();
        });

        modelBuilder.Entity<StateProvince>(entity =>
        {
            entity.HasOne(sp => sp.Country)
                .WithMany(c => c.StateProvinces)
                .HasForeignKey(sp => sp.CountryId)
                .IsRequired();
        });

        modelBuilder.Entity<FriendRequest>(entity =>
        {
            entity.HasOne<Profile>(fr => fr.SenderProfile)
                .WithMany()
                .HasForeignKey(fr => fr.SenderProfileId)
                .IsRequired();

            entity.HasOne<Profile>(fr => fr.ReceiverProfile)
                .WithMany()
                .HasForeignKey(fr => fr.ReceiverProfileId)
                .IsRequired();
        });

        modelBuilder.Entity<Friendship>(entity =>
        {
            entity.HasOne<Profile>(f => f.Profile1)
                .WithMany()
                .HasForeignKey(f => f.ProfileId1)
                .IsRequired();
            
            entity.HasOne<Profile>(f => f.Profile2)
                .WithMany()
                .HasForeignKey(f => f.ProfileId2)
                .IsRequired();
        });
        
        base.OnModelCreating(modelBuilder);
    }
}