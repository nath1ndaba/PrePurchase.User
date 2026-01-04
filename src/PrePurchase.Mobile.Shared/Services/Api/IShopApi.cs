using PrePurchase.Mobile.Shared.Models.Shop;
using PrePurchase.Mobile.Shared.Models.Common;
using Refit;

namespace PrePurchase.Mobile.Shared.Services.Api;

public interface IShopApi
{
    [Get("/api/shops/search")]
    Task<Models.Common.ApiResponse<PagedResult<ShopDto>>> SearchShopsAsync(
        [Query] string? searchTerm = null,
        [Query] int pageNumber = 1,
        [Query] int pageSize = 20);

    [Get("/api/shops/nearby")]
    Task<Models.Common.ApiResponse<List<ShopDto>>> GetNearbyShopsAsync(
        [Query] double latitude,
        [Query] double longitude,
        [Query] double radiusKm = 50.0);

    [Get("/api/shops/{id}")]
    Task<Models.Common.ApiResponse<ShopDetailsDto>> GetShopByIdAsync(
         Guid id,
         [Query] double? latitude = null,
         [Query] double? longitude = null,
         [Query] bool includeReviews = true
     );

    [Post("/api/shops")]
    Task<Models.Common.ApiResponse<Guid>> CreateShopAsync([Body] CreateShopCommand request);

    [Put("/api/shops/{id}")]
    Task<Models.Common.ApiResponse<ShopDto>> UpdateShopAsync(Guid id, [Body] UpdateShopCommand request);
}
