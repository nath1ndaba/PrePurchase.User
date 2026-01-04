using PrePurchase.Mobile.Shared.Models.Product;
using PrePurchase.Mobile.Shared.Models.Common;
using Refit;

namespace PrePurchase.Mobile.Shared.Services.Api;

public interface IProductApi
{
    [Get("/api/products/search")]
    Task<Models.Common.ApiResponse<PagedResult<ProductDto>>> SearchProductsAsync(
        [Query] string? searchTerm = null,
        [Query] Guid? shopId = null,
        [Query] Guid? categoryId = null,
        [Query] decimal? minPrice = null,
        [Query] decimal? maxPrice = null,
        [Query] bool? inStockOnly = null,
        [Query] int page = 1,
        [Query] int pageSize = 20);

    [Get("/api/products/{id}")]
    Task<Models.Common.ApiResponse<ProductDetailsDto>> GetProductByIdAsync(Guid id);
}
