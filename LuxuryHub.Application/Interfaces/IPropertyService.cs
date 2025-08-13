using LuxuryHub.Application.DTOs;
using LuxuryHub.Application.Requests;

namespace LuxuryHub.Application.Interfaces;

public interface IPropertyService
{
    Task<PropertyDto> CreatePropertyAsync(CreatePropertyRequest request);
    Task<PaginatedResult<PropertyDto>> GetPropertiesAsync(GetPropertiesRequest request);
    Task<PropertyDto> GetPropertyByIdAsync(string id);
    Task<IEnumerable<PropertyImageDto>> GetPropertyImagesAsync(string propertyId);
    Task<IEnumerable<PropertyTraceDto>> GetPropertyTracesAsync(string propertyId);
}
