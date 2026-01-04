using Microsoft.AspNetCore.SignalR.Client;
using PrePurchase.Mobile.Shared.Constants;
using PrePurchase.Mobile.Shared.Models.Notification;
using PrePurchase.Mobile.Shared.Services.Storage;
using System.Text.Json;

namespace PrePurchase.Mobile.Shared.Services.SignalR;

public class SignalRService(ITokenStorage tokenStorage) : ISignalRService, IAsyncDisposable
{
    private HubConnection? _hubConnection;
    private readonly ITokenStorage _tokenStorage = tokenStorage;
    private bool _isDisposed;
    private CancellationTokenSource? _reconnectCts;

    public event EventHandler<RealtimeNotificationDto>? OnNotificationReceived;
    public event EventHandler<bool>? OnConnectionStateChanged;

    public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;
    public string ConnectionState => _hubConnection?.State.ToString() ?? "Disconnected";

    public async Task StartAsync()
    {
        if (_isDisposed)
        {
            Console.WriteLine("SignalR service is disposed, cannot start");
            return;
        }

        if (_hubConnection?.State == HubConnectionState.Connected)
        {
            Console.WriteLine("Already connected to SignalR hub");
            return;
        }

        try
        {
            var token = await _tokenStorage.GetAccessTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("No authentication token available, skipping SignalR connection");
                return;
            }

            Console.WriteLine($"Building SignalR connection to: {ApiConstants.SIGNALR_HUB_URL}");

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(ApiConstants.SIGNALR_HUB_URL, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(token);
                })
                .WithAutomaticReconnect(new[] {
                    TimeSpan.Zero,
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(30)
                })
                .Build();

            SetupEventHandlers();

            Console.WriteLine("Connecting to SignalR hub...");
            await _hubConnection.StartAsync();
            Console.WriteLine($"SignalR connected! State: {_hubConnection.State}");

            OnConnectionStateChanged?.Invoke(this, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to SignalR: {ex.Message}");
            OnConnectionStateChanged?.Invoke(this, false);
            _ = RetryConnectionAsync();
        }
    }

    private void SetupEventHandlers()
    {
        if (_hubConnection == null) return;

        _hubConnection.On<object>("ReceiveNotification", (notification) =>
        {
            try
            {
                Console.WriteLine($"Received notification: {JsonSerializer.Serialize(notification)}");

                var json = JsonSerializer.Serialize(notification);
                var dto = JsonSerializer.Deserialize<RealtimeNotificationDto>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (dto != null)
                {
                    Console.WriteLine($"Parsed notification - Type: {dto.Type}, Title: {dto.Title}");
                    OnNotificationReceived?.Invoke(this, dto);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing notification: {ex.Message}");
            }
        });

        _hubConnection.Closed += async (error) =>
        {
            Console.WriteLine($"SignalR connection closed. Error: {error?.Message}");
            OnConnectionStateChanged?.Invoke(this, false);
            await RetryConnectionAsync();
        };

        _hubConnection.Reconnecting += (error) =>
        {
            Console.WriteLine($"SignalR reconnecting... Error: {error?.Message}");
            OnConnectionStateChanged?.Invoke(this, false);
            return Task.CompletedTask;
        };

        _hubConnection.Reconnected += (connectionId) =>
        {
            Console.WriteLine($"SignalR reconnected! Connection ID: {connectionId}");
            OnConnectionStateChanged?.Invoke(this, true);
            return Task.CompletedTask;
        };
    }

    private async Task RetryConnectionAsync()
    {
        if (_isDisposed || _hubConnection == null)
            return;

        _reconnectCts?.Cancel();
        _reconnectCts = new CancellationTokenSource();

        try
        {
            await Task.Delay(TimeSpan.FromSeconds(5), _reconnectCts.Token);

            if (!_reconnectCts.Token.IsCancellationRequested)
            {
                Console.WriteLine("Retrying SignalR connection...");
                await StartAsync();
            }
        }
        catch (TaskCanceledException)
        {
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during retry: {ex.Message}");
        }
    }

    public async Task StopAsync()
    {
        if (_hubConnection != null)
        {
            try
            {
                Console.WriteLine("Stopping SignalR connection...");

                _reconnectCts?.Cancel();

                await _hubConnection.StopAsync();
                Console.WriteLine("SignalR connection stopped");

                OnConnectionStateChanged?.Invoke(this, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping SignalR: {ex.Message}");
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;

        _reconnectCts?.Cancel();
        _reconnectCts?.Dispose();

        if (_hubConnection != null)
        {
            await _hubConnection.DisposeAsync();
            _hubConnection = null;
        }

        GC.SuppressFinalize(this);
    }
}