using Microsoft.AspNetCore.Components.Authorization;
using PrePurchase.Mobile.Shared.Services.Authentication;
using System.Security.Claims;

public class JwtAuthenticationStateProvider(IAuthenticationService authService) : AuthenticationStateProvider
{
    private readonly IAuthenticationService _authService = authService;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = await _authService.GetCurrentUserAsync();
        var token = await _authService.IsAuthenticatedAsync();

        if (user == null || !token)
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var claims = new List<Claim>
        {
            //new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FirstName),
        };

        // roles
        if (user.Role != null)
        {
            claims.Add(new Claim(ClaimTypes.Role, user.Role));
        }

        var identity = new ClaimsIdentity(claims, "jwt");
        var principal = new ClaimsPrincipal(identity);

        return new AuthenticationState(principal);
    }

    public void NotifyAuthStateChanged() =>
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}
