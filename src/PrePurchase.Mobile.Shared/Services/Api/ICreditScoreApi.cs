using PrePurchase.Mobile.Shared.Models.CreditScore;
using Refit;

namespace PrePurchase.Mobile.Shared.Services.Api;

public interface ICreditScoreApi
{
    [Get("/api/credit-score")]
    Task<Models.Common.ApiResponse<CreditScoreDto>> GetCreditScoreAsync();

    [Post("/api/credit-score/calculate")]
    Task<Models.Common.ApiResponse<CreditScoreDto>> CalculateCreditScoreAsync([Body] CalculateCreditScoreCommand request);
}
