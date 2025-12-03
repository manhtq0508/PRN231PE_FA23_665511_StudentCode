using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO.SilverJewelry;

public class PutSilverJewelryDTO
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
    [RegularExpression(@"^([A-Z][a-z0-9]*)(\s[A-Z][a-z0-9]*)*$", ErrorMessage = "Name must contain only letters, digits and spaces. Each word must begin with a capital letter.")]
    public string Name { get; set; } = null!;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "MetalWeight is required")]
    [Range(0.01, float.MaxValue, ErrorMessage = "MetalWeight must be greater than 0")]
    public float? MetalWeight { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "ProductionYear is required")]
    [Range(1990, int.MaxValue, ErrorMessage = "Production year must be from 1990")]
    public int ProductionYear { get; set; }

    [Required(ErrorMessage = "CategoryId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be greater than 0")]
    public int? CategoryId { get; set; }
}
