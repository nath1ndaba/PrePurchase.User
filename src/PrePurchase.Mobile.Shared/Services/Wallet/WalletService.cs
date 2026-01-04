using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.Wallet;
using PrePurchase.Mobile.Shared.Services.Api;
using Refit;
using static PrePurchase.Mobile.Shared.Constants.ApiConstants;

namespace PrePurchase.Mobile.Shared.Services.Wallet;

public class WalletService : IWalletService
{
    private readonly IWalletApi _walletApi;

    public WalletService(IWalletApi walletApi)
    {
        _walletApi = walletApi;
    }

    public async Task<Result<WalletDto>> GetWalletAsync()
    {
        try
        {
            var response = await _walletApi.GetWalletAsync();

            if (response.Success && response.Data != null)
            {
                return Result<WalletDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<WalletDto>.Failure(response.Errors)
                : Result<WalletDto>.Failure(response.Message ?? "Failed to fetch wallet");
        }
        catch (Exception ex)
        {
            return Result<WalletDto>.Failure($"Error fetching wallet: {ex.Message}");
        }
    }

    public async Task<Result<WalletResponseDto>> LoadWalletAsync(Guid userId, decimal amount, PaymentMethod paymentMethod, PaymentGateway gateway, TransactionType transactionType)
    {
        try
        {
            var command = new LoadWalletCommand
            {
                UserId = userId,
                Amount = amount,
                PaymentMethod = (int)paymentMethod,
                Gateway = (int)gateway,
                TransactionType = (int)transactionType
            };

            var response = await _walletApi.LoadWalletAsync(command);

            if (response.Success && response.Data != null)
            {
                return Result<WalletResponseDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<WalletResponseDto>.Failure(response.Errors)
                : Result<WalletResponseDto>.Failure(response.Message ?? "Failed to load wallet");
        }
        catch (ApiException ex)
        {
            return Result<WalletResponseDto>.Failure($"Error loading wallet: {ex.Message}");
        }
        catch (Exception ex)
        {
            return Result<WalletResponseDto>.Failure($"Error loading wallet: {ex.Message}");
        }
    }

    public async Task<Result<PagedResult<WalletTransactionDto>>> GetWalletTransactionsAsync(int page = 1, int pageSize = 20)
    {
        try
        {
            var response = await _walletApi.GetWalletTransactionsAsync(page, pageSize);

            if (response.Success && response.Data != null)
            {
                return Result<PagedResult<WalletTransactionDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<PagedResult<WalletTransactionDto>>.Failure(response.Errors)
                : Result<PagedResult<WalletTransactionDto>>.Failure(response.Message ?? "Failed to fetch wallet transactions");
        }
        catch (Exception ex)
        {
            return Result<PagedResult<WalletTransactionDto>>.Failure($"Error fetching wallet transactions: {ex.Message}");
        }
    }
}
