namespace PrePurchase.Mobile.Shared.Models.Settlement;

public class SettlementDetailsDto
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PlatformFee { get; set; }
    public decimal NetAmount { get; set; }
    public int TransactionCount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public List<SettlementTransactionDto> Transactions { get; set; } = new();
}

public class SettlementTransactionDto
{
    public Guid TransactionId { get; set; }
    public DateTime CollectionDate { get; set; }
    public decimal Amount { get; set; }
    public decimal Fee { get; set; }
}
