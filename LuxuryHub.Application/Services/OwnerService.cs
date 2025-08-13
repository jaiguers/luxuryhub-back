using AutoMapper;
using LuxuryHub.Application.DTOs;
using LuxuryHub.Application.Interfaces;
using LuxuryHub.Application.Requests;
using LuxuryHub.Domain.Entities;
using LuxuryHub.Domain.Exceptions;
using LuxuryHub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace LuxuryHub.Application.Services;

public class OwnerService : IOwnerService
{
    private readonly IRepository<Owner> _ownerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<OwnerService> _logger;

    public OwnerService(
        IRepository<Owner> ownerRepository,
        IMapper mapper,
        ILogger<OwnerService> logger)
    {
        _ownerRepository = ownerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<OwnerDto> CreateOwnerAsync(CreateOwnerRequest request)
    {
        try
        {
            _logger.LogInformation("Creating new owner with name: {OwnerName}", request.Name);

            var owner = _mapper.Map<Owner>(request);
            var createdOwner = await _ownerRepository.AddAsync(owner);
            var ownerDto = _mapper.Map<OwnerDto>(createdOwner);

            _logger.LogInformation("Successfully created owner with ID: {OwnerId}", ownerDto.Id);

            return ownerDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating owner");
            throw;
        }
    }

    public async Task<OwnerDto> GetOwnerByIdAsync(string id)
    {
        try
        {
            _logger.LogInformation("Retrieving owner with ID: {OwnerId}", id);

            var owner = await _ownerRepository.GetByIdAsync(id);
            if (owner == null)
            {
                throw new NotFoundException("Owner", id);
            }

            var ownerDto = _mapper.Map<OwnerDto>(owner);
            _logger.LogInformation("Successfully retrieved owner with ID: {OwnerId}", id);

            return ownerDto;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving owner with ID: {OwnerId}", id);
            throw;
        }
    }

    public async Task<PaginatedResult<OwnerDto>> GetOwnersAsync(GetOwnersRequest request)
    {
        try
        {
            _logger.LogInformation("Retrieving owners with pagination: Page={PageNumber}, Size={PageSize}",
                request.PageNumber, request.PageSize);

            var owners = await _ownerRepository.GetAllAsync();
            var totalCount = owners.Count();

            var pagedOwners = owners
                .Skip(request.Skip)
                .Take(request.Take)
                .ToList();

            var ownerDtos = _mapper.Map<IEnumerable<OwnerDto>>(pagedOwners);

            var result = new PaginatedResult<OwnerDto>
            {
                Items = ownerDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
                HasPreviousPage = request.PageNumber > 1,
                HasNextPage = request.PageNumber < (int)Math.Ceiling((double)totalCount / request.PageSize)
            };

            _logger.LogInformation("Retrieved {Count} owners out of {TotalCount} total", ownerDtos.Count(), totalCount);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving owners");
            throw;
        }
    }
}
