using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.Settlement;
using PrePurchase.Mobile.Shared.Services.Api;

namespace PrePurchase.Mobile.Shared.Services.Settlement;

public class SettlementService(ISettlementApi settlementApi) : ISettlementService
{
    private readonly ISettlementApi _settlementApi = settlementApi;

    public async Task<Result<PagedResult<SettlementDto>>> GetShopSettlementsAsync(Guid shopId, string? status = null, int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            var response = await _settlementApi.GetShopSettlementsAsync(shopId, status, pageNumber, pageSize);

            if (response.Success && response.Data != null)
            {
                return Result<PagedResult<SettlementDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<PagedResult<SettlementDto>>.Failure(response.Errors)
                : Result<PagedResult<SettlementDto>>.Failure(response.Message ?? "Failed to fetch shop settlements");
        }
        catch (Exception ex)
        {
            return Result<PagedResult<SettlementDto>>.Failure($"Error fetching shop settlements: {ex.Message}");
        }
    }

    public async Task<Result<SettlementDetailsDto>> GetSettlementByIdAsync(Guid id)
    {
        try
        {
            var response = await _settlementApi.GetSettlementByIdAsync(id);

            if (response.Success && response.Data != null)
            {
                return Result<SettlementDetailsDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<SettlementDetailsDto>.Failure(response.Errors)
                : Result<SettlementDetailsDto>.Failure(response.Message ?? "Failed to fetch settlement details");
        }
        catch (Exception ex)
        {
            return Result<SettlementDetailsDto>.Failure($"Error fetching settlement details: {ex.Message}");
        }
    }

    public async Task<Result<SettlementSummaryDto>> GetSettlementSummaryAsync(Guid shopId)
    {
        try
        {
            var response = await _settlementApi.GetSettlementSummaryAsync(shopId);

            if (response.Success && response.Data != null)
            {
                return Result<SettlementSummaryDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<SettlementSummaryDto>.Failure(response.Errors)
                : Result<SettlementSummaryDto>.Failure(response.Message ?? "Failed to fetch settlement summary");
        }
        catch (Exception ex)
        {
            return Result<SettlementSummaryDto>.Failure($"Error fetching settlement summary: {ex.Message}");
        }
    }

    public async Task<Result<PagedResult<SettlementDto>>> GetPendingSettlementsAsync(int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            var response = await _settlementApi.GetPendingSettlementsAsync(pageNumber, pageSize);

            if (response.Success && response.Data != null)
            {
                return Result<PagedResult<SettlementDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<PagedResult<SettlementDto>>.Failure(response.Errors)
                : Result<PagedResult<SettlementDto>>.Failure(response.Message ?? "Failed to fetch pending settlements");
        }
        catch (Exception ex)
        {
            return Result<PagedResult<SettlementDto>>.Failure($"Error fetching pending settlements: {ex.Message}");
        }
    }

    public async Task<Result<SettlementDto>> ProcessSettlementAsync(Guid id)
    {
        try
        {
            var response = await _settlementApi.ProcessSettlementAsync(id);

            if (response.Success && response.Data != null)
            {
                return Result<SettlementDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<SettlementDto>.Failure(response.Errors)
                : Result<SettlementDto>.Failure(response.Message ?? "Failed to process settlement");
        }
        catch (Exception ex)
        {
            return Result<SettlementDto>.Failure($"Error processing settlement: {ex.Message}");
        }
    }

    public async Task<Result<Guid>> CreateManualSettlementAsync(Guid shopId, DateTime periodStart, DateTime periodEnd)
    {
        try
        {
            var command = new CreateManualSettlementCommand
            {
                ShopId = shopId,
                PeriodStart = periodStart,
                PeriodEnd = periodEnd
            };

            var response = await _settlementApi.CreateManualSettlementAsync(command);

            if (response.Success && response.Data != default)
            {
                return Result<Guid>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<Guid>.Failure(response.Errors)
                : Result<Guid>.Failure(response.Message ?? "Failed to create manual settlement");
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure($"Error creating manual settlement: {ex.Message}");
        }
    }
}
