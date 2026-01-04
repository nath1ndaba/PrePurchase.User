using PrePurchase.Mobile.Shared.Models.Common;

namespace PrePurchase.Mobile.Shared.Models.Product;

public class ProductDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public List<string>? Images { get; set; }
    public decimal Price { get; set; }
    public string? Unit { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public List<ShopProductDto> AvailableAt { get; set; } = new();
}

public class ShopProductDto
{
    public Guid ShopProductId { get; set; }
    public Guid ShopId { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool InStock { get; set; }
    public double Distance { get; set; }
}
