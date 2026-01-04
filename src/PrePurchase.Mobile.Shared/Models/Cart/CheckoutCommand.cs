namespace PrePurchase.Mobile.Shared.Models.Cart;

public class CheckoutCommand
{
    public Guid UserId { get; set; }
    public Guid ShopId { get; set; }
    public Guid? AuthorizedCollectorId { get; set; }
}
