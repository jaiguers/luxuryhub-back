using System.ComponentModel.DataAnnotations;

namespace LuxuryHub.Application.Requests;

public class CreateOwnerRequest
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name must not exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    [StringLength(200, ErrorMessage = "Address must not exceed 200 characters")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Photo is required")]
    [StringLength(500, ErrorMessage = "Photo URL must not exceed 500 characters")]
    public string Photo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Birthday is required")]
    public DateTime Birthday { get; set; }
}
