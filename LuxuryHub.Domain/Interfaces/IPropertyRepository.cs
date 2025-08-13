using LuxuryHub.Domain.Entities;

namespace LuxuryHub.Domain.Interfaces;

public interface IPropertyRepository : IRepository<Property>
{
    Task<IEnumerable<Property>> GetPropertiesWithFiltersAsync(
        string? name = null,
        string? address = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int skip = 0,
        int take = 10);

    Task<int> GetPropertiesCountAsync(
        string? name = null,
        string? address = null,
        decimal? minPrice = null,
        decimal? maxPrice = null);

    Task<IEnumerable<Property>> GetPropertiesWithOwnerAsync(
        int skip = 0,
        int take = 10);

    Task<Property?> GetPropertyWithOwnerAsync(string id);
}
