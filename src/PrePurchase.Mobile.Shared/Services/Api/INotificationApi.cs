using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.Notification;
using Refit;

namespace PrePurchase.Mobile.Shared.Services.Api
{
    public interface INotificationApi
    {
        [Post("/api/notifications/fcm-token")]
        Task<Models.Common.ApiResponse<NotificationDto?>> RegisterFcmTokenAsync(
            [Body] RegisterFcmTokenCommand request);

        [Get("/api/notifications")]
        Task<Models.Common.ApiResponse<PagedResult<NotificationDto>>> GetNotificationsAsync(
            [Query] bool unreadOnly = false,
            [AliasAs("page")] int pageNumber = 1,
            [Query] int pageSize = 20);

        [Get("/api/notifications/unread-count")]
        Task<Models.Common.ApiResponse<int>> GetUnreadCountAsync();

        [Post("/api/notifications/{id}/mark-read")]
        Task<Models.Common.ApiResponse<object?>> MarkAsReadAsync(Guid id);

        [Post("/api/notifications/mark-all-read")]
        Task<Models.Common.ApiResponse<object?>> MarkAllAsReadAsync();

        [Delete("/api/notifications/{id}")]
        Task<Models.Common.ApiResponse<object?>> DeleteNotificationAsync(Guid id);
    }
}
