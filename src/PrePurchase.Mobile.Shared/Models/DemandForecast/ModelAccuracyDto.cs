namespace PrePurchase.Mobile.Shared.Models.DemandForecast;

public class ModelAccuracyDto
{
    public double OverallAccuracy { get; set; }
    public int TotalForecasts { get; set; }
    public int AccurateForecasts { get; set; }
    public string? ModelVersion { get; set; }
    public DateTime LastCalculated { get; set; }
}
