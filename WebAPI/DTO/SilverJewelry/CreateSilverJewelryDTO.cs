using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO.SilverJewelry;

public class CreateSilverJewelryDTO
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "MetalWeight is required")]
    [Range(0.01, float.MaxValue, ErrorMessage = "MetalWeight must be greater than 0")]
    public float? MetalWeight { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "ProductionYear is required")]
    [Range(1990, int.MaxValue, ErrorMessage = "Production year must be from 1990")]
    public int ProductionYear { get; set; }

    [Required(ErrorMessage = "CategoryId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be greater than 0")]
    public int? CategoryId { get; set; }
}
