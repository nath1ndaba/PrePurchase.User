using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrePurchase.Mobile.Shared.Models.Notification;
using PrePurchase.Mobile.Shared.Services.SignalR;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Media;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Plugin.Maui.Audio; // For AudioManager
namespace PrePurchase.Mobile.Shared.Services.Notification;

public interface IRealtimeNotificationHandler
{
    void Initialize(NavigationManager navigationManager);
    void Shutdown();
}

public class RealtimeNotificationHandler : IRealtimeNotificationHandler
{
    private readonly ISignalRService _signalRService;
    private readonly ISnackbar _snackbar;
    private NavigationManager? _navigationManager;
    private bool _isInitialized;

    public RealtimeNotificationHandler(
        ISignalRService signalRService,
        ISnackbar snackbar)
    {
        _signalRService = signalRService;
        _snackbar = snackbar;
    }

    public void Initialize(NavigationManager navigationManager)
    {
        if (_isInitialized)
            return;

        _navigationManager = navigationManager;
        _signalRService.OnNotificationReceived += HandleNotificationReceived;
        _isInitialized = true;

        Console.WriteLine("Realtime notification handler initialized");
    }

    public void Shutdown()
    {
        if (!_isInitialized)
            return;

        _signalRService.OnNotificationReceived -= HandleNotificationReceived;
        _isInitialized = false;
        _navigationManager = null;

        Console.WriteLine("Realtime notification handler shutdown");
    }

    private void HandleNotificationReceived(object? sender, RealtimeNotificationDto notification)
    {
        try
        {
            Console.WriteLine($"Displaying notification: {notification.Title} - {notification.Message}");

            var severity = GetSeverityForType(notification.Type);

            _snackbar.Add(
                $"<strong>{notification.Title}</strong><br/>{notification.Message}",
                severity,
                config =>
                {
                    config.VisibleStateDuration = 5000;
                    config.ShowCloseIcon = true;
                    config.SnackbarVariant = Variant.Filled;
                    config.Action = "View";
                    config.ActionColor = Color.Inherit;
                    config.OnClick = _ =>
                    {
                        HandleNotificationClick(notification);
                        return Task.CompletedTask;
                    };
                });

            PlayNotificationSound();
            VibrateDevice();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error displaying notification: {ex.Message}");
        }
    }

    private Severity GetSeverityForType(string type)
    {
        return type.ToLower() switch
        {
            "new_order" => Severity.Info,
            "order_status_changed" => Severity.Success,
            "wallet_updated" => Severity.Info,
            "low_stock_alert" => Severity.Warning,
            "settlement_processed" => Severity.Success,
            "system_alert" => Severity.Warning,
            _ => Severity.Normal
        };
    }

    private void HandleNotificationClick(RealtimeNotificationDto notification)
    {
        if (_navigationManager == null)
            return;

        try
        {
            Console.WriteLine($"Notification clicked: {notification.Type}");

            var navigateTo = notification.Type.ToLower() switch
            {
                "new_order" when notification.Data?.ContainsKey("transactionId") == true
                    => $"/order-confirmation?transactionId={notification.Data["transactionId"]}",

                "order_status_changed" when notification.Data?.ContainsKey("transactionId") == true
                    => $"/order-confirmation?transactionId={notification.Data["transactionId"]}",

                "wallet_updated"
                    => "/wallet",

                "low_stock_alert" when notification.Data?.ContainsKey("productName") == true
                    => "/shop/products",

                "settlement_processed" when notification.Data?.ContainsKey("settlementId") == true
                    => $"/settlements",

                _ => "/notifications"
            };

            _navigationManager.NavigateTo(navigateTo);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error navigating from notification: {ex.Message}");
        }
    }

    private void PlayNotificationSound()
    {
        try
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var stream = await FileSystem.OpenAppPackageFileAsync("notification.mp3");
                    if (stream != null)
                    {
                        var player = AudioManager.Current.CreatePlayer(stream);
                        player.Volume = 0.5;
                        player.Play();
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Notification sound file not found");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error playing notification sound: {ex.Message}");
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error playing sound: {ex.Message}");
        }
    }

    private void VibrateDevice()
    {
        try
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    if (Vibration.Default.IsSupported)
                    {
                        Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(200));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error vibrating device: {ex.Message}");
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error vibrating: {ex.Message}");
        }
    }
}