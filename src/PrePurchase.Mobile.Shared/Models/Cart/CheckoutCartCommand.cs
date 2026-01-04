namespace PrePurchase.Mobile.Shared.Models.Cart;

// Matches backend CheckoutCartCommand (NOT CheckoutCommand!)
public class CheckoutCartCommand
{
    public Guid UserId { get; set; }
    public Guid CartId { get; set; }
    public Guid? AuthorizedCollectorId { get; set; }
    public decimal PlatformFeePercentage { get; set; } = 0.05m; // 5% default
}
