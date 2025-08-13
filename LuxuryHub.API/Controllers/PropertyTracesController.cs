using LuxuryHub.Application.DTOs;
using LuxuryHub.Application.Interfaces;
using LuxuryHub.Application.Requests;
using LuxuryHub.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LuxuryHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PropertyTracesController : ControllerBase
{
    private readonly IPropertyTraceService _propertyTraceService;
    private readonly ILogger<PropertyTracesController> _logger;

    public PropertyTracesController(IPropertyTraceService propertyTraceService, ILogger<PropertyTracesController> logger)
    {
        _propertyTraceService = propertyTraceService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new property trace
    /// </summary>
    /// <param name="request">Property trace creation request</param>
    /// <returns>Created property trace details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PropertyTraceDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PropertyTraceDto>> CreatePropertyTrace([FromBody] CreatePropertyTraceRequest request)
    {
        try
        {
            _logger.LogInformation("Creating new property trace for property ID: {PropertyId}", request.IdProperty);

            var createdTrace = await _propertyTraceService.CreatePropertyTraceAsync(request);
            
            return CreatedAtAction(nameof(GetPropertyTraceById), new { id = createdTrace.Id }, createdTrace);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating property trace");
            throw;
        }
    }

    /// <summary>
    /// Get a property trace by its ID
    /// </summary>
    /// <param name="id">Property trace ID</param>
    /// <returns>Property trace details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PropertyTraceDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PropertyTraceDto>> GetPropertyTraceById(string id)
    {
        try
        {
            var trace = await _propertyTraceService.GetPropertyTraceByIdAsync(id);
            return Ok(trace);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving property trace with ID: {TraceId}", id);
            throw;
        }
    }

    /// <summary>
    /// Get all property traces with pagination
    /// </summary>
    /// <param name="request">Pagination parameters</param>
    /// <returns>Paginated list of property traces</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<PropertyTraceDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PaginatedResult<PropertyTraceDto>>> GetPropertyTraces([FromQuery] GetPropertyTracesRequest request)
    {
        try
        {
            var result = await _propertyTraceService.GetPropertyTracesAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving property traces");
            throw;
        }
    }
}
