using PrePurchase.Mobile.Shared.Models.Cart;
using PrePurchase.Mobile.Shared.Models.Common;

namespace PrePurchase.Mobile.Shared.Services.Cart;

public interface ICartService
{
    Task<Result<ShoppingCartDto>> GetCartAsync();
    Task<Result<List<CartItemDetailDto>>> GetCartItemsAsync();
    Task<Result<CartSummaryDto>> GetCartSummaryAsync();
    Task<Result<Guid>> AddToCartAsync(Guid userId, Guid shopId, Guid shopProductId, int quantity);
    Task<Result<ShoppingCartDto>> UpdateCartItemAsync(Guid userId, Guid shopProductId, int quantity);
    Task<Result<ShoppingCartDto>> RemoveCartItemAsync(Guid shopProductId);
    Task<Result<bool>> ClearCartAsync();
    Task<Result<Guid>> CheckoutAsync(Guid userId, Guid cartId, Guid? authorizedCollectorId = null, decimal platformFeePercentage = 0.05m);
}