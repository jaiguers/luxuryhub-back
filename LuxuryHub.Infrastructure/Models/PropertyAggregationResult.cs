using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using LuxuryHub.Domain.Entities;

namespace LuxuryHub.Infrastructure.Models;

public class PropertyAggregationResult
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("address")]
    public string Address { get; set; } = string.Empty;

    [BsonElement("price")]
    public decimal Price { get; set; }

    [BsonElement("codeInternal")]
    public string CodeInternal { get; set; } = string.Empty;

    [BsonElement("year")]
    public int Year { get; set; }

    [BsonElement("idOwner")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdOwner { get; set; } = string.Empty;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [BsonElement("owner")]
    public Owner? Owner { get; set; }

    [BsonElement("mainImage")]
    public string? MainImage { get; set; }
}
