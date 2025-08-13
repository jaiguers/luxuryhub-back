using LuxuryHub.Application.DTOs;
using LuxuryHub.Application.Requests;

namespace LuxuryHub.Application.Interfaces;

public interface IOwnerService
{
    Task<OwnerDto> CreateOwnerAsync(CreateOwnerRequest request);
    Task<OwnerDto> GetOwnerByIdAsync(string id);
    Task<PaginatedResult<OwnerDto>> GetOwnersAsync(GetOwnersRequest request);
}
