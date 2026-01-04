namespace PrePurchase.Mobile.Shared.Models.Settlement;

public class SettlementSummaryDto
{
    public decimal TotalSettled { get; set; }
    public decimal PendingSettlement { get; set; }
    public decimal CurrentMonthSales { get; set; }
    public int CompletedSettlements { get; set; }
    public int PendingSettlements { get; set; }
}
