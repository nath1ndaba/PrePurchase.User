// Updated AuthenticationService.cs
using Microsoft.AspNetCore.Components.Authorization;
using PrePurchase.Mobile.Shared.Models.Auth;
using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.User;
using PrePurchase.Mobile.Shared.Services.Api;
using PrePurchase.Mobile.Shared.Services.Storage;
using Refit;
using System.Text.Json;
using static PrePurchase.Mobile.Shared.Constants.ApiConstants;

namespace PrePurchase.Mobile.Shared.Services.Authentication;

public class AuthenticationService(
    IAuthApi authApi,
    ITokenStorage tokenStorage,
    AuthenticationStateProvider authProvider) : IAuthenticationService
{
    private readonly IAuthApi _authApi = authApi;
    private readonly ITokenStorage _tokenStorage = tokenStorage;
    private readonly AuthenticationStateProvider _authProvider = authProvider;

    public async Task<Result<AuthResponse>> LoginAsync(LoginCommand request)
    {
        try
        {
            var response = await _authApi.LoginAsync(request);

            if (response.Success && response.Data != null)
            {
                // Store tokens
                await _tokenStorage.StoreAccessTokenAsync(response.Data.AccessToken);
                await _tokenStorage.StoreRefreshTokenAsync(response.Data.RefreshToken);


                UserProfileDto user = new UserProfileDto
                {
                    Id = response.Data.UserId,
                    Email = response.Data.Email,
                    FirstName = response.Data.FirstName,
                    LastName = response.Data.LastName,
                    Role = response.Data.Role,
                    ShopIds = response.Data.ShopIds
                };
                // Store user data
                var userJson = JsonSerializer.Serialize(user);
                await _tokenStorage.StoreUserDataAsync(userJson);
                (_authProvider as CustomAuthStateProvider)?.NotifyUserAuthentication();

                return Result<AuthResponse>.Success(response.Data);
            }

            return Result<AuthResponse>.Failure(response.Message ?? "Login failed");
        }
        catch (ApiException ex)
        {
            return Result<AuthResponse>.Failure($"Login error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return Result<AuthResponse>.Failure($"Login error: {ex.Message}");
        }
    }

    public async Task<Result<AuthResponse>> RegisterAsync(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string password,
        UserRole role)
    {
        try
        {
            var request = new RegisterCommand
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                PhoneNumber = phoneNumber,
                Role = role
            };

            var response = await _authApi.RegisterAsync(request);

            if (response.Success && response.Data != null)
            {
                // Store tokens
                await _tokenStorage.StoreAccessTokenAsync(response.Data.AccessToken);
                await _tokenStorage.StoreRefreshTokenAsync(response.Data.RefreshToken);

                // Store user data
                var userJson = JsonSerializer.Serialize(response.Data);
                await _tokenStorage.StoreUserDataAsync(userJson);

                return Result<AuthResponse>.Success(response.Data);
            }

            return Result<AuthResponse>.Failure(response.Message ?? "Registration failed");
        }
        catch (Exception ex)
        {
            return Result<AuthResponse>.Failure($"Registration error: {ex.Message}");
        }
    }

    public async Task<bool> RefreshTokenAsync()
    {
        try
        {
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();
            if (string.IsNullOrEmpty(refreshToken))
                return false;

            var request = new RefreshTokenCommand { RefreshToken = refreshToken };
            var response = await _authApi.RefreshTokenAsync(request);

            if (response.Success && response.Data != null)
            {
                await _tokenStorage.StoreAccessTokenAsync(response.Data.AccessToken);
                await _tokenStorage.StoreRefreshTokenAsync(response.Data.RefreshToken);
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        await _tokenStorage.ClearTokensAsync();
        (_authProvider as CustomAuthStateProvider)?.NotifyUserLogout();
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await _tokenStorage.GetAccessTokenAsync();

        if (string.IsNullOrEmpty(token))
            return false;

        // Check if token is expired
        if (TokenHelper.IsTokenExpired(token))
        {
            // Try to refresh
            var refreshed = await RefreshTokenAsync();
            return refreshed;
        }

        // Check if token is expiring soon (within 5 minutes) and proactively refresh
        if (TokenHelper.IsTokenExpiringSoon(token, minutesThreshold: 5))
        {
            // Refresh in background, but don't wait for it
            _ = Task.Run(async () => await RefreshTokenAsync());
        }

        return true;
    }

    public async Task<UserProfileDto?> GetCurrentUserAsync()
    {
        try
        {
            var userJson = await _tokenStorage.GetUserDataAsync();
            if (string.IsNullOrEmpty(userJson))
                return null;

            return JsonSerializer.Deserialize<UserProfileDto>(userJson);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> ValidateAndRefreshTokenAsync()
    {
        var token = await _tokenStorage.GetAccessTokenAsync();

        if (string.IsNullOrEmpty(token))
            return false;

        if (TokenHelper.IsTokenExpired(token))
        {
            return await RefreshTokenAsync();
        }

        return true;
    }
}