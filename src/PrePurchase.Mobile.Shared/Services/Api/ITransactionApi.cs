using PrePurchase.Mobile.Shared.Models.Transaction;
using PrePurchase.Mobile.Shared.Models.Common;
using Refit;

namespace PrePurchase.Mobile.Shared.Services.Api;

public interface ITransactionApi
{
    [Get("/api/transactions/my-transactions")]
    Task<Models.Common.ApiResponse<PagedResult<TransactionDto>>> GetMyTransactionsAsync(
        [Query] string? status = null,
        [Query] int pageNumber = 1,
        [Query] int pageSize = 20);

    [Get("/api/transactions/{id}")]
    Task<Models.Common.ApiResponse<TransactionDetailsDto>> GetTransactionByIdAsync(Guid id);

    [Post("/api/transactions/{id}/approve")]
    Task<Models.Common.ApiResponse<TransactionDetailsDto>> ApproveTransactionAsync(Guid id);

    [Post("/api/transactions/{id}/reject")]
    Task<Models.Common.ApiResponse<TransactionDetailsDto>> RejectTransactionAsync(Guid id, [Body] RejectTransactionCommand request);

    [Post("/api/transactions/{id}/collect")]
    Task<Models.Common.ApiResponse<TransactionDetailsDto>> CollectTransactionAsync(Guid id, [Body] CollectTransactionCommand request);

    [Get("/api/transactions/shop/{shopId}")]
    Task<Models.Common.ApiResponse<PagedResult<TransactionDto>>> GetShopTransactionsAsync(
      Guid shopId,
      [Query] TransactionStatusDto? status = null,
      [Query] DateTime? fromDate = null,
      [Query] int pageNumber = 1,
      [Query] int pageSize = 20);
}
