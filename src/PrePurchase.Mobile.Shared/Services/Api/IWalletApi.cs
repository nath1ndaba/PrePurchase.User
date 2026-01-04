using PrePurchase.Mobile.Shared.Models.Wallet;
using PrePurchase.Mobile.Shared.Models.Common;
using Refit;

namespace PrePurchase.Mobile.Shared.Services.Api;

public interface IWalletApi
{
    [Get("/api/wallets/my-wallet")]
    Task<Models.Common.ApiResponse<WalletDto>> GetWalletAsync();

    [Post("/api/wallets/load")]
    Task<Models.Common.ApiResponse<WalletResponseDto>> LoadWalletAsync([Body] LoadWalletCommand request);

    [Get("/api/wallets/transactions")]
    Task<Models.Common.ApiResponse<PagedResult<WalletTransactionDto>>> GetWalletTransactionsAsync(
        [Query] int page = 1,
        [Query] int pageSize = 20);
}
