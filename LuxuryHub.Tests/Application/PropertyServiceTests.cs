using AutoMapper;
using FluentAssertions;
using LuxuryHub.Application.DTOs;
using LuxuryHub.Application.Requests;
using LuxuryHub.Application.Services;
using LuxuryHub.Domain.Entities;
using LuxuryHub.Domain.Exceptions;
using LuxuryHub.Domain.Interfaces;
using LuxuryHub.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace LuxuryHub.Tests.Application;

public class PropertyServiceTests
{
    private readonly Mock<IPropertyRepository> _propertyRepositoryMock;
    private readonly Mock<IRepository<PropertyImage>> _propertyImageRepositoryMock;
    private readonly Mock<IRepository<PropertyTrace>> _propertyTraceRepositoryMock;
    private readonly Mock<IRepository<Owner>> _ownerRepositoryMock;
    private readonly Mock<ICacheService> _cacheServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<PropertyService>> _loggerMock;
    private readonly PropertyService _propertyService;

    public PropertyServiceTests()
    {
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _propertyImageRepositoryMock = new Mock<IRepository<PropertyImage>>();
        _propertyTraceRepositoryMock = new Mock<IRepository<PropertyTrace>>();
        _ownerRepositoryMock = new Mock<IRepository<Owner>>();
        _cacheServiceMock = new Mock<ICacheService>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<PropertyService>>();

        _propertyService = new PropertyService(
            _propertyRepositoryMock.Object,
            _propertyImageRepositoryMock.Object,
            _propertyTraceRepositoryMock.Object,
            _ownerRepositoryMock.Object,
            _cacheServiceMock.Object,
            _mapperMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task GetPropertiesAsync_WithValidRequest_ReturnsPaginatedResult()
    {
        // Arrange
        var request = new GetPropertiesRequest
        {
            PageNumber = 1,
            PageSize = 10,
            Name = "Test Property"
        };

        var properties = new List<Property>
        {
            new Property { Id = "1", Name = "Test Property 1", Price = 100000 },
            new Property { Id = "2", Name = "Test Property 2", Price = 200000 }
        };

        var propertyDtos = new List<PropertyDto>
        {
            new PropertyDto { Id = "1", Name = "Test Property 1", Price = 100000 },
            new PropertyDto { Id = "2", Name = "Test Property 2", Price = 200000 }
        };

        _propertyRepositoryMock.Setup(x => x.GetPropertiesWithFiltersAsync(
            request.Name, request.Address, request.MinPrice, request.MaxPrice, request.Skip, request.Take))
            .ReturnsAsync(properties);

        _propertyRepositoryMock.Setup(x => x.GetPropertiesCountAsync(
            request.Name, request.Address, request.MinPrice, request.MaxPrice))
            .ReturnsAsync(2);

        _mapperMock.Setup(x => x.Map<IEnumerable<PropertyDto>>(properties))
            .Returns(propertyDtos);

        // Act
        var result = await _propertyService.GetPropertiesAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(2);
        result.TotalCount.Should().Be(2);
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
        result.TotalPages.Should().Be(1);
        result.HasPreviousPage.Should().BeFalse();
        result.HasNextPage.Should().BeFalse();
    }

    [Fact]
    public async Task GetPropertyByIdAsync_WithValidId_ReturnsProperty()
    {
        // Arrange
        var propertyId = "1";
        var property = new Property { Id = propertyId, Name = "Test Property", Price = 100000 };
        var propertyDto = new PropertyDto { Id = propertyId, Name = "Test Property", Price = 100000 };

        _propertyRepositoryMock.Setup(x => x.GetPropertyWithOwnerAsync(propertyId))
            .ReturnsAsync(property);

        _mapperMock.Setup(x => x.Map<PropertyDto>(property))
            .Returns(propertyDto);

        // Act
        var result = await _propertyService.GetPropertyByIdAsync(propertyId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(propertyId);
        result.Name.Should().Be("Test Property");
        result.Price.Should().Be(100000);
    }

    [Fact]
    public async Task GetPropertyByIdAsync_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        var propertyId = "invalid-id";

        _propertyRepositoryMock.Setup(x => x.GetPropertyWithOwnerAsync(propertyId))
            .ReturnsAsync((Property?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => 
            _propertyService.GetPropertyByIdAsync(propertyId));
    }

    [Fact]
    public async Task GetPropertyImagesAsync_WithValidPropertyId_ReturnsImages()
    {
        // Arrange
        var propertyId = "1";
        var images = new List<PropertyImage>
        {
            new PropertyImage { Id = "1", IdProperty = propertyId, File = "image1.jpg", Enabled = true },
            new PropertyImage { Id = "2", IdProperty = propertyId, File = "image2.jpg", Enabled = true }
        };

        var imageDtos = new List<PropertyImageDto>
        {
            new PropertyImageDto { Id = "1", IdProperty = propertyId, File = "image1.jpg", Enabled = true },
            new PropertyImageDto { Id = "2", IdProperty = propertyId, File = "image2.jpg", Enabled = true }
        };

        _propertyRepositoryMock.Setup(x => x.ExistsAsync(propertyId))
            .ReturnsAsync(true);

        _propertyImageRepositoryMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(images);

        _mapperMock.Setup(x => x.Map<IEnumerable<PropertyImageDto>>(It.IsAny<IEnumerable<PropertyImage>>()))
            .Returns(imageDtos);

        // Act
        var result = await _propertyService.GetPropertyImagesAsync(propertyId);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().OnlyContain(img => img.IdProperty == propertyId && img.Enabled);
    }

    [Fact]
    public async Task GetPropertyImagesAsync_WithInvalidPropertyId_ThrowsNotFoundException()
    {
        // Arrange
        var propertyId = "invalid-id";

        _propertyRepositoryMock.Setup(x => x.ExistsAsync(propertyId))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => 
            _propertyService.GetPropertyImagesAsync(propertyId));
    }

    [Fact]
    public async Task GetPropertyTracesAsync_WithValidPropertyId_ReturnsTraces()
    {
        // Arrange
        var propertyId = "1";
        var traces = new List<PropertyTrace>
        {
            new PropertyTrace { Id = "1", IdProperty = propertyId, DateSale = DateTime.Now, Value = 100000, Tax = 5000 },
            new PropertyTrace { Id = "2", IdProperty = propertyId, DateSale = DateTime.Now.AddDays(-1), Value = 95000, Tax = 4750 }
        };

        var traceDtos = new List<PropertyTraceDto>
        {
            new PropertyTraceDto { Id = "1", IdProperty = propertyId, DateSale = DateTime.Now, Value = 100000, Tax = 5000 },
            new PropertyTraceDto { Id = "2", IdProperty = propertyId, DateSale = DateTime.Now.AddDays(-1), Value = 95000, Tax = 4750 }
        };

        _propertyRepositoryMock.Setup(x => x.ExistsAsync(propertyId))
            .ReturnsAsync(true);

        _propertyTraceRepositoryMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(traces);

        _mapperMock.Setup(x => x.Map<IEnumerable<PropertyTraceDto>>(It.IsAny<IEnumerable<PropertyTrace>>()))
            .Returns(traceDtos);

        // Act
        var result = await _propertyService.GetPropertyTracesAsync(propertyId);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().OnlyContain(trace => trace.IdProperty == propertyId);
    }
}
