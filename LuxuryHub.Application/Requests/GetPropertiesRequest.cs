using System.ComponentModel.DataAnnotations;

namespace LuxuryHub.Application.Requests;

public class GetPropertiesRequest
{
    [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100")]
    public int PageSize { get; set; } = 10;

    [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
    public int PageNumber { get; set; } = 1;

    public string? Name { get; set; }
    public string? Address { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Minimum price must be greater than or equal to 0")]
    public decimal? MinPrice { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Maximum price must be greater than or equal to 0")]
    public decimal? MaxPrice { get; set; }

    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;
}
