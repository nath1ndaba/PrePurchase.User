using PrePurchase.Mobile.Shared.Models.Cart;
using PrePurchase.Mobile.Shared.Models.Common;
using Refit;

namespace PrePurchase.Mobile.Shared.Services.Api;

public interface ICartApi
{
    // GET /api/cart - Get current shopping cart
    [Get("/api/cart")]
    Task<Models.Common.ApiResponse<ShoppingCartDto>> GetCartAsync();

    // GET /api/cart/items - Get detailed cart items
    [Get("/api/cart/items")]
    Task<Models.Common.ApiResponse<List<CartItemDetailDto>>> GetCartItemsAsync();

    // GET /api/cart/summary - Get cart summary
    [Get("/api/cart/summary")]
    Task<Models.Common.ApiResponse<CartSummaryDto>> GetCartSummaryAsync();

    // POST /api/cart/items - Add item (returns Guid, not ShoppingCartDto!)
    [Post("/api/cart/items")]
    Task<Models.Common.ApiResponse<Guid>> AddToCartAsync([Body] AddToCartCommand request);

    // PUT /api/cart/items - Update cart item
    [Put("/api/cart/items")]
    Task<Models.Common.ApiResponse<ShoppingCartDto>> UpdateCartItemAsync([Body] UpdateCartItemCommand request);

    // DELETE /api/cart/items/{shopProductId} - Remove item
    [Delete("/api/cart/items/{shopProductId}")]
    Task<Models.Common.ApiResponse<ShoppingCartDto>> RemoveCartItemAsync(Guid shopProductId);

    // DELETE /api/cart - Clear cart (returns ApiResponse with no data!)
    [Delete("/api/cart")]
    Task<Models.Common.ApiResponse<string>> ClearCartAsync();

    // POST /api/cart/checkout - Checkout (returns Guid transaction ID, not CheckoutResponse!)
    [Post("/api/cart/checkout")]
    Task<Models.Common.ApiResponse<Guid>> CheckoutAsync([Body] CheckoutCartCommand request);

    // GET /api/cart/validate - Validate cart
    [Get("/api/cart/validate")]
    Task<Models.Common.ApiResponse<CartValidationResult>> ValidateCartAsync();
}