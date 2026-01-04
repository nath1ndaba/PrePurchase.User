using PrePurchase.Mobile.Shared.Models.Collector;
using PrePurchase.Mobile.Shared.Models.Common;

namespace PrePurchase.Mobile.Shared.Services.AuthorizedCollector;

public interface IAuthorizedCollectorService
{
    Task<Result<List<AuthorizedCollectorDto>>> GetMyCollectorsAsync();
    Task<Result<Guid>> AddCollectorAsync(Guid userId, string collectorName, string collectorPhone, string? relationship = null);
    Task<Result<AuthorizedCollectorDto>> UpdateCollectorAsync(Guid id, Guid collectorId, Guid userId, string collectorName, string collectorPhone, string? relationship = null);
    Task<Result> DeactivateCollectorAsync(Guid id);
}
