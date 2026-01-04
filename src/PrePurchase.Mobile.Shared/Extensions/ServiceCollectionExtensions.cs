using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using PrePurchase.Mobile.Shared.Constants;
using PrePurchase.Mobile.Shared.Handlers;
using PrePurchase.Mobile.Shared.Services.Api;
using PrePurchase.Mobile.Shared.Services.Authentication;
using PrePurchase.Mobile.Shared.Services.AuthorizedCollector;
using PrePurchase.Mobile.Shared.Services.Cart;
using PrePurchase.Mobile.Shared.Services.Category;
using PrePurchase.Mobile.Shared.Services.CreditScore;
using PrePurchase.Mobile.Shared.Services.DemandForecast;
using PrePurchase.Mobile.Shared.Services.Notification;
using PrePurchase.Mobile.Shared.Services.Product;
using PrePurchase.Mobile.Shared.Services.Settlement;
using PrePurchase.Mobile.Shared.Services.Shop;
using PrePurchase.Mobile.Shared.Services.Storage;
using PrePurchase.Mobile.Shared.Services.Transaction;
using PrePurchase.Mobile.Shared.Services.User;
using PrePurchase.Mobile.Shared.Services.Wallet;
using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PrePurchase.Mobile.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        var baseUri = new Uri(ApiConstants.API_BASE_URL);
        var timeout = TimeSpan.FromSeconds(ApiConstants.AppSettings.REQUEST_TIMEOUT_SECONDS);

        // MudBlazor
        services.AddMudServices();

        // ============================================
        // CRITICAL ORDER: Token storage FIRST
        // ============================================
        services.AddAuthorizationCore();
        services.AddScoped<ITokenStorage, SecureTokenStorage>();

        // ============================================
        // CRITICAL: Register core services FIRST

        services.AddScoped<IAuthenticationService, AuthenticationService>();

        // ============================================
        // Register Auth API FIRST (without handler)
        // ============================================
        services.AddRefitClient<IAuthApi>(sp => CreateRefitSettings())
            .ConfigureHttpClient(c => { c.BaseAddress = baseUri; c.Timeout = timeout; });

        // ============================================
        // Register AuthTokenHandler AFTER IAuthApi
        // ============================================
        services.AddTransient<AuthTokenHandler>();

        // ============================================
        // Register all other APIs WITH auth handler
        // ============================================
        services.AddRefitClient<IUserApi>(sp => CreateRefitSettings())
            .ConfigureHttpClient(c => { c.BaseAddress = baseUri; c.Timeout = timeout; })
            .AddHttpMessageHandler<AuthTokenHandler>();

        services.AddRefitClient<IShopApi>(sp => CreateRefitSettings())
            .ConfigureHttpClient(c => { c.BaseAddress = baseUri; c.Timeout = timeout; })
            .AddHttpMessageHandler<AuthTokenHandler>();

        services.AddRefitClient<IProductApi>(sp => CreateRefitSettings())
            .ConfigureHttpClient(c => { c.BaseAddress = baseUri; c.Timeout = timeout; })
            .AddHttpMessageHandler<AuthTokenHandler>();

        services.AddRefitClient<ICartApi>(sp => CreateRefitSettings())
            .ConfigureHttpClient(c => { c.BaseAddress = baseUri; c.Timeout = timeout; })
            .AddHttpMessageHandler<AuthTokenHandler>();

        services.AddRefitClient<ITransactionApi>(sp => CreateRefitSettings())
            .ConfigureHttpClient(c => { c.BaseAddress = baseUri; c.Timeout = timeout; })
            .AddHttpMessageHandler<AuthTokenHandler>();

        services.AddRefitClient<IWalletApi>(sp => CreateRefitSettings())
            .ConfigureHttpClient(c => { c.BaseAddress = baseUri; c.Timeout = timeout; })
            .AddHttpMessageHandler<AuthTokenHandler>();

        services.AddRefitClient<IAuthorizedCollectorApi>(sp => CreateRefitSettings())
            .ConfigureHttpClient(c => { c.BaseAddress = baseUri; c.Timeout = timeout; })
            .AddHttpMessageHandler<AuthTokenHandler>();

        services.AddRefitClient<ICategoryApi>(sp => CreateRefitSettings())
            .ConfigureHttpClient(c => { c.BaseAddress = baseUri; c.Timeout = timeout; })
            .AddHttpMessageHandler<AuthTokenHandler>();

        services.AddRefitClient<INotificationApi>(sp => CreateRefitSettings())
            .ConfigureHttpClient(c => { c.BaseAddress = baseUri; c.Timeout = timeout; })
            .AddHttpMessageHandler<AuthTokenHandler>();

        services.AddRefitClient<ISettlementApi>(sp => CreateRefitSettings())
            .ConfigureHttpClient(c => { c.BaseAddress = baseUri; c.Timeout = timeout; })
            .AddHttpMessageHandler<AuthTokenHandler>();

        services.AddRefitClient<IDemandForecastApi>(sp => CreateRefitSettings())
            .ConfigureHttpClient(c => { c.BaseAddress = baseUri; c.Timeout = timeout; })
            .AddHttpMessageHandler<AuthTokenHandler>();

        services.AddRefitClient<ICreditScoreApi>(sp => CreateRefitSettings())
            .ConfigureHttpClient(c => { c.BaseAddress = baseUri; c.Timeout = timeout; })
            .AddHttpMessageHandler<AuthTokenHandler>();


        // Authentication state provider
        services.AddCascadingAuthenticationState();
        services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
        services.AddScoped<IAuthorizedCollectorService, AuthorizedCollectorService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICreditScoreService, CreditScoreService>();
        services.AddScoped<IDemandForecastService, DemandForecastService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISettlementService, SettlementService>();
        services.AddScoped<IShopService, ShopService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddSingleton<ICartStateService, CartStateService>();

        return services;
    }

    private static RefitSettings CreateRefitSettings()
    {
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        // Remove any existing JsonStringEnumConverter
        for (int i = serializerOptions.Converters.Count - 1; i >= 0; i--)
        {
            if (serializerOptions.Converters[i] is JsonStringEnumConverter)
                serializerOptions.Converters.RemoveAt(i);
        }

        return new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(serializerOptions)
        };
    }
}