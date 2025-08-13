using Microsoft.Extensions.Options;
using MongoDB.Driver;
using LuxuryHub.Domain.Entities;

namespace LuxuryHub.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("owners");
    public IMongoCollection<Property> Properties => _database.GetCollection<Property>("properties");
    public IMongoCollection<PropertyImage> PropertyImages => _database.GetCollection<PropertyImage>("propertyImages");
    public IMongoCollection<PropertyTrace> PropertyTraces => _database.GetCollection<PropertyTrace>("propertyTraces");
}
