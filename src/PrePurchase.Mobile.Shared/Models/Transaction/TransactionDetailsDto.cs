namespace PrePurchase.Mobile.Shared.Models.Transaction;

public class TransactionDetailsDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public Guid ShopId { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public DateTime? RejectedAt { get; set; }
    public DateTime? CollectionDate { get; set; }
    public string? RejectionReason { get; set; }
    public Guid? CollectorId { get; set; }
    public string? CollectorName { get; set; }
    public List<TransactionItemDto> Items { get; set; } = new();
}

public class TransactionItemDto
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal SubTotal { get; set; }
}
