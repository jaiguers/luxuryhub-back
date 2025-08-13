using System.ComponentModel.DataAnnotations;

namespace LuxuryHub.Application.Requests;

public class CreatePropertyImageRequest
{
    [Required(ErrorMessage = "Property ID is required")]
    public string IdProperty { get; set; } = string.Empty;

    [Required(ErrorMessage = "File is required")]
    [StringLength(500, ErrorMessage = "File URL must not exceed 500 characters")]
    public string File { get; set; } = string.Empty;

    public bool Enabled { get; set; } = true;
}
