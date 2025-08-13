using LuxuryHub.Application.DTOs;
using LuxuryHub.Application.Requests;

namespace LuxuryHub.Application.Interfaces;

public interface IPropertyTraceService
{
    Task<PropertyTraceDto> CreatePropertyTraceAsync(CreatePropertyTraceRequest request);
    Task<PropertyTraceDto> GetPropertyTraceByIdAsync(string id);
    Task<PaginatedResult<PropertyTraceDto>> GetPropertyTracesAsync(GetPropertyTracesRequest request);
}
