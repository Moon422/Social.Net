namespace Social.Net.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SocialDbContext>
{
    var optionsBuilder = new DbContextOptionsBuilder<ShopDbContext>();

    var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
    optionsBuilder.UseMySql("server=localhost;user=shopdb_user;password=shopdb;database=shopnetdb", serverVersion);

    return new ShopDbContext(optionsBuilder.Options);
}