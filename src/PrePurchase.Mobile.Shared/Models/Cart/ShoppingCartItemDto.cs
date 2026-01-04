namespace PrePurchase.Mobile.Shared.Models.Cart;

// Matches backend ShoppingCartItemDto
public class ShoppingCartItemDto
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
