namespace PrePurchase.Mobile.Shared.Models.CreditScore;

public class CreditScoreDto
{
    public Guid Id { get; set; }
    public int Score { get; set; }
    public string? ScoreBand { get; set; }
    public int TransactionCount { get; set; }
    public decimal AverageMonthlySpend { get; set; }
    public int ConsecutiveMonthsActive { get; set; }
    public double OnTimeCollectionRate { get; set; }
    public DateTime LastCalculated { get; set; }
    public string? Factors { get; set; }
    public List<string>? Benefits { get; set; }
}
