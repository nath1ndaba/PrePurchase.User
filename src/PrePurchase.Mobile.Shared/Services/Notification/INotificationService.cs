// INotificationService.cs
using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.Notification;

namespace PrePurchase.Mobile.Shared.Services.Notification;

public interface INotificationService
{
    Task<Result> RegisterFcmTokenAsync(string token, string deviceType);
    Task<Result<PagedResult<NotificationDto>>> GetNotificationsAsync(bool unreadOnly = false, int pageNumber = 1, int pageSize = 20);
    Task<Result<int>> GetUnreadCountAsync();
    Task<Result> MarkAsReadAsync(Guid id);
    Task<Result> MarkAllAsReadAsync();
    Task<Result> DeleteNotificationAsync(Guid id);
    //Task<Result> SendTestNotificationAsync();
}