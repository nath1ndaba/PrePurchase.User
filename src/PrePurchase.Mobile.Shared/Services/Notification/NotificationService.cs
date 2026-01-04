// NotificationService.cs
using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.Notification;
using PrePurchase.Mobile.Shared.Services.Api;

namespace PrePurchase.Mobile.Shared.Services.Notification;

public class NotificationService(INotificationApi notificationApi) : INotificationService
{
    private readonly INotificationApi _notificationApi = notificationApi;

    public async Task<Result> RegisterFcmTokenAsync(string token, string deviceType)
    {
        try
        {
            var command = new RegisterFcmTokenCommand
            {
                Token = token,
                DeviceType = deviceType
            };

            var response = await _notificationApi.RegisterFcmTokenAsync(command);

            if (response.Success)
            {
                return Result.Success();
            }

            return response.Errors?.Count == 0
                ? Result.Failure(response.Errors)
                : Result.Failure(response.Message ?? "Failed to register FCM token");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error registering FCM token: {ex.Message}");
        }
    }

    public async Task<Result<PagedResult<NotificationDto>>> GetNotificationsAsync(bool unreadOnly = false, int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            var response = await _notificationApi.GetNotificationsAsync(unreadOnly, pageNumber, pageSize);

            if (response.Success && response.Data != null)
            {
                return Result<PagedResult<NotificationDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<PagedResult<NotificationDto>>.Failure(response.Errors)
                : Result<PagedResult<NotificationDto>>.Failure(response.Message ?? "Failed to fetch notifications");
        }
        catch (Exception ex)
        {
            return Result<PagedResult<NotificationDto>>.Failure($"Error fetching notifications: {ex.Message}");
        }
    }

    public async Task<Result<int>> GetUnreadCountAsync()
    {
        try
        {
            var response = await _notificationApi.GetUnreadCountAsync();

            if (response.Success)
            {
                return Result<int>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<int>.Failure(response.Errors)
                : Result<int>.Failure(response.Message ?? "Failed to fetch unread count");
        }
        catch (Exception ex)
        {
            return Result<int>.Failure($"Error fetching unread count: {ex.Message}");
        }
    }

    public async Task<Result> MarkAsReadAsync(Guid id)
    {
        try
        {
            var response = await _notificationApi.MarkAsReadAsync(id);

            if (response.Success)
            {
                return Result.Success();
            }

            return response.Errors?.Count == 0
                ? Result.Failure(response.Errors)
                : Result.Failure(response.Message ?? "Failed to mark notification as read");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error marking notification as read: {ex.Message}");
        }
    }

    public async Task<Result> MarkAllAsReadAsync()
    {
        try
        {
            var response = await _notificationApi.MarkAllAsReadAsync();

            if (response.Success)
            {
                return Result.Success();
            }

            return response.Errors?.Count == 0
                ? Result.Failure(response.Errors)
                : Result.Failure(response.Message ?? "Failed to mark all notifications as read");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error marking all notifications as read: {ex.Message}");
        }
    }

    public async Task<Result> DeleteNotificationAsync(Guid id)
    {
        try
        {
            var response = await _notificationApi.DeleteNotificationAsync(id);

            if (response.Success)
            {
                return Result.Success();
            }

            return response.Errors?.Count == 0
                ? Result.Failure(response.Errors)
                : Result.Failure(response.Message ?? "Failed to delete notification");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error deleting notification: {ex.Message}");
        }
    }

    //public async Task<Result> SendTestNotificationAsync()
    //{
    //    try
    //    {
    //        var response = await _notificationApi.SendTestNotificationAsync();

    //        if (response.Success)
    //        {
    //            return Result.Success();
    //        }

    //        return response.Errors?.Count == 0
    //            ? Result.Failure(response.Errors)
    //            : Result.Failure(response.Message ?? "Failed to send test notification");
    //    }
    //    catch (Exception ex)
    //    {
    //        return Result.Failure($"Error sending test notification: {ex.Message}");
    //    }
    //}
}