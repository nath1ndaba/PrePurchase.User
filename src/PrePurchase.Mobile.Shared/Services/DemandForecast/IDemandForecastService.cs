using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.DemandForecast;

namespace PrePurchase.Mobile.Shared.Services.DemandForecast;

public interface IDemandForecastService
{
    Task<Result<DemandForecastDto>> GenerateForecastAsync(Guid shopId, int forecastDays = 30);
    Task<Result<PagedResult<DemandForecastDto>>> GetDemandForecastsAsync(Guid shopId, int pageNumber = 1, int pageSize = 20);
    Task<Result<List<LowStockAlertDto>>> GetLowStockAlertsAsync(Guid shopId);
    Task<Result<ModelAccuracyDto>> GetModelAccuracyAsync(Guid shopId);
}
