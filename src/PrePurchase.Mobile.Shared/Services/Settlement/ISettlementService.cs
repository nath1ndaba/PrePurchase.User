using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.Settlement;

namespace PrePurchase.Mobile.Shared.Services.Settlement;

public interface ISettlementService
{
    Task<Result<PagedResult<SettlementDto>>> GetShopSettlementsAsync(Guid shopId, string? status = null, int pageNumber = 1, int pageSize = 20);
    Task<Result<SettlementDetailsDto>> GetSettlementByIdAsync(Guid id);
    Task<Result<SettlementSummaryDto>> GetSettlementSummaryAsync(Guid shopId);
    Task<Result<PagedResult<SettlementDto>>> GetPendingSettlementsAsync(int pageNumber = 1, int pageSize = 20);
    Task<Result<SettlementDto>> ProcessSettlementAsync(Guid id);
    Task<Result<Guid>> CreateManualSettlementAsync(Guid shopId, DateTime periodStart, DateTime periodEnd);
}
