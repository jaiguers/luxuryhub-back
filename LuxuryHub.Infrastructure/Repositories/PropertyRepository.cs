using LuxuryHub.Domain.Entities;
using LuxuryHub.Domain.Interfaces;
using LuxuryHub.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Bson;

namespace LuxuryHub.Infrastructure.Repositories;

public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
{
    private readonly IMongoCollection<Owner> _ownersCollection;

    public PropertyRepository(
        MongoDbContext context,
        ILogger<PropertyRepository> logger) 
        : base(context.Properties, logger)
    {
        _ownersCollection = context.Owners;
    }

    public async Task<IEnumerable<Property>> GetPropertiesWithFiltersAsync(
        string? name = null,
        string? address = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int skip = 0,
        int take = 10)
    {
        try
        {
            var filter = Builders<Property>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(name))
            {
                filter &= Builders<Property>.Filter.Regex("name", new BsonRegularExpression(name, "i"));
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                filter &= Builders<Property>.Filter.Regex("address", new BsonRegularExpression(address, "i"));
            }

            if (minPrice.HasValue)
            {
                filter &= Builders<Property>.Filter.Gte("price", minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                filter &= Builders<Property>.Filter.Lte("price", maxPrice.Value);
            }

            var properties = await _collection
                .Find(filter)
                .Sort(Builders<Property>.Sort.Descending("createdAt"))
                .Skip(skip)
                .Limit(take)
                .ToListAsync();

            _logger.LogInformation("Retrieved {Count} properties with filters", properties.Count);
            return properties;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving properties with filters");
            throw;
        }
    }

    public async Task<int> GetPropertiesCountAsync(
        string? name = null,
        string? address = null,
        decimal? minPrice = null,
        decimal? maxPrice = null)
    {
        try
        {
            var filter = Builders<Property>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(name))
            {
                filter &= Builders<Property>.Filter.Regex("name", new BsonRegularExpression(name, "i"));
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                filter &= Builders<Property>.Filter.Regex("address", new BsonRegularExpression(address, "i"));
            }

            if (minPrice.HasValue)
            {
                filter &= Builders<Property>.Filter.Gte("price", minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                filter &= Builders<Property>.Filter.Lte("price", maxPrice.Value);
            }

            var count = await _collection.CountDocumentsAsync(filter);
            _logger.LogInformation("Counted {Count} properties with filters", count);
            return (int)count;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error counting properties with filters");
            throw;
        }
    }

    public async Task<IEnumerable<Property>> GetPropertiesWithOwnerAsync(int skip = 0, int take = 10)
    {
        try
        {
            var pipeline = new[]
            {
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "owners" },
                    { "localField", "idOwner" },
                    { "foreignField", "_id" },
                    { "as", "owner" }
                }),
                new BsonDocument("$unwind", new BsonDocument
                {
                    { "path", "$owner" },
                    { "preserveNullAndEmptyArrays", true }
                }),
                new BsonDocument("$sort", new BsonDocument("createdAt", -1)),
                new BsonDocument("$skip", skip),
                new BsonDocument("$limit", take)
            };

            var properties = await _collection.Aggregate<Property>(pipeline).ToListAsync();
            _logger.LogInformation("Retrieved {Count} properties with owner information", properties.Count);
            return properties;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving properties with owner information");
            throw;
        }
    }

    public async Task<Property?> GetPropertyWithOwnerAsync(string id)
    {
        try
        {
            var pipeline = new[]
            {
                new BsonDocument("$match", new BsonDocument("_id", ObjectId.Parse(id))),
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "owners" },
                    { "localField", "idOwner" },
                    { "foreignField", "_id" },
                    { "as", "owner" }
                }),
                new BsonDocument("$unwind", new BsonDocument
                {
                    { "path", "$owner" },
                    { "preserveNullAndEmptyArrays", true }
                })
            };

            var property = await _collection.Aggregate<Property>(pipeline).FirstOrDefaultAsync();
            _logger.LogInformation("Retrieved property with ID: {PropertyId}, Owner found: {HasOwner}", id, property?.Owner != null);
            return property;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving property with owner information for ID: {PropertyId}", id);
            throw;
        }
    }

    protected override object GetEntityId(Property entity)
    {
        return entity.Id;
    }
}
