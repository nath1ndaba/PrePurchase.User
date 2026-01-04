namespace PrePurchase.Mobile.Shared.Services.Cart;

public interface ICartStateService
{
    int CartItemCount { get; }
    event Action? OnCartUpdated;
    Task RefreshCartCountAsync();
    Task NotifyCartUpdatedAsync();
}