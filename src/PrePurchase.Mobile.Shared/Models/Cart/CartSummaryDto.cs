namespace PrePurchase.Mobile.Shared.Models.Cart;

// Matches backend CartSummaryDto
public class CartSummaryDto
{
    public int ItemCount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool HasCart { get; set; }
    public Guid? CartId { get; set; }
    public Guid? ShopId { get; set; }
}
