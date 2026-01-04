namespace PrePurchase.Mobile.Shared.Models.DemandForecast;

public class LowStockAlertDto
{
    public Guid ShopProductId { get; set; }
    public string? ProductName { get; set; }
    public string? Brand { get; set; }
    public string? Size { get; set; }
    public int CurrentStock { get; set; }
    public int PredictedDemandNext7Days { get; set; }
    public int RecommendedReorderQuantity { get; set; }
    public string? AlertLevel { get; set; }
}
