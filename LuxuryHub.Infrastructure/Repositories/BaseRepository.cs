using LuxuryHub.Domain.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;

namespace LuxuryHub.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly IMongoCollection<T> _collection;
    protected readonly ILogger<BaseRepository<T>> _logger;

    protected BaseRepository(IMongoCollection<T> collection, ILogger<BaseRepository<T>> logger)
    {
        _collection = collection;
        _logger = logger;
    }

    public virtual async Task<T?> GetByIdAsync(string id)
    {
        try
        {
            var filter = Builders<T>.Filter.Eq("_id", MongoDB.Bson.ObjectId.Parse(id));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving entity with ID: {Id}", id);
            throw;
        }
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all entities");
            throw;
        }
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        try
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding entity");
            throw;
        }
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        try
        {
            var filter = Builders<T>.Filter.Eq("_id", GetEntityId(entity));
            var result = await _collection.ReplaceOneAsync(filter, entity);
            
            if (result.ModifiedCount == 0)
            {
                throw new InvalidOperationException("Entity not found for update");
            }
            
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating entity");
            throw;
        }
    }

    public virtual async Task DeleteAsync(string id)
    {
        try
        {
            var filter = Builders<T>.Filter.Eq("_id", MongoDB.Bson.ObjectId.Parse(id));
            var result = await _collection.DeleteOneAsync(filter);
            
            if (result.DeletedCount == 0)
            {
                throw new InvalidOperationException("Entity not found for deletion");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting entity with ID: {Id}", id);
            throw;
        }
    }

    public virtual async Task<bool> ExistsAsync(string id)
    {
        try
        {
            var filter = Builders<T>.Filter.Eq("_id", MongoDB.Bson.ObjectId.Parse(id));
            return await _collection.Find(filter).AnyAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking existence of entity with ID: {Id}", id);
            throw;
        }
    }

    protected abstract object GetEntityId(T entity);
}
