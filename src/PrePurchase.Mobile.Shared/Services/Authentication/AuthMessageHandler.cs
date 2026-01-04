using System.Net;
using System.Net.Http.Headers;
using PrePurchase.Mobile.Shared.Services.Storage;
using PrePurchase.Mobile.Shared.Services.Api;
using PrePurchase.Mobile.Shared.Models.Auth;

namespace PrePurchase.Mobile.Shared.Handlers;

public class AuthTokenHandler(ITokenStorage storage, IAuthApi authApi) : DelegatingHandler
{
    private readonly ITokenStorage _storage = storage;
    private readonly IAuthApi _authApi = authApi;
    private readonly SemaphoreSlim _refreshSemaphore = new(1, 1);
    private bool _isRefreshing = false;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Skip auth for login/register/refresh endpoints
        var path = request.RequestUri?.PathAndQuery ?? "";
        if (path.Contains("/api/auth/login") ||
            path.Contains("/api/auth/register") ||
            path.Contains("/api/auth/refresh"))
        {
            return await base.SendAsync(request, cancellationToken);
        }

        // Get access token
        var accessToken = await _storage.GetAccessTokenAsync();

        // Add token with Bearer prefix
        if (!string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        // Send request
        var response = await base.SendAsync(request, cancellationToken);

        // If 401 and not already refreshing, try to refresh token ONCE
        if (response.StatusCode == HttpStatusCode.Unauthorized && !_isRefreshing)
        {
            await _refreshSemaphore.WaitAsync(cancellationToken);

            try
            {
                _isRefreshing = true;

                var refreshToken = await _storage.GetRefreshTokenAsync();
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return response; // No refresh token, return 401
                }

                // Call refresh endpoint
                var refreshCommand = new RefreshTokenCommand { RefreshToken = refreshToken };
                var refreshResponse = await _authApi.RefreshTokenAsync(refreshCommand);

                if (refreshResponse.Success && refreshResponse.Data != null)
                {
                    // Store new tokens
                    await _storage.StoreAccessTokenAsync(refreshResponse.Data.AccessToken);
                    await _storage.StoreRefreshTokenAsync(refreshResponse.Data.RefreshToken);

                    // Create NEW request with new token
                    var newRequest = new HttpRequestMessage(request.Method, request.RequestUri)
                    {
                        Version = request.Version
                    };

                    // Add Bearer token to new request
                    newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", refreshResponse.Data.AccessToken);

                    // Copy other headers (skip Authorization as we already set it)
                    foreach (var header in request.Headers)
                    {
                        if (header.Key != "Authorization")
                        {
                            newRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
                        }
                    }

                    // Copy content if present
                    if (request.Content != null)
                    {
                        var contentBytes = await request.Content.ReadAsByteArrayAsync(cancellationToken);
                        newRequest.Content = new ByteArrayContent(contentBytes);

                        foreach (var header in request.Content.Headers)
                        {
                            newRequest.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                        }
                    }

                    // Retry with new token
                    response = await base.SendAsync(newRequest, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token refresh failed: {ex.Message}");
            }
            finally
            {
                _isRefreshing = false;
                _refreshSemaphore.Release();
            }
        }

        return response;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _refreshSemaphore?.Dispose();
        }
        base.Dispose(disposing);
    }
}