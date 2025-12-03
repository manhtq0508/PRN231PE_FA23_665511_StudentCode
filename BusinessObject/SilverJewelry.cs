namespace BusinessObject;

public class SilverJewelry
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public float MetalWeight { get; set; }
    public decimal Price { get; set; }
    public int ProductionYear { get; set; }
    public DateOnly CreatedDate { get; set; }
    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;
}
