using LuxuryHub.Domain.Entities;
using LuxuryHub.Domain.Interfaces;
using LuxuryHub.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace LuxuryHub.Infrastructure.Repositories;

public class OwnerRepository : BaseRepository<Owner>
{
    public OwnerRepository(MongoDbContext context, ILogger<OwnerRepository> logger)
        : base(context.Owners, logger)
    {
    }

    protected override object GetEntityId(Owner entity)
    {
        return entity.Id;
    }
}

public class PropertyImageRepository : BaseRepository<PropertyImage>
{
    public PropertyImageRepository(MongoDbContext context, ILogger<PropertyImageRepository> logger)
        : base(context.PropertyImages, logger)
    {
    }

    protected override object GetEntityId(PropertyImage entity)
    {
        return entity.Id;
    }
}

public class PropertyTraceRepository : BaseRepository<PropertyTrace>
{
    public PropertyTraceRepository(MongoDbContext context, ILogger<PropertyTraceRepository> logger)
        : base(context.PropertyTraces, logger)
    {
    }

    protected override object GetEntityId(PropertyTrace entity)
    {
        return entity.Id;
    }
}
