using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.DemandForecast;
using PrePurchase.Mobile.Shared.Services.Api;

namespace PrePurchase.Mobile.Shared.Services.DemandForecast;

public class DemandForecastService(IDemandForecastApi demandForecastApi) : IDemandForecastService
{
    private readonly IDemandForecastApi _demandForecastApi = demandForecastApi;

    public async Task<Result<DemandForecastDto>> GenerateForecastAsync(Guid shopId, int forecastDays = 30)
    {
        try
        {
            var command = new GenerateDemandForecastCommand
            {
                ShopId = shopId,
                ForecastDays = forecastDays
            };

            var response = await _demandForecastApi.GenerateForecastAsync(command);

            if (response.Success && response.Data != null)
            {
                return Result<DemandForecastDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<DemandForecastDto>.Failure(response.Errors)
                : Result<DemandForecastDto>.Failure(response.Message ?? "Failed to generate forecast");
        }
        catch (Exception ex)
        {
            return Result<DemandForecastDto>.Failure($"Error generating forecast: {ex.Message}");
        }
    }

    public async Task<Result<PagedResult<DemandForecastDto>>> GetDemandForecastsAsync(Guid shopId, int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            var response = await _demandForecastApi.GetDemandForecastsAsync(shopId, pageNumber, pageSize);

            if (response.Success && response.Data != null)
            {
                return Result<PagedResult<DemandForecastDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<PagedResult<DemandForecastDto>>.Failure(response.Errors)
                : Result<PagedResult<DemandForecastDto>>.Failure(response.Message ?? "Failed to fetch demand forecasts");
        }
        catch (Exception ex)
        {
            return Result<PagedResult<DemandForecastDto>>.Failure($"Error fetching demand forecasts: {ex.Message}");
        }
    }

    public async Task<Result<List<LowStockAlertDto>>> GetLowStockAlertsAsync(Guid shopId)
    {
        try
        {
            var response = await _demandForecastApi.GetLowStockAlertsAsync(shopId);

            if (response.Success && response.Data != null)
            {
                return Result<List<LowStockAlertDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<List<LowStockAlertDto>>.Failure(response.Errors)
                : Result<List<LowStockAlertDto>>.Failure(response.Message ?? "Failed to fetch low stock alerts");
        }
        catch (Exception ex)
        {
            return Result<List<LowStockAlertDto>>.Failure($"Error fetching low stock alerts: {ex.Message}");
        }
    }

    public async Task<Result<ModelAccuracyDto>> GetModelAccuracyAsync(Guid shopId)
    {
        try
        {
            var response = await _demandForecastApi.GetModelAccuracyAsync(shopId);

            if (response.Success && response.Data != null)
            {
                return Result<ModelAccuracyDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<ModelAccuracyDto>.Failure(response.Errors)
                : Result<ModelAccuracyDto>.Failure(response.Message ?? "Failed to fetch model accuracy");
        }
        catch (Exception ex)
        {
            return Result<ModelAccuracyDto>.Failure($"Error fetching model accuracy: {ex.Message}");
        }
    }
}
