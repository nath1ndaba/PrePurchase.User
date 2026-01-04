using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.CreditScore;
using PrePurchase.Mobile.Shared.Services.Api;

namespace PrePurchase.Mobile.Shared.Services.CreditScore;

public class CreditScoreService(ICreditScoreApi creditScoreApi) : ICreditScoreService
{
    private readonly ICreditScoreApi _creditScoreApi = creditScoreApi;

    public async Task<Result<CreditScoreDto>> GetCreditScoreAsync()
    {
        try
        {
            var response = await _creditScoreApi.GetCreditScoreAsync();

            if (response.Success && response.Data != null)
            {
                return Result<CreditScoreDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<CreditScoreDto>.Failure(response.Errors)
                : Result<CreditScoreDto>.Failure(response.Message ?? "Failed to fetch credit score");
        }
        catch (Exception ex)
        {
            return Result<CreditScoreDto>.Failure($"Error fetching credit score: {ex.Message}");
        }
    }

    public async Task<Result<CreditScoreDto>> CalculateCreditScoreAsync(Guid userId)
    {
        try
        {
            var command = new CalculateCreditScoreCommand
            {
                UserId = userId
            };

            var response = await _creditScoreApi.CalculateCreditScoreAsync(command);

            if (response.Success && response.Data != null)
            {
                return Result<CreditScoreDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<CreditScoreDto>.Failure(response.Errors)
                : Result<CreditScoreDto>.Failure(response.Message ?? "Failed to calculate credit score");
        }
        catch (Exception ex)
        {
            return Result<CreditScoreDto>.Failure($"Error calculating credit score: {ex.Message}");
        }
    }
}
