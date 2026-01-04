using Microsoft.AspNetCore.Components.Authorization;
using PrePurchase.Mobile.Shared.Services.Storage;
using System.Security.Claims;

public class CustomAuthStateProvider(ITokenStorage tokenStorage) : AuthenticationStateProvider
{
    private readonly ITokenStorage _tokenStorage = tokenStorage;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _tokenStorage.GetAccessTokenAsync();

        if (string.IsNullOrEmpty(token))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("jwt", token)
        }, "jwt"));

        return new AuthenticationState(user);
    }

    public void NotifyUserAuthentication()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))
        ));
    }
}
