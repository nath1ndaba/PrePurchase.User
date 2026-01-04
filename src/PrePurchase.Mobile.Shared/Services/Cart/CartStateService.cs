namespace PrePurchase.Mobile.Shared.Services.Cart;

public class CartStateService(ICartService cartService) : ICartStateService
{
    private readonly ICartService _cartService = cartService;
    private int _cartItemCount;

    public int CartItemCount => _cartItemCount;
    public event Action? OnCartUpdated;

    public async Task RefreshCartCountAsync()
    {
        try
        {
            // Use the lightweight summary endpoint instead of full cart
            var result = await _cartService.GetCartSummaryAsync();
            if (result.IsSuccess && result.Data != null)
            {
                _cartItemCount = result.Data.ItemCount;
            }
            else
            {
                _cartItemCount = 0;
            }
        }
        catch
        {
            _cartItemCount = 0;
        }
    }

    public async Task NotifyCartUpdatedAsync()
    {
        await RefreshCartCountAsync();
        OnCartUpdated?.Invoke();
    }
}