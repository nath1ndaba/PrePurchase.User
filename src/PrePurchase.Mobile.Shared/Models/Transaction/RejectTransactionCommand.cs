namespace PrePurchase.Mobile.Shared.Models.Transaction;

public class RejectTransactionCommand
{
    public Guid TransactionId { get; set; }
    public Guid ShopOwnerId { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
}
