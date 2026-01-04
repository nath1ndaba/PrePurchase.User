using Microsoft.Maui.Storage;
using PrePurchase.Mobile.Shared.Constants;

namespace PrePurchase.Mobile.Shared.Services.Storage;

public class SecureTokenStorage : ITokenStorage
{
    public async Task StoreAccessTokenAsync(string token)
    {
        await SecureStorage.SetAsync(ApiConstants.StorageKeys.ACCESS_TOKEN, token);
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        return await SecureStorage.GetAsync(ApiConstants.StorageKeys.ACCESS_TOKEN);
    }

    public async Task StoreRefreshTokenAsync(string token)
    {
        await SecureStorage.SetAsync(ApiConstants.StorageKeys.REFRESH_TOKEN, token);
    }

    public async Task<string?> GetRefreshTokenAsync()
    {
        return await SecureStorage.GetAsync(ApiConstants.StorageKeys.REFRESH_TOKEN);
    }

    public async Task StoreUserDataAsync(string userData)
    {
        await SecureStorage.SetAsync(ApiConstants.StorageKeys.USER_DATA, userData);
    }

    public async Task<string?> GetUserDataAsync()
    {
        return await SecureStorage.GetAsync(ApiConstants.StorageKeys.USER_DATA);
    }

    public Task ClearTokensAsync()
    {
        SecureStorage.Remove(ApiConstants.StorageKeys.ACCESS_TOKEN);
        SecureStorage.Remove(ApiConstants.StorageKeys.REFRESH_TOKEN);
        SecureStorage.Remove(ApiConstants.StorageKeys.USER_DATA);
        return Task.CompletedTask;
    }
}
