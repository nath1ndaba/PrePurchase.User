namespace PrePurchase.Mobile.Shared.Models.DemandForecast;

public class DemandForecastDto
{
    public Guid Id { get; set; }
    public Guid ShopProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int CurrentStock { get; set; }
    public int PredictedDemand { get; set; }
    public int RecommendedStock { get; set; }
    public double ConfidenceScore { get; set; }
    public DateTime ForecastDate { get; set; }
    public DateTime GeneratedAt { get; set; }
}
