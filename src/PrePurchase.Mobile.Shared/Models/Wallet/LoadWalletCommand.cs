using static PrePurchase.Mobile.Shared.Constants.ApiConstants;

namespace PrePurchase.Mobile.Shared.Models.Wallet;

public class LoadWalletCommand
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public int PaymentMethod { get; set; }  // PaymentMethod enum
    public int Gateway { get; set; }  // PaymentGateway enum
    public int TransactionType { get; set; }  // TransactionType enum
}
