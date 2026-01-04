using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.CreditScore;

namespace PrePurchase.Mobile.Shared.Services.CreditScore;

public interface ICreditScoreService
{
    Task<Result<CreditScoreDto>> GetCreditScoreAsync();
    Task<Result<CreditScoreDto>> CalculateCreditScoreAsync(Guid userId);
}
