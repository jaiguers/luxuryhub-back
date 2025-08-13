using LuxuryHub.Application.DTOs;
using LuxuryHub.Application.Interfaces;
using LuxuryHub.Application.Requests;
using LuxuryHub.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LuxuryHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PropertyImagesController : ControllerBase
{
    private readonly IPropertyImageService _propertyImageService;
    private readonly ILogger<PropertyImagesController> _logger;

    public PropertyImagesController(IPropertyImageService propertyImageService, ILogger<PropertyImagesController> logger)
    {
        _propertyImageService = propertyImageService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new property image
    /// </summary>
    /// <param name="request">Property image creation request</param>
    /// <returns>Created property image details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PropertyImageDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PropertyImageDto>> CreatePropertyImage([FromBody] CreatePropertyImageRequest request)
    {
        try
        {
            _logger.LogInformation("Creating new property image for property ID: {PropertyId}", request.IdProperty);

            var createdImage = await _propertyImageService.CreatePropertyImageAsync(request);
            
            return CreatedAtAction(nameof(GetPropertyImageById), new { id = createdImage.Id }, createdImage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating property image");
            throw;
        }
    }

    /// <summary>
    /// Get a property image by its ID
    /// </summary>
    /// <param name="id">Property image ID</param>
    /// <returns>Property image details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PropertyImageDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PropertyImageDto>> GetPropertyImageById(string id)
    {
        try
        {
            var image = await _propertyImageService.GetPropertyImageByIdAsync(id);
            return Ok(image);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving property image with ID: {ImageId}", id);
            throw;
        }
    }

    /// <summary>
    /// Get all property images with pagination
    /// </summary>
    /// <param name="request">Pagination parameters</param>
    /// <returns>Paginated list of property images</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<PropertyImageDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PaginatedResult<PropertyImageDto>>> GetPropertyImages([FromQuery] GetPropertyImagesRequest request)
    {
        try
        {
            var result = await _propertyImageService.GetPropertyImagesAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving property images");
            throw;
        }
    }
}
