using PrePurchase.Mobile.Shared.Models.DemandForecast;
using PrePurchase.Mobile.Shared.Models.Common;
using Refit;

namespace PrePurchase.Mobile.Shared.Services.Api;

public interface IDemandForecastApi
{
    [Post("/api/demand-forecast/generate")]
    Task<Models.Common.ApiResponse<DemandForecastDto>> GenerateForecastAsync([Body] GenerateDemandForecastCommand request);

    [Get("/api/demand-forecast/{shopId}")]
    Task<Models.Common.ApiResponse<PagedResult<DemandForecastDto>>> GetDemandForecastsAsync(
        Guid shopId,
        [Query] int pageNumber = 1,
        [Query] int pageSize = 20);

    [Get("/api/demand-forecast/{shopId}/low-stock-alerts")]
    Task<Models.Common.ApiResponse<List<LowStockAlertDto>>> GetLowStockAlertsAsync(Guid shopId);

    [Get("/api/demand-forecast/{shopId}/model-accuracy")]
    Task<Models.Common.ApiResponse<ModelAccuracyDto>> GetModelAccuracyAsync(Guid shopId);
}
