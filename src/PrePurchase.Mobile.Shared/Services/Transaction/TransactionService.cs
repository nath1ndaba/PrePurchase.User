using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.Transaction;
using PrePurchase.Mobile.Shared.Services.Api;

namespace PrePurchase.Mobile.Shared.Services.Transaction;

public class TransactionService(ITransactionApi transactionApi) : ITransactionService
{
    private readonly ITransactionApi _transactionApi = transactionApi;

    public async Task<Result<PagedResult<TransactionDto>>> GetShopTransactionsAsync(
        Guid shopId,
        TransactionStatusDto? status = null,
        DateTime? fromDate = null,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _transactionApi.GetShopTransactionsAsync(
                                shopId,
                                status,
                                fromDate,
                                pageNumber,
                                pageSize);

            if (response.Success && response.Data is not null)
            {
                return Result<PagedResult<TransactionDto>>.Success(response.Data);
            }

            if (response.Errors is { Count: > 0 })
            {
                return Result<PagedResult<TransactionDto>>.Failure(response.Errors);
            }

            return Result<PagedResult<TransactionDto>>.Failure(
                response.Message ?? "Failed to fetch transactions");
        }
        catch (Exception ex)
        {
            return Result<PagedResult<TransactionDto>>.Failure(
                $"Error fetching transactions: {ex.Message}");
        }
    }

    public async Task<Result<PagedResult<TransactionDto>>> GetMyTransactionsAsync(string? status = null, int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            var response = await _transactionApi.GetMyTransactionsAsync(status, pageNumber, pageSize);

            if (response.Success && response.Data != null)
            {
                return Result<PagedResult<TransactionDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<PagedResult<TransactionDto>>.Failure(response.Errors)
                : Result<PagedResult<TransactionDto>>.Failure(response.Message ?? "Failed to fetch transactions");
        }
        catch (Exception ex)
        {
            return Result<PagedResult<TransactionDto>>.Failure($"Error fetching transactions: {ex.Message}");
        }
    }

    public async Task<Result<TransactionDetailsDto>> GetTransactionByIdAsync(Guid id)
    {
        try
        {
            var response = await _transactionApi.GetTransactionByIdAsync(id);

            if (response.Success && response.Data != null)
            {
                return Result<TransactionDetailsDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<TransactionDetailsDto>.Failure(response.Errors)
                : Result<TransactionDetailsDto>.Failure(response.Message ?? "Failed to fetch transaction details");
        }
        catch (Exception ex)
        {
            return Result<TransactionDetailsDto>.Failure($"Error fetching transaction details: {ex.Message}");
        }
    }

    public async Task<Result<TransactionDetailsDto>> ApproveTransactionAsync(Guid id)
    {
        try
        {
            var response = await _transactionApi.ApproveTransactionAsync(id);

            if (response.Success && response.Data != null)
            {
                return Result<TransactionDetailsDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<TransactionDetailsDto>.Failure(response.Errors)
                : Result<TransactionDetailsDto>.Failure(response.Message ?? "Failed to approve transaction");
        }
        catch (Exception ex)
        {
            return Result<TransactionDetailsDto>.Failure($"Error approving transaction: {ex.Message}");
        }
    }

    public async Task<Result<TransactionDetailsDto>> RejectTransactionAsync(Guid id, Guid transactionId, Guid shopOwnerId, string? reason = null, string? notes = null)
    {
        try
        {
            var command = new RejectTransactionCommand
            {
                TransactionId = transactionId,
                ShopOwnerId = shopOwnerId,
                Reason = reason,
                Notes = notes
            };

            var response = await _transactionApi.RejectTransactionAsync(id, command);

            if (response.Success && response.Data != null)
            {
                return Result<TransactionDetailsDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<TransactionDetailsDto>.Failure(response.Errors)
                : Result<TransactionDetailsDto>.Failure(response.Message ?? "Failed to reject transaction");
        }
        catch (Exception ex)
        {
            return Result<TransactionDetailsDto>.Failure($"Error rejecting transaction: {ex.Message}");
        }
    }

    public async Task<Result<TransactionDetailsDto>> CollectTransactionAsync(Guid id, Guid transactionId, string? collectorName = null, Guid? authorizedCollectorId = null, List<CollectItemDto>? itemsToCollect = null)
    {
        try
        {
            var command = new CollectTransactionCommand
            {
                TransactionId = transactionId,
                CollectorName = collectorName,
                AuthorizedCollectorId = authorizedCollectorId,
                ItemsToCollect = itemsToCollect
            };

            var response = await _transactionApi.CollectTransactionAsync(id, command);

            if (response.Success && response.Data != null)
            {
                return Result<TransactionDetailsDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<TransactionDetailsDto>.Failure(response.Errors)
                : Result<TransactionDetailsDto>.Failure(response.Message ?? "Failed to collect transaction");
        }
        catch (Exception ex)
        {
            return Result<TransactionDetailsDto>.Failure($"Error collecting transaction: {ex.Message}");
        }
    }
}
