using System.ComponentModel.DataAnnotations;

namespace LuxuryHub.Application.Requests;

public class CreatePropertyTraceRequest
{
    [Required(ErrorMessage = "Date sale is required")]
    public DateTime DateSale { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name must not exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Value is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to 0")]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "Tax is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Tax must be greater than or equal to 0")]
    public decimal Tax { get; set; }

    [Required(ErrorMessage = "Property ID is required")]
    public string IdProperty { get; set; } = string.Empty;
}
