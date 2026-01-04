namespace PrePurchase.Mobile.Shared.Models.Settlement;

public class CreateManualSettlementCommand
{
    public Guid ShopId { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
}
