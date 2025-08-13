using System.ComponentModel.DataAnnotations;

namespace LuxuryHub.Application.Requests;

public class GetOwnersRequest
{
    [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100")]
    public int PageSize { get; set; } = 10;

    [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
    public int PageNumber { get; set; } = 1;

    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;
}
