using PrePurchase.Mobile.Shared.Models.Collector;
using Refit;

namespace PrePurchase.Mobile.Shared.Services.Api;

public interface IAuthorizedCollectorApi
{
    [Get("/api/authorized-collectors")]
    Task<Models.Common.ApiResponse<List<AuthorizedCollectorDto>>> GetMyCollectorsAsync();

    [Post("/api/authorized-collectors")]
    Task<Models.Common.ApiResponse<Guid>> AddCollectorAsync([Body] AddAuthorizedCollectorCommand request);

    [Put("/api/authorized-collectors/{id}")]
    Task<Models.Common.ApiResponse<AuthorizedCollectorDto>> UpdateCollectorAsync(Guid id, [Body] UpdateAuthorizedCollectorCommand request);

    [Delete("/api/authorized-collectors/{id}")]
    Task<Models.Common.ApiResponse<AuthorizedCollectorDto>> DeactivateCollectorAsync(Guid id);
}
