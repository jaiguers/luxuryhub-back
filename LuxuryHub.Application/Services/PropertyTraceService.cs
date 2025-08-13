using AutoMapper;
using LuxuryHub.Application.DTOs;
using LuxuryHub.Application.Interfaces;
using LuxuryHub.Application.Requests;
using LuxuryHub.Domain.Entities;
using LuxuryHub.Domain.Exceptions;
using LuxuryHub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace LuxuryHub.Application.Services;

public class PropertyTraceService : IPropertyTraceService
{
    private readonly IRepository<PropertyTrace> _propertyTraceRepository;
    private readonly IRepository<Property> _propertyRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PropertyTraceService> _logger;

    public PropertyTraceService(
        IRepository<PropertyTrace> propertyTraceRepository,
        IRepository<Property> propertyRepository,
        IMapper mapper,
        ILogger<PropertyTraceService> logger)
    {
        _propertyTraceRepository = propertyTraceRepository;
        _propertyRepository = propertyRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PropertyTraceDto> CreatePropertyTraceAsync(CreatePropertyTraceRequest request)
    {
        try
        {
            _logger.LogInformation("Creating new property trace for property ID: {PropertyId}", request.IdProperty);

            // Verify property exists
            var propertyExists = await _propertyRepository.ExistsAsync(request.IdProperty);
            if (!propertyExists)
            {
                throw new NotFoundException("Property", request.IdProperty);
            }

            var propertyTrace = new PropertyTrace
            {
                DateSale = request.DateSale,
                Name = request.Name,
                Value = request.Value,
                Tax = request.Tax,
                IdProperty = request.IdProperty,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdTrace = await _propertyTraceRepository.AddAsync(propertyTrace);
            var traceDto = _mapper.Map<PropertyTraceDto>(createdTrace);

            _logger.LogInformation("Successfully created property trace with ID: {TraceId}", traceDto.Id);

            return traceDto;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating property trace");
            throw;
        }
    }

    public async Task<PropertyTraceDto> GetPropertyTraceByIdAsync(string id)
    {
        try
        {
            _logger.LogInformation("Retrieving property trace with ID: {TraceId}", id);

            var trace = await _propertyTraceRepository.GetByIdAsync(id);
            if (trace == null)
            {
                throw new NotFoundException("PropertyTrace", id);
            }

            var traceDto = _mapper.Map<PropertyTraceDto>(trace);
            _logger.LogInformation("Successfully retrieved property trace with ID: {TraceId}", id);

            return traceDto;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving property trace with ID: {TraceId}", id);
            throw;
        }
    }

    public async Task<PaginatedResult<PropertyTraceDto>> GetPropertyTracesAsync(GetPropertyTracesRequest request)
    {
        try
        {
            _logger.LogInformation("Retrieving property traces with pagination: Page={PageNumber}, Size={PageSize}",
                request.PageNumber, request.PageSize);

            var traces = await _propertyTraceRepository.GetAllAsync();

            // Filter by property if specified
            if (!string.IsNullOrEmpty(request.IdProperty))
            {
                traces = traces.Where(trace => trace.IdProperty == request.IdProperty);
            }

            // Order by date sale descending
            traces = traces.OrderByDescending(trace => trace.DateSale);

            var totalCount = traces.Count();

            var pagedTraces = traces
                .Skip(request.Skip)
                .Take(request.Take)
                .ToList();

            var traceDtos = _mapper.Map<IEnumerable<PropertyTraceDto>>(pagedTraces);

            var result = new PaginatedResult<PropertyTraceDto>
            {
                Items = traceDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
                HasPreviousPage = request.PageNumber > 1,
                HasNextPage = request.PageNumber < (int)Math.Ceiling((double)totalCount / request.PageSize)
            };

            _logger.LogInformation("Retrieved {Count} property traces out of {TotalCount} total", traceDtos.Count(), totalCount);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving property traces");
            throw;
        }
    }
}
