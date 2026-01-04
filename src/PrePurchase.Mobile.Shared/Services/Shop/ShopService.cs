using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.Shop;
using PrePurchase.Mobile.Shared.Services.Api;

namespace PrePurchase.Mobile.Shared.Services.Shop;

public class ShopService(IShopApi shopApi) : IShopService
{
    private readonly IShopApi _shopApi = shopApi;

    public async Task<Result<PagedResult<ShopDto>>> SearchShopsAsync(string? searchTerm = null, int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            var response = await _shopApi.SearchShopsAsync(searchTerm, pageNumber, pageSize);

            if (response.Success && response.Data != null)
            {
                return Result<PagedResult<ShopDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<PagedResult<ShopDto>>.Failure(response.Errors)
                : Result<PagedResult<ShopDto>>.Failure(response.Message ?? "Failed to search shops");
        }
        catch (Exception ex)
        {
            return Result<PagedResult<ShopDto>>.Failure($"Error searching shops: {ex.Message}");
        }
    }

    public async Task<Result<List<ShopDto>>> GetNearbyShopsAsync(double latitude, double longitude, double radiusKm = 50.0)
    {
        try
        {
            var response = await _shopApi.GetNearbyShopsAsync(latitude, longitude, radiusKm);

            
            if (response.Success && response.Data != null)
            {
                return Result<List<ShopDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<List<ShopDto>>.Failure(response.Errors)
                : Result<List<ShopDto>>.Failure(response.Message ?? "Failed to fetch nearby shops");
        }
        catch (Exception ex)
        {
            return Result<List<ShopDto>>.Failure($"Error fetching nearby shops: {ex.Message}");
        }
    }

    public async Task<Result<ShopDetailsDto>> GetShopByIdAsync(Guid id, double latitude, double longitude, bool incluedeReviews = true)
    {
        try
        {
            var response = await _shopApi.GetShopByIdAsync(id, latitude, longitude, incluedeReviews);

            if (response.Success && response.Data != null)
            {
                return Result<ShopDetailsDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0  
                ? Result<ShopDetailsDto>.Failure(response.Errors)
                : Result<ShopDetailsDto>.Failure(response.Message ?? "Failed to fetch shop details");
        }
        catch (Exception ex)
        {
            return Result<ShopDetailsDto>.Failure($"Error fetching shop details: {ex.Message}");
        }
    }

    public async Task<Result<Guid>> CreateShopAsync(Guid ownerId, string name, string? description, string? street,
        string? city, string? postalCode, string? suburb, string? province, double latitude, double longitude, double serviceRadiusKm)
    {
        try
        {
            var command = new CreateShopCommand
            {
                OwnerId = ownerId,
                Name = name,
                Description = description,
                Street = street,
                City = city,
                PostalCode = postalCode,
                Suburb = suburb,
                Province = province,
                Latitude = latitude,
                Longitude = longitude,
                ServiceRadiusKm = serviceRadiusKm
            };

            var response = await _shopApi.CreateShopAsync(command);

            if (response.Success && response.Data != default)
            {
                return Result<Guid>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<Guid>.Failure(response.Errors)
                : Result<Guid>.Failure(response.Message ?? "Failed to create shop");
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure($"Error creating shop: {ex.Message}");
        }
    }

    public async Task<Result<ShopDto>> UpdateShopAsync(Guid id, Guid shopId, Guid ownerId, string name, string? description,
        string? street, string? city, string? postalCode, string? suburb, string? province, double serviceRadiusKm,
        string? openingTime, string? closingTime, List<string>? closedDays)
    {
        try
        {
            var command = new UpdateShopCommand
            {
                ShopId = shopId,
                OwnerId = ownerId,
                Name = name,
                Description = description,
                Street = street,
                City = city,
                PostalCode = postalCode,
                Suburb = suburb,
                Province = province,
                ServiceRadiusKm = serviceRadiusKm,
                OpeningTime = openingTime,
                ClosingTime = closingTime,
                ClosedDays = closedDays
            };

            var response = await _shopApi.UpdateShopAsync(id, command);

            if (response.Success && response.Data != null)
            {
                return Result<ShopDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<ShopDto>.Failure(response.Errors)
                : Result<ShopDto>.Failure(response.Message ?? "Failed to update shop");
        }
        catch (Exception ex)
        {
            return Result<ShopDto>.Failure($"Error updating shop: {ex.Message}");
        }
    }
}
