using PrePurchase.Mobile.Shared.Models.Settlement;
using PrePurchase.Mobile.Shared.Models.Common;
using Refit;

namespace PrePurchase.Mobile.Shared.Services.Api;

public interface ISettlementApi
{
    [Get("/api/settlements/shop/{shopId}")]
    Task<Models.Common.ApiResponse<PagedResult<SettlementDto>>> GetShopSettlementsAsync(
        Guid shopId,
        [Query] string? status = null,
        [Query] int pageNumber = 1,
        [Query] int pageSize = 20);

    [Get("/api/settlements/{id}")]
    Task<Models.Common.ApiResponse<SettlementDetailsDto>> GetSettlementByIdAsync(Guid id);

    [Get("/api/settlements/shop/{shopId}/summary")]
    Task<Models.Common.ApiResponse<SettlementSummaryDto>> GetSettlementSummaryAsync(Guid shopId);

    [Get("/api/settlements/pending")]
    Task<Models.Common.ApiResponse<PagedResult<SettlementDto>>> GetPendingSettlementsAsync(
        [Query] int pageNumber = 1,
        [Query] int pageSize = 20);

    [Post("/api/settlements/{id}/process")]
    Task<Models.Common.ApiResponse<SettlementDto>> ProcessSettlementAsync(Guid id);

    [Post("/api/settlements/manual")]
    Task<Models.Common.ApiResponse<Guid>> CreateManualSettlementAsync([Body] CreateManualSettlementCommand request);
}
