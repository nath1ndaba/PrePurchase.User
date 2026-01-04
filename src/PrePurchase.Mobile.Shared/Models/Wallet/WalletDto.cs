namespace PrePurchase.Mobile.Shared.Models.Wallet;

public class WalletDto
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; }
    public decimal LifetimeSpent { get; set; }
    public decimal LifetimeLoaded { get; set; }
}

public class WalletResponseDto
{
    public decimal NewBalance { get; set; }
    public decimal AmountLoaded { get; set; }
    public string TransactionId { get; set; } = string.Empty;
}
