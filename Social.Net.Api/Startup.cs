using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Social.Net.Core.Attributes.DependencyRegistrars;
using Social.Net.Data;

namespace Social.Net.Api;

public class Startup(IConfiguration configuration)
{
    private static void RegisterService(IServiceCollection services)
    {
        var assemblies = new List<Assembly>();
        var currentAssembly = Assembly.GetExecutingAssembly();
        assemblies.Add(currentAssembly);
        assemblies.AddRange(currentAssembly.GetReferencedAssemblies()
            .Select(Assembly.Load));

        foreach (var assembly in assemblies)
        {
            try
            {
                var types = assembly.GetTypes();
            
                foreach (var type in types)
                {
                    if (!type.IsClass || type.IsAbstract || type.IsInterface)
                    {
                        continue;
                    }
            
                    if (type.GetCustomAttribute<SingletonDependencyAttribute>() is { } sda)
                    {
                        services.AddSingleton(sda.DependencyType, type);
                    }
                    else if (type.GetCustomAttribute<ScopedDependencyAttribute>() is { } sda2)
                    {
                        services.AddScoped(sda2.DependencyType, type);
                    }
                    else if (type.GetCustomAttribute<TransientDependencyAttribute>() is { } tda)
                    {
                        services.AddTransient(tda.DependencyType, type);
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                foreach (var loaderException in ex.LoaderExceptions)
                {
                    Console.WriteLine(loaderException?.Message);
                }
            }
        }
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddAuthentication().AddJwtBearer(
            options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        configuration.GetSection("Secret").Value!
                    ))
                };
            }
        );
        services.AddAuthorization();
        services.AddMemoryCache();
        services.AddAutoMapper(option => option.AddProfile<AutoMapperProfile>());

        services.AddDbContext<SocialDbContext>(options =>
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
            var connectionString = configuration.GetConnectionString("mysql");
            options.UseMySql(connectionString, serverVersion);
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        RegisterService(services);

        // Configure Swagger for API documentation
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}