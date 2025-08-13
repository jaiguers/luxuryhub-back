using System.ComponentModel.DataAnnotations;

namespace LuxuryHub.Application.Requests;

public class CreatePropertyRequest
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name must not exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    [StringLength(200, ErrorMessage = "Address must not exceed 200 characters")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Code internal is required")]
    [StringLength(50, ErrorMessage = "Code internal must not exceed 50 characters")]
    public string CodeInternal { get; set; } = string.Empty;

    [Required(ErrorMessage = "Year is required")]
    [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Owner ID is required")]
    public string IdOwner { get; set; } = string.Empty;
}
