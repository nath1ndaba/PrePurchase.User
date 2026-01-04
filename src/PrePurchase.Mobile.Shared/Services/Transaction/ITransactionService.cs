using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.Transaction;
using System.Transactions;

namespace PrePurchase.Mobile.Shared.Services.Transaction;

public interface ITransactionService
{
    Task<Result<PagedResult<TransactionDto>>> GetMyTransactionsAsync(string? status = null, int pageNumber = 1, int pageSize = 20);
    Task<Result<PagedResult<TransactionDto>>> GetShopTransactionsAsync(
        Guid shopId,
        TransactionStatusDto? status = null,
        DateTime? fromDate = null,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default);
    Task<Result<TransactionDetailsDto>> GetTransactionByIdAsync(Guid id);
    Task<Result<TransactionDetailsDto>> ApproveTransactionAsync(Guid id);
    Task<Result<TransactionDetailsDto>> RejectTransactionAsync(Guid id, Guid transactionId, Guid shopOwnerId, string? reason = null, string? notes = null);
    Task<Result<TransactionDetailsDto>> CollectTransactionAsync(Guid id, Guid transactionId, string? collectorName = null, Guid? authorizedCollectorId = null, List<CollectItemDto>? itemsToCollect = null);
}
