namespace PrePurchase.Mobile.Shared.Models.Transaction;

public class TransactionDto
{
    public Guid TransactionId { get; set; }
    public string? TransactionNumber { get; set; }
    public TransactionStatusDto? Status { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ShopName { get; set; }
    public string? QrCode { get; set; }
    public int? UnavailableItemsCount { get; set; }
    public decimal? RefundAmount { get; set; }
}
public enum TransactionStatusDto
{
    PendingApproval = 0,  // ? NEW - Waiting for shop owner
    Approved = 1,          // ? NEW - Shop approved, ready for collection
    PartiallyApproved = 2, // ? NEW - Some items unavailable
    Completed = 3,         // ? RENAMED - Was status 1
    Collected = 4,         // Was status 2
    PartiallyCollected = 5,// Was status 3
    Cancelled = 6,         // Was status 4
    Refunded = 7,          // Was status 5
    Expired = 8,            // Was status 6
    Rejected = 9
}