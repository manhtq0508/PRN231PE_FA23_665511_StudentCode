using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class SilverJewelryViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [RegularExpression(@"^([A-Z][a-z0-9]*)(\s[A-Z][a-z0-9]*)*$", ErrorMessage = "Name must contain only letters, digits and spaces. Each word must begin with a capital letter.")]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = "Metal Weight is required")]
    [Range(0.01, float.MaxValue, ErrorMessage = "Metal Weight must be greater than 0")]
    public float MetalWeight { get; set; }
    
    [Required(ErrorMessage = "Price is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Production Year is required")]
    [Range(1900, int.MaxValue, ErrorMessage = "Production Year must be greater than or equal to 1900")]
    public int ProductionYear { get; set; }
    
    [Required(ErrorMessage = "Created Date is required")]
    public DateOnly CreatedDate { get; set; }
    
    [Required(ErrorMessage = "Category is required")]
    public int CategoryId { get; set; }
    
    public string? CategoryName { get; set; }
}

public class SilverJewelrySearchViewModel
{
    public string? Name { get; set; }
    public float? MinMetalWeight { get; set; }
    public float? MaxMetalWeight { get; set; }
}

public class CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
