namespace PrePurchase.Mobile.Shared.Models.Cart;

// Matches backend UpdateCartItemCommand
public class UpdateCartItemCommand
{
    public Guid UserId { get; set; }
    public Guid ShopProductId { get; set; }
    public int Quantity { get; set; }
}
