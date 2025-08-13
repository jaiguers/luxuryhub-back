using AutoMapper;
using LuxuryHub.Application.DTOs;
using LuxuryHub.Application.Interfaces;
using LuxuryHub.Application.Requests;
using LuxuryHub.Domain.Entities;
using LuxuryHub.Domain.Exceptions;
using LuxuryHub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace LuxuryHub.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRepository<PropertyImage> _propertyImageRepository;
    private readonly IRepository<PropertyTrace> _propertyTraceRepository;
    private readonly IRepository<Owner> _ownerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PropertyService> _logger;

    public PropertyService(
        IPropertyRepository propertyRepository,
        IRepository<PropertyImage> propertyImageRepository,
        IRepository<PropertyTrace> propertyTraceRepository,
        IRepository<Owner> ownerRepository,
        IMapper mapper,
        ILogger<PropertyService> logger)
    {
        _propertyRepository = propertyRepository;
        _propertyImageRepository = propertyImageRepository;
        _propertyTraceRepository = propertyTraceRepository;
        _ownerRepository = ownerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PropertyDto> CreatePropertyAsync(CreatePropertyRequest request)
    {
        try
        {
            _logger.LogInformation("Creating new property with name: {PropertyName}", request.Name);

            // Verify owner exists
            var ownerExists = await _ownerRepository.ExistsAsync(request.IdOwner);
            if (!ownerExists)
            {
                throw new NotFoundException("Owner", request.IdOwner);
            }

            var property = new Property
            {
                Name = request.Name,
                Address = request.Address,
                Price = request.Price,
                CodeInternal = request.CodeInternal,
                Year = request.Year,
                IdOwner = request.IdOwner,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdProperty = await _propertyRepository.AddAsync(property);
            var propertyDto = _mapper.Map<PropertyDto>(createdProperty);

            _logger.LogInformation("Successfully created property with ID: {PropertyId}", propertyDto.Id);

            return propertyDto;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating property");
            throw;
        }
    }

    public async Task<PaginatedResult<PropertyDto>> GetPropertiesAsync(GetPropertiesRequest request)
    {
        try
        {
            _logger.LogInformation("Retrieving properties with filters: Name={Name}, Address={Address}, MinPrice={MinPrice}, MaxPrice={MaxPrice}, Page={PageNumber}, Size={PageSize}",
                request.Name, request.Address, request.MinPrice, request.MaxPrice, request.PageNumber, request.PageSize);

            var properties = await _propertyRepository.GetPropertiesWithFiltersAsync(
                request.Name,
                request.Address,
                request.MinPrice,
                request.MaxPrice,
                request.Skip,
                request.Take);

            var totalCount = await _propertyRepository.GetPropertiesCountAsync(
                request.Name,
                request.Address,
                request.MinPrice,
                request.MaxPrice);

            var propertyDtos = _mapper.Map<IEnumerable<PropertyDto>>(properties);

            var result = new PaginatedResult<PropertyDto>
            {
                Items = propertyDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
                HasPreviousPage = request.PageNumber > 1,
                HasNextPage = request.PageNumber < (int)Math.Ceiling((double)totalCount / request.PageSize)
            };

            _logger.LogInformation("Retrieved {Count} properties out of {TotalCount} total", propertyDtos.Count(), totalCount);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving properties");
            throw;
        }
    }

    public async Task<PropertyDto> GetPropertyByIdAsync(string id)
    {
        try
        {
            _logger.LogInformation("Retrieving property with ID: {PropertyId}", id);

            var property = await _propertyRepository.GetPropertyWithOwnerAsync(id);
            if (property == null)
            {
                throw new NotFoundException("Property", id);
            }

            var propertyDto = _mapper.Map<PropertyDto>(property);
            _logger.LogInformation("Successfully retrieved property with ID: {PropertyId}", id);

            return propertyDto;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving property with ID: {PropertyId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<PropertyImageDto>> GetPropertyImagesAsync(string propertyId)
    {
        try
        {
            _logger.LogInformation("Retrieving images for property ID: {PropertyId}", propertyId);

            // Verify property exists
            var propertyExists = await _propertyRepository.ExistsAsync(propertyId);
            if (!propertyExists)
            {
                throw new NotFoundException("Property", propertyId);
            }

            var images = await _propertyImageRepository.GetAllAsync();
            var propertyImages = images.Where(img => img.IdProperty == propertyId && img.Enabled);

            var imageDtos = _mapper.Map<IEnumerable<PropertyImageDto>>(propertyImages);
            _logger.LogInformation("Retrieved {Count} images for property ID: {PropertyId}", imageDtos.Count(), propertyId);

            return imageDtos;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving images for property ID: {PropertyId}", propertyId);
            throw;
        }
    }

    public async Task<IEnumerable<PropertyTraceDto>> GetPropertyTracesAsync(string propertyId)
    {
        try
        {
            _logger.LogInformation("Retrieving traces for property ID: {PropertyId}", propertyId);

            // Verify property exists
            var propertyExists = await _propertyRepository.ExistsAsync(propertyId);
            if (!propertyExists)
            {
                throw new NotFoundException("Property", propertyId);
            }

            var traces = await _propertyTraceRepository.GetAllAsync();
            var propertyTraces = traces.Where(trace => trace.IdProperty == propertyId)
                                     .OrderByDescending(trace => trace.DateSale);

            var traceDtos = _mapper.Map<IEnumerable<PropertyTraceDto>>(propertyTraces);
            _logger.LogInformation("Retrieved {Count} traces for property ID: {PropertyId}", traceDtos.Count(), propertyId);

            return traceDtos;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving traces for property ID: {PropertyId}", propertyId);
            throw;
        }
    }
}
