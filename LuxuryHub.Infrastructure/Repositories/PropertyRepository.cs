using LuxuryHub.Domain.Entities;
using LuxuryHub.Domain.Interfaces;
using LuxuryHub.Infrastructure.Data;
using LuxuryHub.Infrastructure.Models;
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
            // Build match filter
            var matchFilter = new BsonDocument();
            
            if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(address))
            {
                var textFilter = new BsonDocument();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    textFilter.Add("name", new BsonRegularExpression(name, "i"));
                }
                if (!string.IsNullOrWhiteSpace(address))
                {
                    textFilter.Add("address", new BsonRegularExpression(address, "i"));
                }
                matchFilter.Add("$or", new BsonArray { textFilter });
            }

            if (minPrice.HasValue || maxPrice.HasValue)
            {
                var priceFilter = new BsonDocument();
                if (minPrice.HasValue)
                {
                    priceFilter.Add("$gte", minPrice.Value);
                }
                if (maxPrice.HasValue)
                {
                    priceFilter.Add("$lte", maxPrice.Value);
                }
                matchFilter.Add("price", priceFilter);
            }

            // If no filters, use empty match
            if (matchFilter.ElementCount == 0)
            {
                matchFilter = new BsonDocument();
            }

            var pipeline = new[]
            {
                new BsonDocument("$match", matchFilter),
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
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "propertyImages" },
                    { "localField", "_id" },
                    { "foreignField", "idProperty" },
                    { "as", "images" }
                }),
                new BsonDocument("$addFields", new BsonDocument
                {
                    { "mainImage", new BsonDocument("$ifNull", new BsonArray
                    {
                        new BsonDocument("$arrayElemAt", new BsonArray
                        {
                            new BsonDocument("$filter", new BsonDocument
                            {
                                { "input", "$images" },
                                { "cond", new BsonDocument("$eq", new BsonArray { "$$this.enabled", true }) }
                            }),
                            0
                        }),
                        BsonNull.Value
                    })}
                }),
                new BsonDocument("$project", new BsonDocument
                {
                    { "_id", 1 },
                    { "name", 1 },
                    { "address", 1 },
                    { "price", 1 },
                    { "codeInternal", 1 },
                    { "year", 1 },
                    { "idOwner", 1 },
                    { "createdAt", 1 },
                    { "updatedAt", 1 },
                    { "owner", 1 },
                    { "mainImage", new BsonDocument("$ifNull", new BsonArray { "$mainImage.file", BsonNull.Value }) }
                }),
                new BsonDocument("$sort", new BsonDocument("createdAt", -1)),
                new BsonDocument("$skip", skip),
                new BsonDocument("$limit", take)
            };

            var aggregationResults = await _collection.Aggregate<PropertyAggregationResult>(pipeline).ToListAsync();

            // Convert to Property entities
            var properties = aggregationResults.Select(result => new Property
            {
                Id = result.Id,
                Name = result.Name,
                Address = result.Address,
                Price = result.Price,
                CodeInternal = result.CodeInternal,
                Year = result.Year,
                IdOwner = result.IdOwner,
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt,
                Owner = result.Owner,
                MainImage = result.MainImage
            }).ToList();

            _logger.LogInformation("Retrieved {Count} properties with optimized aggregation pipeline", properties.Count);
            return properties;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving properties with optimized aggregation pipeline");
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
            var property = await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (property == null)
            {
                return null;
            }

            // Get owner for the property
            var owner = await _ownersCollection.Find(o => o.Id == property.IdOwner).FirstOrDefaultAsync();
            property.Owner = owner;

            _logger.LogInformation("Retrieved property with ID: {PropertyId}, Owner found: {HasOwner}", 
                id, property.Owner != null);
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
