using PrePurchase.Mobile.Shared.Models.Notification;

namespace PrePurchase.Mobile.Shared.Services.SignalR;

public interface ISignalRService
{
    /// <summary>
    /// Connect to SignalR hub with authentication
    /// </summary>
    Task StartAsync();

    /// <summary>
    /// Disconnect from SignalR hub
    /// </summary>
    Task StopAsync();

    /// <summary>
    /// Check if connected to hub
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Event fired when a new notification is received
    /// </summary>
    event EventHandler<RealtimeNotificationDto>? OnNotificationReceived;

    /// <summary>
    /// Event fired when connection state changes
    /// </summary>
    event EventHandler<bool>? OnConnectionStateChanged;

    /// <summary>
    /// Get current connection state
    /// </summary>
    string ConnectionState { get; }
}
