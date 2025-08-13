using LuxuryHub.Application.DTOs;
using LuxuryHub.Application.Interfaces;
using LuxuryHub.Application.Requests;
using LuxuryHub.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LuxuryHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OwnersController : ControllerBase
{
    private readonly IOwnerService _ownerService;
    private readonly ILogger<OwnersController> _logger;

    public OwnersController(IOwnerService ownerService, ILogger<OwnersController> logger)
    {
        _ownerService = ownerService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new owner
    /// </summary>
    /// <param name="request">Owner creation request</param>
    /// <returns>Created owner details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(OwnerDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<OwnerDto>> CreateOwner([FromBody] CreateOwnerRequest request)
    {
        try
        {
            _logger.LogInformation("Creating new owner with name: {OwnerName}", request.Name);

            var createdOwner = await _ownerService.CreateOwnerAsync(request);
            
            return CreatedAtAction(nameof(GetOwnerById), new { id = createdOwner.Id }, createdOwner);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating owner");
            throw;
        }
    }

    /// <summary>
    /// Get an owner by its ID
    /// </summary>
    /// <param name="id">Owner ID</param>
    /// <returns>Owner details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OwnerDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<OwnerDto>> GetOwnerById(string id)
    {
        try
        {
            var owner = await _ownerService.GetOwnerByIdAsync(id);
            return Ok(owner);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving owner with ID: {OwnerId}", id);
            throw;
        }
    }

    /// <summary>
    /// Get all owners with pagination
    /// </summary>
    /// <param name="request">Pagination parameters</param>
    /// <returns>Paginated list of owners</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<OwnerDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PaginatedResult<OwnerDto>>> GetOwners([FromQuery] GetOwnersRequest request)
    {
        try
        {
            var result = await _ownerService.GetOwnersAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving owners");
            throw;
        }
    }
}
