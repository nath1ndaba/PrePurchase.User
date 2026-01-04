using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.Shop;

namespace PrePurchase.Mobile.Shared.Services.Shop;

public interface IShopService
{
    Task<Result<PagedResult<ShopDto>>> SearchShopsAsync(string? searchTerm = null, int pageNumber = 1, int pageSize = 20);
    Task<Result<List<ShopDto>>> GetNearbyShopsAsync(double latitude, double longitude, double radiusKm = 50.0);
    Task<Result<ShopDetailsDto>> GetShopByIdAsync(Guid id, double latitude, double longitude, bool includeReviews = true);
    Task<Result<Guid>> CreateShopAsync(Guid ownerId, string name, string? description, string? street, string? city,
        string? postalCode, string? suburb, string? province, double latitude, double longitude, double serviceRadiusKm);
    Task<Result<ShopDto>> UpdateShopAsync(Guid id, Guid shopId, Guid ownerId, string name, string? description,
        string? street, string? city, string? postalCode, string? suburb, string? province, double serviceRadiusKm,
        string? openingTime, string? closingTime, List<string>? closedDays);
}
