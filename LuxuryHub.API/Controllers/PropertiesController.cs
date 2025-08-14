using LuxuryHub.Application.DTOs;
using LuxuryHub.Application.Interfaces;
using LuxuryHub.Application.Requests;
using LuxuryHub.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LuxuryHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly ILogger<PropertiesController> _logger;

    public PropertiesController(IPropertyService propertyService, ILogger<PropertiesController> logger)
    {
        _propertyService = propertyService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new property
    /// </summary>
    /// <param name="request">Property creation request</param>
    /// <returns>Created property details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PropertyDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PropertyDto>> CreateProperty([FromBody] CreatePropertyRequest request)
    {
        try
        {
            _logger.LogInformation("Creating new property with name: {PropertyName}", request.Name);

            var createdProperty = await _propertyService.CreatePropertyAsync(request);
            
            return CreatedAtAction(nameof(GetPropertyById), new { id = createdProperty.Id }, createdProperty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating property");
            throw;
        }
    }

    /// <summary>
    /// Get all properties with optional filtering and pagination
    /// </summary>
    /// <param name="request">Filter and pagination parameters</param>
    /// <returns>Paginated list of properties</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<PropertyDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PaginatedResult<PropertyDto>>> GetProperties([FromQuery] GetPropertiesRequest request)
    {
        try
        {
            _logger.LogInformation("GET /api/properties called with request: {@Request}", request);
            var result = await _propertyService.GetPropertiesAsync(request);
            
            // Log the first property to debug
            var firstProperty = result.Items.FirstOrDefault();
            if (firstProperty != null)
            {
                _logger.LogInformation("First property - Owner: {@Owner}", firstProperty.Owner);
            }
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving properties");
            throw;
        }
    }

    /// <summary>
    /// Get a property by its ID
    /// </summary>
    /// <param name="id">Property ID</param>
    /// <returns>Property details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PropertyDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PropertyDto>> GetPropertyById(string id)
    {
        try
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            return Ok(property);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving property with ID: {PropertyId}", id);
            throw;
        }
    }

    /// <summary>
    /// Get property images by property ID
    /// </summary>
    /// <param name="propertyId">Property ID</param>
    /// <returns>List of property images</returns>
    [HttpGet("{propertyId}/images")]
    [ProducesResponseType(typeof(IEnumerable<PropertyImageDto>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<PropertyImageDto>>> GetPropertyImages(string propertyId)
    {
        try
        {
            var images = await _propertyService.GetPropertyImagesAsync(propertyId);
            return Ok(images);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving images for property ID: {PropertyId}", propertyId);
            throw;
        }
    }

    /// <summary>
    /// Get property traces by property ID
    /// </summary>
    /// <param name="propertyId">Property ID</param>
    /// <returns>List of property traces</returns>
    [HttpGet("{propertyId}/traces")]
    [ProducesResponseType(typeof(IEnumerable<PropertyTraceDto>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<PropertyTraceDto>>> GetPropertyTraces(string propertyId)
    {
        try
        {
            var traces = await _propertyService.GetPropertyTracesAsync(propertyId);
            return Ok(traces);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving traces for property ID: {PropertyId}", propertyId);
            throw;
        }
    }
}
