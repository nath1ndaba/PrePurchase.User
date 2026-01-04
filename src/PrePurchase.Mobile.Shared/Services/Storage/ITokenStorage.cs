namespace PrePurchase.Mobile.Shared.Services.Storage;

public interface ITokenStorage
{
    Task StoreAccessTokenAsync(string token);
    Task<string?> GetAccessTokenAsync();
    
    Task StoreRefreshTokenAsync(string token);
    Task<string?> GetRefreshTokenAsync();
    
    Task StoreUserDataAsync(string userData);
    Task<string?> GetUserDataAsync();
    
    Task ClearTokensAsync();
}
