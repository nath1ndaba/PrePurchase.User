using System.IdentityModel.Tokens.Jwt;

namespace PrePurchase.Mobile.Shared.Services.Authentication;

public static class TokenHelper
{
    public static bool IsTokenExpired(string token)
    {
        if (string.IsNullOrEmpty(token))
            return true;

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken.ValidTo < DateTime.UtcNow;
        }
        catch
        {
            return true;
        }
    }

    public static bool IsTokenExpiringSoon(string token, int minutesThreshold = 5)
    {
        if (string.IsNullOrEmpty(token))
            return true;

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken.ValidTo < DateTime.UtcNow.AddMinutes(minutesThreshold);
        }
        catch
        {
            return true;
        }
    }

    public static DateTime? GetTokenExpiration(string token)
    {
        if (string.IsNullOrEmpty(token))
            return null;

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.ValidTo;
        }
        catch
        {
            return null;
        }
    }

    public static string? GetClaimValue(string token, string claimType)
    {
        if (string.IsNullOrEmpty(token))
            return null;

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }
        catch
        {
            return null;
        }
    }
}