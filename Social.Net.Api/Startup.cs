using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Social.Net.Core.Attributes.DependencyRegistrars;
using Social.Net.Data;

namespace Social.Net.Api;

public class Startup(IConfiguration configuration)
{
    private static void RegisterService(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        foreach (var assembly in assemblies)
        {
            try
            {
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (!type.IsClass || type.IsAbstract || type.IsInterface ||
                        type.GetCustomAttribute<DependencyAttribute>() is not { } da)
                    {
                        continue;
                    }

                    switch (da)
                    {
                        case SingletonDependencyAttribute:
                            services.AddSingleton(da.DependencyType, type);
                            break;
                        case ScopedDependencyAttribute:
                            services.AddScoped(da.DependencyType, type);
                            break;
                        case TransientDependencyAttribute:
                            services.AddTransient(da.DependencyType, type);
                            break;
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
        services.AddAuthorization();

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

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}