namespace PrePurchase.Mobile.Shared.Models.Cart;

// Matches backend CartItemDetailDto (for GET /api/cart/items)
public class CartItemDetailDto
{
    public Guid Id { get; set; }
    public Guid ShopProductId { get; set; }
    public string? ProductName { get; set; }
    public string? Brand { get; set; }
    public string? Size { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public int AvailableStock { get; set; }
    public bool IsAvailable { get; set; }
    public string? ImageUrl { get; set; }
}
