namespace PrePurchase.Mobile.Shared.Models.Cart;

public class CartItemDto
{
    public Guid Id { get; set; }
    public Guid ShopProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal { get; set; }
    public string? ImageUrl { get; set; }
}
