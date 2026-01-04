namespace PrePurchase.Mobile.Shared.Models.Cart;

public class CheckoutResponse
{
    public Guid TransactionId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
