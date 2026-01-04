namespace PrePurchase.Mobile.Shared.Models.Cart;

// Matches backend ShoppingCartDto
public class ShoppingCartDto
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public string? ShopName { get; set; }
    public List<ShoppingCartItemDto>? Items { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalItems { get; set; }
}
