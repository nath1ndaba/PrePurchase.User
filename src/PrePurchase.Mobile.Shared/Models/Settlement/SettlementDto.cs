namespace PrePurchase.Mobile.Shared.Models.Settlement;

public class SettlementDto
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public string? ShopName { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public decimal TotalSales { get; set; }
    public decimal PlatformFees { get; set; }
    public decimal SettlementAmount { get; set; }
    public string? Status { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? PaymentReference { get; set; }
    public DateTime CreatedAt { get; set; }
}
