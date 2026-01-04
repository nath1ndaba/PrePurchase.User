namespace PrePurchase.Mobile.Shared.Models.Wallet;

public class WalletTransactionDto
{
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public decimal Amount { get; set; }
    public decimal BalanceBefore { get; set; }
    public decimal BalanceAfter { get; set; }
    public string? Reference { get; set; }
    public DateTime CreatedAt { get; set; }
}
