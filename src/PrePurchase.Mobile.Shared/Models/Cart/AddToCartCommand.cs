namespace PrePurchase.Mobile.Shared.Models.Cart;

// Matches backend AddToCartCommand
public class AddToCartCommand
{
    public Guid UserId { get; set; }
    public Guid ShopId { get; set; }
    public Guid ShopProductId { get; set; }
    public int Quantity { get; set; }
}
