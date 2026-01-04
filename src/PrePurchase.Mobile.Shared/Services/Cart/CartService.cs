using PrePurchase.Mobile.Shared.Models.Cart;
using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Services.Api;

namespace PrePurchase.Mobile.Shared.Services.Cart;

public class CartService(ICartApi cartApi) : ICartService
{
    private readonly ICartApi _cartApi = cartApi;

    public async Task<Result<ShoppingCartDto>> GetCartAsync()
    {
        try
        {
            var response = await _cartApi.GetCartAsync();

            if (response.Success && response.Data != null)
            {
                return Result<ShoppingCartDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<ShoppingCartDto>.Failure(response.Errors)
                : Result<ShoppingCartDto>.Failure(response.Message ?? "Failed to fetch cart");
        }
        catch (Exception ex)
        {
            return Result<ShoppingCartDto>.Failure($"Error fetching cart: {ex.Message}");
        }
    }

    public async Task<Result<List<CartItemDetailDto>>> GetCartItemsAsync()
    {
        try
        {
            var response = await _cartApi.GetCartItemsAsync();

            if (response.Success && response.Data != null)
            {
                return Result<List<CartItemDetailDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<List<CartItemDetailDto>>.Failure(response.Errors)
                : Result<List<CartItemDetailDto>>.Failure(response.Message ?? "Failed to fetch cart items");
        }
        catch (Exception ex)
        {
            return Result<List<CartItemDetailDto>>.Failure($"Error fetching cart items: {ex.Message}");
        }
    }

    public async Task<Result<Guid>> AddToCartAsync(Guid userId, Guid shopId, Guid shopProductId, int quantity)
    {
        try
        {
            var command = new AddToCartCommand
            {
                UserId = userId,
                ShopId = shopId,
                ShopProductId = shopProductId,
                Quantity = quantity
            };

            var response = await _cartApi.AddToCartAsync(command);

            if (response.Success && response.Data != Guid.Empty)
            {
                return Result<Guid>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<Guid>.Failure(response.Errors)
                : Result<Guid>.Failure(response.Message ?? "Failed to add item to cart");
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure($"Error adding to cart: {ex.Message}");
        }
    }

    public async Task<Result<ShoppingCartDto>> UpdateCartItemAsync(Guid userId, Guid shopProductId, int quantity)
    {
        try
        {
            var command = new UpdateCartItemCommand
            {
                UserId = userId,
                ShopProductId = shopProductId,
                Quantity = quantity
            };

            var response = await _cartApi.UpdateCartItemAsync(command);

            if (response.Success && response.Data != null)
            {
                return Result<ShoppingCartDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<ShoppingCartDto>.Failure(response.Errors)
                : Result<ShoppingCartDto>.Failure(response.Message ?? "Failed to update cart item");
        }
        catch (Exception ex)
        {
            return Result<ShoppingCartDto>.Failure($"Error updating cart item: {ex.Message}");
        }
    }

    public async Task<Result<ShoppingCartDto>> RemoveCartItemAsync(Guid shopProductId)
    {
        try
        {
            var response = await _cartApi.RemoveCartItemAsync(shopProductId);

            if (response.Success && response.Data != null)
            {
                return Result<ShoppingCartDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<ShoppingCartDto>.Failure(response.Errors)
                : Result<ShoppingCartDto>.Failure(response.Message ?? "Failed to remove item from cart");
        }
        catch (Exception ex)
        {
            return Result<ShoppingCartDto>.Failure($"Error removing cart item: {ex.Message}");
        }
    }

    public async Task<Result<bool>> ClearCartAsync()
    {
        try
        {
            var response = await _cartApi.ClearCartAsync();

            if (response.Success)
            {
                return Result<bool>.Success(true);
            }

            return response.Errors?.Count == 0
                ? Result<bool>.Failure(response.Errors)
                : Result<bool>.Failure(response.Message ?? "Failed to clear cart");
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Error clearing cart: {ex.Message}");
        }
    }

    public async Task<Result<Guid>> CheckoutAsync(Guid userId, Guid cartId, Guid? authorizedCollectorId = null, decimal platformFeePercentage = 0.05m)
    {
        try
        {
            var command = new CheckoutCartCommand
            {
                UserId = userId,
                CartId = cartId,
                AuthorizedCollectorId = authorizedCollectorId,
                PlatformFeePercentage = platformFeePercentage
            };

            var response = await _cartApi.CheckoutAsync(command);

            if (response.Success && response.Data != Guid.Empty)
            {
                return Result<Guid>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<Guid>.Failure(response.Errors)
                : Result<Guid>.Failure(response.Message ?? "Checkout failed");
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure($"Error during checkout: {ex.Message}");
        }
    }

    public async Task<Result<CartSummaryDto>> GetCartSummaryAsync()
    {
        try
        {
            var response = await _cartApi.GetCartSummaryAsync();

            if (response.Success && response.Data != null)
            {
                return Result<CartSummaryDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<CartSummaryDto>.Failure(response.Errors)
                : Result<CartSummaryDto>.Failure(response.Message ?? "Failed to fetch cart summary");
        }
        catch (Exception ex)
        {
            return Result<CartSummaryDto>.Failure($"Error fetching cart summary: {ex.Message}");
        }
    }
}