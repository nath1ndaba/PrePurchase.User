using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.Wallet;
using static PrePurchase.Mobile.Shared.Constants.ApiConstants;

namespace PrePurchase.Mobile.Shared.Services.Wallet;

public interface IWalletService
{
    Task<Result<WalletDto>> GetWalletAsync();
    Task<Result<WalletResponseDto>> LoadWalletAsync(Guid userId, decimal amount, PaymentMethod paymentMethod, PaymentGateway gateway, TransactionType transactionType);
    Task<Result<PagedResult<WalletTransactionDto>>> GetWalletTransactionsAsync(int pageNumber = 1, int pageSize = 20);
}
