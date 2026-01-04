namespace PrePurchase.Mobile.Shared.Models.Product;

public class ProductDto
{
    // From your JSON - these property names MUST match exactly
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CurrentStock { get; set; }
    public bool IsAvailable { get; set; }
    public string ImageUrl { get; set; }

    // If you want to use "Name" in your UI instead of "ProductName"
    public string Name => ProductName;

    // If you need CategoryName (you're using it in your UI but it's not in the JSON)
    public string CategoryName { get; set; } = string.Empty;
}
