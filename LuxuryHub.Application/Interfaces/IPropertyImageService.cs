using LuxuryHub.Application.DTOs;
using LuxuryHub.Application.Requests;

namespace LuxuryHub.Application.Interfaces;

public interface IPropertyImageService
{
    Task<PropertyImageDto> CreatePropertyImageAsync(CreatePropertyImageRequest request);
    Task<PropertyImageDto> GetPropertyImageByIdAsync(string id);
    Task<PaginatedResult<PropertyImageDto>> GetPropertyImagesAsync(GetPropertyImagesRequest request);
}
