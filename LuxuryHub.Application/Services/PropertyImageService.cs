using AutoMapper;
using LuxuryHub.Application.DTOs;
using LuxuryHub.Application.Interfaces;
using LuxuryHub.Application.Requests;
using LuxuryHub.Domain.Entities;
using LuxuryHub.Domain.Exceptions;
using LuxuryHub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace LuxuryHub.Application.Services;

public class PropertyImageService : IPropertyImageService
{
    private readonly IRepository<PropertyImage> _propertyImageRepository;
    private readonly IRepository<Property> _propertyRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PropertyImageService> _logger;

    public PropertyImageService(
        IRepository<PropertyImage> propertyImageRepository,
        IRepository<Property> propertyRepository,
        IMapper mapper,
        ILogger<PropertyImageService> logger)
    {
        _propertyImageRepository = propertyImageRepository;
        _propertyRepository = propertyRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PropertyImageDto> CreatePropertyImageAsync(CreatePropertyImageRequest request)
    {
        try
        {
            _logger.LogInformation("Creating new property image for property ID: {PropertyId}", request.IdProperty);

            // Verify property exists
            var propertyExists = await _propertyRepository.ExistsAsync(request.IdProperty);
            if (!propertyExists)
            {
                throw new NotFoundException("Property", request.IdProperty);
            }

            var propertyImage = new PropertyImage
            {
                IdProperty = request.IdProperty,
                File = request.File,
                Enabled = request.Enabled,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdImage = await _propertyImageRepository.AddAsync(propertyImage);
            var imageDto = _mapper.Map<PropertyImageDto>(createdImage);

            _logger.LogInformation("Successfully created property image with ID: {ImageId}", imageDto.Id);

            return imageDto;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating property image");
            throw;
        }
    }

    public async Task<PropertyImageDto> GetPropertyImageByIdAsync(string id)
    {
        try
        {
            _logger.LogInformation("Retrieving property image with ID: {ImageId}", id);

            var image = await _propertyImageRepository.GetByIdAsync(id);
            if (image == null)
            {
                throw new NotFoundException("PropertyImage", id);
            }

            var imageDto = _mapper.Map<PropertyImageDto>(image);
            _logger.LogInformation("Successfully retrieved property image with ID: {ImageId}", id);

            return imageDto;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving property image with ID: {ImageId}", id);
            throw;
        }
    }

    public async Task<PaginatedResult<PropertyImageDto>> GetPropertyImagesAsync(GetPropertyImagesRequest request)
    {
        try
        {
            _logger.LogInformation("Retrieving property images with pagination: Page={PageNumber}, Size={PageSize}",
                request.PageNumber, request.PageSize);

            var images = await _propertyImageRepository.GetAllAsync();

            // Filter by property if specified
            if (!string.IsNullOrEmpty(request.IdProperty))
            {
                images = images.Where(img => img.IdProperty == request.IdProperty);
            }

            var totalCount = images.Count();

            var pagedImages = images
                .Skip(request.Skip)
                .Take(request.Take)
                .ToList();

            var imageDtos = _mapper.Map<IEnumerable<PropertyImageDto>>(pagedImages);

            var result = new PaginatedResult<PropertyImageDto>
            {
                Items = imageDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
                HasPreviousPage = request.PageNumber > 1,
                HasNextPage = request.PageNumber < (int)Math.Ceiling((double)totalCount / request.PageSize)
            };

            _logger.LogInformation("Retrieved {Count} property images out of {TotalCount} total", imageDtos.Count(), totalCount);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving property images");
            throw;
        }
    }
}
