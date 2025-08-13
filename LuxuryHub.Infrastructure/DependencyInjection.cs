using LuxuryHub.Domain.Entities;
using LuxuryHub.Domain.Interfaces;
using LuxuryHub.Infrastructure.Data;
using LuxuryHub.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LuxuryHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure MongoDB settings
        var mongoDbSettings = new MongoDbSettings
        {
            ConnectionString = configuration.GetConnectionString("MongoDb") ?? "mongodb://localhost:27017/RealEstateDB",
            DatabaseName = configuration.GetSection("MongoDbSettings:DatabaseName").Value ?? "RealEstateDB",
            Collections = new Collections
            {
                Owners = "owners",
                Properties = "properties",
                PropertyImages = "propertyImages",
                PropertyTraces = "propertyTraces"
            }
        };
        
        services.Configure<MongoDbSettings>(options =>
        {
            options.ConnectionString = mongoDbSettings.ConnectionString;
            options.DatabaseName = mongoDbSettings.DatabaseName;
            options.Collections = mongoDbSettings.Collections;
        });

        // Register MongoDB context
        services.AddSingleton<MongoDbContext>();

        // Register repositories
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IRepository<Property>, PropertyRepository>();
        services.AddScoped<IRepository<Owner>, OwnerRepository>();
        services.AddScoped<IRepository<PropertyImage>, PropertyImageRepository>();
        services.AddScoped<IRepository<PropertyTrace>, PropertyTraceRepository>();

        return services;
    }
}
