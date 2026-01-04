namespace PrePurchase.Mobile.Shared.Models.DemandForecast;

public class GenerateDemandForecastCommand
{
    public Guid ShopId { get; set; }
    public int ForecastDays { get; set; } = 30;
}
