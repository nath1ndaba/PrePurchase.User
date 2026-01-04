namespace PrePurchase.Mobile.Shared.Constants;

public static class ApiConstants
{
    // API Base URL - Change this to your backend URL
    /// <summary>
     public const string API_BASE_URL = "https://localhost:3115"; // Development (HTTPS)
    /// </summary>
    //public const string API_BASE_URL = "https://prepurchase-backend.onrender.com";// Production

    // SignalR Hub URL
    public const string SIGNALR_HUB_URL = $"{API_BASE_URL}/hubs/notifications";

    // Endpoints
    public static class Endpoints
    {
        // Authentication
        public const string AUTH_LOGIN = "/api/auth/login";
        public const string AUTH_REGISTER = "/api/auth/register";
        public const string AUTH_REFRESH = "/api/auth/refresh";

        // Users
        public const string USERS_PROFILE = "/api/users/me";
        public const string USERS_CHANGE_PASSWORD = "/api/users/change-password";

        // Shops
        public const string SHOPS_SEARCH = "/api/shops/search";
        public const string SHOPS_NEARBY = "/api/shops/nearby";
        public const string SHOPS = "/api/shops";
        public static string ShopDetails(Guid shopId) => $"/api/shops/{shopId}";
        public static string UpdateShop(Guid shopId) => $"/api/shops/{shopId}";

        // Products
        public const string PRODUCTS_SEARCH = "/api/products/search";
        public static string ProductDetails(Guid productId) => $"/api/products/{productId}";

        // Categories
        public const string CATEGORIES = "/api/categories";
        public const string CATEGORIES_ROOTS = "/api/categories/roots";
        public const string CATEGORIES_HIERARCHY = "/api/categories/hierarchy";
        public static string CategoryById(Guid categoryId) => $"/api/categories/{categoryId}";
        public static string ActivateCategory(Guid categoryId) => $"/api/categories/{categoryId}/activate";
        public static string DeactivateCategory(Guid categoryId) => $"/api/categories/{categoryId}/deactivate";

        // Shopping Cart
        public const string CART_ITEMS = "/api/cart/items";
        public const string CART_CLEAR = "/api/cart";
        public const string CART_CHECKOUT = "/api/cart/checkout";
        public static string RemoveCartItem(Guid shopProductId) => $"/api/cart/items/{shopProductId}";

        // Transactions
        public const string TRANSACTIONS_MY_TRANSACTIONS = "/api/transactions/my-transactions";
        public static string TransactionDetails(Guid transactionId) => $"/api/transactions/{transactionId}";
        public static string ApproveTransaction(Guid transactionId) => $"/api/transactions/{transactionId}/approve";
        public static string RejectTransaction(Guid transactionId) => $"/api/transactions/{transactionId}/reject";
        public static string CollectTransaction(Guid transactionId) => $"/api/transactions/{transactionId}/collect";

        // Wallet
        public const string WALLETS_MY_WALLET = "/api/wallets/my-wallet";
        public const string WALLETS_LOAD = "/api/wallets/load";
        public const string WALLETS_TRANSACTIONS = "/api/wallets/transactions";

        // Authorized Collectors
        public const string AUTHORIZED_COLLECTORS = "/api/authorized-collectors";
        public static string UpdateCollector(Guid collectorId) => $"/api/authorized-collectors/{collectorId}";
        public static string DeactivateCollector(Guid collectorId) => $"/api/authorized-collectors/{collectorId}";

        // Credit Score
        public const string CREDIT_SCORE = "/api/credit-score";
        public const string CREDIT_SCORE_CALCULATE = "/api/credit-score/calculate";

        // Demand Forecast
        public const string DEMAND_FORECAST_GENERATE = "/api/demand-forecast/generate";
        public static string DemandForecasts(Guid shopId) => $"/api/demand-forecast/{shopId}";
        public static string LowStockAlerts(Guid shopId) => $"/api/demand-forecast/{shopId}/low-stock-alerts";
        public static string ModelAccuracy(Guid shopId) => $"/api/demand-forecast/{shopId}/model-accuracy";

        // Notifications
        public const string NOTIFICATIONS = "/api/notifications";
        public const string NOTIFICATIONS_FCM_TOKEN = "/api/notifications/fcm-token";
        public const string NOTIFICATIONS_UNREAD_COUNT = "/api/notifications/unread-count";
        public const string NOTIFICATIONS_MARK_ALL_READ = "/api/notifications/mark-all-read";
        public const string NOTIFICATIONS_TEST = "/api/notifications/test";
        public static string MarkNotificationRead(Guid notificationId) => $"/api/notifications/{notificationId}/mark-read";
        public static string DeleteNotification(Guid notificationId) => $"/api/notifications/{notificationId}";

        // Settlements
        public static string ShopSettlements(Guid shopId) => $"/api/settlements/shop/{shopId}";
        public static string SettlementDetails(Guid settlementId) => $"/api/settlements/{settlementId}";
        public static string SettlementSummary(Guid shopId) => $"/api/settlements/shop/{shopId}/summary";
        public const string SETTLEMENTS_PENDING = "/api/settlements/pending";
        public static string ProcessSettlement(Guid settlementId) => $"/api/settlements/{settlementId}/process";
        public const string SETTLEMENTS_MANUAL = "/api/settlements/manual";

        // Root (Health Check)
        public const string ROOT = "/";

        // Swagger Documentation
        public const string SWAGGER_JSON = "/swagger/v1/swagger.json";
    }

    // Storage Keys
    public static class StorageKeys
    {
        public const string ACCESS_TOKEN = "access_token";
        public const string REFRESH_TOKEN = "refresh_token";
        public const string USER_DATA = "user_data";
        public const string FCM_TOKEN = "fcm_token";
    }

    // App Settings
    public static class AppSettings
    {
        public const int REQUEST_TIMEOUT_SECONDS = 30;
        public const int MAX_RETRY_ATTEMPTS = 3;
        public const int PAGE_SIZE = 20;
    }

    // Enums (matching backend)
    public enum UserRole
    {
        Resident = 0,
        ShopOwner = 1,
        Collector = 2,
        Admin = 3
    }

    public enum PaymentMethod
    {
        Cash = 0,
        Card = 1,
        EFT = 2,
        MobileMoney = 3,
        Wallet = 4
    }

    public enum PaymentGateway
    {
        None = 0,
        PayFast = 1,
        PayStack = 2,
        Stripe = 3,
        Internal = 4
    }

    public enum TransactionType
    {
        PrePurchase = 0,
        WalletLoad = 1,
        Refund = 2,
        Settlement = 3
    }

    public enum WalletTransactionType
    {
        Load = 0,
        Purchase = 1,
        Refund = 2,
        Withdrawal = 3
    }
}