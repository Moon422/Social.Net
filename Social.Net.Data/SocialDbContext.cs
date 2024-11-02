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
            entity.HasOne<Address>()
                .WithMany()
                .HasForeignKey(p => p.PresentAddressId)
                .IsRequired();

            entity.HasOne<Address>()
                .WithMany()
                .HasForeignKey(p => p.PermanentAddressId);
        });
        
        modelBuilder.Entity<Password>(entity =>
        {
            entity.Property(e => e.Hash)
                .HasMaxLength(60)
                .IsFixedLength()
                .IsRequired();

            entity.HasOne<Profile>()
                .WithOne()
                .HasForeignKey<Password>(p => p.ProfileId)
                .IsRequired();
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasOne<StateProvince>()
                .WithMany()
                .HasForeignKey(a => a.StateProvinceId)
                .IsRequired();
        });

        modelBuilder.Entity<StateProvince>(entity =>
        {
            entity.HasOne<Country>()
                .WithMany()
                .HasForeignKey(sp => sp.CountryId)
                .IsRequired();
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.TwoLetterIsoCode)
                .IsFixedLength()
                .IsRequired();

            entity.Property(e => e.ThreeLetterIsoCode)
                .IsFixedLength()
                .IsRequired();
        });

        modelBuilder.Entity<FriendRequest>(entity =>
        {
            entity.HasOne<Profile>()
                .WithMany()
                .HasForeignKey(fr => fr.SenderProfileId)
                .IsRequired();

            entity.HasOne<Profile>()
                .WithMany()
                .HasForeignKey(fr => fr.ReceiverProfileId)
                .IsRequired();
        });

        modelBuilder.Entity<Friendship>(entity =>
        {
            entity.HasOne<Profile>()
                .WithMany()
                .HasForeignKey(f => f.ProfileId1)
                .IsRequired();
            
            entity.HasOne<Profile>()
                .WithMany()
                .HasForeignKey(f => f.ProfileId2)
                .IsRequired();
        });
        
        base.OnModelCreating(modelBuilder);
    }
}